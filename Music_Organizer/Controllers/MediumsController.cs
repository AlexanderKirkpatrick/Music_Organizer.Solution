using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Music_Organizer.Models;
using System.Collections.Generic;
using System.Linq;

namespace Music_Organizer.Controllers
{
  public class MediumsController : Controller
  {
    private readonly Music_OrganizerContext _db;

    public MediumsController(Music_OrganizerContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Medium> model = _db.Mediums.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Medium medium)
    {
      _db.Mediums.Add(medium);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisMedium = _db.Mediums
        .Include(Medium => Medium.JoinEntities)
        .ThenInclude(join => join.Artist)
        .Include(Medium => Medium.JoinMediumAlbum)
        .ThenInclude(join => join.Album)
        .FirstOrDefault(Medium => Medium.MediumId == id);
      return View(thisMedium);
    }
    
    public ActionResult Edit(int id)
    {
      var thisMedium = _db.Mediums.FirstOrDefault(medium => medium.MediumId == id);
      return View(thisMedium);
    }

    [HttpPost]
    public ActionResult Edit(Medium medium)
    {
      _db.Entry(medium).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisMedium = _db.Mediums.FirstOrDefault(medium => medium.MediumId == id);
      return View(thisMedium);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisMedium = _db.Mediums.FirstOrDefault(medium => medium.MediumId == id);
      _db.Mediums.Remove(thisMedium);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddAlbum(int id)
    {
      var thisMedium = _db.Mediums.FirstOrDefault(medium => medium.MediumId == id);
      ViewBag.AlbumId = new SelectList(_db.Albums, "AlbumId", "Name");
      return View(thisMedium);
    }

    [HttpPost]
    public ActionResult AddAlbum(Medium medium, int AlbumId)
    {
      if (AlbumId != 0)
      {
        _db.MediumAlbum.Add(new MediumAlbum() { AlbumId = AlbumId, MediumId = medium.MediumId });
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteAlbum(int joinId)
    {
        var joinEntry = _db.MediumAlbum.FirstOrDefault(entry => entry.MediumAlbumId == joinId);
        _db.MediumAlbum.Remove(joinEntry);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
  }
}