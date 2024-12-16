using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe.Checkout;
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
        public IActionResult OnPost()
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
                
                //_unitOfWork.ShoppingCart.RemoveRange(ShoppingCartList); // clear ShoppingCart
                _unitOfWork.Save();

                var domain = "https://localhost:44362"; //domeniul aplicatiei noastre : 
                var options = new SessionCreateOptions //cream o sesiune (createOption) care e pt checkout
                {
                    LineItems = new List<SessionLineItemOptions>() //lista de SessionLineItemOptions -> in care cream 
                    ,
                    PaymentMethodTypes = new List<string>
                    {
                        "card",
                    },
                    Mode = "payment",
                    SuccessUrl = domain + $"/customer/cart/OrderConfirmation?id={OrderHeader.Id}", // daca pay-ment-ul e realizat cu succes -> redirectionam spre ruta , preluam order id-ul pentru care afisam orderconfirm
                    CancelUrl = domain + "/customer/cart/index", //redirectionare spre cancelUrl
                }; //configurarea optiunilor pt stripe

                //Add line Items
                foreach(var item in ShoppingCartList)
                {
                    var sessionLineItem = new SessionLineItemOptions // un nou obiectde SessinLineItemsOpt
                    {
                        PriceData = new SessionLineItemPriceDataOptions //nou obj unde definim toate optiunile pt produsuele noastre
                        {
                            UnitAmount = (long)(item.MenuItem.Price * 100), //setam prop UnitAmount care e pretul intregului cos ( din tabelul OrderHeader.OrderTotal)
                            Currency = "usd", // setam moneda
                            ProductData = new SessionLineItemPriceDataProductDataOptions //generam un nou obj de product inline
                            {
                                Name = item.MenuItem.Name 
                            },
                        },
                        Quantity = item.Count

                    };
                    options.LineItems.Add(sessionLineItem);
                }

                var service = new SessionService(); //cream sesiunea 
                Session session = service.Create(options); // cream un obj de tip stripe checkout session
                
                Response.Headers.Add("Location", session.Url);
                OrderHeader.SessionId= session.Id; //populam session Id
                OrderHeader.PaymentIntentId = session.PaymentIntentId;
                _unitOfWork.Save();
                return new StatusCodeResult(303); // returnam statusul 303, care e un alt mod de a redirectiona
            }
            return Page();

        }
    }
}
