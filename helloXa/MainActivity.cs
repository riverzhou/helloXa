using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.BottomNavigation;
using System;
using System.Collections.Generic;

namespace helloXa
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnItemSelectedListener
    {
        private TextView textMessage;
        private readonly List<String> showInfo = new List<String>();
        private readonly int showCount = 28;
        //private readonly Queue<string> printQueue = new Queue<string>();
        //private readonly AutoResetEvent printEvent = new AutoResetEvent(false);

        BleScaner scanner = null;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            textMessage = FindViewById<TextView>(Resource.Id.message);

            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnItemSelectedListener(this);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.navigation_home:
                    if (scanner == null)
                    {
                        scanner = new BleScaner(this);
                    }
                    scanner?.StartAsync();
                    //textMessage.SetText(Resource.String.title_home);
                    return true;
                case Resource.Id.navigation_dashboard:
                    textMessage.SetText(Resource.String.title_dashboard);
                    return true;
                case Resource.Id.navigation_notifications:
                    textMessage.SetText(Resource.String.title_notifications);
                    return true;
                case Resource.Id.navigation_misc:
                    textMessage.SetText(Resource.String.title_misc);
                    return true;
            }
            return false;
        }

        public void Print(String info)
        {
            lock (textMessage)
            {
                showInfo.Add(info);
                if (showInfo.Count > showCount)
                {
                    showInfo.RemoveAt(0);
                }
                String output = "";
                foreach (String s in showInfo)
                {
                    output += s + "\n";
                }
                textMessage.SetText(output, TextView.BufferType.Normal);
            }
        }
    }
}

