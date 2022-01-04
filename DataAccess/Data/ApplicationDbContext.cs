using DataAccess.Enttities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<HotelRoom> HotelRooms { get; set; }
        public DbSet<HotelImagesUrl> HotelImagesUrls { get; set; }
        public DbSet<HotelEmenity> HotelEmenities { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
    }
}
