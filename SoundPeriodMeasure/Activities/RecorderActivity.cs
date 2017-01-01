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

namespace SoundPeriodMeasure.Activities
{
    [Activity(Label = "Nowe nagranie")]
    public class RecorderActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Recorder);

            SetDelegates();
        }

        private void SetDelegates()
        {
            var startMeasureButton = FindViewById<Button>(Resource.Id.startMeasure);
            startMeasureButton.Click += delegate
            {
                NewMeasure();
            };

            var saveButton = FindViewById<Button>(Resource.Id.saveMeasure);
            saveButton.Click += delegate
            {
                SaveMeasure();
            };
        }

        private void SaveMeasure()
        {
            throw new NotImplementedException();
        }

        private void NewMeasure()
        {
            throw new NotImplementedException();
        }
    }
}