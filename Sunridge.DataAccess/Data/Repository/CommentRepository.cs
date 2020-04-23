using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Data;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Collections.Generic;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private readonly ApplicationDbContext _db;

        public CommentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetCommentListOrDropdown()
        {
            return _db.Comment.Select(i => new SelectListItem()
            {
                Value = i.CommentId.ToString(),
                Text = i.Content.ToString()
            });
        }

        public void Update(Comment comment)
        {
            var objFromDb = _db.Comment.FirstOrDefault(s => s.CommentId == comment.CommentId);
            objFromDb.LotHistoryId = comment.LotHistoryId;
            objFromDb.FormResponseId = comment.FormResponseId;
            objFromDb.OwnerId = comment.OwnerId;
            objFromDb.Content = comment.Content;
            objFromDb.Date = comment.Date;
            objFromDb.Private = comment.Private;
            objFromDb.LotHistory = comment.LotHistory;
            objFromDb.FormResponse = comment.FormResponse;
            objFromDb.Owner = comment.Owner;


            _db.SaveChanges();
        }
    }
}
