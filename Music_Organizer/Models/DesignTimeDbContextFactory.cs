using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Music_Organizer.Models
{
  public class Music_OrganizerContextFactory : IDesignTimeDbContextFactory<Music_OrganizerContext>
  {

    Music_OrganizerContext IDesignTimeDbContextFactory<Music_OrganizerContext>.CreateDbContext(string[] args)
    {
      IConfigurationRoot configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json")
          .Build();

      var builder = new DbContextOptionsBuilder<Music_OrganizerContext>();

      builder.UseMySql(configuration["ConnectionStrings:DefaultConnection"], ServerVersion.AutoDetect(configuration["ConnectionStrings:DefaultConnection"]));

      return new Music_OrganizerContext(builder.Options);
    }
  }
}