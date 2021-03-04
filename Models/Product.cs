using System;
using System.Collections.Generic;
using System.Globalization;

namespace StoreAPI.Models
{
    public class Product
    {
        public string ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public int TVA { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; }
        public List<string> Pictures { get; set; }
        public ProductFeatures FeaturesLists { get; set; }

        public Product(Dictionary<string, object> productData)
        {
            Name = productData["name"].ToString();
            ProductID = productData["productID"].ToString();
            Description = productData["description"].ToString();
            Brand = productData["brand"].ToString();
            TVA = Int32.Parse(productData["tva"].ToString());
            Price = float.Parse(productData["price"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
            Stock = Int32.Parse(productData["stock"].ToString());
            var convertList = (List<object>)productData["pictures"];
            Pictures = new List<string>();

            foreach (var item in convertList)
            {
                Pictures.Add(item.ToString());
            }
        }
    }
}
