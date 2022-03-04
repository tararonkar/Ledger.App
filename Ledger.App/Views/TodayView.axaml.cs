using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Ledger.App.Views;

public partial class TodayView : UserControl
{
    public TodayView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}