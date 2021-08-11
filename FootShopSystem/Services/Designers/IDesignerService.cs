namespace FootShopSystem.Services.Designers
{
    using FootShopSystem.Data.Models;
    public interface IDesignerService
    {
        public bool IsDesigner(string userId);

        public int IdByUser(string userId);

        public void AddDesignerToDb(Designer designerData);

    }
}
