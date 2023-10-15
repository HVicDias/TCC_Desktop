using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UPW.Database
{
    public class DatabaseConnection
    {
        class Count
        {
            private int count_;

            public int Quantidade { get => count_; set => count_ = value; }
        }

        private static DatabaseConnection instance;
        private const string ConnectionString = "Server=tcp:my-tcc.database.windows.net,1433;Initial Catalog=ResidencialAmar;Persist Security Info=False;User ID=hvdias;Password=@Defdef6778@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private SqlConnection sqlConnection;

        public int ID_CUIDADORA = -1;
        public int ID_ADMIN = -1;

        private DatabaseConnection()
        {
            sqlConnection = new SqlConnection(ConnectionString);
        }

        public static DatabaseConnection Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DatabaseConnection();
                }
                return instance;
            }
        }

        public void ExecuteCommandQuery(string query)
        {
            SqlCommand command = new SqlCommand(query, sqlConnection);
            sqlConnection.Open();
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public List<T> ExecuteQuery<T>(string query)
        {
            List<T> list = new List<T>();
            //try
            //{
                SqlCommand command = new SqlCommand(query, sqlConnection);
                sqlConnection.Open();
                var dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    var type = typeof(T);
                    T obj = (T)Activator.CreateInstance(type);
                    foreach (var prop in type.GetProperties())
                    {
                        var propType = prop.PropertyType;
                        prop.SetValue(obj, Convert.ChangeType(dataReader[prop.Name].ToString(), propType));
                    }
                    list.Add(obj);
                }
                sqlConnection.Close();
            //} catch (Exception) { }
            return list;
        }

        public bool VerifyLogin(string query)
        {
            List<Count> list = ExecuteQuery<Count>(query);
            if (list == null || list.Count() == 0)
            {
                return false;
            }

            return list[0].Quantidade == 1; 
        }

        public int ReturnSingleValue(string query)
        {
            List<Count> list = ExecuteQuery<Count>(query);
            if (list == null || list.Count() == 0)
            {
                return -1;
            }

            return list[0].Quantidade;
        }
    }
}
