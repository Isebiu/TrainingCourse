using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe;
using Stripe.Climate;
using Training.DataAccess.Repository.IRepository;
using Training.Models;
using Training.Models.VierwModel;
using Training.Utility;

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
        public IActionResult OnPostCompleteOrder(int orderId)
        {
            _unitOfWork.OrderHeader.UpdateStatus(orderId, SD.StatusCompleted);
            _unitOfWork.Save();
            
            return RedirectToPage("OrderList");
        }
        public IActionResult OnPostCancelOrder(int orderId)
        {
            _unitOfWork.OrderHeader.UpdateStatus(orderId, SD.StatusCancelled);
            _unitOfWork.Save();
            
            return RedirectToPage("OrderList");
        }
        public IActionResult OnPostRefundOrder(int orderId)
        {
            //////-Refund-
            OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(u => u.Id == orderId);
            var options = new RefundCreateOptions
            { //avem nevoie de 3 proprietati pt returnarea banilor
                Reason = RefundReasons.RequestedByCustomer,
                PaymentIntent = orderHeader.PaymentIntentId
            };

            //pt returnare trebuie creat un service de tip refund
            var service = new RefundService();
            //cream un Refund obj(al stripe-ului)
            Refund refund = service.Create(options);
            ///////--
            


            _unitOfWork.OrderHeader.UpdateStatus(orderId, SD.StatusRefunded);
            _unitOfWork.Save();
            
            return RedirectToPage("OrderList");
        }
    }
}
