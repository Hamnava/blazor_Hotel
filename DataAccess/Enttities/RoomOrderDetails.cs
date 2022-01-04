using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Enttities
{
    public class RoomOrderDetails
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string StripeSessionId { get; set; }
        [Required]
        public DateTime CheckInDate { get; set; }
        [Required]
        public DateTime CheckOutDate { get; set; }
        public DateTime ActualCheckInDate { get; set; }
        public DateTime ActualCheckOutDate { get; set; }
        public double TotalCost { get; set; }
        public int RoomId { get; set; }
        public bool IsSuccessFulPayment { get; set; }
        public string Status { get; set; }
        [Required]
        public string Name { get; set; }
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }


        [ForeignKey("RoomId")]
        public HotelRoom HotelRoom { get; set; }
    }
}
