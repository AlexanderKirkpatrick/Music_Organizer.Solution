using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Music_Organizer.Models;
using System.Collections.Generic;
using System.Linq;

namespace Music_Organizer.Controllers
{
  public class AlbumsController : Controller
  {
    private readonly Music_OrganizerContext _db;

    public AlbumsController(Music_OrganizerContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Album> model = _db.Albums.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      ViewBag.MediumId = new SelectList(_db.Mediums, "MediumId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Album album, int MediumId)
    {
      _db.Albums.Add(album);
      _db.SaveChanges();
      if (MediumId != 0)
      {
        _db.MediumAlbum.Add(new MediumAlbum() { MediumId = MediumId, AlbumId = album.AlbumId});
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisAlbum = _db.Albums
        .Include(Album => Album.JoinEntities)
        .ThenInclude(join => join.Artist)
        .FirstOrDefault(Album => Album.AlbumId == id);
      return View(thisAlbum);
    }
    
    public ActionResult Edit(int id)
    {
      var thisAlbum = _db.Albums.FirstOrDefault(album => album.AlbumId == id);
      ViewBag.MediumId = new SelectList(_db.Mediums, "MediumId", "Name");
      return View(thisAlbum);
    }

    [HttpPost]
    public ActionResult Edit(Album album, int MediumId)
    {
      if (MediumId != 0)
      {
        _db.MediumAlbum.Add(new MediumAlbum() { MediumId = MediumId, AlbumId = album.AlbumId});
        _db.SaveChanges();
      }
      _db.Entry(album).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisAlbum = _db.Albums.FirstOrDefault(album => album.AlbumId == id);
      return View(thisAlbum);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisAlbum = _db.Albums.FirstOrDefault(album => album.AlbumId == id);
      _db.Albums.Remove(thisAlbum);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}