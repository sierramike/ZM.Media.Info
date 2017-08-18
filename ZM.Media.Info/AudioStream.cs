using System;
using System.Collections.Generic;
using System.Text;

namespace ZM.Media.Info
{
    /// <summary>
    /// Contains an audio stream details.
    /// </summary>
    public class AudioStream : MediaStream
    {
        /// <summary>
        /// Number of channels
        /// </summary>
        [MediaInfoFieldName("Channel(s)")]
        public string Channels { get; set; }
        /// <summary>
        /// Number of channels
        /// </summary>
        [MediaInfoFieldName("Channel(s)")]
        public int[] ChannelsArr { get; set; }
        /// <summary>
        /// Position of channels.
        /// </summary>
        public string ChannelPositions { get; set; }
        /// <summary>
        /// Position of channels.
        /// </summary>
        [MediaInfoFieldName("ChannelPositions")]
        public string[] ChannelPositionsArr { get; set; }
        /// <summary>
        /// Sampling rate.
        /// </summary>
        public double SamplingRate { get; set; }

        /// <summary>
        /// Creates a new instance of AudioStream.
        /// </summary>
        public AudioStream() : base()
        {

        }
    }
}
