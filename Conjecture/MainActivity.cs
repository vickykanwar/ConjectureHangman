using SQLite;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.IO;

namespace Conjecture
{
    [Activity( Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Android.Widget.EditText txt; 
        Android.Widget.Button btn;
        int height = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
       
           /// getsupport

            txt = FindViewById<EditText>(Resource.Id.edtUser);
            
            btn = FindViewById<Button>(Resource.Id.btnLogin);
            btn.Click += new EventHandler(Btntest_Clicked);
        }

        // enter name to start game
        private void Btntest_Clicked(object sender, EventArgs e)
        {
            String usr = txt.Text.ToString();
            if(usr!="")
            { 
                Intent intent = new Intent(this, typeof(HomeScreen));
                StartActivity(intent);
            }
            else
            {
            Toast.MakeText(Application.Context, "Enter Name", ToastLength.Short).Show();
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}