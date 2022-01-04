using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class HotelEmenityDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
       
        public string Description { get; set; }
        [Required]
        public string Time { get; set; }
        [Required]
        public string FontIcon { get; set; }
    }
}
