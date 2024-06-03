using Bigon.Data;
using Bigon.Infrastructure.Entites;
using Microsoft.AspNetCore.Mvc;

namespace BigonWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManufacturerController : Controller
    {
        private DataContext _db;

        public ManufacturerController(DataContext db)
        {
            _db = db;

        }
        public IActionResult Index()
        {

            var manufacturers = _db.Manufacturers
                .Where(c => c.DeletedBy == null)
                .ToList();
            return View(manufacturers);
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Manufacturer manufacturer)
        {
            manufacturer.CreatedAt = DateTime.Now;
            manufacturer.CreatedBy = 1;
            _db.Manufacturers.Add(manufacturer);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var dbmanufacturer = _db.Manufacturers.Find(id);

            if (dbmanufacturer == null) return NotFound();

            return View(dbmanufacturer);
        }

        [HttpPost]
        public IActionResult Edit(Manufacturer manufacturer)
        {
            var dbmanufacturer = _db.Manufacturers.Find(manufacturer.Id);

            if (dbmanufacturer == null) return NotFound();

            dbmanufacturer.Name = manufacturer.Name;

            dbmanufacturer.ModifiedAt = DateTime.Now;
            dbmanufacturer.ModifiedBy = 2;

            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var dbmanufacturer = _db.Manufacturers.Find(id);

            if (dbmanufacturer == null) return NotFound();

            return View(dbmanufacturer);
        }

        public IActionResult Remove(int id)
        {
            var manufacturer = _db.Manufacturers.Find(id);

            if (manufacturer == null)
                return Json(new
                {
                    error = true,
                    message = "Data Tapilmadi"
                });

            manufacturer.DeletedAt = DateTime.Now;
            manufacturer.DeletedBy = 2;

            _db.SaveChanges();

            var manufacturers = _db.Manufacturers
                 .Where(c => c.DeletedBy == null)
                 .ToList();

            return PartialView("_Body", manufacturers);
        }
    }
}
