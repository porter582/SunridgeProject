using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.Pages.ChatRoom
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public ChatRoomModel ChatRoomObj { get; set; }
        public IEnumerable<ChatRoomModel> ChatRoomModelList { get; set; }
        public IEnumerable<ApplicationUser> ApplicationUserList { get; set; }

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet()
        {
            ChatRoomModelList = _unitOfWork.ChatRoomItem.GetAll(null, null, null);
            ApplicationUserList = _unitOfWork.ApplicationUser.GetAll(null, null, null);
            ChatRoomObj = new ChatRoomModel();
            
        }

        public void OnPostClear()
        {
            IEnumerable <ChatRoomModel> chatRoom = _unitOfWork.ChatRoomItem.GetAll(null, null, null);
            foreach (var item in chatRoom)
            {
                _unitOfWork.ChatRoomItem.Remove(item);
            }
            _unitOfWork.Save();
        }
        public void OnPost()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (claim != null)
            {
                ChatRoomObj.ApplicationUserId = claim.Value;
                if (!ModelState.IsValid)
                {
                    //return Page();
                }

                if (ChatRoomObj.Id == 0) //new category
                {
                    _unitOfWork.ChatRoomItem.Add(ChatRoomObj);
                }
                else //else we edit
                {
                    _unitOfWork.ChatRoomItem.Update(ChatRoomObj);
                }
            }
            _unitOfWork.Save();
            ChatRoomModelList = _unitOfWork.ChatRoomItem.GetAll(null, null, null);
            ApplicationUserList = _unitOfWork.ApplicationUser.GetAll(null, null, null);
            //return Page();
        }
    }
}