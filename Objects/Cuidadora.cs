using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPW.Database;

namespace UPW.Objects
{
    public class Cuidadora
    {
        public int ID_CUIDADORA { get; set; }
        public string Nome { get; set; }
        public string RG { get; set; }
        public string Telefone { get; set; }
        public string login { get; set; }
        public string senha { get; set; }
        public string telegram_id { get; set; }
        public string temp_cod { get; set; }
        public int fk_Administradores_ID_ADMIN { get; set; }

        public static bool verifyCuidadoraLogin(string login, string senha)
        {
            string command = "SELECT COUNT(*) AS Quantidade FROM [dbo].[Cuidadora] " +
                             "WHERE login = '" + login + "' AND senha = '" + senha + "';";

            return DatabaseConnection.Instance.VerifyLogin(command);
        }

        public static int getCuidadoraID(string login, string senha)
        {
            string command = "SELECT ID_CUIDADORA AS Quantidade FROM [dbo].[Cuidadora] " +
                             "WHERE login = '" + login + "' AND senha = '" + senha + "';";

            return DatabaseConnection.Instance.ReturnSingleValue(command);
        }

        public void AddCuidadora()
        {
            string commando = "INSERT INTO Cuidadora " +
                "(ID_CUIDADORA, Nome, RG, Telefone, fk_Administradores_ID_ADMIN, login, senha, temp_cod) " +
                "VALUES ((SELECT MAX(Cuidadora.ID_CUIDADORA)+1 from Cuidadora), '" + this.Nome + "', " +
                "'" + this.RG + "', '" + this.Telefone + "', '" + this.fk_Administradores_ID_ADMIN + 
                this.temp_cod + "');";

            DatabaseConnection.Instance.ExecuteCommandQuery(commando);
        }

        public void UpdateCuidadora()
        {
            string commando = "UPDATE Cuidadora" +
                              "SET nome = '" + this.Nome + "'," +
                              "RG = '" + this.RG + "'," +
                              "Telefone = '" + this.Telefone + "'," +
                              "WHERE ID_CUIDADORA = " + this.ID_CUIDADORA;

            DatabaseConnection.Instance.ExecuteCommandQuery(commando);
        }

        public void DeleteCuidadora()
        {
            string commando = "DELETE FROM Cuidadora WHERE ID_CUIDADORA = " + this.ID_CUIDADORA + ";";
            DatabaseConnection.Instance.ExecuteCommandQuery(commando);
        }

        public static List<Cuidadora> GetCuidadoras()
        {
            string command = "SELECT * FROM Cuidadora";
            return DatabaseConnection.Instance.ExecuteQuery<Cuidadora>(command);
        }

    }
}
