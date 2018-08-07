﻿// Copyright (c) Ratish Philip 
//
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal 
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is 
// furnished to do so, subject to the following conditions: 
// 
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software. 
// 
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE. 
//
// This file is part of the CompositionProToolkit project: 
// https://github.com/ratishphilip/CompositionProToolkit
//
// CompositionProToolkit v0.9.0
// 

using System;
using System.Numerics;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Media;
using CompositionProToolkit.Expressions;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.Geometry;

namespace CompositionProToolkit.Controls
{
    /// <summary>
    /// Represents a control that can be used to display profile images.
    /// </summary>
    [WebHostHidden]
    [TemplatePart(Name = "PART_Border", Type = typeof(Border))]
    public sealed class ProfileControl : Control
    {
        #region Constants

        private const double DefaultBlurRadius = 20.0;
        private const double DefaultBorderGap = 10.0;
        private const float FinalRevealScale = 1.1f;
        private static readonly TimeSpan DefaultTintDuration = TimeSpan.FromMilliseconds(200);
        private static readonly TimeSpan DefaultBlurDuration = TimeSpan.FromMilliseconds(200);
        private static readonly TimeSpan DefaultRevealDuration = TimeSpan.FromMilliseconds(1000);
        private static readonly TimeSpan DefaultFadeInDuration = TimeSpan.FromMilliseconds(20);

        #endregion

        #region Fields

        private Compositor _compositor;
        private ICompositionGenerator _generator;
        private CompositionBackdropBrush _backdropBrush;

        private SpriteVisual _bgVisual;
        private LayerVisual _clipVisual;
        private SpriteVisual _imageVisual;

        private IMaskSurface _bgMask;
        private IMaskSurface _clipMask;

        private IImageSurface _imageSurface;
        private IMaskSurface _imageMaskSurface;
        private CompositionSurfaceBrush _imageMaskSurfaceBrush;

        private CompositionEffectBrush _bgBrush;
        private KeyFrameAnimation<Color> _tintAnimation;
        private KeyFrameAnimation<float> _blurAnimation;

        private KeyFrameAnimation<Vector2> _revealAnimation;
        //private KeyFrameAnimation<Vector2> _hideAnimation;
        private readonly ImageSurfaceOptions _imageOptions;

        private KeyFrameAnimation<float> _fadeInAnimation;

        private bool _isLoaded;
        private bool _revealOnLoad;

        private Border _outerBorder;

        #endregion

        #region Dependency Properties

        #region BlurRadius

        /// <summary>
        /// BlurRadius Dependency Property
        /// </summary>
        public static readonly DependencyProperty BlurRadiusProperty =
            DependencyProperty.Register("BlurRadius", typeof(double), typeof(ProfileControl),
                new PropertyMetadata(DefaultBlurRadius, OnBlurRadiusChanged));

        /// <summary>
        /// Gets or sets the BlurRadius property. This dependency property 
        /// indicates the blur radius for the ProfileControl background.
        /// </summary>
        public double BlurRadius
        {
            get => (double)GetValue(BlurRadiusProperty);
            set => SetValue(BlurRadiusProperty, value);
        }

        /// <summary>
        /// Handles changes to the BlurRadius property.
        /// </summary>
        /// <param name="d">ProfileControl</param>
        /// <param name="e">DependencyProperty changed event arguments</param>
        private static void OnBlurRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = (ProfileControl)d;
            ctrl.OnBlurRadiusChanged();
        }

        /// <summary>
        /// Provides the class instance an opportunity to handle changes to the BlurRadius property.
        /// </summary>
        private void OnBlurRadiusChanged()
        {
            if ((_blurAnimation != null))
            {
                _bgBrush?.StartAnimation("Blur.BlurAmount", _blurAnimation);
            }
        }

        #endregion

        #region BorderGap

        /// <summary>
        /// BorderGap Dependency Property
        /// </summary>
        public static readonly DependencyProperty BorderGapProperty =
            DependencyProperty.Register("BorderGap", typeof(double), typeof(ProfileControl),
                new PropertyMetadata(DefaultBorderGap, OnBorderGapChanged));

