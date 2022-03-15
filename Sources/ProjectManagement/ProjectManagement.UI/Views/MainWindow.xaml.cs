using ProjectManagement.UI.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectManagement.UI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Ctor
        public MainWindow(MainViewModel mainViewModels)
        {
            InitializeComponent();
            DataContext = mainViewModels;
            Task.Run(() => LoadDataAsync(mainViewModels));
        }
        #endregion

        #region Methods
        public Task LoadDataAsync(MainViewModel mainViewModels)
        {
            try
            {
                Task.Run(mainViewModels.LoadAsync);
            }
            catch (Exception)
            {
                throw;
            }

            return Task.CompletedTask;
        }
        #endregion
    }
}
