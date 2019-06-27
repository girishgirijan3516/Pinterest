using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;

namespace Pinterest
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class MainActivity : AppCompatActivity
    {
        EditText myUsername, myPassword;
        Button myLoginBtn, mySignup;
        
         
        Android.App.AlertDialog.Builder alert;

        DBHelperClass myDB;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            //initializing objects of form controls
            myUsername = FindViewById<EditText>(Resource.Id.user_name);
            myPassword = FindViewById<EditText>(Resource.Id.user_password);
            myLoginBtn = FindViewById<Button>(Resource.Id.btn_login);
            mySignup = FindViewById<Button>(Resource.Id.btn_signup);

            myDB = new DBHelperClass(this);

            //myUsername.Text = "Username Please";
            // myPassword.Text = "Password Please";
            myLoginBtn.Click += delegate { // login button

                var user_name = myUsername.Text;
                var user_password = myPassword.Text;
                alert = new Android.App.AlertDialog.Builder(this);
                // validation
                if (user_name.Trim().Equals("") || user_name.Length < 0 || user_password.Trim().Equals("") || user_password.Length < 0)
                {
                    alert.SetTitle("Error");
                    alert.SetMessage("Please fill all fields");
                    alert.SetPositiveButton("OK", alertOKButton);
                    Dialog myDialog = alert.Create();
                    myDialog.Show();
                }
                else
                {
                    bool f = myDB.checkUser(user_name.Trim(), user_password.Trim());
                    if (f)
                    {

                        myUsername.Text = ""; myPassword.Text = "";
                        Intent homeScreen = new Intent(this, typeof(Home)); // on success loading signup page   
                        homeScreen.PutExtra("userName", user_name.Trim());
                        homeScreen.PutExtra("userPassword", user_password.Trim());
                        StartActivity(homeScreen);
                    }
                    else
                    {
                        alert.SetTitle("Error");
                        alert.SetMessage("Incorrect email id or password!");
                        alert.SetPositiveButton("OK", alertOKButton);
                        Dialog myDialog = alert.Create();
                        myDialog.Show();
                    }
                }
            };

            mySignup.Click += delegate // signup button
            {
                Intent signupScreen = new Intent(this, typeof(SignUp)); // on success loading signup page
                StartActivity(signupScreen);
            };

            }
        public void alertOKButton(object sender, Android.Content.DialogClickEventArgs e)
        {
            //System.Console.WriteLine("OK Button Pressed");
        }
    }
}