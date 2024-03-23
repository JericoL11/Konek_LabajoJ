using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Konek_LabajoJ.Resources.activities
{

    [Activity(Label = "createPost", Theme = "@style/AppTheme", MainLauncher = false)]
  

    public class createPost : Activity
    {

        ImageView createPost_Back, createPostimage;
        Button  createpostSubmit;
        EditText createTextpost;
        TextView createpostCancel;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout._createpost);

            
            createPostimage = FindViewById<ImageView>(Resource.Id.createPostimage);
            createpostSubmit = FindViewById<Button>(Resource.Id.createpostSubmit);
            createpostCancel = FindViewById<TextView>(Resource.Id.createpostCancel);
            createTextpost = FindViewById<EditText>(Resource.Id.createTextpost);

            createpostCancel.Click += CreatepostCancel_Click1;

        }

        private void CreatepostCancel_Click1(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(MainActivity));

            //to limit the multiple task 
            intent.AddFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
            StartActivity(intent);
        }



    }
}