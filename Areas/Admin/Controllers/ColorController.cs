using Bigon.Data;
using Bigon.Infrastructure.Entites;
using Microsoft.AspNetCore.Mvc;

namespace BigonWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ColorController : Controller
    {
        private DataContext _db;

        public ColorController(DataContext db)
        {
            _db = db;

        }
        public IActionResult Index()
        {
            var colors=_db.Colors
                .Where(c => c.DeletedBy==null)
                .ToList();
            return View(colors);
        }

        public IActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Create(Color color)
        {
           
            _db.Colors.Add(color);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var dbColor=_db.Colors.Find(id);

            if(dbColor == null) return NotFound();

            return View(dbColor);
        }

        [HttpPost]
        public IActionResult Edit(Color color)
        {
            var dbColor=_db.Colors.Find(color.Id);

            if(dbColor == null) return NotFound();

            dbColor.Name= color.Name;
            dbColor.HexCode= color.HexCode;
           

            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var dbColor = _db.Colors.Find(id);

            if (dbColor == null) return NotFound();

            return View(dbColor);
        }   
        
        public IActionResult Remove(int id)
        {
            var dbColor = _db.Colors.Find(id);

            if (dbColor == null)
                return Json(new
                {
                    error=true,
                    message = "Data Tapilmadi"
                });

            _db.Colors.Remove(dbColor);

         

            _db.SaveChanges();

            var colors=_db.Colors
                .Where(c => c.DeletedBy==null)
                .ToList();

            return PartialView("_Body", colors);
        }


    }
}
