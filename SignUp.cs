using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;


namespace Pinterest
{
    [Activity(Label = "SignUp")]
    public class SignUp : Activity
    {
        //Declaration section
        Button myAlreadyLogin, mySignUp;
        EditText myFirstName, myLastName, myEmail, myAge, mySpassword, myScpassword;
        Android.App.AlertDialog.Builder alert;

        DBHelperClass myDB;

        static string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
      @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
      @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        Regex re = new Regex(strRegex);

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SignUp);

            //Object initialization
            myFirstName = FindViewById<EditText>(Resource.Id.txt_first_name);
            myLastName = FindViewById<EditText>(Resource.Id.txt_last_name);
            myEmail = FindViewById<EditText>(Resource.Id.txt_email);
            myAge = FindViewById<EditText>(Resource.Id.txt_age);
            mySpassword = FindViewById<EditText>(Resource.Id.txt_password);
            myScpassword = FindViewById<EditText>(Resource.Id.txt_con_password);

            

            alert = new Android.App.AlertDialog.Builder(this);
            myDB = new DBHelperClass(this);

            myAlreadyLogin = FindViewById<Button>(Resource.Id.btn_already_login);
            mySignUp = FindViewById<Button>(Resource.Id.btn_signup);

            // Function definition
            myAlreadyLogin.Click += delegate
            { // Already login button  
                Intent loginScreen = new Intent(this, typeof(MainActivity)); // on success loading signup page
                StartActivity(loginScreen);
            };
            mySignUp.Click += delegate
            { // Signup button
                alert.SetTitle("Error");
                if (myFirstName.Text.Trim().Equals("") || myFirstName.Text.Length < 0 || myLastName.Text.Trim().Equals("") || myLastName.Text.Length < 0 || myEmail.Text.Trim().Equals("") || myEmail.Text.Length < 0 || myAge.Text.Trim().Equals("") || myAge.Text.Length < 0 || mySpassword.Text.Trim().Equals("") || mySpassword.Text.Length < 0 || myScpassword.Text.Trim().Equals("") || myScpassword.Text.Length < 0 )
                {                    
                    alert.SetMessage("Please fill all fields");
                    alert.SetPositiveButton("OK", alertOKButton);
                    Dialog myDialog = alert.Create();
                    myDialog.Show();
                }
                else if (!re.IsMatch(myEmail.Text.Trim()))
                {
                    alert.SetMessage("Please enter valid Email address");
                    alert.SetPositiveButton("OK", alertOKButton);
                    Dialog myDialog = alert.Create();
                    myDialog.Show();
                }
                else if (mySpassword.Text.Trim() != myScpassword.Text.Trim())
                {
                    alert.SetMessage("Passwords are not matching");
                    alert.SetPositiveButton("OK", alertOKButton);
                    Dialog myDialog = alert.Create();
                    myDialog.Show();
                }
                else
                {
                    Boolean f = myDB.insertValue(myFirstName.Text.Trim(), myLastName.Text.Trim(), myEmail.Text.Trim(), Int32.Parse(myAge.Text.Trim()), mySpassword.Text.Trim());
                    if (f)
                    {
                        myFirstName.Text = ""; myLastName.Text = ""; myEmail.Text = ""; myAge.Text = ""; mySpassword.Text = ""; myScpassword.Text = "";
                        alert.SetMessage("Registration successfull!");
                        
                    }
                    else
                    {                        
                        alert.SetMessage("User already exist!");
                    }
                    alert.SetTitle("Information");
                    alert.SetPositiveButton("OK", redirectToLogin);
                    Dialog myDialog = alert.Create();
                    myDialog.Show();
                }
            };
        }
        public void redirectToLogin(object sender, Android.Content.DialogClickEventArgs e)
        {
            //System.Console.WriteLine("OK Button Pressed");
            Intent LoginScreen = new Intent(this, typeof(MainActivity)); // on success loading signup page
            StartActivity(LoginScreen);
        }
        public void alertOKButton(object sender, Android.Content.DialogClickEventArgs e)
        {
            //System.Console.WriteLine("OK Button Pressed");
        }
    }
}