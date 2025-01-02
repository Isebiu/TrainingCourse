using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe.Checkout;
using Training.DataAccess.Repository.IRepository;
using Training.Models;
using Training.Utility;

namespace Training.Pages.Customer.Cart
{
    public class OrderConfirmationModel : PageModel
    {
        
        private readonly IUnitOfWork _unitOfWork;
        
        public int OrderId { get; set; }
        


        public OrderConfirmationModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
           

        }
        public void OnGet(int id)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(u=>u.Id == id);
            if(orderHeader.SessionId != null)
            {
                var service = new SessionService();
                Session session = service.Get(orderHeader.SessionId);//preluam sesiunea deja existenta
                if(session.PaymentStatus.ToLower()=="paid")
                {
                    orderHeader.Status = SD.StatusApproved; //schimbam statusul order-ului
                    orderHeader.PaymentIntentId = session.PaymentIntentId;
                    _unitOfWork.Save();
                }
                List<ShoppingCart> shoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.AppUserId == orderHeader.UserId).ToList();
                _unitOfWork.ShoppingCart.RemoveRange(shoppingCartList);
                
                _unitOfWork.Save();
                OrderId = id;

            }

        }
    }
}
