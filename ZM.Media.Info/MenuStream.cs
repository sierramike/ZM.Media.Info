using System;
using System.Collections.Generic;
using System.Text;

namespace ZM.Media.Info
{
    /// <summary>
    /// Contains an menu stream details.
    /// </summary>
    public class MenuStream : MediaStream
    {
        /// <summary>
        /// Position of first chapter in the chapter list.
        /// </summary>
        public int Chapters_Pos_Begin { get; set; }
        /// <summary>
        /// Position of last chapter in the chapter list.
        /// </summary>
        public int Chapters_Pos_End { get; set; }

        /// <summary>
        /// List of chapters.
        /// </summary>
        [IgnoreProperty]
        public List<string> Chapters { get; private set; }


        /// <summary>
        /// Creates a new instance of MenuStream.
        /// </summary>
        public MenuStream() : base()
        {
            Chapters = new List<string>();
        }

        public override string Display(bool ShowEmptyProperties = false)
        {
            return string.Format("{0} chapters:\n{1}", Chapters.Count, string.Join("\n", Chapters));
        }
    }
}
