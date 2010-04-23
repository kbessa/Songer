﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SlimDX.Multimedia;
using Songer.SoundAnalysis;

namespace Songer.SoundInput
{
    public abstract class SoundSource
    {
        public abstract void Start();
        public abstract void Stop();
        protected abstract void CaptureLoop();

        private WaveFormat format = SoundSource.GenerateWaveFormat();

        public event EventHandler<SoundDetectedEventArgs> SoundDetected;

        protected WaveFormat Format
        {
            get { return format; }
            set { format = value; }
        }
        
        protected void OnSoundDetected(short[] soundData)
        {
            if (this.SoundDetected != null)
            {
                this.SoundDetected(this, new SoundDetectedEventArgs(soundData));
            }
        }
        
        private static WaveFormat GenerateWaveFormat()
        {
            WaveFormat format = new WaveFormat();

            format.FormatTag = WaveFormatTag.Pcm;
            format.Channels = 1;
            format.BitsPerSample = 16;
            format.SamplesPerSecond = 44100;
            format.BlockAlignment = (short)(format.Channels * (format.BitsPerSample / 8));
            format.AverageBytesPerSecond = format.BlockAlignment * format.SamplesPerSecond;

            return format;
        }
    }
}
