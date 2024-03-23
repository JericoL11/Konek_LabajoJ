using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.RecyclerView.Widget;
using Firebase.Auth;
using Konek_LabajoJ.Resources.activities;
using Konek_LabajoJ.Resources.Adapter;
using Konek_LabajoJ.Resources.DataModels;
using Konek_LabajoJ.Resources.Helper;
using System;
using System.Collections.Generic;
using AlertDialog = Android.App.AlertDialog;
using ToolBar = AndroidX.AppCompat.Widget.Toolbar;
using RecyclerView = AndroidX.RecyclerView.Widget.RecyclerView;

namespace Konek_LabajoJ
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class MainActivity : AppCompatActivity
    {
        //VARIABLES
        ToolBar toolbar;
        AppDataHelper appDataHelper = new AppDataHelper();
        FirebaseAuth mAuth;
        RecyclerView postRecyclerView;
        PostAdapter postAdapter;
        RelativeLayout RL_createPost;

        List<Post> listofPost = new List<Post>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            mAuth = appDataHelper.GetFirebaseAuth();
            toolbar = FindViewById<ToolBar>(Resource.Id.toolbar);
            RL_createPost = FindViewById<RelativeLayout>(Resource.Id.RL_createPost);

            //get from mainlayout
            postRecyclerView = FindViewById<RecyclerView>(Resource.Id.postRecyclerView);
            RL_createPost.Click += RL_createPost_Click;


            CreateData();
            SetUpRecyclerView();
            SetSupportActionBar(toolbar);


        }

        private void RL_createPost_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(createPost));

            //to limit the multiple task 
            intent.AddFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
            StartActivity(intent);
        }

        void SetUpRecyclerView()
        {
            postRecyclerView.SetLayoutManager(new AndroidX.RecyclerView.Widget.LinearLayoutManager(postRecyclerView.Context));

            postAdapter = new PostAdapter(listofPost);
            postRecyclerView.SetAdapter(postAdapter);

        }

         void CreateData()
        {
            listofPost.Add(new Post
            {
                Username = "Jerico Labajo",
                Description = "Hello Xamarin",
                LikeCount = 14

            });

            listofPost.Add(new Post
            {
                Username = "Jane Blns",
                Description = "Making all the this beauty",
                LikeCount = 25

            });

            listofPost.Add(new Post
            {
                Username = "Venus Faith",
                Description = "Hello, From the universe!",
                LikeCount = 95

            });


          

        }

        //inflater
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.feed_menu, menu);
            return true;
        }


        //function for menu items
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            if (id == Resource.Id.action_Logout)
            {
                AlertDialog.Builder builder = new AlertDialog.Builder(this);
                builder.SetTitle("Confirm");
                builder.SetMessage("Are you sure you want to logout?");

                builder.SetPositiveButton("Yes", (sender, e) =>
                {
                    mAuth.SignOut();
                    Toast.MakeText(this, "Logout Successful", ToastLength.Long).Show();
                    var intent = new Intent(this, typeof(Login));

                    //to limit the multiple task 
                    intent.AddFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
                    StartActivity(intent);

                });

                builder.SetNegativeButton("Cancel", (sender, e) =>
                {
                    return;
                });

                Dialog dialog = builder.Show();
                dialog.Show();
            }

            if (id == Resource.Id.action_refresh)
            {
                Toast.MakeText(this, "refreshing", ToastLength.Long).Show();
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}