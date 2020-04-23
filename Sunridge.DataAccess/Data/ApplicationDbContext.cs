using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sunridge.Models;
using Sunridge.Models.ViewModels;

namespace Sunridge.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Key> Key { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<AdminComments> AdminComments { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Banner> Banner { get; set; }
        public DbSet<BoardMember> BoardMember { get; set; }
        public DbSet<ClassifiedCategory> ClassifiedCategory { get; set; }
        public DbSet<ClassifiedListing> ClassifiedListing { get; set; }
        public DbSet<ClassifiedService> ClassifiedService { get; set; }
        public DbSet<ClassifiedImage> ClassifiedImage { get; set; }
        public DbSet<ClassifiedListingVM> ClassifiedListingVM { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<CommonAreaAsset> CommonAreaAsset { get; set; }
        public DbSet<ErrorViewModel> ErrorViewModel { get; set; }
        public DbSet<File> File { get; set; }
        public DbSet<FormResponse> FormResponse { get; set; }
        public DbSet<InKindWorkHours> InKindWorkHours { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<AdminPhotoViewModels> AdminPhotoViewModels { get; set; }
        public DbSet<DashboardViewModel> DashboardViewModel { get; set; }
        public DbSet<LostAndFoundItem> LostAndFoundItem { get; set; }
        public DbSet<Report> ReportItem { get; set; }
        public DbSet<EquipmentHours> EquipmentHoursItem { get; set; }
        public DbSet<LaborHours> LaborHoursItem { get; set; }
        public DbSet<ChatRoomModel> ChatRoomItem { get; set; }
        public DbSet<KeyHistory> KeyHistory { get; set; }
        public DbSet<Lot> Lot { get; set; }
        public DbSet<LotHistory> LotHistory { get; set; }
        public DbSet<LotInventory> LotInventory { get; set; }
        public DbSet<Maintenance> Maintenance { get; set; }
        public DbSet<NewsItem> NewsItem { get; set; }
        public DbSet<OwnerLot> OwnerLot { get; set; }
        public DbSet<Photo> Photo { get; set; }
        public DbSet<PhotoCategories> PhotoCategories { get; set; }
        public DbSet<ScheduledEvent> ScheduledEvent { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<TransactionType> TransactionType { get; set; }
        public DbSet<PropDamageClaimReport> PropDamageClaimReport { get; set; }
    }
}
