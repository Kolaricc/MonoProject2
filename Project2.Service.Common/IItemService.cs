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
        List<Item> GetAllItems();

        Item FindById(Guid id);

        bool AddNewItem(Item item);

        string UpdateItem(Guid id, Item item);

        bool Delete(Guid id);
    }
}
