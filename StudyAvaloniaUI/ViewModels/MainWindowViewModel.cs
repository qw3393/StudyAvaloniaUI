using ReactiveUI;

namespace StudyAvaloniaUI.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private string _boldTitle = "AVALONIA";

    public string BoldTitle
    {
        get => _boldTitle;
        set => this.RaiseAndSetIfChanged(ref _boldTitle, value);
    }
    
    private string _regularTitle = "LOUDNESS METER";
    public string RegularTitle
    {
        get => _regularTitle;
        set => this.RaiseAndSetIfChanged(ref _regularTitle, value);
    }

    private bool _channelConfigurationListIsOpen = false;

    public bool ChannelConfigurationListIsOpen
    {
        get => _channelConfigurationListIsOpen;
        set => this.RaiseAndSetIfChanged(ref _channelConfigurationListIsOpen, value);
    }
    public void ChannelConfigurationButtonPressedCommand() => ChannelConfigurationListIsOpen ^= true;
}