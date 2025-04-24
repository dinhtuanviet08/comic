using System;

namespace ComicSystem.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}
