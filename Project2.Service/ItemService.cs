using Project2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project2.Repository;
using Project2.Service.Common;
namespace Project2.Service
{
    public class ItemService:IItemService
    {
        ItemRepository repository = new ItemRepository();
        public async Task<List<Item>> GetAllItemsAsync()
        {
            return await repository.GetAllItemsAsync();
        }

        public async Task<List<Item>> FindByNameAsync(string name)
        {
            return await repository.FindByNameAsync(name);
        }

        public async Task<Item> FindByIdAsync(Guid id)
        {
            return await repository.FindByIdAsync(id);
        }

        public async Task<string> AddNewItemAsync(Item item)
        {
            return await repository.AddNewItemAsync(item);
        }

        public async Task<string> UpdateItemAsync(Guid id, Item item)
        {
            return await repository.UpdateItemAsync(id, item);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await repository.DeleteAsync(id);
        }
    }
}
