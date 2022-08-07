using System.Collections.Generic;

namespace Music_Organizer.Models
{
    public class Album
    {
        public Album()
        {
            this.JoinEntities = new HashSet<AlbumArtist>();
            AlbumOwned = false;
        }

        public int AlbumId { get; set; }
        public string Name { get; set; }
        public bool AlbumOwned { get; set; }
        public virtual ICollection<AlbumArtist> JoinEntities { get; set; }
    }
}