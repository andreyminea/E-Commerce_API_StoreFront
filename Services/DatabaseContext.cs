using Google.Cloud.Firestore;
using StoreAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAPI.Services
{
    public class DatabaseContext
    {
        private readonly FirestoreDB database;

        public DatabaseContext(FirestoreDB firestoreDB)
        {
            database = firestoreDB;
        }

        public async Task<List<Category>> GetCategories()
        {
            List<Category> result = new List<Category>();
            Query queryCategories = database.db.Collection("Categories");
            QuerySnapshot QSCategories = await queryCategories.GetSnapshotAsync();

            foreach (DocumentSnapshot documentSnapshot in QSCategories.Documents)
            {
                Dictionary<string, object> categDictionary = documentSnapshot.ToDictionary();
                var category = new Category(categDictionary);
                result.Add(category);
            }
            return result;
        }

        public async Task<List<Subcategory>> GetSubcategories(string categoryName)
        {
            var docID = await FindCategoryID(categoryName);

            if(string.IsNullOrWhiteSpace(docID))
            {
                return null;
            }
            Query QSSubcategories = database.db.Collection("Categories")
                            .Document(docID).Collection("Subcategories");

            QuerySnapshot querySSubcategories = await QSSubcategories.GetSnapshotAsync();
            List<Subcategory> subcategories = new List<Subcategory>();

            foreach (DocumentSnapshot SubcategSnapshot in querySSubcategories.Documents)
            {
                Dictionary<string, object> subcategDictionary = SubcategSnapshot.ToDictionary();
                var subcategory = new Subcategory(subcategDictionary);
                subcategories.Add(subcategory);
            }
            return subcategories;
        }

        public async Task<List<Product>> GetProducts(string categoryName, string subcategoryName)
        {
            var categoryID = await FindCategoryID(categoryName);

            if(string.IsNullOrWhiteSpace(categoryName))
            {
                return null;
            }
            var subcategoryID = await FindSubcategoryID(subcategoryName, categoryID);
            
            if(string.IsNullOrWhiteSpace(subcategoryName))
            {
                return null;
            }
            CollectionReference productRef = database.db.Collection("Products");
            Query productQuery = productRef.WhereEqualTo("subcategoryID", subcategoryID);
            QuerySnapshot productQS = await productQuery.GetSnapshotAsync();
            List<Product> result = new List<Product>();

            foreach (DocumentSnapshot docSnapshot in productQS.Documents)
            {
                Dictionary<string, object> productData = docSnapshot.ToDictionary();
                Product newProduct = new Product(productData);

                CollectionReference featureRef = productRef.Document(docSnapshot.Id).Collection("Features");
                QuerySnapshot featuresQS = await featureRef.GetSnapshotAsync();

                foreach (DocumentSnapshot snapshot in featuresQS.Documents)
                {
                    Dictionary<string, object> keys = snapshot.ToDictionary();
                    ProductFeatures newFeatureList = new ProductFeatures(keys);
                    newProduct.FeaturesLists = newFeatureList;
                }
                result.Add(newProduct);
            }
            return result;
        }

        public async Task<List<Product>> GetProductsByName(string name)
        {
            CollectionReference productRef = database.db.Collection("Products");
            Query productQuery = productRef.WhereEqualTo("name", name);
            QuerySnapshot productQS = await productQuery.GetSnapshotAsync();
            List<Product> result = new List<Product>();

            foreach (DocumentSnapshot docSnapshot in productQS.Documents)
            {
                Dictionary<string, object> productData = docSnapshot.ToDictionary();
                Product newProduct = new Product(productData);

                CollectionReference featureRef = productRef.Document(docSnapshot.Id).Collection("Features");
                QuerySnapshot featuresQS = await featureRef.GetSnapshotAsync();

                foreach (DocumentSnapshot snapshot in featuresQS.Documents)
                {
                    Dictionary<string, object> keys = snapshot.ToDictionary();
                    ProductFeatures newFeatureList = new ProductFeatures(keys);
                    newProduct.FeaturesLists = newFeatureList;
                }
                result.Add(newProduct);
            }
            return result;
        }

        //////////////////////////////////////////////////////
        //////////////////  HELP METHODS  ////////////////////
        //////////////////////////////////////////////////////

        /// <summary>
        ///     Finds ID for category
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>

        private async Task<string> FindCategoryID(string categoryName)
        {
            Query queryCategories = database.db.Collection("Categories");
            QuerySnapshot QSCategories = await queryCategories.GetSnapshotAsync();

            foreach (DocumentSnapshot categorySnapshot in QSCategories.Documents)
            {
                Dictionary<string, object> categDictionary = categorySnapshot.ToDictionary();
                var category = new Category(categDictionary);

                if (category.CategoryName.Equals(categoryName))
                {
                    return categorySnapshot.Id;
                }
            }
            return null;
        }

        /// <summary>
        ///     Finds ID for subcategory
        /// </summary>
        /// <param name="subcategoryName"></param>
        /// <param name="categoryID"></param>
        /// <returns></returns>

        private async Task<string> FindSubcategoryID(string subcategoryName, string categoryID)
        {
            Query QSSubcategories = database.db.Collection("Categories")
                                    .Document(categoryID).Collection("Subcategories");
            QuerySnapshot querySSubcategories = await QSSubcategories.GetSnapshotAsync();

            foreach (DocumentSnapshot subcategSnapshot in querySSubcategories.Documents)
            {
                Dictionary<string, object> subcategDictionary = subcategSnapshot.ToDictionary();
                var subcategory = new Subcategory(subcategDictionary);
                
                if(subcategory.SubcategoryName.Equals(subcategoryName))
                {
                    return subcategSnapshot.Id;
                }
            }
            return null;
        }
    }
}
