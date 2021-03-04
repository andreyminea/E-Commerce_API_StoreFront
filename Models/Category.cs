using System.Collections.Generic;

namespace StoreAPI.Models
{
    public class Category
    {
        public string CategoryName { get; set; }

        public Category() { }

        public Category(Dictionary<string, object> values)
        {
            CategoryName = values["name"].ToString();
        }
    }
}
