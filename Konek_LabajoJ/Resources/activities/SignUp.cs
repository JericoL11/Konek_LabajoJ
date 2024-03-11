using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using Firebase.Firestore;
using Konek_LabajoJ.Resources.EventListeners;
using Konek_LabajoJ.Resources.Helper;
using System;
//for email validation
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Java.Util;

namespace Konek_LabajoJ.Resources.activities
{
    [Activity(Label = "SignUp", Theme = "@style/AppTheme", MainLauncher = false)]
    public class SignUp : Activity
    {

        EditText txt_Firstname, txt_Lastname, txt_Email, txt_SU_password, txt_SU_Confirmpass;
        Button btn_SignUp;
        TextView lnk_BacktoLogin;

        FirebaseFirestore db;
        FirebaseAuth mAuth;
        TaskCompletionListeners taskCL = new TaskCompletionListeners();


        AppDataHelper AppDataHelpers = new AppDataHelper();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout._SignUp);
            // Create your application here

            //Referencing
            txt_Firstname = FindViewById<EditText>(Resource.Id.txt_Firstname);
            txt_Lastname = FindViewById<EditText>(Resource.Id.txt_Lastname);
            txt_Email = FindViewById<EditText>(Resource.Id.txt_SU_Email);
            txt_SU_password = FindViewById<EditText>(Resource.Id.txt_SU_password);
            txt_SU_Confirmpass = FindViewById<EditText>(Resource.Id.txt_SU_Confirmpass);
            btn_SignUp = FindViewById<Button>(Resource.Id.btn_SignUp);
            lnk_BacktoLogin = FindViewById<TextView>(Resource.Id.lnk_BacktoLogin);
            db = AppDataHelpers.GetFirestore();
            mAuth = AppDataHelpers.GetFirebaseAuth();





            //Event handler
            lnk_BacktoLogin.Click += Lnk_BacktoLogin_Click;
            btn_SignUp.Click += Btn_SignUp_Click;


        }


        #region === email validation === 

        private bool IsEmailValid(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            // Define a regular expression pattern for a valid email address
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            // Create a Regex object
            Regex regex = new Regex(emailPattern);

            // Use the IsMatch method to check if the email matches the pattern
            return regex.IsMatch(email);
        }

        #endregion
        private void Btn_SignUp_Click(object sender, EventArgs e)
        {
            //restrictions


            if (string.IsNullOrEmpty(txt_Firstname.Text) |
            string.IsNullOrEmpty(txt_Lastname.Text) |
            string.IsNullOrEmpty(txt_Email.Text) |
            string.IsNullOrEmpty(txt_SU_password.Text) |
            string.IsNullOrEmpty(txt_SU_Confirmpass.Text))
            {

                Toast.MakeText(this, "Please fill out the form completely.", ToastLength.Short).Show();
                return;
            }

            if (txt_SU_password.Text.Length < 8 || txt_SU_password.Text.Length > 16)
            {
                Toast.MakeText(this, "Password should have 8 characters but not more than 16.", ToastLength.Short).Show();
                return;
            }
            //password matching
            if (txt_SU_password.Text != txt_SU_Confirmpass.Text)
            {

                Toast.MakeText(this, "Password doesn't match", ToastLength.Short).Show();
                return;
            }

            //EMAIL verification
            //variable with verification method

            bool isValid = IsEmailValid(txt_Email.Text);

            // Display validation result (you can customize this part based on your requirements)
            if (isValid)
            {
                txt_Email.Error = null; // Clear error message
                                        // UpdateDatabase();


                mAuth.CreateUserWithEmailAndPassword(txt_Email.Text, txt_SU_password.Text).AddOnSuccessListener(
                    this, taskCL).AddOnFailureListener(this, taskCL);

                //success
                taskCL.Success += (success, args) =>
                {
                    HashMap userMap = new HashMap();

                    userMap.Put("FirstName", txt_Firstname.Text);
                    userMap.Put("LastName", txt_Lastname.Text);
                    userMap.Put("Email", txt_Email.Text);
                    userMap.Put("Password", txt_SU_password.Text);

                    //Creating Document in firebase
                    DocumentReference userRef = db.Collection("UserDetails").Document(mAuth.CurrentUser.Uid);

                    //inpserting the data
                    userRef.Set(userMap);

                    Toast.MakeText(this, "Successfully Registered", ToastLength.Short).Show();
                    StartActivity(typeof(Login));


                };


                //failure
                taskCL.Failure += (failure, args) =>
                {
                    Toast.MakeText(this, "Registration Failed" + args.Cause, ToastLength.Short).Show();
                };

            }
            else
            {
                txt_Email.Error = "Invalid email format";
                return;
            }

        }

        private void Lnk_BacktoLogin_Click(object sender, EventArgs e)
        {

            var intent = new Intent(this, typeof(Login));

            //to limit the multiple task 
            intent.AddFlags(ActivityFlags.ClearTask | ActivityFlags.SingleTop);
            StartActivity(intent);
            Default();
        }


        public void Default()
        {
            //default
            txt_Firstname.Text = "";
            txt_Lastname.Text = "";
            txt_Email.Text = "";
            txt_SU_password.Text = "";
            txt_SU_Confirmpass.Text = "";

            txt_Firstname.RequestFocus();


        }

    }
}