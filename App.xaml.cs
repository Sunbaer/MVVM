using Microsoft.Practices.Prism.Events;
using RoundBasedGameMVVM.ViewModels;
using System.Diagnostics;
using System.Windows;

namespace RoundBasedGameMVVM
{


    public partial class App : Application
    {
        /// <summary>
        /// Raises the <see cref="System.Windows.Application.Startup"/>
        /// </summary>
        /// <param name="e">A <see cref="System.Windows.StartupEventArgs"/> that contains
        /// the event data.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            // Get the reference to the current process
            Process thisProc = Process.GetCurrentProcess();

            // Check how many processes have the same name as the current one
            if (Process.GetProcessesByName(thisProc.ProcessName).Length > 1)
            {
                MessageBox.Show("Application is already running");
                Application.Current.Shutdown();
                return;
            }

            // Init event aggregator and services
            IEventAggregator eventAggregator = new EventAggregator();

            // Init view and viewmodel
            MainWindow mainWindow = new MainWindow();
            MainViewModel mainViewModel = new MainViewModel(eventAggregator);
            mainWindow.DataContext = mainViewModel;

            mainWindow.Show();
        }
    }
}
