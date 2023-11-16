using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Models
{
    public class UrlModel
    {
        [Key]
        public int Id { get; set; }
        public string LongUrl { get; set; }
        public string ShortUrl {  get; set; }
        public DateTime CreationDate { get; set; }
        public int TransitionsNumber { get; set; }

        public void CopyFrom(UrlModel fromModel)
        {
            LongUrl = new string (fromModel.LongUrl);
            ShortUrl = new string (fromModel.ShortUrl);
            CreationDate = fromModel.CreationDate;
            TransitionsNumber = fromModel.TransitionsNumber;
        }
    }
}
