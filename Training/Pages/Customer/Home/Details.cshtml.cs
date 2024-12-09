using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Training.DataAccess.Repository.IRepository;
using Training.Models;

namespace Training.Pages.Customer.Home
{
    //[BindProperties]
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public DetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [BindProperty]
        public ShoppingCart ShoppingCart { get; set; }
        public void OnGet(int id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity; //get identitatea utilizatorului curent
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier); //extragem nameIdentifier din toate claim-urile


            ShoppingCart = new()
            {
                AppUserId = claim.Value, //populam userId-ul in shopping Cart AppuserId
                MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(x => x.Id == id, includeProperties: "Category,FoodType"),
                MenuItemId = id //populam MenuItemId 
            };
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid) 
            { 
                //var claimsIdentity = (ClaimsIdentity)User.Identity; //get identitatea utilizatorului curent
                //var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier); //extragem nameIdentifier din toate claim-urile
                //ShoppingCart.AppUserId = claim.Value; //populam userId-ul in shopping Cart AppuserId

                _unitOfWork.ShoppingCart.Add(ShoppingCart);
                _unitOfWork.Save();
                return RedirectToPage("Index");
            }

            return Page();

        }
    }
}
