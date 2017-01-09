using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SoundPeriodMeasure.Helpers
{
    public class Recorder
    {
        private MediaRecorder _recorder;
        private MediaPlayer mPlayer;
        private string _filePath = "/dev/null";
        // This file is used to record voice
        private const double EmaFilter = 0.6;

        public void Start()
        {           
            try
            {
                _recorder = new MediaRecorder();
                _recorder.SetAudioSource(AudioSource.Mic);
                _recorder.SetOutputFormat(OutputFormat.ThreeGpp);
                _recorder.SetOutputFile(_filePath);
                _recorder.SetAudioEncoder(AudioEncoder.AmrNb);


                _recorder.Prepare();
                _recorder.Start();
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
            _recorder.Release();
            _recorder = null;
        }

        public void StartPlaying()
        {
            mPlayer = new MediaPlayer();

            mPlayer.SetDataSource(_filePath);
            mPlayer.Prepare();
            mPlayer.Start();
        }

        public void StopPlaying()
        {
            if (mPlayer == null) return;
            
            mPlayer.Release();
            mPlayer = null;
        }

        public double GetAmplitude()
        {
            if (_recorder != null)
            {
                return (_recorder.MaxAmplitude);
            }

            return 0;
        }
    }
}