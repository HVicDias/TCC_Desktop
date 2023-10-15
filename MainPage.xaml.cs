using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using UPW.Database;
using UPW.NavigationView;
using UPW.Objects;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x416

namespace UPW
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        public bool IsLoading { get; private set; } 
        public bool Loaded { get => !IsLoading; }
        public string LoadingText { get; private set; }
        private Timer timer;
        public string Login;
        public string Password;

        public event PropertyChangedEventHandler PropertyChanged;

        void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainPage()
        {
            IsLoading = true;
            LoadingText = "Loading :)";
            this.InitializeComponent();
            timer = new Timer(FinishedLoading);
            timer.Change(2000, 2000);
        }

        private async void FinishedLoading(object state)
        {
            timer.Dispose();
            IsLoading = false;
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.
                Dispatcher.RunAsync(CoreDispatcherPriority.Normal,() =>
            {
                NotifyPropertyChanged(nameof(IsLoading));
                NotifyPropertyChanged(nameof(Loaded));
            });
            
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (Administrador.verifyAdminLogin(Login, Password))
            {
                DatabaseConnection.Instance.ID_ADMIN = Administrador.getAdminID(Login, Password);
                DatabaseConnection.Instance.ID_CUIDADORA = -1;
                this.Frame.Navigate(typeof(NavigationPage));
            }
            if (Cuidadora.verifyCuidadoraLogin(Login, Password))
            {
                DatabaseConnection.Instance.ID_ADMIN = -1;
                DatabaseConnection.Instance.ID_CUIDADORA = Cuidadora.getCuidadoraID(Login, Password);
                this.Frame.Navigate(typeof(NavigationPage));
            }
        }

        //private void MedPageMonitoringButton_Click(object sender, RoutedEventArgs e)
        //{
        //    this.Frame.Navigate(typeof(NavigationPage));
        //}

        private void UserTextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key.ToString().Equals("Enter"))
            {
                PasswordTextBox.Focus(FocusState.Programmatic);
            }
        }

        private void PasswordTextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key.ToString().Equals("Enter"))
            {
                LoginButton_Click(sender, e);
            }
        }
    }
}
