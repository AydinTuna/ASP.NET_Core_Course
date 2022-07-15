using AbbyWeb.Data;
using AbbyWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Categories
{   // [BindProperties] class dýþýna yazýlýr.
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        //[BindProperty]
        // [BindProperties] diyerek birden fazla prop kullanýldýðý durumlarda tek tek [BindProperty] yazmanýn önüne geçer.
        public Category Category { get; set; }

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet(int id)
        {
            Category = _db.Category.Find(id);
            //Category = _db.Category.FirstOrDefault(u => u.ID == id);
            //Category = _db.Category.SingleOrDefault(u => u.ID == id);
            //Category = _db.Category.Where(u => u.ID == id).FirstOrDefault();
        }

        public async Task<IActionResult> OnPost(Category category)
        {
            if (Category.Name == Category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Category.Name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _db.Category.Update(Category);
                await _db.SaveChangesAsync();
                TempData["Success"] = "Category edited successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
