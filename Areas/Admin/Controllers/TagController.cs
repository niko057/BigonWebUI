using Bigon.Data;
using Bigon.Infrastructure.Entites;
using Microsoft.AspNetCore.Mvc;

namespace BigonWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TagController : Controller
    {
        private DataContext _db;

        public TagController(DataContext db)
        {
            _db = db;

        }
        public IActionResult Index()
        {

            var tags = _db.Tags
                .Where(m => m.DeletedBy == null)
                .ToList();
            return View(tags);
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Tag tag)
        {
            tag.CreatedAt = DateTime.Now;
            tag.CreatedBy = 1;
            _db.Tags.Add(tag);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var dbTag = _db.Tags.Find(id);

            if (dbTag == null) return NotFound();

            return View(dbTag);
        }

        [HttpPost]
        public IActionResult Edit(Tag tag)
        {
            var dbTag = _db.Tags.Find(tag.Id);

            if (dbTag == null) return NotFound();

            dbTag.Name = tag.Name;
          
            dbTag.ModifiedAt = DateTime.Now;
            dbTag.ModifiedBy = 2;

            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var dbTag = _db.Tags.Find(id);

            if (dbTag == null) return NotFound();

            return View(dbTag);
        }

        public IActionResult Remove(int id)
        {
            var dbTag = _db.Tags.Find(id);

            if (dbTag == null)
            {
                return Json(new
                {
                    error = true,
                    message = "Data Tapilmadi"
                });
            }

            dbTag.DeletedAt = DateTime.Now;
            dbTag.DeletedBy = 2;

            _db.SaveChanges();

            var tags = _db.Tags
                .Where(c => c.DeletedBy == null)
                .ToList();

            return PartialView("_Body", tags);
        }
    }
}
