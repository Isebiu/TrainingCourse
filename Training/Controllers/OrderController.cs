using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Training.DataAccess.Repository.IRepository;
using Training.Utility;

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
        //[Route("/OrderList/{status}")]
        public IActionResult GetAllByStatus(string? status)
        {

            //To do: rezolvare primire status in get
            var orderHeaders = _unitOfWork.OrderHeader.GetAll(includeProperties: "AppUser");
            switch (status)
            {
                case "cancelled":
                    orderHeaders = orderHeaders.Where(u => u.Status == SD.StatusCancelled || u.Status == SD.StatusRejected).ToList();
                    break;
                case "completed":
                    orderHeaders = orderHeaders.Where(u => u.Status == SD.StatusCompleted).ToList();
                    break;
                case "inprocess":
                    orderHeaders = orderHeaders.Where(u => u.Status == SD.StatusProcessing || u.Status == SD.StatusApproved).ToList();
                    break;
                case "ready":
                    orderHeaders = orderHeaders.Where(u => u.Status == SD.StatusReady).ToList();
                    break;
            }
            return Json(new {data = orderHeaders });
        }
    }
}
