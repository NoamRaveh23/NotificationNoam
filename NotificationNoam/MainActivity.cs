using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;

namespace NotificationNoam
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Button start , stop;

        public string NOTIFICATION_CHANNEL_ID = "talya";
        public int NOTIFICATION_ID = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            start = (Button)FindViewById(Resource.Id.bts);
            stop = (Button)FindViewById(Resource.Id.btp);
            start.Click += Start_Click;
            stop.Click += Stop_Click;
        }

        private void Stop_Click(object sender, System.EventArgs e)
        {
            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.Cancel(NOTIFICATION_ID);
        }

        [System.Obsolete]
        private void Start_Click(object sender, System.EventArgs e)
        {
            Intent i = new Intent(this, typeof(SecondActivity));
            i.PutExtra("key", "new message");
            PendingIntent pendingIntent = PendingIntent.GetActivity(this, 0, i, 0);
            Notification.Builder notificationBuilder = new Notification.Builder(this)
            .SetSmallIcon(Resource.Drawable.icon)
            .SetContentTitle("SUCCESS")
            .SetContentText("Notification is working");
            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationBuilder.SetContentIntent(pendingIntent);
            //Build.VERSION_CODES.O - is a reference to API level 26 (Android Oreo which is Android 8)
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                NotificationChannel notificationChannel = new NotificationChannel(NOTIFICATION_CHANNEL_ID,
                "NOTIFICATION_CHANNEL_NAME", NotificationImportance.High);
                notificationBuilder.SetChannelId(NOTIFICATION_CHANNEL_ID);
                notificationManager.CreateNotificationChannel(notificationChannel);
            }
            notificationManager.Notify(NOTIFICATION_ID, notificationBuilder.Build());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}