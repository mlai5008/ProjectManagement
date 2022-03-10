using ProjectManagement.UI.ViewModels;
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
            mainViewModels.Load();
        } 
        #endregion
    }
}
