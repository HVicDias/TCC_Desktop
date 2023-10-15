using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using UPW.Database;
using UPW.NavigationView;
using UPW.Objects;
using UPW.Pages.Pacientes;
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

namespace UPW.Pages
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class EldersMenuPage : Page, INotifyPropertyChanged
    {
        public Idoso idoso = null;
        public bool isNotAdminView = DatabaseConnection.Instance.ID_ADMIN != -1;
        public bool isIdosoSelected {  get; set; } = false;
        public bool ShouldShowAtualizar {
            get { return isNotAdminView && isIdosoSelected; } 
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public EldersMenuPage()
        {
            this.InitializeComponent();
            idoso = null;
            List<Idoso> idosos = Idoso.GetIdosos();
            foreach(Idoso idoso in idosos)
            {
                ComboSelecione.Items.Add(idoso.Nome);
            }
        }

        private void AtualizarCadastro_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AtualizacaoDePacientePage));
        }

        private void VisualizarResidente_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ConsultarPacientePage));
        }

        private void CadastrarResidente_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CadastroPacientePage));
        }

        private void ComboSelecione_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ComboSelecione.SelectedItem != null)
            {
                isIdosoSelected = true;
            }
            else
            {
                isIdosoSelected = false;
            }
            NotifyPropertyChanged(nameof(isIdosoSelected));
            NotifyPropertyChanged(nameof(ShouldShowAtualizar));

        }
    }
}
