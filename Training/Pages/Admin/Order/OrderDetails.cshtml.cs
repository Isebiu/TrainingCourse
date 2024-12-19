using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Training.DataAccess.Repository.IRepository;
using Training.Models;
using Training.Models.VierwModel;

namespace Training.Pages.Admin.Order
{
    public class OrderDetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderDetailVM OrderDetailsVM { get; set; }
        public OrderDetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet(int id)
        {
            OrderDetailsVM = new()
            {
                //populam OrderHeader si list Details din db
                OrderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(u => u.Id == id, includeProperties:"AppUser"),
                OrderDetailsList = _unitOfWork.OrderDetails.GetAll(u => u.OrderId == id).ToList()
            };
        }
    }
}
