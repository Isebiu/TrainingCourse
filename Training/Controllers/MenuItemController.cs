using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Training.DataAccess.Repository.IRepository;

namespace Training.Controllers
{
    [Route("api/[controller]")] //define the route that will access this controller api/[controllerName]
    [ApiController] //
    public class MenuItemController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnv;

        public MenuItemController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnv)
        {
            _unitOfWork = unitOfWork;
            _hostEnv = hostEnv;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var menuItemList = _unitOfWork.MenuItem.GetAll(includeProperties: "Category,FoodType"); //returnam toate itemele din meniu in variabila noastra
            return Json(new { data = menuItemList }); //json care va fi returnat de API 
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.MenuItem.GetFirstOrDefault(u=>u.Id == id);
            _unitOfWork.MenuItem.Remove(objFromDb);
            _unitOfWork.Save();
            //delete the old image
            var oldImagePath = Path.Combine(_hostEnv.WebRootPath, objFromDb.Image.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }


            return Json(new { success = true, message = "Dlete success." }); //json care va fi returnat de API 
        }
    }
}
