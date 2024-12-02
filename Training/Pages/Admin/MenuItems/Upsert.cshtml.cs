using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Training.DataAccess.Data;
using Training.DataAccess.Repository;
using Training.DataAccess.Repository.IRepository;
using Training.Models;

namespace Training.Pages.Admin.MenuItems
{
    [BindProperties]
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnv;
        //[BindProperty]
        public MenuItem MenuItem { get; set; }
        public IList<SelectListItem> CategoryList { get; set; }
        public IList<SelectListItem> FoodTypeList { get; set; }

        public UpsertModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnv)
        {
            _unitOfWork = unitOfWork;
            MenuItem = new MenuItem();
            _hostEnv = hostEnv;
        }
        public void OnGet(int? id)
        {
            if (id != null)
            {
                //Edit request
                //populam menu item 
                MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(u => u.Id== id);
            }


            //populam dropdown-ul in get handler
            CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem() //folosim proiectii cu Select si cream un select list item 
            {                                                                             //dupa text si valoare din categoria primita din db  
                Text = i.Name,
                Value = i.Id.ToString()
            }).ToList(); // convertim in lista pt ca asta e tipul prop
            FoodTypeList = _unitOfWork.FoodType.GetAll().Select(i => new SelectListItem() //folosim proiectii cu Select si cream un select list item 
            {                                                                             //dupa text si valoare din categoria primita din db  
                Text = i.Name,
                Value = i.Id.ToString()
            }).ToList(); // convertim in lista pt ca asta e tipul prop
        }
        public async Task<IActionResult> OnPost()
        {
            string webRootPath = _hostEnv.WebRootPath; //pointing to wwwroot folder
            var files = HttpContext.Request.Form.Files;
            if(MenuItem.Id == 0)
            {
                //Create
                string fileName_new = Guid.NewGuid().ToString(); //new file care sa aibe un nume unic
                var uploads = Path.Combine(webRootPath, @"images\menuItems"); // ne ofera locul unde tb sa incarcam fisierul nostru 
                var extension = Path.GetExtension(files[0].FileName);//ca sa ne asiguram ca toate fisierele au aceeasi extensie 

                using (var fileStream = new FileStream(Path.Combine(uploads, fileName_new + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                MenuItem.Image = @"\images\menuItems\" + fileName_new + extension;

                _unitOfWork.MenuItem.Add(MenuItem);
                _unitOfWork.Save();
                //return RedirectToPage("Index");
            }
            else
            {
                //Edit
                var objFromDb = _unitOfWork.MenuItem.GetFirstOrDefault(u=>u.Id == MenuItem.Id);
                if (files.Count > 0)
                {
                    string fileName_new = Guid.NewGuid().ToString(); //new file care sa aibe un nume unic
                    var uploads = Path.Combine(webRootPath, @"images\menuItems"); // ne ofera locul unde tb sa incarcam fisierul nostru 
                    var extension = Path.GetExtension(files[0].FileName);//ca sa ne asiguram ca toate fisierele au aceeasi extensie 

                    //delete the old image
                    var oldImagePath = Path.Combine(webRootPath, objFromDb.Image.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                    //new upload
                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName_new + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    MenuItem.Image = @"\images\menuItems\" + fileName_new + extension;
                }
                else
                {
                    MenuItem.Image=objFromDb.Image; //doar pt siguranta
                }

                _unitOfWork.MenuItem.Update(MenuItem);
                _unitOfWork.Save();
            }
            return RedirectToPage("./Index");
        }
    }
}
