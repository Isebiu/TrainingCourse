using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Training.DataAccess.Data;
using Training.DataAccess.Repository.IRepository;
using Training.Models;

namespace Training.Pages.Admin.FoodTypes
{
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public FoodType FoodType { get; set; }
        public DeleteModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet(int id)
        {
            FoodType = _unitOfWork.FoodType.GetFirstOrDefault(u => u.Id == id);
        }
        public async Task<IActionResult> OnPost()
        {
            if (FoodType != null)
            {
                _unitOfWork.FoodType.Remove(FoodType);
                _unitOfWork.Save();
                TempData["success"]="Food Type deleted succesfully!";
                return RedirectToPage("Index");
            }
            return Page();

        }
    }
}
