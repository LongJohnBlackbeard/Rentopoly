using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Rentopoly.Rentals;

/// <summary>
/// Interaction logic for ViewRentalsView.xaml
/// </summary>
public partial class ViewRentalsView : UserControl
{
    public ViewRentalsView()
    {
        InitializeComponent();
    }

    private async void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is ViewRentalsViewModel viewModel)
        {
            await viewModel.RefreshCommand.ExecuteAsync(null);
        }
    }
}

