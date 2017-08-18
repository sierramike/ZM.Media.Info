using System;
using System.Collections.Generic;
using System.Text;

namespace ZM.Media.Info
{
    /// <summary>
    /// Contains a video stream details.
    /// </summary>
    public class VideoStream : MediaStream
    {
        /// <summary>
        /// Width in pixels.
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Height in pixels.
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// Pixel Aspect ratio.
        /// </summary>
        public double PixelAspectRatio { get; set; }
        /// <summary>
        /// Display Aspect ratio.
        /// </summary>
        public double DisplayAspectRatio { get; set; }
        /// <summary>
        /// Active Format Description (AFD value).
        /// </summary>
        public string ActiveFormatDescription { get; set; }
        /// <summary>
        /// Frame rate mode (CFR, VFR).
        /// </summary>
        public string FrameRate_Mode { get; set; }
        /// <summary>
        /// NTSC or PAL.
        /// </summary>
        public string Standard { get; set; }
        /// <summary>
        /// ColorSpace.
        /// </summary>
        public string ColorSpace { get; set; }
        /// <summary>
        /// ChromaSubsampling.
        /// </summary>
        public string ChromaSubsampling { get; set; }
        /// <summary>
        /// ScanType.
        /// </summary>
        public string ScanType { get; set; }
        /// <summary>
        /// ScanOrder.
        /// </summary>
        public string ScanOrder { get; set; }






        /// <summary>
        /// Creates a new instance of VideoStream.
        /// </summary>
        public VideoStream() : base()
        {

        }
    }
}
