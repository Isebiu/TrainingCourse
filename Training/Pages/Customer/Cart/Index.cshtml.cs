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
    public class IndexModel : PageModel
    {
        public IList<ShoppingCart>? ShoppingCartList { get; set; }
        private readonly IUnitOfWork _unitOfWork;
        public double TotalPrice { get; set; }

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            TotalPrice = 0;
        }
        public void OnGet()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity; //get identitatea utilizatorului curent
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier); //extragem nameIdentifier din toate claim-urile

            if (claim != null)
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u=> u.AppUserId == claim.Value,
                    includeProperties:"MenuItem"); //populam lista cu menu-items din shopping cart-ul user-ului logat
                foreach(var item in ShoppingCartList)
                {
                    TotalPrice += item.MenuItem.Price * item.Count;
                }
            }

        }
        //[ActionName("Plus")]
        public IActionResult OnPostPlus(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u=>u.Id== cartId); //retrieve the cart from db based on id
            _unitOfWork.ShoppingCart.IncrementCount(cart, 1); //incrementare cu 1
            return RedirectToPage("/Customer/Cart/Index");
        }
        public IActionResult OnPostMinus(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId); //retrieve the cart from db based on id
            if (cart.Count == 1)
            {
                var count = _unitOfWork.ShoppingCart.GetAll(u => u.AppUserId == cart.AppUserId).ToList().Count - 1;
                _unitOfWork.ShoppingCart.Remove(cart);
                _unitOfWork.Save();
                HttpContext.Session.SetInt32(SD.SessionCart, count);


            }
            else
            {
                _unitOfWork.ShoppingCart.DecrementCount(cart, 1); //decrementeaza cu 1
            }
            
            return RedirectToPage("/Customer/Cart/Index");
        }

        public IActionResult OnPostRemove(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId); //retrieve the cart from db based on id
            var count = _unitOfWork.ShoppingCart.GetAll(u => u.AppUserId == cart.AppUserId).ToList().Count-1;

            _unitOfWork.ShoppingCart.Remove(cart); //stergem caruciorul din db
            _unitOfWork.Save(); //salvam db-ul in urma stergerii 

            HttpContext.Session.SetInt32(SD.SessionCart, count);
            return RedirectToPage("/Customer/Cart/Index");
        }
    }
}
