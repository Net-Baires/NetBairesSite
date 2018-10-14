using System.Collections.Generic;

namespace NetBaires.Models.Meetup
{
    public class PhotoDetailContainer {
        public List<PhotoDetail> Results { get; set; }
    }
    public class PhotoDetail
    {
        public int id { get; set; }
        public string highres_link { get; set; }
        public string photo_link { get; set; }
        public string thumb_link { get; set; }
        public string type { get; set; }
        public string base_url { get; set; }
        public string link { get; set; }
        public long created { get; set; }
        public long updated { get; set; }
        public int utc_offset { get; set; }
        public Member member { get; set; }
        public Photo_Album photo_album { get; set; }
    }
}