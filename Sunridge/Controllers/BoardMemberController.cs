using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Sunridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardMemberController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment; //know permissions and paths

        public BoardMemberController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _unitOfWork.BoardMember.GetAll(null,null,"ApplicationUser")}); 
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var objFromDb = _unitOfWork.BoardMember.GetFirstOrDefault(u => u.Id == id);
                if (objFromDb == null)
                {
                    return Json(new { success = false, message = "Error while deleting" });
                }

                //physically delete the image in the wwwroot in this path:
                var imagePath =
                    Path.Combine(_hostingEnvironment.WebRootPath, objFromDb.Image.TrimStart('\\'));

                //if path exists, delete it
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                _unitOfWork.BoardMember.Remove(objFromDb);
                _unitOfWork.Save();

            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            return Json(new { success = true, message = "Delete Successful" });
            
        }
    }
}