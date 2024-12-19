using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Training.DataAccess.Repository.IRepository;

namespace Training.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var orderHeaders = _unitOfWork.OrderHeader.GetAll(includeProperties:"AppUser");
            return Json(new {data = orderHeaders });
        }
    }
}
