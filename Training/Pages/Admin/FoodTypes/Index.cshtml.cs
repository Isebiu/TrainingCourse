using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Training.DataAccess.Data;
using Training.DataAccess.Repository.IRepository;
using Training.Models;

namespace Training.Pages.Admin.FoodTypes
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public IList<FoodType> FoodTypes { get; set; }
        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet()
        {
            FoodTypes = _unitOfWork.FoodType.GetAll();
        }
    }
}
