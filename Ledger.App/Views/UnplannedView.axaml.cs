using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Ledger.App.Views;

public partial class UnplannedView : UserControl
{
    public UnplannedView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}