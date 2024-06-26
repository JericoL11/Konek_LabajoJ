﻿using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.Hardware.Camera2.Params;
using Android.OS;
using Android.Renderscripts;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Firebase.Storage;
using Konek_LabajoJ.Resources.EventListeners;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using AlertDialog = AndroidX.AppCompat.App.AlertDialog;
using Bitmap = Android.Graphics.Bitmap;

namespace Konek_LabajoJ.Resources.activities
{

    [Activity(Label = "createPost")]


    public class createPost : AppCompatActivity
    {

        ImageView createPost_Back, createPostimage;
        Button createpostSubmit;
        EditText createTextpost;
        TextView createpostCancel;
        TaskCompletionListeners listener = new TaskCompletionListeners();

        byte[] fileBytes;


        readonly string[] permissionGroup =
        {
               Manifest.Permission.ReadExternalStorage,
               Manifest.Permission.WriteExternalStorage,
               Manifest.Permission.Camera
    };

      /*  CameraClass cc = new CameraClass();*/      
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            //referencing
            SetContentView(Resource.Layout._createpost);

            
            createPostimage = FindViewById<ImageView>(Resource.Id.createPostimage);
            createpostSubmit = FindViewById<Button>(Resource.Id.createpostSubmit);
            createpostCancel = FindViewById<TextView>(Resource.Id.createpostCancel);
            createTextpost = FindViewById<EditText>(Resource.Id.createTextpost);

            createpostCancel.Click += CreatepostCancel_Click1;
            createPostimage.Click += CreatePostimage_Click ;
            createpostSubmit.Click += CreatepostSubmit_Click;

            //calling the permission method
            RequestPermissions(permissionGroup, 0);
        }

        private void CreatepostSubmit_Click(object sender, EventArgs e)
        {
            StorageReference sr;

            if (fileBytes != null)
            {
                sr = FirebaseStorage.Instance.GetReference("postImages/*" + generateRandomString(10));
                sr.PutBytes(fileBytes)
                    .AddOnSuccessListener(listener)
                    .AddOnFailureListener(listener);

                listener.Success += (obj, args) =>
                {

                    AlertDialog.Builder builder = new AlertDialog.Builder(this);
                    builder.SetMessage("Image successfully uploaded");

                    builder.SetPositiveButton("okay", (sender, e) => {

                        StartActivity(typeof(MainActivity));
                        return;
                    });

                    Dialog dialog = builder.Create();
                    dialog.Show();
                };

                listener.Failure += (obj, args) =>
                {

                    AlertDialog.Builder builder = new AlertDialog.Builder(this);
                    builder.SetMessage("Upload failed due to " + args.Cause);

                    builder.SetPositiveButton("okay", (sender, e) => {

                        StartActivity(typeof(MainActivity));
                        return;
                    });

                    Dialog dialog = builder.Create();
                    dialog.Show();
                };
            }
        }


        private void CreatePostimage_Click(object sender, EventArgs e)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.SetMessage("Change Photo");

            builder.SetPositiveButton("Take Photo", (sender, e) =>{
                takePhoto();
            });

            builder.SetNegativeButton("Upload Photo", (sender, e) => {
                selectPhoto();  
            });

            Dialog dialog = builder.Create();
            dialog.Show();  
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        async void takePhoto()
        {
           await CrossMedia.Current.Initialize();

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                CompressionQuality = 20,
                Directory = "Sample",
                Name = $"{generateRandomString(5)}{DateTime.Now.ToShortDateString()}_konek.jpg"
            }); 
          
            if(file == null)
            {
                return;
            }

            byte[] imageArray = System.IO.File.ReadAllBytes(file.Path);
            fileBytes = imageArray;
            Bitmap bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
            createPostimage.SetImageBitmap(bitmap);
        }


       async void selectPhoto()
      {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsTakePhotoSupported)
            {
                AlertDialog.Builder builder = new AlertDialog.Builder(this);
                builder.SetMessage("Photo not supported.");

                builder.SetPositiveButton("OK", (sender, e) => {
                    return;
                });
            }

            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                CompressionQuality = 20,

            });

            if (file == null)
            {
                return;
            }

            byte[] imageArray = System.IO.File.ReadAllBytes(file.Path);
            fileBytes = imageArray;
            Bitmap bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
            createPostimage.SetImageBitmap(bitmap);


        }

            private void CreatepostCancel_Click1(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(MainActivity));

            //to limit the multiple task 
            intent.AddFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
            StartActivity(intent);
        }

        string generateRandomString(int length)
        {
            Random random = new Random();
            char[] allowChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

            string eResult = "";

            for(int i =0; i < length; i++)
            {
                eResult += allowChars[random.Next(0, allowChars.Length)];
            }
            return eResult;
        }


    }
}