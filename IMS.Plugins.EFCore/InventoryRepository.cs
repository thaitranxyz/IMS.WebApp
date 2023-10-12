using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace IMS.Plugins.EFCore
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly IMSContext db;

        public InventoryRepository(IMSContext db)
        {
            this.db = db;
        }
        public async Task<IEnumerable<Inventory>> GetInventoriesByName(string name)
        {
            return await this.db.Inventories.Where(x => x.InventoryName.Contains(name, StringComparison.OrdinalIgnoreCase) || string.IsNullOrWhiteSpace(name)).ToListAsync();
        }

        public async Task<IEnumerable<Inventory>> GetInventories()
        {
            return await this.db.Inventories.ToListAsync();
        }

        public async Task AddInventoryAsync(Inventory inventory)
        {
            this.db.Inventories.Add(inventory); 
            await this.db.SaveChangesAsync();
        }

        public async Task UpdateInventoryAsync(Inventory inventory)
        {
            // Prevent having same inventory name 
            if (db.Inventories.Any(x => x.InventoryId != inventory.InventoryId && x.InventoryName.Equals(inventory.InventoryName, StringComparison.OrdinalIgnoreCase))) return;

            var inv = this.db.Inventories.Find(inventory.InventoryId); 
            if (inv != null)
            {
                //inv.InventoryName = inventory.InventoryName;
                //inv.Quantity = inventory.Quantity;
                //inv.Price = inventory.Price;    

                this.db.Inventories.Update(inventory);
                await this.db.SaveChangesAsync();
            }
        }

        public async Task DeleteInventoryAsync(Inventory inventory)
        {
            if (inventory != null)
            {
                this.db.Inventories.Remove(inventory);
                await this.db.SaveChangesAsync();
            }
        }

        public async Task<Inventory?> GetInventoryByIdAsync(int id)
        {
            return await this.db.Inventories.FindAsync(id);
        }

        
    }
}