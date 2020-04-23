using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Data;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Collections.Generic;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    public class ChatRoomRepository : Repository<ChatRoomModel>, IChatRoomRepository
    {
        private readonly ApplicationDbContext _db;

        public ChatRoomRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetChatRoomListOrDropdown()
        {
            return _db.ChatRoomItem.Select(i => new SelectListItem()
            {
                Value = i.Id.ToString(),
                Text = i.Id.ToString()
            });
        }

        public void Update(ChatRoomModel chatRoom)
        {
            var objFromDb = _db.ChatRoomItem.FirstOrDefault(s => s.Id == chatRoom.Id);
            objFromDb.Post = chatRoom.Post;
            objFromDb.ApplicationUserId = chatRoom.ApplicationUserId;
            objFromDb.ApplicationUser = chatRoom.ApplicationUser;

            _db.SaveChanges();
        }
    }
}
