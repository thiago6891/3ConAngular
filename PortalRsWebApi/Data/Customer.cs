using System;
using System.ComponentModel.DataAnnotations;

namespace PortalRSApi.Data
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

       
        public string Name { get; set; }

        public bool Active { get; set; }

        public DateTime RegisterDate { get; set; }
    }
}