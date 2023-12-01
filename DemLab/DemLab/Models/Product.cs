using System;
using System.Collections.Generic;
using System.Text;

namespace DemLab.Models
{
    class Product
    {
        //public int ID { get; set; }
        public string Title { get; set; }
        //public string Description { get; set; }
        //public float Price { get; set; }
        public string JobType { get; set; }
        public string LastDateToApply { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }

        public string Salary { get; set; }
        public Uri CompanyImage { get; set; }
    }
}
