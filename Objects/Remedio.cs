using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPW.Database;

namespace UPW.Objects
{
    public class Remedio
    {
        public int ID_REMEDIO { get; set; }
        public string nome { get; set; }
        public string dosagem_prescrita { get; set; }
        public int intervalo_entre_doses { get; set; }
        public string classe_tearapeutica { get; set; }
        public string fk_Administradores_ID_ADMIN { get; set; }
        public string fk_Cuidadora_ID_CUIDADORA { get; set; }
        public string fk_Pacientes_ID_PACIENTE { get; set; }
        public string fk_medico_ID_MEDICO { get; set; }
        public string unidade_de_medida { get; set; }
        public string inicio_tratamento { get; set; }
        public string modo_de_aplicacao { get; set; }
        public string controlado { get; set; }
        public string duracao { get; set; }
        public string uso_continuo { get; set; }

        public void AddRemedio()
        {
            string commando = "INSERT INTO Remedio " +
                "(ID_REMEDIO, nome, dosagem_prescrita, intervalo_entre_doses, classe_tearapeutica, " +
                "fk_Administradores_ID_ADMIN, fk_Cuidadora_ID_CUIDADORA, " +
                "fk_Pacientes_ID_PACIENTE, fk_medico_ID_MEDICO, unidade_de_medida, inicio_tratamento, " +
                "modo_de_aplicacao, controlado, duracao, uso_continuo) " +
                "VALUES ((SELECT MAX(Remedio.ID_REMEDIO)+1 from Remedio), '" + this.nome + "', '" +
                this.dosagem_prescrita + "', " + this.intervalo_entre_doses + ", '" + 
                this.classe_tearapeutica + "', " +
                this.fk_Administradores_ID_ADMIN + ", " + this.fk_Cuidadora_ID_CUIDADORA + "," + 
                this.fk_Pacientes_ID_PACIENTE + ", " + this.fk_medico_ID_MEDICO + ", '" +
                this.unidade_de_medida + "', '" + this.inicio_tratamento + "', '" +
                this.modo_de_aplicacao + "', " + this.controlado + ", " +
                this.duracao + ", " + this.uso_continuo+ ");";

            DatabaseConnection.Instance.ExecuteCommandQuery(commando);
        }

        public void UpdateRemedio()
        {
            string commando = "UPDATE Remedio" +
                              "SET nome = '" + this.nome + "'," +
                              "dosagem_prescrita = '" + this.dosagem_prescrita + "'," +
                              "intervalo_entre_doses = " + this.intervalo_entre_doses + "," +
                              "classe_tearapeutica = '" + this.classe_tearapeutica + "'," +
                              "fk_medico_ID_MEDICO = " + this.fk_medico_ID_MEDICO + "," +
                              "unidade_de_medida = '" + this.unidade_de_medida + "'," +
                              "modo_de_aplicacao = '" + this.modo_de_aplicacao + "'" +
                              "controlado = " + this.controlado + "," +
                              "duracao = " + this.duracao + "," +
                              "uso_continuo = " + this.uso_continuo + " " +
                              "WHERE ID_REMEDIO = " + this.ID_REMEDIO + ";";

            DatabaseConnection.Instance.ExecuteCommandQuery(commando);
        }

        public void DeleteRemedio()
        {
            string commando = "DELETE FROM Remedio WHERE ID_REMEDIO = " + this.ID_REMEDIO + ";";
            DatabaseConnection.Instance.ExecuteCommandQuery(commando);
        }

        public static List<Remedio> GetRemedios()
        {
            string command = "SELECT * FROM Remedio";
            return DatabaseConnection.Instance.ExecuteQuery<Remedio>(command);
        }
    }
}
