using System;

namespace ZM.Media.Info
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class MediaInfoFieldNameAttribute : Attribute
    {
        public readonly string FieldName;

        public MediaInfoFieldNameAttribute(string fieldName)
        {
            FieldName = fieldName;
        }
    }
}