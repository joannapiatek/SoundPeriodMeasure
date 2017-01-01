using System;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Android.Views;
using SoundPeriodMeasure.Activities;

namespace SoundPeriodMeasure
{
    [Activity(Label = "SoundPeriodMeasure", MainLauncher = true, Icon = "@drawable/wave")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            SetDelegates();
        }

        private void SetDelegates()
        {
            var newMeasureButton = FindViewById<Button>(Resource.Id.newMeasure);
            newMeasureButton.Click += delegate 
            {
                StartActivity(typeof(RecorderActivity));
            };

            var showMeasuresButton = FindViewById<Button>(Resource.Id.showMeasures);
            showMeasuresButton.Click += delegate
            {
                StartActivity(typeof(SavedResultsActivity));
            };
        }
    }
}

