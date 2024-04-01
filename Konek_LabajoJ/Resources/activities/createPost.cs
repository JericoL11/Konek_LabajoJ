using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Hardware.Camera2.Params;
using Android.OS;
using Android.Renderscripts;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
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
            createPostimage.Click += CreatePostimage_Click ;
        }

        private void CreatePostimage_Click(object sender, EventArgs e)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);

            builder.SetMessage("Change Photo");
            builder.SetPositiveButton("Take Photo", (sender, e) =>{
           //     takePhoto();
            });

            builder.SetNegativeButton("Upload Photo", (sender, e) => {
                //     uploadPhoto();
            });
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        //DARIA NAKA 10 MINS
    /*    async void takePhoto()
        {
            await CrossMedia.Current.Initialize();
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions){

                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                CompressionQuality = 20,
                Directory = "Sample",
                Name = Generate
            });
         }*/


        private void CreatepostCancel_Click1(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(MainActivity));

            //to limit the multiple task 
            intent.AddFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
            StartActivity(intent);
        }



    }
}