        /// <summary>
        /// Gets or sets the BorderGap property. This dependency property 
        /// indicates the gap between the outer border and the inner border
        /// of the ProfileControl.
        /// </summary>
        public double BorderGap
        {
            get => (double)GetValue(BorderGapProperty);
            set => SetValue(BorderGapProperty, value);
        }

        /// <summary>
        /// Handles changes to the BorderGap property.
        /// </summary>
        /// <param name="d">ProfileControl</param>
        /// <param name="e">DependencyProperty changed event arguments</param>
        private static void OnBorderGapChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = (ProfileControl)d;
            ctrl.OnBorderGapChanged();
        }

        /// <summary>
        /// Provides the class instance an opportunity to handle changes to the BorderGap property.
        /// </summary>
        private void OnBorderGapChanged()
        {
            InvalidateArrange();
        }

        #endregion

        #region FluidReveal

        /// <summary>
        /// FluidReveal Dependency Property
        /// </summary>
        public static readonly DependencyProperty FluidRevealProperty =
            DependencyProperty.Register("FluidReveal", typeof(bool), typeof(ProfileControl),
                new PropertyMetadata(true));

        /// <summary>
        /// Gets or sets the FluidReveal property. This dependency property 
        /// indicates whether the reveal animation should automatically be
        /// played when the Source property of the ProfileControl changes.
        /// If set to False, the image specified by the Source property is
        /// displayed directly without any animation.
        /// </summary>
        public bool FluidReveal
        {
            get => (bool)GetValue(FluidRevealProperty);
            set => SetValue(FluidRevealProperty, value);
        }

        #endregion

        #region RevealDuration

        /// <summary>
        /// RevealDuration Dependency Property
        /// </summary>
        public static readonly DependencyProperty RevealDurationProperty =
            DependencyProperty.Register("RevealDuration", typeof(TimeSpan), typeof(ProfileControl),
                new PropertyMetadata(DefaultRevealDuration, OnRevealDurationChanged));

        /// <summary>
        /// Gets or sets the RevealDuration property. This dependency property 
        /// indicates the duration of the reveal animation.
        /// </summary>
        public TimeSpan RevealDuration
        {
            get => (TimeSpan)GetValue(RevealDurationProperty);
            set => SetValue(RevealDurationProperty, value);
        }

        /// <summary>
        /// Handles changes to the RevealDuration property.
        /// </summary>
        /// <param name="d">ProfileControl</param>
        /// <param name="e">DependencyProperty changed event arguments</param>
        private static void OnRevealDurationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = (ProfileControl)d;
            var newRevealDuration = ctrl.RevealDuration;
            ctrl.OnRevealDurationChanged(newRevealDuration);
        }

        /// <summary>
        /// Provides the class instance an opportunity to handle changes to the RevealDuration property.
        /// </summary>
        /// <param name="newRevealDuration">New Value</param>
        private void OnRevealDurationChanged(TimeSpan newRevealDuration)
        {
            if (_revealAnimation != null)
            {
                _revealAnimation.Duration = newRevealDuration;
            }
        }

        #endregion

        #region Source

        /// <summary>
        /// Source Dependency Property
        /// </summary>
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(Uri), typeof(ProfileControl),
                new PropertyMetadata(null, OnSourceChanged));

        /// <summary>
        /// Gets or sets the Source property. This dependency property 
        /// indicates the Uri of the image to be displayed in the ProfileControl.
        /// </summary>
        public Uri Source
        {
            get => (Uri)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        /// <summary>
        /// Handles changes to the Source property.
        /// </summary>
        /// <param name="d">ProfileControl</param>
        /// <param name="e">DependencyProperty changed event arguments</param>
        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = (ProfileControl)d;
            ctrl.OnSourceChanged();
        }

        /// <summary>
        /// Provides the class instance an opportunity to handle changes to the Source property.
        /// </summary>
        private async void OnSourceChanged()
        {
            if (_imageVisual != null)
            {
                if (FluidReveal)
                {
                    await RevealWithAnimation();
                }
                else
                {
                    await _imageSurface.RedrawAsync(Source, _imageVisual.Size.ToSize(),
                        _imageOptions);
                }
            }
        }

        #endregion

        #region Stretch

        /// <summary>
        /// Stretch Dependency Property
        /// </summary>
        public static readonly DependencyProperty StretchProperty =
            DependencyProperty.Register("Stretch", typeof(Stretch), typeof(ProfileControl),
                new PropertyMetadata(Stretch.Uniform, OnStretchChanged));

        /// <summary>
        /// Gets or sets the Stretch property. This dependency property 
        /// indicates how the content is resized to fill its allocated space.
        /// </summary>
        public Stretch Stretch
        {
            get => (Stretch)GetValue(StretchProperty);
            set => SetValue(StretchProperty, value);
        }

        /// <summary>
        /// Handles changes to the Stretch property.
        /// </summary>
        /// <param name="d">ProfileControl</param>
        /// <param name="e">DependencyProperty changed event arguments</param>
        private static void OnStretchChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = (ProfileControl)d;
            var newStretch = ctrl.Stretch;
            ctrl.OnStretchChanged(newStretch);
        }

        /// <summary>
        /// Provides the class instance an opportunity to handle changes to the Stretch property.
        /// </summary>
        /// <param name="newStretch">New Value</param>
        private void OnStretchChanged(Stretch newStretch)
        {
            _imageOptions.Stretch = newStretch;
            if (_imageVisual != null)
            {
                InvalidateArrange();
            }
        }

        #endregion

        #region Tint

        /// <summary>
        /// Tint Dependency Property
        /// </summary>
        public static readonly DependencyProperty TintProperty =
            DependencyProperty.Register("Tint", typeof(Color), typeof(ProfileControl),
                new PropertyMetadata(Colors.White, OnTintChanged));

        /// <summary>
        /// Gets or sets the Tint property. This dependency property 
        /// indicates the color overlay on the background of the ProfileControl.
        /// </summary>
        public Color Tint
        {
            get => (Color)GetValue(TintProperty);
            set => SetValue(TintProperty, value);
        }

        /// <summary>
        /// Handles changes to the Tint property.
        /// </summary>
        /// <param name="d">ProfileControl</param>
        /// <param name="e">DependencyProperty changed event arguments</param>
        private static void OnTintChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = (ProfileControl)d;
            ctrl.OnTintChanged();
        }

        /// <summary>
        /// Provides the class instance an opportunity to handle changes to the Tint property.
        /// </summary>
        private void OnTintChanged()
        {
            if ((_tintAnimation != null))
            {
                _bgBrush?.StartAnimation("Color.Color", _tintAnimation);
            }
        }

        #endregion

        #endregion

        #region Construction / Initialization

        /// <summary>
        /// ctor
        /// </summary>
        public ProfileControl()
        {
            _imageOptions = ImageSurfaceOptions.Default;
            _imageOptions.AutoResize = false;

            _revealOnLoad = false;
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Handler method when the Template is applied to the ProfileControl
        /// </summary>
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _outerBorder = GetTemplateChild("PART_Border") as Border;
        }

        /// <summary>
        /// Handler method for the Arrange Layout phase.
        /// </summary>
        /// <param name="finalSize">Final size of the control.</param>
        /// <returns>Size</returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            var size = base.ArrangeOverride(finalSize);

            // Create/Arrange composition elements
            ArrangeComposition(finalSize);

            return size;
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handler method which is invoked when the ProfileControl is loaded.
        /// </summary>
        /// <param name="sender">ProfileControl</param>
        /// <param name="e">RoutedEventArgs</param>
        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            _isLoaded = true;
            if ((_revealOnLoad) && (_imageVisual != null))
            {
                _revealOnLoad = false;
                // Invoke the reveal animation
                await RevealWithAnimation();
            }
        }

        /// <summary>
        /// Handler method which is invoked when the ProfileControl is unloaded.
        /// </summary>
        /// <param name="sender">ProfileControl</param>
        /// <param name="e">RoutedEventArgs</param>
        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            _isLoaded = false;
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Creates and arranges the Composition elements of the
        /// ProfileControl.
        /// </summary>
        /// <param name="finalSize">Size of the ProfileControl</param>
        private async void ArrangeComposition(Size finalSize)
        {
            // If compositor is null, it means none of the visuals
            // have been initialized yet.
            if (_compositor == null)
            {
                await InitComposition();
            }

            // Update the visuals based on the current control Size and Padding
            var padding = Padding.CollapseThickness();
            var outerWidth = (float)Math.Max(0, finalSize.Width - padding.Width);
            var outerHeight = (float)Math.Max(0, finalSize.Height - padding.Height);
            var offsetX = (float)((finalSize.Width - outerWidth) / 2f);
            var offsetY = (float)((finalSize.Height - outerHeight) / 2f);
            var outerSize = new Vector2(outerWidth, outerHeight);

            var gap = new Vector2((float)BorderGap);
            var innerWidth = Math.Max(0, outerWidth - gap.X * 2f);
            var innerHeight = Math.Max(0, outerHeight - gap.Y * 2f);
            var innerOffset = new Vector3(gap, 0);
            var innerSize = new Vector2(innerWidth, innerHeight);

            // Background Visual
            _bgVisual.Size = outerSize;
            var bgGeometry = CanvasGeometry.CreateEllipse(_generator.Device,
                offsetX + outerWidth / 2f,
                offsetY + outerHeight / 2f,
                outerWidth / 2f,
                outerHeight / 2f);
            _bgMask.Redraw(outerSize.ToSize(), bgGeometry);
            _bgVisual.Offset = new Vector3(offsetX, offsetY, 0);

            // Clip LayerVisual
            _clipVisual.Size = innerSize;
            _clipVisual.Offset = innerOffset;
            var clipGeometry = CanvasGeometry.CreateEllipse(_generator.Device,
                innerWidth / 2f,
                innerHeight / 2f,
                innerWidth / 2f,
                innerHeight / 2f);
            _clipMask.Redraw(innerSize.ToSize(), clipGeometry);

            // Image Visual
            _imageVisual.Size = innerSize;

            await _imageSurface.RedrawAsync(Source, _imageVisual.Size.ToSize(), _imageOptions);
            _imageMaskSurface.Redraw(innerSize.ToSize(), clipGeometry);
            _imageMaskSurfaceBrush.CenterPoint = innerSize * 0.5f;

            // Adjust the corner radius of the border so that the hit area
            // of the ProfileControl is correct.
            if (_outerBorder != null)
            {
                // Setting the corner radius to the largest side length will ensure an elliptical
                // shape if the adjacent sides are unequal otherwise it will be a circular shape.
                _outerBorder.CornerRadius = new CornerRadius(Math.Max(outerWidth, outerHeight));
            }
        }

        /// <summary>
        /// Initializes the Composition components of the ProfileControl
        /// </summary>
        /// <returns>Task</returns>
        private async Task InitComposition()
        {
            _compositor = Window.Current.Compositor;
            _generator = _compositor.CreateCompositionGenerator();
            _backdropBrush = _compositor.CreateBackdropBrush();

            // Background
            _bgVisual = _compositor.CreateSpriteVisual();
            _clipVisual = _compositor.CreateLayerVisual();
            _imageVisual = _compositor.CreateSpriteVisual();

            _bgMask = _generator.CreateMaskSurface(Vector2.Zero.ToSize(), null);
            _bgBrush = _compositor.CreateMaskedBackdropBrush(_bgMask, Tint, (float)BlurRadius, _backdropBrush);
            _bgVisual.Brush = _bgBrush;

            // Clip Layer
            _clipMask = _generator.CreateMaskSurface(Vector2.Zero.ToSize(), null);
            var clipEffect = new CompositeEffect
            {
                Mode = CanvasComposite.DestinationIn,
                Sources =
                {
                    new CompositionEffectSourceParameter("source"),
                    new CompositionEffectSourceParameter("clipMask")
                }

            };

            // Create Effect Factory
            var factory = _compositor.CreateEffectFactory(clipEffect, null);
            // Create Effect Brush
            var clipBrush = factory.CreateBrush();
            clipBrush.SetSourceParameter("clipMask", _compositor.CreateSurfaceBrush(_clipMask));

            _clipVisual.Effect = clipBrush;
            _bgVisual.Children.InsertAtTop(_clipVisual);

            // Image Visual
            _imageVisual = _compositor.CreateSpriteVisual();
            _clipVisual.Children.InsertAtTop(_imageVisual);

            var autoReveal = FluidReveal;
            _revealOnLoad = (!_isLoaded) && (Source != null) && autoReveal;

            // If autoReveal is enabled load the image onto the surface only after the control is loaded
            _imageSurface =
                await _generator.CreateImageSurfaceAsync(autoReveal ? null : Source, Vector2.Zero.ToSize(), _imageOptions);
            _imageMaskSurface = _generator.CreateMaskSurface(Vector2.Zero.ToSize(), null);
            _imageMaskSurfaceBrush = _compositor.CreateSurfaceBrush(_imageMaskSurface);

            var maskBrush = _compositor.CreateMaskBrush();
            maskBrush.Source = _compositor.CreateSurfaceBrush(_imageSurface);
            maskBrush.Mask = _imageMaskSurfaceBrush;
            _imageVisual.Brush = maskBrush;

            // 
            // Animations
            //
            var easing = _compositor.CreateLinearEasingFunction();

            // Tint
            _tintAnimation = _compositor.GenerateColorKeyFrameAnimation().HavingDuration(DefaultTintDuration);
            _tintAnimation.InsertFinalValueKeyFrame(1f, easing);

            // Blur
            _blurAnimation = _compositor.GenerateScalarKeyFrameAnimation().HavingDuration(DefaultBlurDuration);
            _blurAnimation.InsertFinalValueKeyFrame(1f, easing);

            // Reveal
            _revealAnimation = _compositor.GenerateVector2KeyFrameAnimation()
                .HavingDuration(RevealDuration);

            _revealAnimation.InsertKeyFrames(new KeyFrame<Vector2>(0f, new Vector2(0)),
                new KeyFrame<Vector2>(1f, new Vector2(FinalRevealScale), _compositor.CreateEaseOutQuinticEasingFunction()));

            // Fade In
            _fadeInAnimation = _compositor.GenerateScalarKeyFrameAnimation()
                .HavingDuration(DefaultFadeInDuration);

            _fadeInAnimation.InsertKeyFrames(new KeyFrame<float>(0f, 0f),
                new KeyFrame<float>(1f, 1f));

            //_hideAnimation = _compositor.GenerateVector2KeyFrameAnimation()
            //    .HavingDuration(TimeSpan.FromMilliseconds(750));

            //_hideAnimation.InsertKeyFrames(new KeyFrame<Vector2>(0f, new Vector2(1.2f)),
            //    new KeyFrame<Vector2>(1f, new Vector2(0f), _compositor.CreateEaseOutQuinticEasingFunction()));

            ElementCompositionPreview.SetElementChildVisual(this, _bgVisual);
        }

        /// <summary>
        /// Displays the current image using a reveal animation
        /// </summary>
        /// <returns>Task</returns>
        private async Task RevealWithAnimation()
        {
            _imageVisual.Opacity = 0;
            await _imageSurface.RedrawAsync(Source, _imageVisual.Size.ToSize(),
                _imageOptions);

            _imageVisual.StartAnimation(() => _imageVisual.Opacity, _fadeInAnimation);
            _imageMaskSurfaceBrush.StartAnimation(() => _imageMaskSurfaceBrush.ScaleXY(), _revealAnimation);
        }

        #endregion
    }
}
