namespace UrlShortener.Services
{
    public class DataValidator
    {
        private readonly DateTime InternetBirthDate = new DateTime(1969,10,29);

        private const string ShortUrlSart = "https://localhost:7230/";

        public bool IsUrlValid(string url)
        {
            Uri validatedUri;
            return Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out validatedUri);
        }

        public bool IsShortUrlValid(string url)
        {
            return IsUrlValid(url) && url.StartsWith(ShortUrlSart);
        }

        public bool IsDateValid(DateTime dateTime) => 
            (dateTime < DateTime.Now) && (dateTime > InternetBirthDate);

        public bool IsTransitionsNumberValid(int transitionsNumber) => transitionsNumber >= 0;
    }
}
