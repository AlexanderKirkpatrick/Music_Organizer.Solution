namespace Music_Organizer.Models
{
  public class MediumArtist
    {       
        public int MediumArtistId { get; set; }
        public int ArtistId { get; set; }
        public int MediumId { get; set; }
        public virtual Artist Artist { get; set; }
        public virtual Medium Medium { get; set; }
    }
}