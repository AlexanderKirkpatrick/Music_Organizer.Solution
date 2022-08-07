using Microsoft.EntityFrameworkCore;

namespace Music_Organizer.Models
{
  public class Music_OrganizerContext : DbContext
  {
    public DbSet<Album> Albums { get; set; }
    public DbSet<Artist> Artists { get; set; }
    public DbSet<Medium> Mediums { get; set; }
    public DbSet<AlbumArtist> AlbumArtist { get; set; }
    public DbSet<MediumArtist> MediumArtist { get; set; }
    public DbSet<MediumAlbum> MediumAlbum { get; set; }

    public Music_OrganizerContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    optionsBuilder.UseLazyLoadingProxies();
    }
  }
}