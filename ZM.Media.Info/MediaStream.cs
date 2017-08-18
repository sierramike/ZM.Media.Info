using System;
using System.Collections.Generic;
using System.Text;

namespace ZM.Media.Info
{
    /// <summary>
    /// Contains a media stream details.
    /// </summary>
    public abstract class MediaStream : Stream
    {
        /// <summary>
        /// Codec ID.
        /// </summary>
        public string CodecID { get; set; }
        /// <summary>
        /// Duration in milliseconds.
        /// </summary>
        public double Duration { get; set; }
        /// <summary>
        /// BitRate in bit/s.
        /// </summary>
        public string BitRate { get; set; }
        /// <summary>
        /// BitRate in bit/s.
        /// </summary>
        [MediaInfoFieldName("BitRate")]
        public long[] BitRateArr { get; set; }
        /// <summary>
        /// BitRate mode (CBR/VBR).
        /// </summary>
        public string BitRate_Mode { get; set; }
        /// <summary>
        /// BitRate mode (CBR/VBR).
        /// </summary>
        [MediaInfoFieldName("BitRate_Mode")]
        public string[] BitRate_ModeArr { get; set; }
        /// <summary>
        /// Size of the stream in bytes.
        /// </summary>
        public long StreamSize { get; set; }
        /// <summary>
        /// Frames per second.
        /// </summary>
        public double FrameRate { get; set; }
        /// <summary>
        /// Number of frames.
        /// </summary>
        public long FrameCount { get; set; }
        /// <summary>
        /// BitDepth.
        /// </summary>
        public int BitDepth { get; set; }
        /// <summary>
        /// Compression mode (Lossy or Lossless).
        /// </summary>
        public string Compression_Mode { get; set; }
        /// <summary>
        /// Compression mode (Lossy or Lossless).
        /// </summary>
        [MediaInfoFieldName("Compression_Mode")]
        public string[] Compression_ModeArr { get; set; }
        /// <summary>
        /// Delay fixed in the stream (relative) in ms.
        /// </summary>
        public long Delay { get; set; }


        /// <summary>
        /// Creates a new instance of MediaStream.
        /// </summary>
        public MediaStream()
        {

        }

        /// <summary>
        /// Duration
        /// </summary>
        [IgnoreProperty]
        public TimeSpan DurationSpan
        {
            get { return new TimeSpan(TimeSpan.TicksPerMillisecond * (long)Duration); }
        }
    }
}
