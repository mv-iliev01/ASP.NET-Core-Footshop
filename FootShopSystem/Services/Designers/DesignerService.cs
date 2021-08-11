using FootShopSystem.Data;
using FootShopSystem.Data.Models;
using System.Linq;

namespace FootShopSystem.Services.Designers
{
    public class DesignerService : IDesignerService
    {
        private readonly FootshopDbContext data;

        public DesignerService(FootshopDbContext data) 
            => this.data = data;

        public void AddDesignerToDb(Designer designerData)
        {
            this.data.Designers.Add(designerData);
            this.data.SaveChanges();
        }

        public int IdByUser(string userId)
        => this.data
                .Designers
                .Where(d => d.UserId == userId)
                .Select(d => d.Id)
                .FirstOrDefault();

        public bool IsDesigner(string userId)
            => data
            .Designers
            .Any(d => d.UserId == userId);
       
        
    }
}
