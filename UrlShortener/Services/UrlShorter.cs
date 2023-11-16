namespace UrlShortener.Services
{
    public class UrlShorter
    {
        private const string AllowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private const int ShortUrlLength = 6;
        
        public string GetSortUrl()
        {
            var shortUrn = new char[ShortUrlLength];
            var currentDate = DateTime.Now;
            var seed = currentDate.Day + currentDate.Hour + currentDate.Minute + currentDate.Second;
            var rand = new Random(seed);

            for(var i = 0; i< ShortUrlLength; ++i)
            {
                shortUrn[i] = AllowedChars[rand.Next(AllowedChars.Length)];
            }

            return "https://localhost:7230/" +  new string (shortUrn);
        }
    }
}
