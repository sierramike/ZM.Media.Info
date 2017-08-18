using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZM.Media.Info
{
    /// <summary>
    /// Contains a stream details.
    /// </summary>
    public abstract class Stream
    {
        /// <summary>
        /// ID of the stream.
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// The unique ID for this stream, should be copied with stream copy.
        /// </summary>
        public string UniqueID { get; set; }
        /// <summary>
        /// (Generic)Title of file.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Language (2-letter ISO 639-1 if exists, else 3-letter ISO 639-2, and with optional ISO 3166-1 country separated by a dash if available, e.g. en, en-us, zh-cn).
        /// </summary>
        public string Language { get; set; }
        /// <summary>
        /// Service kind, e.g. visually impaired, commentary, voice over.
        /// </summary>
        public string ServiceKind { get; set; }
        /// <summary>
        /// Set if that track should not be used.
        /// </summary>
        public string Disabled { get; set; }
        /// <summary>
        /// Set if that track should be used if no language found matches the user preference.
        /// </summary>
        public string Default { get; set; }
        /// <summary>
        /// Set if that track should be used if no language found matches the user preference.
        /// </summary>
        public string Forced { get; set; }

        /// <summary>
        /// File format.
        /// </summary>
        public string Format { get; set; }
        /// <summary>
        /// Information about the format.
        /// </summary>
        [MediaInfoFieldName("Format/Info")]
        public string FormatInfo { get; set; }
        /// <summary>
        /// Link to a description of this format.
        /// </summary>
        [MediaInfoFieldName("Format/Url")]
        public string FormatUrl { get; set; }
        /// <summary>
        /// Commercial name used by vendor for theses setings or Format field if there is no difference.
        /// </summary>
        public string Format_Commercial { get; set; }
        /// <summary>
        /// Version of this format.
        /// </summary>
        public string Format_Version { get; set; }
        /// <summary>
        /// Profile of the Format (old XML: 'Profile@Level' format).
        /// </summary>
        public string Format_Profile { get; set; }
        /// <summary>
        /// Profile of the Format (old XML: 'Profile@Level' format).
        /// </summary>
        [MediaInfoFieldName("Format_Profile")]
        public string[] Format_ProfileArr { get; set; }
        /// <summary>
        /// Level of the Format (only MIXML).
        /// </summary>
        public string Format_Level { get; set; }
        /// <summary>
        /// Compression method used.
        /// </summary>
        public string Format_Compression { get; set; }
        /// <summary>
        /// Settings needed for decoder used.
        /// </summary>
        public string Format_Settings { get; set; }

        /// <summary>
        /// Name of the software package used to create the file, such as Microsoft WaveEdit.
        /// </summary>
        public string Encoded_Application { get; set; }
        /// <summary>
        /// Name of the product.
        /// </summary>
        public string Encoded_Application_Name { get; set; }
        /// <summary>
        /// Version of the product.
        /// </summary>
        public string Encoded_Application_Version { get; set; }
        /// <summary>
        /// Software used to create the file.
        /// </summary>
        public string Encoded_Library { get; set; }
        /// <summary>
        /// Parameters used by the software.
        /// </summary>
        public string Encoded_Library_Settings { get; set; }
        /// <summary>
        /// Parameters used by the software.
        /// </summary>
        [MediaInfoFieldName("Encoded_Library_Settings")]
        public string[] Encoded_Library_SettingsArr { get; set; }
        /// <summary>
        /// Operating System of encoding-software.
        /// </summary>
        public string Encoded_OperatingSystem { get; set; }

        /// <summary>
        /// The menu ID for this stream in this file.
        /// </summary>
        public int MenuID { get; set; }


        /// <summary>
        /// Creates a new instance of Stream.
        /// </summary>
        public Stream()
        {

        }

        /// <summary>
        /// Display all properties in a text.
        /// </summary>
        /// <returns>Displayed properties.</returns>
        public virtual string Display(bool ShowEmptyProperties = false)
        {
            return RecurseDisplay(GetType(), ShowEmptyProperties);
        }

        /// <summary>
        /// Recurse through inheritence to display properties.
        /// </summary>
        /// <param name="T">Current type to display.</param>
        /// <returns>Displayed properties.</returns>
        protected virtual string RecurseDisplay(Type T, bool ShowEmptyProperties)
        {
            string s = Display(T, ShowEmptyProperties);
            if (T.BaseType != null)
            {
                s = string.Format("{0}{1}", RecurseDisplay(T.BaseType, ShowEmptyProperties), s);
            }
            return s;
        }

        /// <summary>
        /// Display type specific properties.
        /// </summary>
        /// <param name="T">Type of which properties should be displayed.</param>
        /// <returns>Displayed properties.</returns>
        protected virtual string Display(Type T, bool ShowEmptyProperties)
        {
            var s = string.Empty;

            var props = T.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly).OrderBy(x => x.Name).ToList();
            foreach (var prop in props)
            {
                if (prop.PropertyType.IsArray)
                {
                    string[] values = null;
                    var o = prop.GetValue(this);
                    if (prop.PropertyType == typeof(string[]))
                        values = (string[])o;
                    else if (prop.PropertyType == typeof(int[]))
                        values = ((int[])o).Select(x => x.ToString()).ToArray();
                    else if (prop.PropertyType == typeof(long[]))
                        values = ((long[])o).Select(x => x.ToString()).ToArray();
                    else if (prop.PropertyType == typeof(double[]))
                        values = ((double[])o).Select(x => x.ToString()).ToArray();

                    if (values != null)
                        s += string.Format("{0}: [{1}]\n", prop.Name, string.Join("],[", values));
                    else if (ShowEmptyProperties)
                        s += string.Format("{0}: []\n", prop.Name);
                }
                else
                {
                    if (!string.IsNullOrEmpty(prop.GetValue(this, null).ToString()))
                        s += string.Format("{0}: {1}\n", prop.Name, prop.GetValue(this, null).ToString());
                    else if (ShowEmptyProperties)
                        s += string.Format("{0}:\n", prop.Name);
                }
            }

            return s;
        }
    }
}
