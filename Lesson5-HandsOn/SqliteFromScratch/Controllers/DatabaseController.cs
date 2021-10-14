using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

namespace SqliteFromScratch.Controllers {
    // MVC is handling the routing for you.
    [Route("api/[Controller]")]
    public class DatabaseController : Controller {
        // api/database
        [HttpGet("tracks")]
        public List<Track> GetTracks(){
            System.Console.WriteLine("loading tracks database");
            string sql = $"SELECT * FROM tracks LIMIT 200;";
            return Data<Track>.GetData(sql);
        }

        [HttpGet("customers")]
        public List<Customer> GetCustomers(){
            System.Console.WriteLine("loading customers database");
            string sql = $"SELECT * FROM customers LIMIT 200;";    
            return Data<Customer>.GetData(sql);
        }

        [HttpGet("20-customers")]
        public List<Customer> Get20Customers(){
            System.Console.WriteLine("loading customers database");
            string sql = $"SELECT * FROM customers LIMIT 20;";    
            return Data<Customer>.GetData(sql);
        }

        [HttpGet("employees")]
        public List<Employee> GetEmployees(){
            System.Console.WriteLine("loading employees database");
            string sql = $"SELECT * FROM employees LIMIT 200;";
            return Data<Employee>.GetData(sql);
        }

        [HttpGet("employees-pre-2003")]
        public List<Employee> GetEmployeesPre2003(){
            System.Console.WriteLine("loading employees database");
            string sql = $"SELECT * FROM employees WHERE HireDate < '2003-01-01';";
            return Data<Employee>.GetData(sql);
        }

        public static T GetObject<T>() where T : new(){
            return new T();
        }

        class Data<T> where T : new(){

            static public List<T> GetData(string sql) {
                //Create a list to store the data
                
                List<T> listT = new List<T>();
                
                // GetFullPath will complete the path for the file named passed in as a string.
                string dataSource = "Data Source=" + Path.GetFullPath("chinook.db");

                // Initialize the connection to the .db file.
                using(SqliteConnection connection = new SqliteConnection(dataSource)){
                    connection.Open();

                    // create a string to hold the SQL command.
                    
                    // create a new SQL command by combining the location and command string.

                    using(SqliteCommand command = new SqliteCommand(sql, connection)){
                        using(SqliteDataReader reader = command.ExecuteReader()){

                            System.Console.WriteLine("Reading Data");
                            //read in the members / properties of the datatype T
                            PropertyInfo[] properties = typeof(T).GetProperties();
                            //parse out the data type of the property and the property names into a list
                            List<string[]> propertyNames = new List<string[]>();
                            foreach(var property in properties){
                                //System.Console.WriteLine(property.ToString());
                                //property[0] datatype
                                //property[1] property name
                                propertyNames.Add(property.ToString().Split(' '));
                            }
                            //keep track of what line the reader is on (for debugging)
                            int lineIndex = 1;
                            while(reader.Read()){
                                //store parsed data in t
                                T t = new T();
                                //iterate over each property and set them to the reader value at the appropriate index
                                System.Int32 i = 0;
                                foreach (var property in propertyNames)
                                {
                                    //System.Console.WriteLine(property[0]);
                                    //check to see if the data is null before entering
                                    if(reader.IsDBNull(i)){
                                        System.Console.WriteLine("Null at line " + lineIndex + " at pos[" + i + "]");
                                    }
                                    // map the data to the model.
                                    else if((property[0] == "Int32" || property[0] == "System.Nullable`1[System.Int32]")){
                                        t.GetType().GetProperty(property[1]).SetValue(t, reader.GetInt32(i));
                                    }
                                    else if(property[0] == "System.String"){
                                        t.GetType().GetProperty(property[1]).SetValue(t, reader.GetString(i));
                                    }
                                    i++;
                                }
                                //add to the list to be returned
                                listT.Add(t);
                                lineIndex++;
                            }
                        }
                    }
                    // close the connection
                    System.Console.WriteLine("Connection Closed");
                    connection.Close();
                }
                return listT;
            }
        }

    }
}