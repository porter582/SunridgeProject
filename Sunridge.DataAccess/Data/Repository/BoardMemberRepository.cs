using Sunridge.Data;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository
{
    public class BoardMemberRepository : Repository<BoardMember>, IBoardMemberRepository
    {
        private readonly ApplicationDbContext _db;

        public BoardMemberRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(BoardMember boardMember)
        {
            var BoardMemberFromDb = _db.BoardMember.FirstOrDefault(s => s.Id == boardMember.Id);

            BoardMemberFromDb.BoardRole = boardMember.BoardRole;
            BoardMemberFromDb.ApplicationUserId = boardMember.ApplicationUserId;
            //BoardMemberFromDb.ApplicationUser = boardMember.ApplicationUser;
            BoardMemberFromDb.DisplayOrder = boardMember.DisplayOrder;
            if (boardMember.Image != null)
            {
                BoardMemberFromDb.Image = boardMember.Image;
            }

            _db.SaveChanges();
        }
    }
}
