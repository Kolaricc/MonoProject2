using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project2.Model;

namespace Project2.Service.Common
{
    public interface IItemService
    {
        Task<List<Item>> GetAllItemsAsync();

        Task<Item> FindByIdAsync(Guid id);

        Task<List<Item>> FindByNameAsync(string name);

        Task<string> AddNewItemAsync(Item item);

        Task<string> UpdateItemAsync(Guid id, Item item);

        Task<bool> DeleteAsync(Guid id);
    }
}
