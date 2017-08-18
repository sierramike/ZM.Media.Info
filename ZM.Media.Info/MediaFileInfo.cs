using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZM.Media.Info
{
    /// <summary>
    /// Contains all details related to a media file.
    /// </summary>
    public class MediaFileInfo
    {
        /// <summary>
        /// General details of the media file.
        /// </summary>
        public GeneralStream GeneralInfo { get; private set; }
        /// <summary>
        /// Video streams details.
        /// </summary>
        public List<VideoStream> VideoStreams { get; private set; }
        /// <summary>
        /// Audio streams details.
        /// </summary>
        public List<AudioStream> AudioStreams { get; private set; }
        /// <summary>
        /// Text streams details.
        /// </summary>
        public List<TextStream> TextStreams { get; private set; }
        /// <summary>
        /// Menu streams details.
        /// </summary>
        public List<MenuStream> MenuStreams { get; private set; }

        /// <summary>
        /// Creates a new instance of MediaFileInfo.
        /// </summary>
        public MediaFileInfo()
        {
            GeneralInfo = new GeneralStream();
            VideoStreams = new List<VideoStream>();
            AudioStreams = new List<AudioStream>();
            TextStreams = new List<TextStream>();
            MenuStreams = new List<MenuStream>();
        }

        /// <summary>
        /// Display all properties in a text.
        /// </summary>
        /// <returns>Displayed properties.</returns>
        public virtual string Display(bool ShowEmptyProperties = false)
        {
            var s = string.Empty;

            s += string.Format("General information:\n{0}\n", GeneralInfo.Display(ShowEmptyProperties));

            int iVS = VideoStreams.Count;
            s += string.Format("Number of video streams: {0}\n\n", iVS);
            for (int i = 0; i < iVS; i++)
                s += string.Format("Video stream {0}:\n{1}\n\n", i, VideoStreams[i].Display(ShowEmptyProperties));

            int iAS = AudioStreams.Count;
            s += string.Format("Number of audio streams: {0}\n\n", iAS);
            for (int i = 0; i < iAS; i++)
                s += string.Format("Audio stream {0}:\n{1}\n\n", i, AudioStreams[i].Display(ShowEmptyProperties));

            int iTS = TextStreams.Count;
            s += string.Format("Number of text streams: {0}\n\n", iTS);
            for (int i = 0; i < iTS; i++)
                s += string.Format("Text stream {0}:\n{1}\n\n", i, TextStreams[i].Display(ShowEmptyProperties));

            int iMS = MenuStreams.Count;
            s += string.Format("Number of menu streams: {0}\n\n", iMS);
            for (int i = 0; i < iMS; i++)
                s += string.Format("Menu stream {0}:\n{1}\n\n", i, MenuStreams[i].Display(ShowEmptyProperties));

            return s;
        }
    }
}
