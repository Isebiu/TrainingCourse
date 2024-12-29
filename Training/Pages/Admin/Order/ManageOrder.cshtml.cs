using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Training.DataAccess.Repository.IRepository;
using Training.Models;
using Training.Models.VierwModel;
using Training.Utility;

namespace Training.Pages.Admin.Order
{
    public class ManageOrderModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public List<OrderDetailVM> OrderDetailsVM { get; set; } //OrderDetail view model pt a include mai multe prop intr-un sg view model
        public ManageOrderModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet()
        {
            OrderDetailsVM = new();
            List<OrderHeader> orderHeaders = _unitOfWork.OrderHeader.GetAll(u => u.Status == SD.StatusApproved || u.Status == SD.StatusProcessing).ToList(); //preluam din db toate ordHeaders cu acele statusuri
            foreach(var order in orderHeaders)
            {
                OrderDetailVM individual = new OrderDetailVM()
                {
                    OrderHeader = order,
                    OrderDetailsList = _unitOfWork.OrderDetails.GetAll(u => u.OrderId == order.Id).ToList()
                };
                OrderDetailsVM.Add(individual);
            }

        }
    }
}
