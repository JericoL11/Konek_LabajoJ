using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Firebase.Auth;
using Konek_LabajoJ.Resources.activities;
using Konek_LabajoJ.Resources.Helper;
using AlertDialog = Android.App.AlertDialog;
using ToolBar = AndroidX.AppCompat.Widget.Toolbar; 

namespace Konek_LabajoJ
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class MainActivity : AppCompatActivity
    {
        //VARIABLES
        ToolBar toolbar;
        AppDataHelper appDataHelper = new AppDataHelper();
        FirebaseAuth mAuth;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            mAuth = appDataHelper.GetFirebaseAuth();
            toolbar = FindViewById<ToolBar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);


        }

        //inlater
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