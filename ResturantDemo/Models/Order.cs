using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ResturantDemo.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime TimeCreated { get; set; } = DateTime.Now;

        public bool Fulfilled { get; set; } = false;

        public string Location { get; set; }

        public string CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<MenuItem> Items { get; set; } = new HashSet<MenuItem>();

        [DisplayFormat(DataFormatString = "{0:C}")]
        [NotMapped]
        public double TotalPrice
        {
            get
            {
                return Items.Sum(s => s.Price);
            }
        }

    }
}