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
    public class BannerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment; //know permissions and paths

        public BannerController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _unitOfWork.Banner.GetAll(null, null, null) });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var objFromDb = _unitOfWork.Banner.GetFirstOrDefault(u => u.Id == id);
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

                _unitOfWork.Banner.Remove(objFromDb);
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