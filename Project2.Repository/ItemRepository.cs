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
        string connectionString = "Data Source=DESKTOP-U9ANVTR;Initial Catalog=test;Integrated Security=True";
        public async Task<List<Item>> GetAllItemsAsync()
        {
            using (SqlConnection connectionAsync = new SqlConnection(connectionString))
            {
                List<Item> items = new List<Item>();
                SqlCommand selectItems = new SqlCommand("SELECT * FROM Item;", connectionAsync);
                await connectionAsync.OpenAsync();

                SqlDataReader readerAsync = await selectItems.ExecuteReaderAsync();

                if (readerAsync.HasRows)
                {
                    while (await readerAsync.ReadAsync())
                    {
                        Item item = new Item();
                        item.Set((Guid)readerAsync[0], (string)readerAsync[1], (string)readerAsync[2], (Guid)readerAsync[3], (decimal)readerAsync[4], (DateTime)readerAsync[5]);
                        items.Add(item);
                    }
                }
                readerAsync.Close();
                return items;
            }
        }

        public async Task<List<Item>> FindByNameAsync(string name)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<Item> items = new List<Item>();
                SqlCommand selectItems = new SqlCommand("SELECT * FROM Item Where Item.Name = @name;", connection);
                await connection.OpenAsync();
                selectItems.Parameters.AddWithValue("@name", name);
                SqlDataReader readerAsync = await selectItems.ExecuteReaderAsync();

                if (readerAsync.HasRows)
                {
                    while (await readerAsync.ReadAsync())
                    {

                        Item item = new Item();
                        item.Set((Guid)readerAsync[0], (string)readerAsync[1], (string)readerAsync[2], (Guid)readerAsync[3], (decimal)readerAsync[4], (DateTime)readerAsync[5]);
                        items.Add(item);
                    }
                }
                readerAsync.Close();
                return items;
            }
        }

        public async Task<Item> FindByIdAsync(Guid id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Item item = new Item();
                SqlCommand selectItem = new SqlCommand("SELECT * FROM Item Where Item.Id = @id;", connection);
                await connection.OpenAsync();
                selectItem.Parameters.AddWithValue("@id", id);
                SqlDataReader readerAsync = await selectItem.ExecuteReaderAsync();

                if (readerAsync.HasRows)
                {
                    await readerAsync.ReadAsync();
                    item.Set((Guid)readerAsync[0], (string)readerAsync[1], (string)readerAsync[2], (Guid)readerAsync[3], (decimal)readerAsync[4], (DateTime)readerAsync[5]);
                }
                readerAsync.Close();
                return item;
            }
        }

        public async Task<string> AddNewItemAsync(Item item)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Guid companyId;
                SqlCommand getCompany = new SqlCommand("Select Id From Company where Id = @CompanyId;", connection);
                await connection.OpenAsync();
                getCompany.Parameters.AddWithValue("@CompanyId", item.CompanyId);
                SqlDataReader readerAsync = getCompany.ExecuteReader();
                if (readerAsync.HasRows)
                {
                    await readerAsync.ReadAsync();
                    companyId = (Guid)readerAsync[0];
                }
                else
                {
                    readerAsync.Close();
                    return "Company not found!";
                }
                readerAsync.Close();
                if (item.Category == null || item.Name == null || item.Price == 0) 
                {
                    return "Some parameters missing!";
                }
                SqlCommand insertItem = new SqlCommand("Insert Into Item Values(@Id,@Category,@Name,@CompanyId,@Price,@AdditionTime);", connection);
                insertItem.Parameters.AddWithValue("@Id", item.Id);
                insertItem.Parameters.AddWithValue("@Category", item.Category);
                insertItem.Parameters.AddWithValue("@Name", item.Name);
                insertItem.Parameters.AddWithValue("@CompanyId", item.CompanyId);
                insertItem.Parameters.AddWithValue("@Price", item.Price.ToString(System.Globalization.CultureInfo.InvariantCulture));
                insertItem.Parameters.AddWithValue("@AdditionTime", item.AdditionTime);
                await insertItem.ExecuteNonQueryAsync();
                return "Item added";
            }
        }

        public async Task<string> UpdateItemAsync(Guid id, Item item)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand getItem = new SqlCommand("Select * From Item where Id = @id;", connection);
                getItem.Parameters.AddWithValue("@id", id);
                await connection.OpenAsync();
                SqlDataReader readerAsync = await getItem.ExecuteReaderAsync();
                if (!readerAsync.HasRows)
                {
                    readerAsync.Close();
                    return "Item not found!";
                }
                readerAsync.Read();
                DateTime additionTime = (DateTime)readerAsync[5];
                readerAsync.Close();
                SqlCommand getCompany = new SqlCommand("Select * From Company where Id = @id;", connection);
                getCompany.Parameters.AddWithValue("@id", item.CompanyId);
                readerAsync = await getCompany.ExecuteReaderAsync();
                if (!readerAsync.HasRows)
                {
                    readerAsync.Close();
                    return "Company not found!";
                }
                readerAsync.Close();
                SqlCommand command = new SqlCommand("Update Item set category = @Category, name = @Name, companyid = @CompanyId, price = @Price, AdditionTime = @AdditionTime where id = @id;", connection);
                command.Parameters.AddWithValue("@Category", item.Category);
                command.Parameters.AddWithValue("@Name", item.Name);
                command.Parameters.AddWithValue("@CompanyId", item.CompanyId);
                command.Parameters.AddWithValue("@Price", item.Price.ToString(System.Globalization.CultureInfo.InvariantCulture));
                command.Parameters.AddWithValue("@AdditionTime", additionTime);
                command.Parameters.AddWithValue("@id", id);
                await command.ExecuteNonQueryAsync();
                return "Item successfully changed";
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand findItem = new SqlCommand("Select * From Item where Id = @id;", connection);
                findItem.Parameters.AddWithValue("@id", id);
                await connection.OpenAsync();
                SqlDataReader reader = await findItem.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    reader.Close();
                    SqlCommand deleteItem = new SqlCommand("Delete From Item where Id = @id;", connection);
                    deleteItem.Parameters.AddWithValue("@id", id);
                    await deleteItem.ExecuteNonQueryAsync();
                    return true;
                }
                reader.Close();
                return false;
            }
        }
    }
}
