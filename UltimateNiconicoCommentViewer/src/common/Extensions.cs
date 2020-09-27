namespace UltimateNiconicoCommentViewer.src.common
{
    public static class Extensions
    {
        public static bool NotEmpty(this string str) => !str.Equals(string.Empty);

        public static bool NotNull(this object obj) => obj != null;

        public static bool IsEmpty(this string str) => str == string.Empty;

        public static bool IsNotNumber(this string str)
        {
            var id = 0;
            return !int.TryParse(str, out id);
        }

        public static bool IsNull(this string str) => str == null;




    }
}
