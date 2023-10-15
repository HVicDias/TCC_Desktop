using Microsoft.Data.SqlClient.DataClassification;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UPW.Database;
using UPW.NavigationView;
using UPW.Objects;
using UPW.Pages.Pacientes.Familiares;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace UPW.Pages.Pacientes
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class CadastroPacientePage : Page
    {
        public string Nome;
        public string DataDeNascimento;
        public string Convenio;
        public string RG;
        public string CPF;
        public string SUS;
        public string Sexo;
        public string Observacoes;
        public string Alergias;
        //private Idoso idoso;
        public CadastroPacientePage()
        {
            this.InitializeComponent();
        }

        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    //idoso = JsonConvert.DeserializeObject<Idoso>((string)e.Parameter);

        //    base.OnNavigatedTo(e);
        //}

        private async void CadastrarResponsável_Click(object sender, RoutedEventArgs e)
        {
            Idoso idoso = new Idoso();
            if (Nome != null && Nome?.Count() >= 1)
            {
                idoso.Nome = Nome;
            }
            else
            {
                await Helpers.ShowMessageDialog("Nome Invalido");
                return;
            }

            if (RG != null && RG?.Count() >= 5)
            {
                idoso.RG = RG;
            }
            else
            {
                await Helpers.ShowMessageDialog("RG Invalido");
                return;
            }

            if (Helpers.IsCpf(CPF))
            {
                idoso.CPF = CPF;
            }
            else
            {
                await Helpers.ShowMessageDialog("CPF Invalido");
                return;
            }

            if (DataPicker.Date.HasValue)
            {
                idoso.data_de_nascimento = DataPicker.Date.ToString();
            }
            else
            {
                await Helpers.ShowMessageDialog("Data de Aniversario Invalida");
                return;
            }

            if (RadioFeminino.IsChecked == true)
            {
                idoso.Sexo = Sexo = "Feminino";
            }
            else if (RadioMasculino.IsChecked == true)
            {
                idoso.Sexo = Sexo = "Masculino";
            }
            else
            {
                await Helpers.ShowMessageDialog("Gênero Invalido");
                return;
            }

            if (Convenio != null || Convenio?.Count() >= 1)
            {
                idoso.Convenio = Convenio;
            }

            if (SUS != "" || SUS.Count() <= 1)
            {
                idoso.SUS = SUS;
            }


            if (Observacoes != null || Observacoes?.Count() >= 1)
            {
                idoso.Observacoes = Observacoes;
            }

            if (Alergias != null || Alergias?.Count() >= 1)
            {
                idoso.Alergias = Alergias;
            }

            string confirmIdosoSubmission = "Confirme os dados inseridos\n\n" +
                                            "Nome: " + idoso.Nome + "\n" +
                                            "Data de nascimento: " + idoso.data_de_nascimento.ToString().Substring(0,10) + "\n" +
                                            "Convenio: " + idoso.Convenio + "\n" +
                                            "RG: " + idoso.RG + "\n" +
                                            "CPF: " + idoso.CPF + "\n" +
                                            "SUS: " + idoso.SUS + "\n" +
                                            "Gênero: " +idoso.Sexo + "\n" +
                                            "Observacoes: " + idoso.Observacoes + "\n" +
                                            "Alergias: " + idoso.Alergias;

            var cmd = await Helpers.ConfirmCancelDialog(confirmIdosoSubmission);


            if (cmd.Label.Equals("Confirm"))
            {
                idoso.fk_Administradores_ID_ADMIN =  DatabaseConnection.Instance.ID_ADMIN;
                idoso.AddPaciente();
                string Param = JsonConvert.SerializeObject(idoso);
                this.Frame.Navigate(typeof(CadastrarFamiliarPage), Param);
            }
        }
    }
}
