using System;
using System.Diagnostics;
using Android.Media;
using SoundPeriodMeasure.SmallClasses;

namespace SoundPeriodMeasure.Helpers
{
    public class Recorder
    {
        private MediaRecorder _recorder;
        private MediaPlayer mPlayer;
        private const string DefaultPath = "/dev/null";
        private string _filePath;

        private Stopwatch _watch;
        private long _startTime;

        public void Start(string filePath = DefaultPath)
        {
            _filePath = filePath;
            try
            {
                _recorder = new MediaRecorder();
                _recorder.SetAudioSource(AudioSource.Mic);
                _recorder.SetOutputFormat(OutputFormat.ThreeGpp);
                _recorder.SetOutputFile(_filePath);
                _recorder.SetAudioEncoder(AudioEncoder.AmrNb);
               
                _recorder.Prepare();
                _recorder.Start();
                _watch = Stopwatch.StartNew();
                _startTime = _watch.ElapsedMilliseconds;
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.StackTrace);
            }
        }

        public void Stop()
        {
            if (_recorder == null) return;

            _recorder.Stop();
            _watch.Stop();
            _recorder.Release();
            _recorder = null;
        }

        //public void StartPlaying()
        //{
        //    mPlayer = new MediaPlayer();

        //    mPlayer.SetDataSource(_filePath);
        //    mPlayer.Prepare();
        //    mPlayer.Start();
        //}

        //public void StopPlaying()
        //{
        //    if (mPlayer == null) return;
            
        //    mPlayer.Release();
        //    mPlayer = null;
        //}

        public AmplitudeInTime GetAmplitudeInTime()
        {
            if (_recorder != null)
            {
                return new AmplitudeInTime(_recorder.MaxAmplitude, _watch.ElapsedMilliseconds - _startTime);
            }

            return new AmplitudeInTime();
        }
    }
}