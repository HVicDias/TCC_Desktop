using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPW.Database;

namespace UPW.Objects
{
    public class Responsavel
    {
        public int ID_RESPONSAVEL { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public string nome { get; set; }
        public string telefone { get; set; }
        public string email { get; set; }
        public string cod_temporario { get; set; }
        public string parentesco { get; set; }

        public void AddResponsavel(int ID_IDOSO)
        {
            string commando = "INSERT INTO Responsaveis " +
                "(ID_RESPONSAVEL, RG, CPF, nome, telefone, email, cod_temporario) " +
                "VALUES ((SELECT MAX(Responsaveis.ID_RESPONSAVEL)+1 from Responsaveis), '" + this.RG + "', " +
                "'" + this.CPF + "', '" + this.nome + "', '" + this.telefone + "', '" + this.email + "', " +
                "'" + this.cod_temporario + "');";
            DatabaseConnection.Instance.ExecuteCommandQuery(commando);

            commando = "INSERT INTO possui " +
                "(fk_Pacientes_ID_PACIENTE, fk_Responsaveis_ID_RESPONSAVEL, parentesco) " +
                "VALUES (" + ID_IDOSO + ",  ((SELECT MAX(Responsaveis.ID_RESPONSAVEL) from Responsaveis), '" + this.parentesco + "');";
            DatabaseConnection.Instance.ExecuteCommandQuery(commando);
        }

        public void UpdateResponsavel()
        {
            string commando = "UPDATE Responsaveis" +
                              "SET RG = '" + this.RG + "'," +
                              "CPF = '" + this.CPF + "'," +
                              "nome = '" + this.nome + "'," +
                              "telefone = '" + this.telefone + "'," +
                              "email = '" + this.email + "'" +
                              "WHERE ID_RESPONSAVEL = " + this.ID_RESPONSAVEL;

            DatabaseConnection.Instance.ExecuteCommandQuery(commando);
        }

        public void DeleteResponsavel()
        {
            string commando = "DELETE FROM Responsaveis WHERE ID_RESPONSAVEL = " + this.ID_RESPONSAVEL + ";";
            DatabaseConnection.Instance.ExecuteCommandQuery(commando);
        }

        public static List<Responsavel> GetResponsaveis(int ID_IDOSO)
        {
            string command = "SELECT * FROM Responsaveis INNER JOIN possui " +
                "ON ID_RESPONSAVEL = fk_Responsaveis_ID_RESPONSAVEL " +
                "AND fk_Pacientes_ID_PACIENTE = "+ ID_IDOSO + ";";
            return DatabaseConnection.Instance.ExecuteQuery<Responsavel>(command);
        }
    }
}
