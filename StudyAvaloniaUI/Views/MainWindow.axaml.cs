using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using StudyAvaloniaUI.ViewModels;

namespace StudyAvaloniaUI.Views;
 
public partial class MainWindow : Window
{
    #region Private Members

    private Control mChannelConfigPopup;
    private Control mChannelConfigButton;
    private Control mMainGrid;

    #endregion

    #region Constructor
    public MainWindow()
    {
        InitializeComponent();

        mChannelConfigButton = this.FindControl<Control>("ChannelConfigurationButton") ?? throw new Exception("Cannot find Channel Configuration Button by name");
        mChannelConfigPopup = this.FindControl<Control>("ChannelConfigurationPopup") ?? throw new Exception("Cannot find Channel Configuration Popup by name");
        mMainGrid = this.FindControl<Control>("MainGrid") ?? throw new Exception("Cannot find Channel main Grid");
    }
    #endregion
    
    public override void Render(DrawingContext context)
    {
        base.Render(context);

        // Get relative position of button, in relation to main grid
        var position = mChannelConfigButton.TranslatePoint(new Point(), mMainGrid) ??
                       throw new Exception("Cannot get TranslatePoint from Configuration Button");
        
        // Set margin of popup so it appears bottom left of button
        mChannelConfigPopup.Margin = new Thickness(
            position.X,
            0,
            0,
            mMainGrid.Bounds.Height - position.Y - mChannelConfigButton.Bounds.Height);
    }

    private void InputElement_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (DataContext != null) ((MainWindowViewModel)DataContext).ChannelConfigurationButtonPressedCommand();
    }
}