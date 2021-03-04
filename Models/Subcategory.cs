using System.Collections.Generic;

namespace StoreAPI.Models
{
    public class Subcategory
    {
        public string SubcategoryName { get; set; }

        public Subcategory(Dictionary<string, object> values)
        {
            SubcategoryName = values["name"].ToString();
        }
    }
}
