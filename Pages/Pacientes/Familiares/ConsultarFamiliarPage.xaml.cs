using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UPW.NavigationView;
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
    public sealed partial class ConsultarFamiliarPage : Page
    {
        public string Nome;
        public string RG;
        public string CPF;
        public string Telefone;
        public string Email;
        public string cod_temporario;

        public ConsultarFamiliarPage()
        {
            this.InitializeComponent();
        }

        private void Finalizar_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(EldersMenuPage));
        }
    }
}
