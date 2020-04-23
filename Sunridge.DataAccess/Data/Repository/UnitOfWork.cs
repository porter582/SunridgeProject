using Sunridge.Data;
using Sunridge.DataAccess.Data.Repository.IRepository;

namespace Sunridge.DataAccess.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IAdminCommentsRepository AdminComments { get; private set; }
        public IAddressRepository Address { get; private set; }
        public IBannerRepository Banner { get; private set; }
        public IBoardMemberRepository BoardMember { get; private set; }
        public IClassifiedCategoryRepository ClassifiedCategory { get; private set; }
        public IClassifiedImageRepository ClassifiedImage { get; private set; }
        public IClassifiedListingRepository ClassifiedListing { get; private set; }
        public IClassifiedServiceRepository ClassifiedService { get; private set; }
        public IClassifiedListingVMRepository ClassifiedListingVM { get; private set; }
        public ICommentRepository Comment { get; private set; }
        public ICommonAreaAssetRepository CommonAreaAsset { get; private set; }
        public IErrorViewModelRepository ErrorViewModel { get; private set; }
        public IFileRepository File { get; private set; }
        public IFormResponseRepository FormResponse { get; private set; }
        public IInKindWorkHoursRepository InKindWorkHours { get; private set; }
        public IInventoryRepository Inventory { get; private set; }
        public IAdminPhotoViewModelsRepository AdminPhotoViewModels { get; private set; }
        public IDashboardViewModelRepository DashboardViewModel { get; private set; }
        public ILostAndFoundItemRepository LostAndFoundItem { get; private set; }
        public IReportRepository ReportItem { get; private set; }
        public IEquipmentHoursRepository EquipmentHoursItem { get; private set; }
        public ILaborHoursRepository LaborHoursItem { get; private set; }
        public IPropDamageClaimReportRepository PropDamageClaimReport { get; private set; }

        public IChatRoomRepository ChatRoomItem { get; private set; }
        public IKeyRepository Key { get; private set; }
        public IKeyHistoryRepository KeyHistory { get; private set; }
        public ILotRepository Lot { get; private set; }
        public ILotHistoryRepository LotHistory { get; private set; }
        public ILotInventoryRepository LotInventory { get; private set; }
        public IMaintenanceRepository Maintenance { get; private set; }
        public INewsItemRepository NewsItem { get; private set; }
        public IOwnerLotRepository OwnerLot { get; private set; }
        public IPhotoRepository Photo { get; private set; }
        public IPhotoCategoriesRepository PhotoCategories { get; private set; }
        public IScheduledEventsRepository ScheduledEvents { get; private set; }
        public ITransactionRepository Transaction { get; private set; }
        public ITransactionTypeRepository TransactionType { get; private set; }


        //Grabs a connection to the actual db to connect to this class
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            ApplicationUser = new ApplicationUserRepository(_db);
            AdminComments = new AdminCommentsRepository(_db);
            Address = new AddressRepository(_db);
            Banner = new BannerRepository(_db);
            BoardMember = new BoardMemberRepository(_db);
            ClassifiedCategory = new ClassifiedCategoryRepository(_db);
            ClassifiedListing = new ClassifiedListingRepository(_db);
            ClassifiedService = new ClassifiedServiceRepository(_db);
            ClassifiedImage = new ClassifiedImageRepository(_db);
            ClassifiedListingVM = new ClassifiedListingVMRepository(_db);
            Comment = new CommentRepository(_db);
            CommonAreaAsset = new CommonAreaAssetRepository(_db);
            ErrorViewModel = new ErrorViewModelRepository(_db);
            File = new FileRepository(_db);
            FormResponse = new FormResponseRepository(_db);
            InKindWorkHours = new InKindWorkHoursRepository(_db);
            Inventory = new InventoryRepository(_db);
            AdminPhotoViewModels = new AdminPhotoViewModelsRepository(_db);
            DashboardViewModel = new DashboardViewModelRepository(_db);
            LostAndFoundItem = new LostAndFoundItemRepository(_db);
            ReportItem = new ReportRepository(_db);
            EquipmentHoursItem = new EquipmentHoursItemRepository(_db);
            LaborHoursItem = new LaborHoursItemRepository(_db);
            ChatRoomItem = new ChatRoomRepository(_db);
            Key = new KeyRepository(_db);
            KeyHistory = new KeyHistoryRepository(_db);
            Lot = new LotRepository(_db);
            LotHistory = new LotHistoryRepository(_db);
            LotInventory = new LotInventoryRepository(_db);
            Maintenance = new MaintenanceRepository(_db);
            NewsItem = new NewsItemRepository(_db);
            OwnerLot = new OwnerLotRepository(_db);
            Photo = new PhotoRepository(_db);
            PhotoCategories = new PhotoCategoriesRepository(_db);
            ScheduledEvents = new ScheduledEventsRepository(_db);
            Transaction = new TransactionRepository(_db);
            TransactionType = new TransactionTypeRepository(_db);
            PropDamageClaimReport = new PropDamageClaimReportRepository(_db);
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
