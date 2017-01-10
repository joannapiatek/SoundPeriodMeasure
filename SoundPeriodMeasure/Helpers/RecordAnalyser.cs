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
using Android.Media;

namespace SoundPeriodMeasure.Helpers
{
    public class RecordAnalyser
    {
        public void ProcessToChartData(string path)
        {
            //// Instantiate MediaCodec
            //String mMime = "audio/3gpp";
            //_codec = MediaCodec.CreateDecoderByType(mMime);

            //// Create Media format and configure media codec
            //MediaFormat mMediaFormat = new MediaFormat();
            //mMediaFormat = MediaFormat.CreateAudioFormat(mMime,
            //mMediaFormat.GetInteger(MediaFormat.KeySampleRate),
            //mMediaFormat.GetInteger(MediaFormat.KeyChannelCount));

            //_codec.Configure(mMediaFormat, null, null, 0);
            //_codec.Start();

            //// Capture output from MediaCodec (Should process inside a thread)
            //MediaCodec.BufferInfo buf_info = new MediaCodec.BufferInfo();
            //int outputBufferIndex = _codec.DequeueOutputBuffer(buf_info, 0);
            //byte[] pcm = new byte[buf_info.Size];
            //mOutputBuffers[outputBufferIndex].get(pcm, 0, buf_info.Size);
        }
    }
}