using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAppAPI.Model
{
    public class Post
    {
        public int Id { get; set; }
        public string Experience { get; set; }
        public string Venue { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Distance { get; set; }
        public string CategoryName { get; set; }
        public string CategoryId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
