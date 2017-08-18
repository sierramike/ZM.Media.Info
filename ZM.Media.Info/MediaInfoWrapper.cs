using MediaInfoLib;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ZM.Media.Info
{
    /// <summary>
    /// Wraps Media Info library queries into a friendly .NET library.
    /// </summary>
    public class MediaInfoWrapper
    {
        /// <summary>
        /// Creates a new instance of MediaInfoWrapper.
        /// </summary>
        public MediaInfoWrapper()
        {
        }

        /// <summary>
        /// Gets generic library Capacities description.
        /// </summary>
        /// <returns>Generic library Capacities description.</returns>
        public static string Info_Capacities()
        {
            MediaInfo MI = new MediaInfo();
            var s = MI.Option("Info_Version", Constants.INFO_VERSION_VALUE);
            if (s.Length == 0)
                throw new Exception(string.Format("MediaInfo.Dll: this version of the DLL is not compatible. Version found: {0}", s));

            return MI.Option("Info_Capacities");
        }

        /// <summary>
        /// Gets generic library Codecs description.
        /// </summary>
        /// <returns>Generic library Codecs description.</returns>
        public static string Info_Codecs()
        {
            MediaInfo MI = new MediaInfo();
            var s = MI.Option("Info_Version", Constants.INFO_VERSION_VALUE);
            if (s.Length == 0)
                throw new Exception(string.Format("MediaInfo.Dll: this version of the DLL is not compatible. Version found: {0}", s));

            return MI.Option("Info_Codecs");
        }

        /// <summary>
        /// Gets generic library Parameters description.
        /// </summary>
        /// <returns>Generic library Parameters description.</returns>
        public static string Info_Parameters()
        {
            MediaInfo MI = new MediaInfo();
            var s = MI.Option("Info_Version", Constants.INFO_VERSION_VALUE);
            if (s.Length == 0)
                throw new Exception(string.Format("MediaInfo.Dll: this version of the DLL is not compatible. Version found: {0}", s));

            return MI.Option("Info_Parameters");
        }

        /// <summary>
        /// Reads a media file details.
        /// </summary>
        /// <param name="fileName">File name to be read.</param>
        /// <returns>Media file details.</returns>
        public virtual MediaFileInfo Read(string fileName)
        {
            var FileInfo = new MediaFileInfo();
            var MI = new MediaInfoLib.MediaInfo();

            var s = MI.Option("Info_Version", Constants.INFO_VERSION_VALUE);
            if (s.Length == 0)
                throw new Exception(string.Format("MediaInfo.Dll: this version of the DLL is not compatible. Version found: {0}", s));

            MI.Open(fileName);

            ReadProperties(MI, FileInfo.GeneralInfo, StreamKind.General, 0);

            int iNbVideoStreams = int.Parse(MI.Get(StreamKind.Video, 0, "StreamCount"));
            int iNbAudioStreams = int.Parse(MI.Get(StreamKind.Audio, 0, "StreamCount"));
            int iNbTextStreams = int.Parse(MI.Get(StreamKind.Text, 0, "StreamCount"));
            int iNbMenuStreams = int.Parse(MI.Get(StreamKind.Menu, 0, "StreamCount"));

            for (int i = 0; i < iNbVideoStreams; i++)
            {
                var vs = new VideoStream();
                ReadProperties(MI, vs, StreamKind.Video, i);
                FileInfo.VideoStreams.Add(vs);
            }

            for (int i = 0; i < iNbAudioStreams; i++)
            {
                var aus = new AudioStream();
                ReadProperties(MI, aus, StreamKind.Audio, i);
                FileInfo.AudioStreams.Add(aus);
            }

            for (int i = 0; i < iNbTextStreams; i++)
            {
                var ts = new TextStream();
                ReadProperties(MI, ts, StreamKind.Text, i);
                FileInfo.TextStreams.Add(ts);
            }

            for (int i = 0; i < iNbMenuStreams; i++)
            {
                var ms = new MenuStream();
                ReadProperties(MI, ms, StreamKind.Menu, i);
                for (int j = ms.Chapters_Pos_Begin; j < ms.Chapters_Pos_End; j++)
                {
                    ms.Chapters.Add(MI.Get(StreamKind.Menu, i, j));
                }
                FileInfo.MenuStreams.Add(ms);
            }

            return FileInfo;
        }

        /// <summary>
        /// Populates all properties by querying the media info library.
        /// </summary>
        /// <param name="MI">MediaInfo library instance.</param>
        /// <param name="o">Object to be populated.</param>
        /// <param name="streamKind">Stream kind to query.</param>
        /// <param name="streamNumber">Stream number to query.</param>
        protected void ReadProperties(MediaInfo MI, object o, StreamKind streamKind, int streamNumber)
        {
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";

            var T = o.GetType();
            var props = T.GetProperties().OrderBy(x => x.Name).ToArray();
            foreach (var prop in props)
            {
                var propName = GetMediaInfoFieldName(prop);
                if (!Attribute.IsDefined(prop, typeof(IgnorePropertyAttribute)))
                {
                    var s = MI.Get(streamKind, streamNumber, propName).Trim();
                    var sArr = s.Split('/').Select(x => x.Trim()).ToArray();

                    if (prop.PropertyType == typeof(int))
                        prop.SetValue(o, (s == string.Empty ? 0 : int.Parse(s)));
                    else if (prop.PropertyType == typeof(long))
                        prop.SetValue(o, (s == string.Empty ? 0 : long.Parse(s)));
                    else if (prop.PropertyType == typeof(double))
                        prop.SetValue(o, (s == string.Empty ? 0 : double.Parse(s, nfi)));
                    else if (prop.PropertyType == typeof(string))
                        prop.SetValue(o, s);

                    else if (prop.PropertyType == typeof(string[]) && s.Length > 0)
                        prop.SetValue(o, sArr);
                    else if (prop.PropertyType == typeof(int[]) && s.Length > 0)
                        prop.SetValue(o, sArr.Select(x => int.Parse(x)).ToArray());
                    else if (prop.PropertyType == typeof(long[]) && s.Length > 0)
                        prop.SetValue(o, sArr.Select(x => long.Parse(x)).ToArray());
                    else if (prop.PropertyType == typeof(double[]) && s.Length > 0)
                        prop.SetValue(o, sArr.Select(x => double.Parse(x)).ToArray());
                }
            }
        }

        /// <summary>
        /// Get the media info library property real name by reading attributes.
        /// </summary>
        /// <param name="prop">Property to be checked.</param>
        /// <returns>Property real name.</returns>
        protected virtual string GetMediaInfoFieldName(PropertyInfo prop)
        {
            var v = prop.GetCustomAttributes<MediaInfoFieldNameAttribute>(true);
            if (v.Count() > 0)
                return v.First().FieldName;
            else
                return prop.Name;
        }
    }
}
