using System;

namespace IDDDCommon
{
    public class AssertionConcern
    {
        protected AssertionConcern() {  }
        
        protected void AssertArgumentNotEmpty(string value, string message)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentException(message);
        }
        
        protected void AssertArgumentLength(string value, int maximum, string message)
        {
            var length = value.Trim().Length;
            if (length > maximum) throw new ArgumentOutOfRangeException(message);
        }

        protected void AssertArgumentLength(string value, int minimum, int maximum, string message)
        {
            var length = value.Trim().Length;
            if ( minimum > length || length > maximum) throw new ArgumentOutOfRangeException(message);
        }

        protected void AssertArgumentEquals(object object1, object object2, string message)
        {
            if (object1 != object2) throw new ArgumentException(message);
        }

        protected void AssertArrangeNotNull(object obj, string message)
        {
            if (obj == null) throw new ArgumentNullException();
        }
    }
}