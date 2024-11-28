using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Training.DataAccess.Data;
using Training.DataAccess.Repository.IRepository;
using Training.Models;

namespace Training.Pages.Admin.FoodTypes
{
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public FoodType FoodType { get; set; }
        public EditModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet(int id)
        {
            FoodType = _unitOfWork.FoodType.GetFirstOrDefault(u=>u.Id == id);
        }
        public async Task<IActionResult> OnPost()
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.FoodType.Update(FoodType);
                _unitOfWork.Save();
                TempData["success"]="Food Type updated succesfully!";
                return RedirectToPage("Index");

            }
            return Page();
        }
    }
}
