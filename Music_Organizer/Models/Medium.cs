using System.Collections.Generic;

namespace Music_Organizer.Models
{
    public class Medium
    {
        public Medium()
        {
            this.JoinEntities = new HashSet<MediumArtist>();
            this.JoinMediumAlbum = new HashSet<MediumAlbum>();
        }

        public int MediumId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<MediumArtist> JoinEntities { get; set; }
        public virtual ICollection<MediumAlbum> JoinMediumAlbum { get; set; }
    }
}