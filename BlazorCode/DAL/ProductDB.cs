using InventoryChecker.Data;
using InventoryChecker.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryChecker.DAL
{
    public class ProductDB : IDAL
    {
        public string ConnectionString { get; set; }
        public ProductDB()
        {
            ConnectionString = "Server = ZBC-AA-ANTO6789; Database = Freezer; Integrated Security = true";
        }

        public void AddProduct(Product product)
        {
            //Open SQL connection
            //insert into Product(product.Name, product.Category)
            //foreach entry in product.Storagetype
                //insert into ProductAmount(product.Name, product.StorageType.Type, 0)
        }
        public List<Product> GetProductsByCategory(string category)
        {
            List<Product> productList = new List<Product>();

            using(SqlConnection cnn = new SqlConnection(ConnectionString))
            {
                try 
                {
                    //"exec get_products_by_category @category = @pCategory"
                    //"exec get_storagetype_by_product @product = @pProduct"
                    string productQuery = "select PName from product where category = '" + category + "'";
                    List<string> pList = new List<string>();
                    cnn.Open();

                    using (SqlCommand pCmd = new SqlCommand(productQuery, cnn))
                    {
                        //pCmd.Parameters.Add("@pCategory", SqlDbType.VarChar).Value = category;
                        SqlDataReader reader = pCmd.ExecuteReader();

                        while (reader.Read())
                        {
                            pList.Add((string)reader["PName"]);
                        }
                        reader.Close();
                    }
                    foreach (string p in pList)
                    {
                        List<ProductStorage> productstorageList = new List<ProductStorage>();
                        string storageQuery = "select storagetype, amount from productamount where product = '" + p + "'";
                        using (SqlCommand sCmd = new SqlCommand(storageQuery, cnn))
                        {
                            //sCmd.Parameters.Add("@pProduct", SqlDbType.VarChar).Value = (string)productReader["product"];
                            SqlDataReader reader = sCmd.ExecuteReader();
                            while (reader.Read())
                            {
                                ProductStorage ps = new ProductStorage((string)reader["storagetype"], (int)reader["amount"]);
                                productstorageList.Add(ps);
                            }
                            reader.Close();
                        }
                        Product product = new Product(p, category, productstorageList);
                        productList.Add(product);
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return productList;
        }
        public void UpdateProductAmount(string product, string storagetype, string amount)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try 
                {
                    conn.Open();
                    string updateQuery = "update productamount set amount = amount " + amount + " where product = '" + product + "' and storagetype = '" + storagetype + "'";
                    using(SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public List<string> GetCategories()
        {
            List<string> categories = new List<string>();

            using(SqlConnection cnn = new SqlConnection(ConnectionString))
            {
                try
                {
                    string categoryQuery = "select CName from Category";
                    cnn.Open();
                    using (SqlCommand cCmd = new SqlCommand(categoryQuery, cnn))
                    {
                        SqlDataReader reader = cCmd.ExecuteReader();
                        while(reader.Read())
                        {
                            categories.Add((string)reader["CName"]);
                        }
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return categories;
        }
        public List<string> GetStorageTypes()
        {
            //Open SQL connection
            //select SType from StorageType
            //add each result to List<string>

            List<string> storages = new List<string>() { "Kasser","Pladder","stk","Æsker"};
            return storages;
        }
    }
}
