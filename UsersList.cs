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
    [Activity(Label = "UsersList")]
    public class UsersList : Activity
    {
        Android.App.AlertDialog.Builder alert;
        DBHelperClass myDB;

        ListView myUsersList;
        string[] names = new string[] { "Amith", "Prasanna", "Shriya", "Raul" };

        ArrayAdapter<string> Myadapter;
        ArrayAdapter<string> MyTempAdapter;
        SearchView mySearch;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.UsersList);

            // Get ListView object from xml
            mySearch = FindViewById<SearchView>(Resource.Id.searchID);
            myUsersList = FindViewById<ListView>(Resource.Id.myListUsers);

            // Defined Array values to show in ListView
            Myadapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, names);
            // Assign adapter to ListView
            myUsersList.Adapter = Myadapter;

            myUsersList.ItemClick += List1_ItemClick;
            mySearch.QueryTextChange += searchUsers;
        }
        private void List1_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //throw new System.NotImplementedException();
            System.Console.WriteLine(names[e.Position]);
        }
        public void searchUsers(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            var mySearchValue = e.NewText;
            string temp;
            //System.Console.WriteLine("Search Text is :  is \n\n " + mySearchValue);

            List<string> tempArray = new List<string>();
            for (int i = 0; i < names.Length; ++i)
            {
                temp = names[i].ToLower();
                if (temp.Contains(mySearchValue.ToLower()))
                {
                    tempArray.Add(names[i]);

                }
            }
            if (tempArray.Count > 0)
            {
                Myadapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, tempArray);
                myUsersList.Adapter = Myadapter;
            }

        }
    }
}