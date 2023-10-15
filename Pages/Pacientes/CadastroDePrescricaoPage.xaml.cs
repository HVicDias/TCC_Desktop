using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

namespace UPW.Pages.Pacientes
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class CadastroDePrescricaoPage : Page
    {
        bool wasCadastrarChecked = false;
        bool wasContinuoChecked = false;
        bool wasControlChecked = false;

        public CadastroDePrescricaoPage()
        {
            this.InitializeComponent();
        }

        private void CadastrarMed_Click(object sender, RoutedEventArgs e)
        {
            if (wasCadastrarChecked)
            {
                CadastrarMed.IsChecked = false;
                wasCadastrarChecked = false;
            }
            else
            {
                wasCadastrarChecked = true;
            }
        }

        private void RadioContinuo_Click(object sender, RoutedEventArgs e)
        {
            if (wasContinuoChecked)
            {
                RadioContinuo.IsChecked = false;
                wasContinuoChecked = false;
            }
            else
            {
                wasContinuoChecked = true;
            }
        }

        private void RadioControl_Click(object sender, RoutedEventArgs e)
        {
            if (wasControlChecked)
            {
                RadioControl.IsChecked = false;
                wasControlChecked = false;
            }
            else
            {
                wasControlChecked = true;
            }
        }
    }
}
