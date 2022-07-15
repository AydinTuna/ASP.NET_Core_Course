using AbbyWeb.Data;
using AbbyWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Categories
{   // [BindProperties] class dýþýna yazýlýr.
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        //[BindProperty]
        // [BindProperties] diyerek birden fazla prop kullanýldýðý durumlarda tek tek [BindProperty] yazmanýn önüne geçer.
        public Category Category { get; set; }

        public DeleteModel(ApplicationDbContext db)
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
         
            var categoryFromDb = _db.Category.Find(Category.ID);
            if (categoryFromDb != null)
            {
                _db.Category.Remove(categoryFromDb);
                await _db.SaveChangesAsync();
                TempData["Success"] = "Category deleted successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
