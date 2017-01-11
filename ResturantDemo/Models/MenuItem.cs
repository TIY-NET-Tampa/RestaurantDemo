﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ResturantDemo.Models
{
    public class MenuItem
    {
        public MenuItem()
        {

        }
        public MenuItem(MenuItem s)
        {
            this.Name = s.Name;
            this.Description = s.Description;
            this.CategoryId = s.CategoryId;
            this.Id = s.Id;
            this.Price = s.Price;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Price { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

    }
}