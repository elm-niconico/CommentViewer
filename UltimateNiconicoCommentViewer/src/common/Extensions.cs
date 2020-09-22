using System;
using System.Collections.Generic;
using System.Text;

namespace UltimateNiconicoCommentViewer.src.common
{
    static class Extensions
    {
        public static bool NotEmpty(this string str) => !str.Equals(string.Empty);

        public static bool NotNull(this object obj) => obj != null;

        public static bool IsEmpty(this string str) => str == string.Empty; 
    }
}
