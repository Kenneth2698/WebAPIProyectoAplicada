using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIProyecto1.Models
{
    public class Product
    {
        public Product(int id, string name, int price, string description, string image, int status, int provider)
        {
            Id = id;
            Name = name;
            Price = price;
            Description = description;
            Image = image;
            Status = status;
            Provider = provider;
        }

        public Product()
        {
            Id = 0;
            Name = "";
            Price = 0;
            Description = "";
            Image = "";
            Status = 0;
            Provider = 0;

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Status { get; set; }
        public int Provider { get; set; }

    }
}
