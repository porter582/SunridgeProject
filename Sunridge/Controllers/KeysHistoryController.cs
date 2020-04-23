using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Sunridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeysHistoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public KeysHistoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _unitOfWork.KeyHistory.GetAll(null, null, "Key,Lot") });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.KeyHistory.GetFirstOrDefault(u => u.KeyHistoryId == id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.KeyHistory.Remove(objFromDb);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }
    }
}