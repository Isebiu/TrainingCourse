using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Training.DataAccess.Repository.IRepository;
using Training.Utility;

namespace Training.ViewComponents
{
    public class ShoppingCartViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            int count = 0;
            if (claim != null)
            {
                //user is logged in
                if (HttpContext.Session.GetInt32(SD.SessionCart) != null)
                {
                    return View(HttpContext.Session.GetInt32(SD.SessionCart));
                }
                else
                {
                    count = _unitOfWork.ShoppingCart.GetAll(u => u.AppUserId == claim.Value).Count;
                    HttpContext.Session.SetInt32(SD.SessionCart, count);
                    return View(count);
                }
            }
            else
            {
                HttpContext.Session.Clear();
                return View(count);
            }
        }
    }
}
