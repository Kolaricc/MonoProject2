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
        public List<Item> GetAllItems()
        {
            return repository.GetAllItems();
        }

        public List<Item> FindByName(string name)
        {
            return repository.FindByName(name);
        }

        public Item FindById(Guid id)
        {
            return repository.FindById(id);
        }

        public bool AddNewItem(Item item)
        {
            return repository.AddNewItem(item);
        }

        public string UpdateItem(Guid id, Item item)
        {
            return repository.UpdateItem(id, item);
        }

        public bool Delete(Guid id)
        {
            return repository.Delete(id);
        }
    }
}
