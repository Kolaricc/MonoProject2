using Project2.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project2.Repository.Common;

namespace Project2.Repository
{
    public class ItemRepository : IItemRepository
    {
        public List<Item> GetAllItems()
        {
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-U9ANVTR;Initial Catalog=test;Integrated Security=True"))
            {
                List<Item> items = new List<Item>();
                SqlCommand selectItems = new SqlCommand("SELECT * FROM Item;", connection);
                connection.Open();

                SqlDataReader reader = selectItems.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Item item = new Item();
                        item.Set((Guid)reader[0], (string)reader[1], (string)reader[2], (Guid)reader[3], (decimal)reader[4]);
                        items.Add(item);
                    }
                }
                reader.Close();
                return items;
            }
        }

        public List<Item> FindByName(string name)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-U9ANVTR;Initial Catalog=test;Integrated Security=True"))
            {
                List<Item> items = new List<Item>();
                SqlCommand selectItems = new SqlCommand($"SELECT * FROM Item Where Item.Name = '{name}';", connection);
                connection.Open();
                SqlDataReader reader = selectItems.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        Item item = new Item();
                        item.Set((Guid)reader[0], (string)reader[1], (string)reader[2], (Guid)reader[3], (decimal)reader[4]);
                        items.Add(item);
                    }
                }
                reader.Close();
                return items;
            }
        }

        public Item FindById(Guid id)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-U9ANVTR;Initial Catalog=test;Integrated Security=True"))
            {
                Item item = new Item();
                SqlCommand selectItem = new SqlCommand($"SELECT * FROM Item Where Item.Id = '{id}';", connection);
                connection.Open();
                SqlDataReader reader = selectItem.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    item.Set((Guid)reader[0], (string)reader[1], (string)reader[2], (Guid)reader[3], (decimal)reader[4]);
                }
                reader.Close();
                return item;
            }
        }

        public bool AddNewItem(Item item)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-U9ANVTR;Initial Catalog=test;Integrated Security=True"))
            {
                Guid companyId;
                SqlCommand getCompany = new SqlCommand($"Select Id From Company where Id = '{item.CompanyId}';", connection);
                connection.Open();
                SqlDataReader reader = getCompany.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    companyId = (Guid)reader[0];
                }
                else
                {
                    reader.Close();
                    return false;
                }
                reader.Close();
                SqlCommand insertItem = new SqlCommand($"Insert Into Item Values('{item.Id}','{item.Category}','{item.Name}','{companyId}',{item.Price.ToString(System.Globalization.CultureInfo.InvariantCulture)});", connection);
                insertItem.ExecuteReader();
                return true;
            }
        }

        public string UpdateItem(Guid id, Item item)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-U9ANVTR;Initial Catalog=test;Integrated Security=True"))
            {
                SqlCommand getItem = new SqlCommand($"Select * From Item where Id = '{id}';", connection);
                connection.Open();
                SqlDataReader reader = getItem.ExecuteReader();
                if (!reader.HasRows)
                {
                    return "Item not found!";
                }
                reader.Close();
                SqlCommand getCompany = new SqlCommand($"Select * From Company where Id = '{item.CompanyId}';", connection);
                reader = getCompany.ExecuteReader();
                if (!reader.HasRows)
                {
                    return "Company not found!";
                }
                reader.Close();
                SqlCommand command = new SqlCommand($"Update Item set category = '{item.Category}', name = '{item.Name}', companyid = '{item.CompanyId}', price = {item.Price.ToString(System.Globalization.CultureInfo.InvariantCulture)} where id = '{id}';", connection);
                command.ExecuteReader();
                return "Item successfully changed";
            }
        }

        public bool Delete(Guid id)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-U9ANVTR;Initial Catalog=test;Integrated Security=True"))
            {
                SqlCommand findItem = new SqlCommand($"Select * From Item where Id = '{id}';", connection);
                connection.Open();
                SqlDataReader reader = findItem.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Close();
                    SqlCommand command = new SqlCommand($"Delete From Item where Id = '{id}';", connection);
                    command.ExecuteReader();
                    return true;
                }
                reader.Close();
                return false;
            }
        }
    }
}
