using System;

namespace IDDDCommon
{
    public static class StringExtension
    {
        public static int TryParseDefault(this string @this, int defaultValue)
        {
            var result = defaultValue;

            try
            {
                result = int.Parse(@this);
            }
            catch (Exception)
            {
                // パースエラーが発生しても握り潰す。
            }

            return result;
        }
    }
}