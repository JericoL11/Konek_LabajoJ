using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using Firebase.Firestore;
using Firebase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Konek_LabajoJ.Resources.Helper
{
    public class AppDataHelper
    {
        public FirebaseFirestore GetFirestore()
        {
            var app = FirebaseApp.InitializeApp(Application.Context);
            FirebaseFirestore database;

            //if null
            if (app == null)
            {
                var options = new FirebaseOptions.Builder()
                    .SetProjectId("konek-labajoj-6efc5")
                    .SetApplicationId("konek-labajoj-6efc5")
                    .SetApiKey("AIzaSyCSXTtQHh61eIRJd_bJ8S37k8nGmFMecRQ")
                    .SetStorageBucket("konek-labajoj-6efc5.appspot.com")
                    .Build();
                app = FirebaseApp.InitializeApp(Application.Context, options);
                database = FirebaseFirestore.GetInstance(app);

            }
            else
            {
                database = FirebaseFirestore.GetInstance(app);
            }

            return database;

        }


        //Auth
        public FirebaseAuth GetFirebaseAuth()
        {
            var app = FirebaseApp.InitializeApp(Application.Context);
            FirebaseAuth mAuth;

            //if null
            if (app == null)
            {
                var options = new FirebaseOptions.Builder()
                   .SetProjectId("konek-labajoj-6efc5")
                    .SetApplicationId("konek-labajoj-6efc5")
                    .SetApiKey("AIzaSyCSXTtQHh61eIRJd_bJ8S37k8nGmFMecRQ")
                    .SetStorageBucket("konek-labajoj-6efc5.appspot.com")
                    .Build();
                app = FirebaseApp.InitializeApp(Application.Context, options);
                mAuth = FirebaseAuth.Instance;

            }
            else
            {
                mAuth = FirebaseAuth.Instance;
            }

            return mAuth;

        }
    }

}