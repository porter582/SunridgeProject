using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.Data;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository
{
    public class NewsItemRepository : Repository<NewsItem>, INewsItemRepository
    {
        private readonly ApplicationDbContext _db;

        public NewsItemRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetNewsItemListOrDropdown()
        {
            return _db.NewsItem.Select(i => new SelectListItem()
            {
                Text = i.NewsItemId.ToString(),
                Value = i.Header.ToString()
            });
        }

        public void Update(NewsItem newsItem)
        {
            var objFromDb = _db.NewsItem.FirstOrDefault(s => s.NewsItemId == newsItem.NewsItemId);

            objFromDb.Content = newsItem.Content;
            objFromDb.FileName = newsItem.FileName;
            objFromDb.FilePath = newsItem.FilePath;
            objFromDb.Header = newsItem.Header;
            objFromDb.NewsItemId = newsItem.NewsItemId;
            objFromDb.Year = newsItem.Year;

            _db.SaveChanges();
        }
    }
}
