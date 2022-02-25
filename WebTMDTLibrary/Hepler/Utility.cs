namespace WebTMDTLibrary.Helper
{
    public static class Utility
    {
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string ConstructAddressString(string addno,string street,string district,string ward,string city)
        {
            return $"{addno} {street}, {district}, {ward}, {city}";
        }
    }
}
