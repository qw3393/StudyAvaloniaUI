using System;
using System.Diagnostics;
using System.Threading;
using System.Timers;
using Avalonia;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Avalonia.Threading;
using Timer = System.Threading.Timer;

namespace StudyAvaloniaUI.Views;

public class AnimatedPopup : ContentControl
{
    #region Private Members

    /// <summary>
    /// The speed of the animation in FPS
    /// </summary>
    private TimeSpan mFramerate = TimeSpan.FromSeconds(1 / 60.0);

    
    // Calculate total ticks that make up the animation time
    private int mTotalTicks => (int)(_animationTime.TotalSeconds / mFramerate.TotalSeconds);
    /// <summary>
    /// Store the controls desired size
    /// </summary>

    private Size mDesiredSize;

    /// <summary>
    /// A flag for when we are animating
    /// </summary>

    private bool mAnimating;

    /// <summary>
    /// Keeps track of if we have found the desired 100% width/height auto size
    /// </summary>
    private bool mSizeFound;

    /// <summary>
    /// The animation UI timer
    /// </summary>
    private DispatcherTimer mAnimationTimer;

    /// <summary>
    /// The timeout timer to detect when auto-sizing has finished firing
    /// </summary>
    private Timer mSizingTimer;

    /// <summary>
    /// The current position in the animation
    /// </summary>
    private int mAnimationCurrentTick;

    /// <summary>
    /// Sets whether the control should be opening or closing
    /// </summary>
    private bool mOpen = false;
    
    #endregion

    #region Public Properties

    /// <summary>
    /// Indicates if the control is currently opened
    /// </summary>
    public bool IsOpened => mAnimationCurrentTick >= mTotalTicks;
    
    #region Animation Time
    
    private TimeSpan _animationTime = TimeSpan.FromSeconds(3);

    public static readonly DirectProperty<AnimatedPopup, TimeSpan> AnimationTimeProperty = AvaloniaProperty.RegisterDirect<AnimatedPopup, TimeSpan>(
        nameof(AnimationTime), o => o.AnimationTime, (o, v) => o.AnimationTime = v);

    public TimeSpan AnimationTime
    {
        get => _animationTime;
        set => SetAndRaise(AnimationTimeProperty, ref _animationTime, value);
    }

    #endregion
    
    #endregion

    #region Public Commands
    
    public void Open()
    {
        mOpen = true;
        
        // Update animation
        UpdateAnimation();
    }
    
    public void Close()
    {
        mOpen = false;
        
        // Update animation
        UpdateAnimation();
    }
    #endregion
    
    #region Constructor
    
    /// <summary>
    /// Default constructor
    /// </summary>
    public AnimatedPopup()
    {
        // Make a new dispatch timer
        mAnimationTimer = new DispatcherTimer()
        {
            // Set the timer to run 60 times a second
            Interval = mFramerate
        };

        mSizingTimer = new Timer((t)=>
        {
            // If we have already calculated the size...
            if (mSizeFound)
                // No longer accept new sizes
                return;
            
            // We have noew found our desired size
            mSizeFound = true;

            Dispatcher.UIThread.InvokeAsync(() =>
            {
                // Set the desired size
                mDesiredSize = DesiredSize - Margin;
                
                // Update animation
                UpdateAnimation();
            });

        });
        
        // Callback on every tick
        mAnimationTimer.Tick += (s, e) => AnimationTick();

    }
    
    #endregion

    #region  Private Methods

    private void UpdateAnimation()
    {
        // Do noting if we still haven't found our initial size
        if (mSizeFound)
            return;
        
        // Start the animation thread again
        mAnimationTimer.Start();
    }
    
    private void AnimationTick()
    {
        // Increment the tick
        mAnimationCurrentTick++;

        // Set animating flag
        mAnimating = true;

        // If we have reached the total ticks...
        if (mAnimationCurrentTick > mTotalTicks)
        {
            // Stop this animation Timer
            mAnimationTimer.Stop();

            // Clear animating flag
            mAnimating = false;

            // Vraek out of code
            return;
        }

        // Get percentage of the way through the current animation
        var percentageAnimated = (float)mAnimationCurrentTick / mTotalTicks;

        // Make an animation easing
        var easing = new QuadraticEaseIn();


        // Calculate final width and height
        var finalWidth = mDesiredSize.Width * easing.Ease(percentageAnimated);
        var finalHeight = mDesiredSize.Height * easing.Ease(percentageAnimated);

        // Do our animation
        Width = finalWidth;
        Height = finalHeight;

        Debug.WriteLine($"Current tick: {mAnimationCurrentTick}");
    }

    #endregion
    
    
    public override void Render(DrawingContext context)
    {
        // If we have not yet found the desired size...
        if (!mSizeFound)
        {
            
        }
        
        base.Render(context);
    }
}