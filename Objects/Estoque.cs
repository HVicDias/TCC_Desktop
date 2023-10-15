using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPW.Database;

namespace UPW.Objects
{
    public class Estoque
    {
        public int ID_ESTOQUE { get; set; }
        public string Dosagem { get; set; }
        public int Quantidade { get; set; }
        public string Unidade_de_quantidade { get; set; }
        public int fk_Remedio_ID_REMEDIO { get; set; }
        public string categoria_regulatoria { get; set; }

        public void AddEstoque()
        {
            string commando = "INSERT INTO Estoque_de_remedios " +
                "(ID_ESTOQUE, Dosagem, Quantidade, Unidade_de_quantidade, " +
                "fk_Remedio_ID_REMEDIO, categoria_regulatoria) " +
                "VALUES ((SELECT MAX(Estoque_de_remedios.ID_ESTOQUE)+1 from Estoque_de_remedios), " +
                "'" + this.Dosagem + "', " +
                this.Quantidade + ", '" + 
                "'" + this.Unidade_de_quantidade + "',  " +
                this.fk_Remedio_ID_REMEDIO + ",  " +
                "'" + this.categoria_regulatoria + "');";

            DatabaseConnection.Instance.ExecuteCommandQuery(commando);
        }

        public void UpdateEstoque()
        {
            string commando = "UPDATE Estoque_de_remedios" +
                              "SET Quantidade = '" + this.Quantidade + "'," +
                              "WHERE ID_ESTOQUE = " + this.ID_ESTOQUE;

            DatabaseConnection.Instance.ExecuteCommandQuery(commando);
        }

        public void DeleteEstoque()
        {
            string commando = "DELETE FROM Estoque_de_remedios WHERE ID_ESTOQUE = " + this.ID_ESTOQUE + ";";
            DatabaseConnection.Instance.ExecuteCommandQuery(commando);
        }

        public static List<Estoque> GetEstoques()
        {
            string command = "SELECT * FROM Estoque_de_remedios";
            return DatabaseConnection.Instance.ExecuteQuery<Estoque>(command);
        }
    }
}
