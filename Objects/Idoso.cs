using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPW.Database;
using Windows.UI.Xaml.Controls.Primitives;

namespace UPW.Objects
{
    public class Idoso
    {
        public int ID_PACIENTE { get; set; }
        public string Nome { get; set; }
        public string data_de_nascimento { get; set; }
        public string Convenio { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public string SUS { get; set; }
        public string Sexo { get; set; }
        public string Observacoes { get; set; }
        public string Alergias { get; set; }
        public string Foto { get; set; }
        public int fk_Administradores_ID_ADMIN { get; set; }

        public Idoso AddPaciente()
        {
            string commando = "INSERT INTO Pacientes " +
                "(ID_PACIENTE, nome, observacoes, alergias, sexo, RG, CPF, Convenio, SUS, " +
                "fk_Administradores_ID_ADMIN, data_de_nascimento) " +
                "VALUES ((SELECT MAX(Pacientes.ID_PACIENTE)+1 from Pacientes), '" + this.Nome + "', " +
                "'" + this.Observacoes + "', '" + this.Alergias + "', '" + this.Sexo + "', '" + this.RG + "', " +
                "'" + this.CPF + "',  '" + this.Convenio + "','" + this.SUS + "', " + 1 + ", " +
                "'" + Helpers.convertData(this.data_de_nascimento) + "');";

            
            DatabaseConnection.Instance.ExecuteCommandQuery(commando);

            this.ID_PACIENTE = DatabaseConnection.Instance.ReturnSingleValue("SELECT MAX(Pacientes.ID_PACIENTE) AS Quantidade from Pacientes);");

            return this;
        }

        public void UpdatePaciente()
        {
            string commando = "UPDATE Pacientes" +
                              "SET nome = '" + this.Nome + "'," +
                              "observacoes = '" + this.Observacoes + "'," +
                              "alergias = '" + this.Alergias + "'," +
                              "sexo = '" + this.Sexo + "'," +
                              "rg = '" + this.RG + "'," +
                              "CPF = '" + this.CPF + "'," + 
                              "Convenio = '" + this.Convenio + "'," +
                              "SUS = " + this.SUS + "," + 
                              "data_de_nascimento = '" + this.data_de_nascimento.ToString().Substring(10) + "'" +
                              "WHERE ID_PACIENTE = " + this.ID_PACIENTE;

            DatabaseConnection.Instance.ExecuteCommandQuery(commando);
        }

        public void DeletePaciente()
        {
            string commando = "DELETE FROM Responsaveis where ID_RESPONSAVEL in (" +
                "select ID_RESPONSAVEL from Responsaveis inner join possui on " +
                "ID_RESPONSAVEL = fk_Responsaveis_ID_RESPONSAVEL inner join Pacientes on " +
                "ID_PACIENTE = fk_Pacientes_ID_PACIENTE where fk_Pacientes_ID_PACIENTE = 3)" + this.ID_PACIENTE + ";";
            DatabaseConnection.Instance.ExecuteCommandQuery(commando);
            commando = "DELETE FROM PACIENTES WHERE ID_PACIENTE = " + this.ID_PACIENTE +";";
            DatabaseConnection.Instance.ExecuteCommandQuery(commando);
        }

        public static List<Idoso> GetIdosos()
        {
            string command = "SELECT * FROM Pacientes";
            return DatabaseConnection.Instance.ExecuteQuery<Idoso>(command);
        }
    }
}
