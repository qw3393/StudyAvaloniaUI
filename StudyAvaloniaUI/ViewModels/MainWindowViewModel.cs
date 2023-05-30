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
}