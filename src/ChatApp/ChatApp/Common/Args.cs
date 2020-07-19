using System;

namespace ChatApp.Common
{
    public static class Args
    {
        public static void NotNull<T>(T value, string name) where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        public static void NotNull<T>(T? value, string name) where T : struct
        {
            if (value == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        public static void NotEmpty(string value, string name)
        {
            NotNull(value, name);
            if (value.Length == 0)
            {
                throw new ArgumentException("一文字以上の文字列である必要があります。", name);
            }
        }
    }
}