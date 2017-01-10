using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.Media.Audiofx;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Xamarin.Android;
using SoundPeriodMeasure.Helpers;

namespace SoundPeriodMeasure.Activities
{
    [Activity(Label = "Nowe nagranie")]
    public class RecorderActivity : Activity
    {
        private const string Stop = "STOP";
        private const string Start = "START";
        private const string FileName = "/record.aac";

        private const int DataPointsSize = 90000;
        private double[] _dataPoints;
        private byte[] _dataPointsByte;

        private PlotView _plotView;
        private Recorder _recorder;
        private Button _recordButton;

        private bool _isRecording = false;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Recorder);

            _plotView = FindViewById<PlotView>(Resource.Id.plotView);
            _plotView.Model = PlotHelper.CreatePlotModel();
            _recordButton = FindViewById<Button>(Resource.Id.startMeasure);
            _recorder = new Recorder();

            SetDelegates();
        }

        private void SetDelegates()
        {
            _recordButton.Click += delegate
            {
                if (_isRecording)
                {
                    StopMeasure();
                }
                else
                {
                    StartMeasure();
                }             
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

        private void StartMeasure()
        {
            _recordButton.Text = Stop;
            
            //_dataPoints = new double[DataPointsSize];
            _isRecording = true;

            var dir = this.GetExternalFilesDir(null).Path;
            string path = dir + FileName;
            _recorder.Start(path);

            //ThreadPool.QueueUserWorkItem(o => SaveRecordAmplitude());
        }

        private void StopMeasure()
        {
            _isRecording = false;           
            _recorder.Stop();
            _recordButton.Text = Start;


            var dir = this.GetExternalFilesDir(null).Path;
            string path = dir + FileName;

            _dataPointsByte = File.ReadAllBytes(path);
             SetPlotData();
        }

        private void SetPlotData()//int lastFilledIndex)
        {
            _plotView.Model.Series.Clear();
            //var series = PlotHelper.CreateLineSeries(_dataPoints, lastFilledIndex);
            var series = PlotHelper.CreateLineSeriesFromByte(_dataPointsByte);
            _plotView.Model.Series.Add(series);
            _plotView.InvalidatePlot();
        }

        //private void SaveRecordAmplitude()
        //{
        //    int lastIndex = -1;
        //    for (int i = 0; i < DataPointsSize; i++)
        //    {
        //        SaveCurrentAmplitude(i);
        //        if (!_isRecording)
        //        {
        //            lastIndex = i;
        //            break;
        //        }
        //    }

        //    RunOnUiThread(() =>
        //    {
        //        StopMeasure();
        //        SetPlotData(lastIndex);
        //    });
        //}

        //private void SaveCurrentAmplitude(int index)
        //{
        //    var amp = _recorder.GetAmplitude();
        //    _dataPoints[index] = amp;
        //}
    }
}