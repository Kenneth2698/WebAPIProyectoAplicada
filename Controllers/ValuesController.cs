using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Npgsql;
using WebAPIProyecto1.Models;

namespace WebAPIProyecto1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost("TransferProducts/{IdProvider}")]
        public int TransferProducts(int IdProvider)
        {
          
            List<Product> products = new List<Product>();
            string server = "163.178.107.130";
            string database = "";
            string uid = "laboratorios";
            string password = "UCRSA.118";

            switch (IdProvider)
            {
                case 1:
                    database = "bk_aw";
                    break;
                case 2:
                    database = "bk_pcm";
                    break;
                case 3:
                    database = "bk_ms";
                    break;
            }
           
          
            string connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            MySqlConnection connection = new MySqlConnection(connectionString);

            string query = "call sp_getProducts()";
            connection.Open();
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
           
          while (dataReader.Read())
          {
              Product p = new Product();
              p.Id = Int32.Parse(dataReader["id"].ToString());
              p.Name = dataReader["name"].ToString();
              p.Price = Int32.Parse(dataReader["price"].ToString());
              p.Description = dataReader["description"].ToString();
              p.Image = dataReader["image"].ToString();
              p.Provider = Int32.Parse(dataReader["idProvider"].ToString());
              products.Add(p);
            }
           
           dataReader.Close();
           connection.Close();


           NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;User Id=postgres; " +"Password=1234;Database=Principal;");
           foreach (Product temp in products )
           {
               conn.Open();
               NpgsqlCommand command = new NpgsqlCommand("INSERT INTO PRODUCT values (DEFAULT,'"+temp.Name+ "',"+ temp.Price + ",'" + temp.Image + "'," + 1 + ",'" + temp.Description + "'," + temp.Provider + ")", conn);
               command.ExecuteScalar();
               conn.Close();
           }
           return 1;
        }
    }
}
