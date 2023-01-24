using Project2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project2.Repository;
using Project2.Service.Common;
using Project2.Repository.Common;

namespace Project2.Service
{
    public class ItemService:IItemService
    {
        IItemRepository itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        public async Task<List<Item>> GetAllItemsAsync()
        {
            return await itemRepository.GetAllItemsAsync();
        }

        public async Task<List<Item>> FindByNameAsync(string name)
        {
            return await itemRepository.FindByNameAsync(name);
        }

        public async Task<Item> FindByIdAsync(Guid id)
        {
            return await itemRepository.FindByIdAsync(id);
        }

        public async Task<string> AddNewItemAsync(Item item)
        {
            return await itemRepository.AddNewItemAsync(item);
        }

        public async Task<string> UpdateItemAsync(Guid id, Item item)
        {
            return await itemRepository.UpdateItemAsync(id, item);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await itemRepository.DeleteAsync(id);
        }
    }
}
