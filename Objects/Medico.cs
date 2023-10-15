using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPW.Database;

namespace UPW.Objects
{
    public class Medico
    {
        int ID_MEDICO_;
        string Especialidade_;
        string Nome_;
        string CRM_;
        string telefone_;
        string observacoes_;

        public int ID_MEDICO { get => ID_MEDICO_; set => ID_MEDICO_ = value; }
        public string Especialidade { get => Especialidade_; set => Especialidade_ = value; }
        public string Nome { get => Nome_; set => Nome_ = value; }
        public string CRM { get => CRM_; set => CRM_ = value; }
        public string telefone { get => telefone_; set => telefone_ = value; }

        public void AddMedico()
        {
            string commando = "INSERT INTO medico " +
                "(ID_MEDICO, Especialidade, Nome, CRM, telefone) "+
                "VALUES ((SELECT MAX(medico.ID_MEDICO)+1 from medico), '" + this.Nome + "', " +
                "'" + this.CRM + "', '" + this.telefone + "');";

            DatabaseConnection.Instance.ExecuteCommandQuery(commando);
        }

        public void UpdateMedico()
        {
            string commando = "UPDATE medico" +
                              "SET nome = '" + this.Nome + "'," +
                              "CRM = '" + this.CRM + "'," +
                              "telefone = '" + this.telefone + "' " +
                              "WHERE ID_MEDICO = " + this.ID_MEDICO;

            DatabaseConnection.Instance.ExecuteCommandQuery(commando);
        }

        public void DeleteMedico()
        {
            string commando = "DELETE FROM medico WHERE ID_MEDICO = " + this.ID_MEDICO + ";";
            DatabaseConnection.Instance.ExecuteCommandQuery(commando);
        }

        public static List<Medico> GetMedicos()
        {
            string command = "SELECT * FROM medico";
            return DatabaseConnection.Instance.ExecuteQuery<Medico>(command);
        }
    }
}
