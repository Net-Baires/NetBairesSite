using NetBaires.Models.Meetup;

namespace NetBaires.Models
{
    public class PhotoViewModel
    {
        public int Id { get; set; }
        public string HighresLink { get; set; }
        public string PhotoLink { get; set; }
        public string ThumbLink { get; set; }
        public PhotoViewModel(PhotoDetail photo)
        {
            Id = photo.id;
            HighresLink = photo.highres_link;
            PhotoLink = photo.photo_link;
            ThumbLink = photo.thumb_link;
        }
      
    }
}