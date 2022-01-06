
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HiddingVila_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StripePaymentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public StripePaymentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpPost]
        public async Task<IActionResult> Create(StripePaymentDTO payment)
        {
            try
            {
                var domain = _configuration.GetValue<string>("HiddenVilla_Client_URL");

                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string>
                    {
                        "card"
                    },
                    LineItems = new List<SessionLineItemOptions>
                    {
                        new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                                UnitAmount=payment.Amount,//convert to cents
                                Currency="usd",
                                ProductData= new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = payment.ProductName
                                }
                            },
                            Quantity=1
                        }
                    },
                    Mode = "payment",
                    SuccessUrl = domain + "/success-payment?session_id={{CHECKOUT_SESSION_ID}}",
                    CancelUrl = domain + payment.ReturnUrl
                };

                var service = new SessionService();
                Session session = await service.CreateAsync(options);

                return Ok(new SucessModel()
                {
                    Data = session.Id
                });

            }
            catch (Exception e)
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = e.Message
                });
            }
        }
        //[HttpPost]
        //public async Task<IActionResult> Create(StripePaymentDTO paymentDTO)
        //{
        //    try
        //    {
        //        var domain = _configuration.GetValue<string>("HiddenVila_Client_Url");

        //        var options = new SessionCreateOptions
        //        {
        //            PaymentMethodTypes = new List<string>
        //            {
        //                "card"
        //            },
        //            LineItems = new List<SessionLineItemOptions>
        //            {
        //                new SessionLineItemOptions
        //                {
        //                    PriceData = new SessionLineItemPriceDataOptions
        //                    {
        //                        UnitAmount = paymentDTO.Amount,
        //                        Currency = "usd",
        //                        ProductData = new SessionLineItemPriceDataProductDataOptions
        //                        {
        //                            Name = paymentDTO.ProductName,
        //                        }
        //                    },
        //                    Quantity = 1
        //                }
        //            },
        //            Mode = "payment",
        //            SuccessUrl = domain + "/success-payment?session_id={{CHECKOUT_SESSION_ID}}",
        //            CancelUrl = domain + paymentDTO.ReturnUrl
        //        };

        //        var service = new SessionService();
        //        Session session = await service.CreateAsync(options);
        //        return Ok(new SucessModel
        //        {
        //            Data = session.Id
        //        });
        //    }
        //    catch (System.Exception e)
        //    {
        //        return BadRequest(new ErrorModel
        //        {
        //            ErrorMessage = e.Message
        //        });
        //    }
        //}
    }
}
