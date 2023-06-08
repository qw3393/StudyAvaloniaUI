using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Metadata;

namespace StudyAvaloniaUI.Views;

public class MyFlyout : FlyoutBase
{
    /// <summary>
    /// Defines the <see cref="Content"/> property
    /// </summary>
    public static readonly StyledProperty<object> ContentProperty =
        AvaloniaProperty.Register<Flyout, object>(nameof(Content));

    /// <summary>
    /// Gets the Classes collection to apply to the FlyoutPresenter this Flyout is hosting
    /// </summary>
    public Classes FlyoutPresenterClasses => _classes ??= new Classes();

    private Classes? _classes;

    /// <summary>
    /// Gets or sets the content to display in this flyout
    /// </summary>
    [Content]
    public object Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }

    protected override Control CreatePresenter()
    {
        return new FlyoutPresenter
        {
            [!ContentControl.ContentProperty] = this[!ContentProperty]
        };
    }

    protected override void OnOpened()
    {
        if (_classes != null)
        {
            var presenter = Popup.Child; 
            var classes = FlyoutPresenterClasses;
            
            if(presenter is null)
            {
                return;
            }
            //Remove any classes no longer in use, ignoring pseudo classes
            for (int i = presenter.Classes.Count - 1; i >= 0; i--)
            {
                if (!classes.Contains(presenter.Classes[i]) &&
                    !presenter.Classes[i].Contains(":"))
                {
                    presenter.Classes.RemoveAt(i);
                }
            }

            //Add new classes
            presenter.Classes.AddRange(classes);
        }
        base.OnOpened();
    }
}