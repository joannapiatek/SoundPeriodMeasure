using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using OxyPlot;
using OxyPlot.Xamarin.Android;
using SoundPeriodMeasure.Helpers;
using SoundPeriodMeasure.SmallClasses;

namespace SoundPeriodMeasure.Activities
{
    [Activity(Label = "New measure")]
    public class RecorderActivity : Activity
    {
        private const string Stop = "STOP";
        private const string Start = "START";
        
        private const int DataPointsSize = 90000;
        private AmplitudeInTime [] _dataPoints;
        private List<AmplitudeInTime> _maximas;

        private PlotView _plotView;
        private Recorder _recorder;
        private Button _recordButton;
        private TextView _resulTextView;

        private bool _isRecording = false;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Recorder);

            _plotView = FindViewById<PlotView>(Resource.Id.plotView);
            _plotView.Model = PlotHelper.CreatePlotModel();

            _resulTextView = FindViewById<TextView>(Resource.Id.resultsView);
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
            if (_isRecording) return;

            var dialogBuilder = new AlertDialog.Builder(this);

            EditText userInput = new EditText(this);
            userInput.InputType = Android.Text.InputTypes.TextVariationNormal;

            dialogBuilder.SetTitle("Enter file Name");
            dialogBuilder.SetView(userInput);
            dialogBuilder.SetPositiveButton(
                "Ok",
                (see, ess) =>
                {
                    if (userInput.Text != string.Empty)
                    {
                        FilesHelper.SaveTextToFile(_resulTextView.Text, userInput.Text);
                        Toast.MakeText(this, "File has been saved.", ToastLength.Long).Show();
                    }
                    else
                    {
                        Toast.MakeText(this, "File hasn't been saved.", ToastLength.Long).Show();
                    }
                    HideKeyboard(userInput);
                });
            dialogBuilder.SetNegativeButton("Cancel", (afk, kfa)
                =>
            {
                HideKeyboard(userInput);
                dialogBuilder.Dispose();
            });

            AlertDialog dialog = dialogBuilder.Create();
            dialog.Show();
            userInput.RequestFocus();
            ShowKeyboard(dialog);           
        }

        private void StartMeasure()
        {
            _recordButton.Text = Stop;
            
            _dataPoints = new AmplitudeInTime[DataPointsSize];
            _isRecording = true;
            _recorder.Start();

            ThreadPool.QueueUserWorkItem(o => SaveRecordAmplitude());
        }

        private void StopMeasure()
        {
            _isRecording = false;           
            _recorder.Stop();
            _recordButton.Text = Start;
        }

        private void SetPlotData()
        {
            _plotView.Model.Series.Clear();
            
            var mainSeries = PlotHelper.CreateLineSeries(_dataPoints);          
            _plotView.Model.Series.Add(mainSeries);
            
            var maximasSeries = PlotHelper.CreateLineSeries(_maximas.ToArray());
            maximasSeries.MarkerStroke = OxyColors.Red;
            _plotView.Model.Series.Add(maximasSeries);

            _plotView.InvalidatePlot();
        }

        private void SaveRecordAmplitude()
        {
            for (int i = 0; i < DataPointsSize; i++)
            {
                SaveCurrentAmplitude(i);
                if (!_isRecording)
                {
                    break;
                }
            }

            RunOnUiThread(() =>
            {
                StopMeasure();
                PrepareAndShowResultsInUi();
            });
        }

        private void PrepareAndShowResultsInUi()
        {
            _dataPoints = _dataPoints.Where(v => v != null && v.Amplitude > 0).ToArray();
            _maximas = DataAnalyser.FindMaximas(_dataPoints);
            SetPlotData();

            var resultsText = TextFormatter.FormatAsResults(_maximas);
            _resulTextView.Text = resultsText;
        }

        private void SaveCurrentAmplitude(int index)
        {
            var ampInTime = _recorder.GetAmplitudeInTime();
            _dataPoints[index] = ampInTime;
        }

        private void ShowKeyboard(AlertDialog dialog)
        {
            dialog.Window.ClearFlags(WindowManagerFlags.NotFocusable | WindowManagerFlags.AltFocusableIm);
            dialog.Window.SetSoftInputMode(SoftInput.StateVisible);

            InputMethodManager imm = (InputMethodManager)this.GetSystemService(InputMethodService);
            imm.ToggleSoftInput(ShowFlags.Forced, 0);
        }

        private void HideKeyboard(EditText userInput)
        {
            InputMethodManager imm = (InputMethodManager)this.GetSystemService(InputMethodService);
            imm.HideSoftInputFromWindow(userInput.WindowToken, 0);
        }
    }
}