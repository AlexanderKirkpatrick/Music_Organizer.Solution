namespace Music_Organizer.Models
{
  public class AlbumArtist
    {       
        public int AlbumArtistId { get; set; }
        public int ArtistId { get; set; }
        public int AlbumId { get; set; }
        public virtual Artist Artist { get; set; }
        public virtual Album Album { get; set; }
    }
}