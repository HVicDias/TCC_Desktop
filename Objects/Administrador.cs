using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPW.Database;
using Windows.UI.Xaml.Controls.Primitives;

namespace UPW.Objects
{
    public class Administrador
    {
        private int ID_ADMIN_;
        private string login_;
        private string senha_;
        private string telegram_id_;

        public int ID_ADMIN { get => ID_ADMIN_; set => ID_ADMIN_ = value; }
        public string login { get => login_; set => login_ = value; }
        public string senha { get => senha_; set => senha_ = value; }
        public string telegram_id { get => telegram_id_; set => telegram_id_ = value; }
    
        public static bool verifyAdminLogin(string login, string senha)
        {
            string command = "SELECT COUNT(*) AS Quantidade FROM [dbo].[Administradores] " +
                             "WHERE login = '"+login+"' AND senha = '"+senha+"';";

            return DatabaseConnection.Instance.VerifyLogin(command);
        }

        public static int getAdminID(string login, string senha)
        {
            string command = "SELECT ID_ADMIN AS Quantidade FROM [dbo].[Administradores] " +
                             "WHERE login = '" + login + "' AND senha = '" + senha + "';";

            return DatabaseConnection.Instance.ReturnSingleValue(command);
        }
    }
}
