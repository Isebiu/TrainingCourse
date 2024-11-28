using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Training.DataAccess.Data;
using Training.DataAccess.Repository.IRepository;
using Training.Models;


namespace Training.Pages.Admin.Categories
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public IList<Category> Categories { get; set; }
        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet()
        {
            Categories = _unitOfWork.Category.GetAll();
        }
    }
}
