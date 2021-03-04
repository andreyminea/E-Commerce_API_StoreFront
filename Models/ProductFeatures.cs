using System.Collections.Generic;

namespace StoreAPI.Models
{
    public class ProductFeatures
    {
        public List<ProductFeatureList> List { get; set; }

        public ProductFeatures(Dictionary<string, object> firebaseData)
        {
            List = new List<ProductFeatureList>();

            foreach (var obj in firebaseData)
            {
                var featureObjs = (Dictionary<string, object>)obj.Value;
                var auxDictionary = new Dictionary<string, string>();

                foreach (var feature in featureObjs)
                {
                    auxDictionary.Add(feature.Key.ToString(), feature.Value.ToString());
                }
                List.Add(new ProductFeatureList(obj.Key.ToString(), auxDictionary));
            }
        }
    }
}
