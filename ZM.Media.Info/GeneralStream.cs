using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZM.Media.Info
{
    /// <summary>
    /// Contains general details.
    /// </summary>
    public class GeneralStream : Stream
    {
        /// <summary>
        /// Internet Media Type (aka MIME Type, Content-Type)
        /// </summary>
        public string InternetMediaType { get; set; }
        /// <summary>
        /// Bit rate of all streams in bps.
        /// </summary>
        public long OverallBitRate { get; set; }
        /// <summary>
        /// File size in bytes.
        /// </summary>
        public long FileSize { get; set; }
        /// <summary>
        /// Owner of the file.
        /// </summary>
        public string Owner { get; set; }


        /// <summary>
        /// Creates a new instance of GeneralInfo.
        /// </summary>
        public GeneralStream() : base()
        {
        }
    }
}
