namespace ObjectExporter.VsPackage.ExtensionMethods
{
    public static class StringExtensions
    {
        public static long Lines(this string s)
        {
            long count = 1;
            int position = 0;

            while ((position = s.IndexOf('\n', position)) != -1)
            {
                count++;
                position++; // Skip this occurrence!
            }

            return count;
        }
    }
}
