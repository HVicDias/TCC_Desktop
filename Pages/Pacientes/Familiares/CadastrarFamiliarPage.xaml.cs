using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using UPW.Database;
using UPW.NavigationView;
using UPW.Objects;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace UPW.Pages.Pacientes.Familiares
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class CadastrarFamiliarPage : Page
    {
        public string Nome;
        public string RG;
        public string CPF;
        public string Telefone;
        public string Email;
        public string cod_temporario;
        public bool ShouldShowRemedios = true;

        private Idoso idoso;

        public CadastrarFamiliarPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            idoso = JsonConvert.DeserializeObject<Idoso>((string)e.Parameter);

            base.OnNavigatedTo(e);
        }

        public async Task<Responsavel> VerificarCampos()
        {
            Responsavel responsavel = new Responsavel();

            if (Nome != null && Nome?.Count() >= 1)
            {
                responsavel.nome = Nome;
            }
            else
            {
                await Helpers.ShowMessageDialog("Nome Invalido");
                return null;
            }

            if (RG != null && RG?.Count() >= 5)
            {
                responsavel.RG = RG;
            }
            else
            {
                await Helpers.ShowMessageDialog("RG Invalido");
                return null;
            }

            if (Helpers.IsCpf(CPF))
            {
                responsavel.CPF = CPF;
            }
            else
            {
                await Helpers.ShowMessageDialog("CPF Invalido");
                return null;
            }

            if (Telefone != null && Telefone?.Count() >= 5)
            {
                responsavel.telefone = Telefone;
            }
            else
            {
                await Helpers.ShowMessageDialog("Telefone Invalido");
                return null;
            }

            if (Email != null && Email?.Count() >= 5)
            {
                responsavel.email = Email;
            }
            else
            {
                await Helpers.ShowMessageDialog("Email Invalido");
                return null;
            }

            if (ParentescoBox.Text != null && ParentescoBox.Text?.Count() >= 2)
            {
                responsavel.parentesco = ParentescoBox.Text;
            }
            else
            {
                await Helpers.ShowMessageDialog("Parentesco Inválido");
                return null;
            }

            responsavel.cod_temporario = Helpers.GenerateRandomCod();

            return responsavel;
        } 

        private async void Finalizar_Click(object sender, RoutedEventArgs e)
        {
            Responsavel responsavel = await VerificarCampos();
            if (responsavel != null)
            {
                string confirmIdosoSubmission = "Confirme os dados inseridos\n\n" +
                                                "Nome: " + responsavel.nome + "\n" +
                                                "RG: " + responsavel.RG + "\n" +
                                                "CPF: " + responsavel.CPF + "\n" +
                                                "Telefone: " + responsavel.telefone + "\n" +
                                                "Parentesco: " + responsavel.parentesco + "\n" +
                                                "Email: " + responsavel.email;

                var cmd = await Helpers.ConfirmCancelDialog(confirmIdosoSubmission);


                if (cmd.Label.Equals("Confirm"))
                {
                    responsavel.AddResponsavel(idoso.ID_PACIENTE);
                    await Helpers.ShowMessageDialog("The Telegram code generated is: " + responsavel.cod_temporario);
                    this.Frame.Navigate(typeof(EldersMenuPage));
                }
            }
        }

        private async void CadastrarRemedios_Click(object sender, RoutedEventArgs e)
        {
            Responsavel responsavel = await VerificarCampos();
            if (responsavel != null)
            {
                string confirmIdosoSubmission = "Confirme os dados inseridos\n\n" +
                                                "Nome: " + responsavel.nome + "\n" +
                                                "RG: " + responsavel.RG + "\n" +
                                                "CPF: " + responsavel.CPF + "\n" +
                                                "SUS: " + responsavel.telefone + "\n" +
                                                "Gênero: " + responsavel.email;

                var cmd = await Helpers.ConfirmCancelDialog(confirmIdosoSubmission);


                if (cmd.Label.Equals("Confirm"))
                {
                    
                    await Helpers.ShowMessageDialog("The Telegram code generated is: " + responsavel.cod_temporario);
                    this.Frame.Navigate(typeof(CadastroDePrescricaoPage));
                }
            }
        }

        private async void CadastrarResponsavel_Click(object sender, RoutedEventArgs e)
        {
            Responsavel responsavel = await VerificarCampos();
            if (responsavel != null)
            {
                string confirmIdosoSubmission = "Confirme os dados inseridos\n\n" +
                                                "Nome: " + responsavel.nome + "\n" +
                                                "RG: " + responsavel.RG + "\n" +
                                                "CPF: " + responsavel.CPF + "\n" +
                                                "SUS: " + responsavel.telefone + "\n" +
                                                "Gênero: " + responsavel.email;

                var cmd = await Helpers.ConfirmCancelDialog(confirmIdosoSubmission);


                if (cmd.Label.Equals("Confirm"))
                {
                    await Helpers.ShowMessageDialog("The Telegram code generated is: " + responsavel.cod_temporario);
                    this.Frame.Navigate(typeof(CadastrarFamiliarPage));
                }
            }
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(EldersMenuPage));
        }
    }
}
