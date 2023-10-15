using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace UPW.Database
{
    public class Helpers
    {
        private static Random random = new Random();

        public static async Task<IUICommand> ShowMessageDialog(string message, string buttonLabel = "Ok")
        {
            MessageDialog dialog = new MessageDialog(message);
            dialog.Commands.Add(new UICommand(buttonLabel, null));
            dialog.DefaultCommandIndex = 1;
            dialog.CancelCommandIndex = 1;
            return await dialog.ShowAsync();
        }

        public static async Task<IUICommand> ConfirmCancelDialog(string message)
        {
            MessageDialog dialog = new MessageDialog(message);
            dialog.Commands.Add(new UICommand("Confirm", null));
            dialog.Commands.Add(new UICommand("Cancel", null));
            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 1;
            return await dialog.ShowAsync();
        }

        public static string GenerateRandomCod()
        {
            string command_check_responsaveis_cods;
            string command_check_enfermeiras_cods;
            string command_check_administradores_cods;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            bool is_unique = true;
            string randomGenerated;
            do
            {
                randomGenerated = new string(Enumerable.Repeat(chars, 8)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
                command_check_enfermeiras_cods = "SELECT COUNT(*) AS Quantidade FROM [dbo].[Cuidadora] where temp_cod = '" + randomGenerated +"';";
                command_check_administradores_cods  = "SELECT COUNT(*) AS Quantidade FROM [dbo].[Administradores] where temp_cod = '" + randomGenerated + "';";
                command_check_responsaveis_cods = "SELECT COUNT(*) AS Quantidade FROM [dbo].[Responsaveis] where cod_temporario = '" + randomGenerated + "';";
                if (DatabaseConnection.Instance.ReturnSingleValue(command_check_enfermeiras_cods) == 0 &&
                    DatabaseConnection.Instance.ReturnSingleValue(command_check_administradores_cods) == 0 &&
                    DatabaseConnection.Instance.ReturnSingleValue(command_check_responsaveis_cods) == 0)
                {
                    is_unique = true;
                } else
                {
                    is_unique = false;
                }
            } while (!is_unique);
            return randomGenerated;
        }

        public static string convertData(string data)
        {
            string converted = data.Substring(0,10);
            converted = data.Substring(6, 4) + "-" + data.Substring(3, 2) +"-" + data.Substring(0,2);
            return converted;
        }

        public static bool IsCpf(string cpf)
        {
            if(cpf == null)
                return false;

            int[] multiplicadores_1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicadores_2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            
            string corpoCPF;
            string digito;
            
            int soma;
            int resto;
            
            // Limpar o cpf recebido
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            
            if (cpf.Length != 11)
                return false;
            
            corpoCPF = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(corpoCPF[i].ToString()) * multiplicadores_1[i];
            
            resto = soma % 11;

            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito = resto.ToString();
            corpoCPF = corpoCPF + digito;
            soma = 0;
            
            for (int i = 0; i < 10; i++)
                soma += int.Parse(corpoCPF[i].ToString()) * multiplicadores_2[i];
            
            resto = soma % 11;

            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }
        
    }
}

