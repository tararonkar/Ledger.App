using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Ledger.App.Views;

public partial class PlannedView : UserControl
{
    public PlannedView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}