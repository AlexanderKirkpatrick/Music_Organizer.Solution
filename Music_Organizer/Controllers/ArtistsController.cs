using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Music_Organizer.Models;
using System.Collections.Generic;
using System.Linq;

namespace Music_Organizer.Controllers
{
  public class ArtistsController : Controller
  {
    private readonly Music_OrganizerContext _db;

    public ArtistsController(Music_OrganizerContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Artist> model = _db.Artists.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      ViewBag.AlbumId = new SelectList(_db.Albums, "AlbumId", "Name");
      ViewBag.MediumId = new SelectList(_db.Mediums, "MediumId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Artist artist, int AlbumId, int MediumId)
    {
      _db.Artists.Add(artist);
      _db.SaveChanges();
      if (AlbumId != 0)
      {
        _db.AlbumArtist.Add(new AlbumArtist() { AlbumId = AlbumId, ArtistId = artist.ArtistId });
        _db.SaveChanges();
      }
      if (MediumId != 0)
      {
        _db.MediumArtist.Add(new MediumArtist() { MediumId = MediumId, ArtistId = artist.ArtistId });
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisArtist = _db.Artists
        .Include(Artist => Artist.JoinEntities)
        .ThenInclude(join => join.Album)
        .FirstOrDefault(Artist => Artist.ArtistId == id);
      return View(thisArtist);
    }

    public ActionResult Edit(int id)
    {
      Artist thisArtist = _db.Artists.FirstOrDefault(artist => artist.ArtistId == id);
      ViewBag.AlbumId = new SelectList(_db.Albums, "AlbumId", "Name");
      ViewBag.MediumId = new SelectList(_db.Mediums, "MediumId", "Name");
      return View(thisArtist);
    }

    [HttpPost]
    public ActionResult Edit(Artist artist, int AlbumId)
    {
      if (AlbumId != 0)
      {
        _db.AlbumArtist.Add(new AlbumArtist() { AlbumId = AlbumId, ArtistId = artist.ArtistId });
      }
      _db.Entry(artist).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisArtist = _db.Artists.FirstOrDefault(artist => artist.ArtistId == id);
      return View(thisArtist);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisArtist = _db.Artists.FirstOrDefault(artist => artist.ArtistId == id);
      _db.Artists.Remove(thisArtist);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddAlbum(int id)
    {
      var thisArtist = _db.Artists.FirstOrDefault(artist => artist.ArtistId == id);
      ViewBag.AlbumId = new SelectList(_db.Albums, "AlbumId", "Name");
      return View(thisArtist);
    }

    [HttpPost]
    public ActionResult AddAlbum(Artist artist, int AlbumId)
    {
      if (AlbumId != 0)
      {
        _db.AlbumArtist.Add(new AlbumArtist() { AlbumId = AlbumId, ArtistId = artist.ArtistId });
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteAlbum(int joinId)
    {
        var joinEntry = _db.AlbumArtist.FirstOrDefault(entry => entry.AlbumArtistId == joinId);
        _db.AlbumArtist.Remove(joinEntry);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
  }
}