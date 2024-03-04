using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using Konek_LabajoJ.Resources.EventListeners;
using Konek_LabajoJ.Resources.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Konek_LabajoJ.Resources.activities
{
    [Activity(Label = "Login", Theme = "@style/AppTheme", MainLauncher = true)]
    public class Login : Activity
    {
        //widgets
        EditText txt_email, txt_password;
        TextView lnk_forgot_password, lnk_signUp;
        Button btn_login;

        AppDataHelper appDataHelper = new AppDataHelper();
        FirebaseAuth mAuth;
        TaskCompletionListeners taskCL = new TaskCompletionListeners();


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout._Login);
            // Create your application here


            //referencing
            txt_email = FindViewById<EditText>(Resource.Id.txt_email);
            txt_password = FindViewById<EditText>(Resource.Id.txt_password);
            lnk_forgot_password = FindViewById<TextView>(Resource.Id.lnk_forgot);
            lnk_signUp = FindViewById<TextView>(Resource.Id.lnk_signUp);
            btn_login = FindViewById<Button>(Resource.Id.btn_login);
            mAuth = appDataHelper.GetFirebaseAuth();
            lnk_signUp.Click += Lnk_signUp_Click;
            btn_login.Click += Btn_login_Click;
        }

        private void Btn_login_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txt_email.Text) || string.IsNullOrEmpty(txt_password.Text))
            {
                Toast.MakeText(this, "Complete the login form", ToastLength.Short).Show();
                return;
            }


            mAuth.SignInWithEmailAndPassword(txt_email.Text, txt_password.Text)
                .AddOnSuccessListener(this, taskCL)
                .AddOnFailureListener(this, taskCL);

            taskCL.Success += (success, args) =>
            {
                Toast.MakeText(this, "Login Successfully", ToastLength.Short).Show();

                var intent = new Intent(this, typeof(MainActivity));

                //to limit the multiple task 
                intent.AddFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
                StartActivity(intent);
       
            };

            taskCL.Failure += (failure, args) =>
            {
                Toast.MakeText(this, "Login Failed: " + args.Cause, ToastLength.Short).Show();

            };

        }

        private void Lnk_signUp_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(SignUp));

            //to limit the multiple task 
            intent.AddFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
            StartActivity(intent);
        }
    }
}