using Helix.Client.ViewModels;
using System.Windows;

namespace Helix.Client;

/// <summary>
/// Represents the application class that is responsible for handling the general application lifecycle and startup configuration.
/// Typically inherits from the Application base class and contains the initialization code for the app.
/// </summary>
public partial class App : Application
{
    /// <summary>
    /// Overrides the OnStartup method to initialize and display three instances of MainWindow,
    /// each with its own ChatViewModel as the DataContext.
    /// </summary>
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var mainWindow1 = new MainWindow();
        var viewModel1 = new ChatViewModel();
        mainWindow1.DataContext = viewModel1;
        mainWindow1.Show();

        var mainWindow2 = new MainWindow();
        var viewModel2 = new ChatViewModel();
        mainWindow2.DataContext = viewModel2;
        mainWindow2.Show();

        var mainWindow3 = new MainWindow();
        var viewModel3 = new ChatViewModel();
        mainWindow3.DataContext = viewModel3;
        mainWindow3.Show();
    }
}