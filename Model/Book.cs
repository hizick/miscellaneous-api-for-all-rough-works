using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookAppAPI.Model
{
    //[Table("Books")]
    public class Book
    {
        public byte Id { get; set; }
        [Required][StringLength(200)]
        public string Name { get; set; }
        [Required][StringLength(200)]
        public string Author { get; set; }
        public string ISBN { get; set; }
        public DateTime LastUpdated { get; set; }
        public float AverageRating { get; set; }
    }
}
