namespace FootShopSystem.Services.Designers
{
    public interface IDesignerService
    {
        public bool IsDesigner(string userId);

        public int IdByUser(string userId);

    }
}
