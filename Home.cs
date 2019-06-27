using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Pinterest
{
    [Activity(Label = "Home")]
    public class Home : Activity
    {
        TextView myText_username;
        string userName, userPass;
        EditText myFirstName, myLastName, myAge, mySpassword, myScpassword;
        Button editBtn ,deleteBtn, usersListBtn;
        DBHelperClass myDB;

        string[] userData;

        Android.App.AlertDialog.Builder alert;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Home);

            myText_username = FindViewById<TextView>(Resource.Id.lb_main_head);
            myDB = new DBHelperClass(this);

            userName = Intent.GetStringExtra("userName");
            userPass = Intent.GetStringExtra("userPassword");
            myText_username.Text = "Welcome, " + myDB.getUsername(userName);
            // Create your application here

            //Object initialization
            myFirstName = FindViewById<EditText>(Resource.Id.txt_first_name);
            myLastName = FindViewById<EditText>(Resource.Id.txt_last_name);

            alert = new Android.App.AlertDialog.Builder(this);

            myAge = FindViewById<EditText>(Resource.Id.txt_age);
            mySpassword = FindViewById<EditText>(Resource.Id.txt_password);
            myScpassword = FindViewById<EditText>(Resource.Id.txt_con_password);

            editBtn = FindViewById<Button>(Resource.Id.editBtn1);
            deleteBtn = FindViewById<Button>(Resource.Id.btnDelete);
            usersListBtn = FindViewById<Button>(Resource.Id.list_of_users_Btn);

            userData = myDB.getUserDetails(userName);// feteching user details

            myFirstName.Text = userData[0];
            myLastName.Text = userData[1];
            myAge.Text = userData[2];
            mySpassword.Text = userData[3];
            myScpassword.Text = userData[3];

            /*myFirstName.Text = myDB.getUserDetails(userName)[0];
            myLastName.Text = myDB.getUserDetails(userName)[1];
            myAge.Text = myDB.getUserDetails(userName)[2];
            mySpassword.Text = myDB.getUserDetails(userName)[3];
            myScpassword.Text = myDB.getUserDetails(userName)[3];
            myFirstName.Enabled = false;
            System.Console.WriteLine("Name from Login ---> " + userName);
            System.Console.WriteLine("Pasword from Login ---> " + userPass);*/


            editBtn.Click += editBtnClicEvent;
            deleteBtn.Click += deleteBtnClicEvent;
            usersListBtn.Click += usersListBtnClicEvent;
        }

        public void usersListBtnClicEvent(object sender, EventArgs e)
        {
            Intent usersListScreen = new Intent(this, typeof(UsersList)); // loading users list page               
            StartActivity(usersListScreen);
        }
        public void deleteBtnClicEvent(object sender, EventArgs e)
        { 
            
            alert.SetTitle("Information");
            alert.SetMessage("Are you sure you want to delete account?");
            alert.SetPositiveButton("Yes", alertDeleteOKButton);
            alert.SetNegativeButton("No", alertOKButton);
            Dialog myDialog = alert.Create();
            myDialog.Show();
        }
        public void editBtnClicEvent(object sender, EventArgs e)
        {
            /* myFirstName.Enabled = true;
             editBtn.Text = "Save";
             string value = myFirstName.Text;*/
            alert.SetTitle("Error");
            if (myFirstName.Text.Trim().Equals("") || myFirstName.Text.Length < 0 || myLastName.Text.Trim().Equals("") || myLastName.Text.Length < 0 || myAge.Text.Trim().Equals("") || myAge.Text.Length < 0 || mySpassword.Text.Trim().Equals("") || mySpassword.Text.Length < 0 || myScpassword.Text.Trim().Equals("") || myScpassword.Text.Length < 0)
            {
                alert.SetMessage("Please fill all fields");
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
                myDB.updateData(myFirstName.Text.Trim(), myLastName.Text.Trim(), userName, Int32.Parse(myAge.Text.Trim()), mySpassword.Text.Trim());
                alert.SetTitle("Information");
                alert.SetMessage("Information Updated successfully!");
                alert.SetPositiveButton("OK", alertSuccessOKButton);
                Dialog myDialog = alert.Create();
                myDialog.Show();

               
            }
        }
        public void alertOKButton(object sender, Android.Content.DialogClickEventArgs e)
        {
            //System.Console.WriteLine("OK Button Pressed");
        }



        public void alertSuccessOKButton(object sender, Android.Content.DialogClickEventArgs e)
        {
            //System.Console.WriteLine("OK Button Pressed");
            Intent homeScreen = new Intent(this, typeof(Home)); // on success loading signup page   
            homeScreen.PutExtra("userName", userName);
            homeScreen.PutExtra("userPassword", userPass);
            StartActivity(homeScreen);
        }
        public void alertDeleteOKButton(object sender, Android.Content.DialogClickEventArgs e)
        {
            //System.Console.WriteLine("OK Button Pressed");
            myDB.deleteUser(userName);
            Intent loginScreen = new Intent(this, typeof(MainActivity)); // on success loading signup page            
            StartActivity(loginScreen);
        }

    }
    
}