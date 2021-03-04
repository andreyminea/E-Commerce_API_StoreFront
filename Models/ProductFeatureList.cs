using System.Collections.Generic;

namespace StoreAPI.Models
{
    public class ProductFeatureList
    {
        public string Name { get; set; }
        public Dictionary<string, string> Features { get; set; }

        public ProductFeatureList(string Name, Dictionary<string, string> Features)
        {
            this.Name = Name;
            this.Features = Features;
        }
    }
}
