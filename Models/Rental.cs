using System;
using System.Collections.Generic;

namespace ComicSystem.Models
{
    public class Rental
    {
        public int RentalId { get; set; }
        public int CustomerId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }

        public Customer Customer { get; set; }
        public List<RentalDetail> RentalDetails { get; set; }

        // Đây là property để binding từ MultiSelectList
        public List<int> ComicIds { get; set; }
    }
}
