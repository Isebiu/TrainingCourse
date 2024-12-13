using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Training.DataAccess.Repository.IRepository;
using Training.Models;
using Training.Utility;

namespace Training.Pages.Customer.Cart
{
    [Authorize]
    public class SummaryModel : PageModel
    {
        public IList<ShoppingCart>? ShoppingCartList { get; set; }
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public OrderHeader OrderHeader { get; set; }


        public SummaryModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            OrderHeader = new OrderHeader();
            
        }
        public void OnGet()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity; //get identitatea utilizatorului curent
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier); //extragem nameIdentifier din toate claim-urile

            if (claim != null)
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.AppUserId == claim.Value,
                    includeProperties: "MenuItem"); //populam lista cu menu-items din shopping cart-ul user-ului logat
                foreach (var item in ShoppingCartList)
                {
                    OrderHeader.OrderTotal += item.MenuItem.Price * item.Count;
                }
                AppUser appUser = _unitOfWork.AppUser.GetFirstOrDefault(u=>u.Id == claim.Value); // retrieve appUser from db
                OrderHeader.Name = appUser.FisrtName + " " + appUser.LastName; //populam OrderHeader name cu appUser name by default
                OrderHeader.PhoneNumber = appUser.PhoneNumber; //populam -//-
                
            }

        }
        public void OnPost()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity; //get identitatea utilizatorului curent
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier); //extragem nameIdentifier din toate claim-urile

            if (claim != null)
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.AppUserId == claim.Value,
                    includeProperties: "MenuItem"); //populam lista cu menu-items din shopping cart-ul user-ului logat
                foreach (var item in ShoppingCartList)
                {
                    OrderHeader.OrderTotal += item.MenuItem.Price * item.Count;
                }

                OrderHeader.Status = SD.StatusPending;
                OrderHeader.UserId = claim.Value;
                _unitOfWork.OrderHeader.Add(OrderHeader);
                _unitOfWork.Save();


                foreach (var item in ShoppingCartList)
                {
                    OrderDetails orderDetails = new OrderDetails()
                    {
                        MenuItemId = item.MenuItemId,
                        OrderId = OrderHeader.Id,
                        Name = item.MenuItem.Name,
                        Price = item.MenuItem.Price,
                        Count = item.Count
                    };
                    _unitOfWork.OrderDetails.Add(orderDetails);
                    
                }
                _unitOfWork.ShoppingCart.RemoveRange(ShoppingCartList); // clear ShoppingCart
                _unitOfWork.Save();
            }

        }
    }
}
