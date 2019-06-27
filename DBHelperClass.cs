using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Database; //Step 1
using Android.Database.Sqlite; //Step 2

namespace Pinterest
{
    public class DBHelperClass : SQLiteOpenHelper
    {
        Context myContex;  // Step 4

        // // Step 6
        public static string DBName = "pinterestDB.db";        

        public string createTableQuery = "CREATE TABLE Users (user_id INTEGER PRIMARY KEY AUTOINCREMENT, first_name text NOT NULL, last_name text NOT NULL,email_id text, age INTEGER, password text);";

        SQLiteDatabase connectionObj;
        public DBHelperClass(Context context) : base(context, name: DBName, factory: null, version: 1) //  // Step 5
        {
            myContex = context;
            connectionObj = WritableDatabase;
        }
        public override void OnCreate(SQLiteDatabase db)
        {
            //throw new NotImplementedException();
            //System.Console.WriteLine("Table query\n \n" + createTableQuery);
            db.ExecSQL(createTableQuery);    // // Step 7
        }
        public Boolean insertValue(string f_name, string l_name, string email_id, int age, string password)
        {



            

            string selectQuery = "SELECT * FROM Users WHERE email_id = '" + email_id + "'";
            ICursor myresut = connectionObj.RawQuery(selectQuery, null);

            if (myresut.Count > 0)
                return false;
            else
            {
                string insertQuery = "INSERT INTO Users(first_name, last_name, email_id, age, password)VALUES('" + f_name + "', '" + l_name + "', '" + email_id + "', " + age + ", '" + password + "')";
                //System.Console.WriteLine("Insert Query \n  \n" + insertQuery);
                connectionObj.ExecSQL(insertQuery);
                return true;
            }
                

            

        }
        public Boolean checkUser(string email_id, string pass)
        {
            string selectQuery = "SELECT * FROM Users WHERE email_id = '" + email_id + "' and password='"+ pass + "'";
            ICursor myresut = connectionObj.RawQuery(selectQuery, null);
            
            if (myresut.Count > 0)
            {
                //
                while (myresut.MoveToNext())
                {                    
                    System.Console.WriteLine("First Name :" + myresut.GetString(myresut.GetColumnIndexOrThrow("first_name")));
                    System.Console.WriteLine("Last Name :" + myresut.GetString(myresut.GetColumnIndexOrThrow("last_name")));
                }

                return true;
            }               
            else
                return false;
        }

        public void updateData(string f_name, string l_name, string email_id, int age, string password)
        {
            string updateQuery = "update Users set first_name = '" + f_name + "',last_name = '"+ l_name + "', age = "+ age + ", password = '"+ password + "' where email_id = '"+ email_id + "'";
            connectionObj.ExecSQL(updateQuery);
        }

        public void deleteUser(string email_id)
        {
            string deleteQuery = "delete from Users where email_id = '" + email_id + "'";
            Console.WriteLine(deleteQuery);
            connectionObj.ExecSQL(deleteQuery);
        }

        public string getUsername(string email_id)
        {
            string username = "";
            string selectQuery = "SELECT * FROM Users WHERE email_id = '" + email_id + "'";
            ICursor myresut = connectionObj.RawQuery(selectQuery, null);
            while (myresut.MoveToNext())
            {
                username =  myresut.GetString(myresut.GetColumnIndexOrThrow("first_name")) + " " + myresut.GetString(myresut.GetColumnIndexOrThrow("last_name"));               
            }
            return username;
        }

        public string[] getUserDetails(string email_id)
        {
            string[] userDetails = new string[4];
            userDetails[0] = "Girish";
            string selectQuery = "SELECT * FROM Users WHERE email_id = '" + email_id + "'";
            ICursor myresut = connectionObj.RawQuery(selectQuery, null);
            while (myresut.MoveToNext())
            {
                userDetails[0] = myresut.GetString(myresut.GetColumnIndexOrThrow("first_name")) ;
                userDetails[1] = myresut.GetString(myresut.GetColumnIndexOrThrow("last_name"));
                userDetails[2] = myresut.GetString(myresut.GetColumnIndexOrThrow("age"));
                userDetails[3] = myresut.GetString(myresut.GetColumnIndexOrThrow("password"));
            }
            return userDetails;
        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            throw new NotImplementedException();
        }
    }
}