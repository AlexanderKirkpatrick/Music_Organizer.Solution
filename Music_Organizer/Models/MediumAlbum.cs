namespace Music_Organizer.Models
{
  public class MediumAlbum
    {       
        public int MediumAlbumId { get; set; }
        public int AlbumId { get; set; }
        public int MediumId { get; set; }
        public virtual Album Album { get; set; }
        public virtual Medium Medium { get; set; }
    }
}