using Google.Cloud.Firestore;
using System;

namespace StoreAPI.Services
{
    public class FirestoreDB
    {
        public FirestoreDb db;

        public FirestoreDB()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"firebaseJson.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
            db = FirestoreDb.Create("adminpanel-4-izon");
        }
    }
}
