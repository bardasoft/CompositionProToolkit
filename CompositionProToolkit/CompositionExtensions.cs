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
// CompositionProToolkit v0.9.5
// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using CompositionProToolkit.Expressions;

namespace CompositionProToolkit
{
    /// <summary>
    /// Extension Methods for Compositor
    /// </summary>
    public static class CompositionExtensions
    {
        /// <summary>
        /// Creates a custom shaped Effect Brush using BackdropBrush and a Mask
        /// </summary>
        /// <param name="compositor">Compositor</param>
        /// <param name="mask">IMaskSurface</param>
        /// <param name="blendColor">Color to blend in the BackdropBrush</param>
        /// <param name="blurAmount">Blur Amount of the Backdrop Brush</param>
        /// <param name="backdropBrush">Backdrop Brush (optional). If not provided, then compositor creates it.</param>
        /// <returns>CompositionEffectBrush</returns>
        public static CompositionEffectBrush CreateMaskedBackdropBrush(this Compositor compositor, IMaskSurface mask,
            Color blendColor, float blurAmount, CompositionBackdropBrush backdropBrush = null)
        {
            // Blur Effect
            var blurEffect = new GaussianBlurEffect()
            {
                Name = "Blur",
                BlurAmount = blurAmount,
                BorderMode = EffectBorderMode.Hard,
                Optimization = EffectOptimization.Balanced,
                Source = new CompositionEffectSourceParameter("backdrop"),
            };

            // Blend Effect
            var blendEffect = new BlendEffect
            {
                Foreground = new ColorSourceEffect
                {
                    Name = "Color",
                    Color = blendColor
                },
                Background = blurEffect,
                Mode = BlendEffectMode.Multiply
            };

            // Composite Effect
            var effect = new CompositeEffect
            {
                Mode = CanvasComposite.DestinationIn,
                Sources =
                {
                    blendEffect,
                    new CompositionEffectSourceParameter("mask")
                }

            };

            // Create Effect Factory
            var factory = compositor.CreateEffectFactory(effect, new[] { "Blur.BlurAmount", "Color.Color" });
            // Create Effect Brush
            var brush = factory.CreateBrush();

            // Set the BackDropBrush
            // If no backdrop brush is provided, create one
            brush.SetSourceParameter("backdrop", backdropBrush ?? compositor.CreateBackdropBrush());

            // Set the Mask
            // Create SurfaceBrush from IMaskSurface
            var maskBrush = compositor.CreateSurfaceBrush(mask.Surface);
            brush.SetSourceParameter("mask", maskBrush);

            return brush;
        }

        /// <summary>
        /// Creates a custom shaped Frosted Glass Effect Brush using BackdropBrush and a Mask
        /// </summary>
        /// <param name="compositor">Compositor</param>
        /// <param name="mask">IMaskSurface</param>
        /// <param name="blendColor">Color to blend in the BackdropBrush</param>
        /// <param name="blurAmount">Blur Amount of the Backdrop Brush</param>
        /// <param name="backdropBrush">Backdrop Brush (optional). If not provided, then compositor creates it.</param>
        /// <param name="multiplyAmount">MultiplyAmount of the ArithmeticCompositeEffect</param>
        /// <param name="colorAmount">Source1Amount of the ArithmeticCompositeEffect</param>
        /// <param name="backdropAmount">Source2Amount of the ArithmeticCompositeEffect</param>
        /// <returns>CompositionEffectBrush</returns>
        public static CompositionEffectBrush CreateFrostedGlassBrush(this Compositor compositor, IMaskSurface mask,
            Color blendColor, float blurAmount, CompositionBackdropBrush backdropBrush = null,
            float multiplyAmount = 0, float colorAmount = 0.5f, float backdropAmount = 0.5f)
        {
            // Create a frosty glass effect 
            var frostEffect = new GaussianBlurEffect
            {
                Name = "Blur",
                BlurAmount = blurAmount,
                BorderMode = EffectBorderMode.Hard,
                Source = new ArithmeticCompositeEffect
                {
                    Name = "Source",
                    MultiplyAmount = multiplyAmount,
                    Source1Amount = backdropAmount,
                    Source2Amount = colorAmount,
                    Source1 = new CompositionEffectSourceParameter("backdrop"),
                    Source2 = new ColorSourceEffect
                    {
                        Name = "BlendColor",
                        Color = blendColor
                    }
                }
            };

            // Composite Effect
            var effect = new CompositeEffect
            {
                Mode = CanvasComposite.DestinationIn,
                Sources =
                {
                    frostEffect,
                    new CompositionEffectSourceParameter("mask")
                }

            };

            // Create Effect Factory
            var factory = compositor.CreateEffectFactory(effect, new[] { "Blur.BlurAmount", "BlendColor.Color" });
            // Create Effect Brush
            var brush = factory.CreateBrush();

            // Set the BackDropBrush
            // If no backdrop brush is provided, create one
            brush.SetSourceParameter("backdrop", backdropBrush ?? compositor.CreateBackdropBrush());

            // Set the Mask
            // Create SurfaceBrush from CompositionMask
            var maskBrush = compositor.CreateSurfaceBrush(mask.Surface);
            brush.SetSourceParameter("mask", maskBrush);

            return brush;
        }

        /// <summary>
        /// Updates the CompositionSurfaceBrush's Stretch and Alignment options
        /// </summary>
        /// <param name="surfaceBrush">CompositionSurfaceBrush</param>
        /// <param name="stretch">Stretch mode</param>
        /// <param name="alignX">Horizontal Alignment</param>
        /// <param name="alignY">Vertical Alignment</param>
        /// <param name="alignXAnimation">The animation to use to update the horizontal alignment of the surface brush</param>
        /// <param name="alignYAnimation">The animation to use to update the vertical alignment of the surface brush</param>
        public static void UpdateSurfaceBrushOptions(this CompositionSurfaceBrush surfaceBrush, Stretch stretch,
            AlignmentX alignX, AlignmentY alignY, ScalarKeyFrameAnimation alignXAnimation = null,
            ScalarKeyFrameAnimation alignYAnimation = null)
        {
            // Stretch Mode
            switch (stretch)
            {
                case Stretch.None:
                    surfaceBrush.Stretch = CompositionStretch.None;
                    break;
                case Stretch.Fill:
                    surfaceBrush.Stretch = CompositionStretch.Fill;
                    break;
                case Stretch.Uniform:
                    surfaceBrush.Stretch = CompositionStretch.Uniform;
                    break;
                case Stretch.UniformToFill:
                    surfaceBrush.Stretch = CompositionStretch.UniformToFill;
                    break;
            }

            // Horizontal Alignment
            var finalAlignX = surfaceBrush.HorizontalAlignmentRatio;
            switch (alignX)
            {
                case AlignmentX.Left:
                    finalAlignX = 0;
                    break;
                case AlignmentX.Center:
                    finalAlignX = 0.5f;
                    break;
                case AlignmentX.Right:
                    finalAlignX = 1;
                    break;
            }

            // If animation is available, animate to the new value
            // otherwise set it explicitly
            if (alignXAnimation == null)
            {
                surfaceBrush.HorizontalAlignmentRatio = finalAlignX;
            }
            else
            {
                alignXAnimation.InsertKeyFrame(1f, finalAlignX);
                surfaceBrush.StartAnimation(() => surfaceBrush.HorizontalAlignmentRatio, alignXAnimation);
            }

            // Vertical Alignment
            var finalAlignY = surfaceBrush.VerticalAlignmentRatio;
            switch (alignY)
            {
                case AlignmentY.Top:
                    finalAlignY = 0;
                    break;
                case AlignmentY.Center:
                    finalAlignY = 0.5f;
                    break;
                case AlignmentY.Bottom:
                    finalAlignY = 1;
                    break;
            }

            // If animation is available, animate to the new value
            // otherwise set it explicitly
            if (alignYAnimation == null)
            {
                surfaceBrush.VerticalAlignmentRatio = finalAlignY;
            }
            else
            {
                alignYAnimation.InsertKeyFrame(1f, finalAlignY);
                surfaceBrush.StartAnimation(() => surfaceBrush.VerticalAlignmentRatio, alignYAnimation);
            }
        }

        /// <summary>
        /// Adds an ExpressionAnimation&lt;T&gt; to the specified animation group.
        /// </summary>
        /// <typeparam name="T">Type of the property being animated.</typeparam>
        /// <param name="animationGroup">Animation Group</param>
        /// <param name="expressionAnimation">ExpressionAnimation&lt;T&gt;</param>
        public static void Add<T>(this CompositionAnimationGroup animationGroup, 
            ExpressionAnimation<T> expressionAnimation)
        {
            animationGroup.Add(expressionAnimation.Animation);
        }

        /// <summary>
        /// Adds a KeyFrameAnimation&lt;T&gt; to the specified animation group.
        /// </summary>
        /// <typeparam name="T">Type of the property being animated.</typeparam>
        /// <param name="animationGroup">Animation Group</param>
        /// <param name="keyframeAnimation">KeyFrameAnimation&lt;T&gt;</param>
        public static void Add<T>(this CompositionAnimationGroup animationGroup, 
            KeyFrameAnimation<T> keyframeAnimation)
        {
            animationGroup.Add(keyframeAnimation.Animation);
        }

        /// <summary>
        /// Adds an ExpressionAnimation&lt;T&gt; to the specified ImplicitAnimationCollection.
        /// </summary>
        /// <typeparam name="T">Type of the property being animated.</typeparam>
        /// <param name="implicitAnimationCollection">ImplicitAnimationCollection</param>
        /// <param name="key">The key specifying the property being targeted with the 
        /// specified ImplicitAnimationCollection.</param>
        /// <param name="expressionAnimation">ExpressionAnimation&lt;T&gt;</param>
        public static void Add<T>(this ImplicitAnimationCollection implicitAnimationCollection,
            Expression<Func<T>> key,
            ExpressionAnimation<T> expressionAnimation)
        {
            implicitAnimationCollection.Add(CompositionExpressionEngine.ParseExpression(key), expressionAnimation.Animation);
        }

        /// <summary>
        /// Adds a KeyFrameAnimation&lt;T&gt; to the specified ImplicitAnimationCollection.
        /// </summary>
        /// <typeparam name="T">Type of the property being animated.</typeparam>
        /// <param name="implicitAnimationCollection">ImplicitAnimationCollection</param>
        /// <param name="key">The key specifying the property being targeted with the 
        /// specified ImplicitAnimationCollection.</param>
        /// <param name="keyframeAnimation">KeyFrameAnimation&lt;T&gt;</param>
        public static void Add<T>(this ImplicitAnimationCollection implicitAnimationCollection,
            Expression<Func<T>> key,
            KeyFrameAnimation<T> keyframeAnimation)
        {
            implicitAnimationCollection.Add(CompositionExpressionEngine.ParseExpression(key), keyframeAnimation.Animation);
        }

        /// <summary>
        /// Gets the first descendant (of Type T) of this dependency object in the visual tree.
        /// </summary>
        /// <typeparam name="T">Type deriving from DependencyObject</typeparam>
        /// <param name="dependencyObject">DependencyObject whose first descendant is to be obtained.</param>
        /// <returns>First descendant (of Type T), if any</returns>
        public static T GetFirstDescendantOfType<T>(this DependencyObject dependencyObject) where T : DependencyObject
        {
            return dependencyObject.GetDescendantsOfType<T>().FirstOrDefault();
        }

        /// <summary>
        /// Gets the descendants (of Type T) of this dependency object in the visual tree.
        /// </summary>
        /// <typeparam name="T">Type deriving from DependencyObject</typeparam>
        /// <param name="dependencyObject">DependencyObject whose descendants are to be obtained.</param>
        /// <returns>Enumerable collection of descendants (of Type T)</returns>
        public static IEnumerable<T> GetDescendantsOfType<T>(this DependencyObject dependencyObject) where T : DependencyObject
        {
            return dependencyObject.GetDescendants().OfType<T>();
        }

        /// <summary>
        /// Gets the descendants of this dependency object in the visual tree.
        /// </summary>
        /// <param name="dependencyObject">DependencyObject whose descendants are to be obtained.</param>
        /// <returns>Enumerable collection of descendants</returns>
        public static IEnumerable<DependencyObject> GetDescendants(this DependencyObject dependencyObject)
        {
            var queue = new Queue<DependencyObject>();
            // Add to queue to obtain its descendants
            queue.Enqueue(dependencyObject);

            while (queue.Count > 0)
            {
                var parent = queue.Dequeue();
                if (parent == null)
                    continue;

                var childrenCount = VisualTreeHelper.GetChildrenCount(parent);
                for (var i = 0; i < childrenCount; i++)
                {
                    var child = VisualTreeHelper.GetChild(parent, i);
                    // Yield for enumeration
                    yield return child;
                    // Add child to queue to obtain its descendants
                    queue.Enqueue(child);
                }
            }
        }

        /// <summary>
        /// Gets the first ancestor (of Type T) of this dependency object in the visual tree.
        /// </summary>
        /// <typeparam name="T">Type deriving from DependencyObject</typeparam>
        /// <param name="dependencyObject">DependencyObject whose first ancestor is to be obtained.</param>
        /// <returns>First ancestor (of Type T), if any</returns>
        public static T GetFirstAncestorOfType<T>(this DependencyObject dependencyObject) where T : DependencyObject
        {
            return dependencyObject.GetAncestorsOfType<T>().FirstOrDefault();
        }

        /// <summary>
        /// Gets the ancestors (of Type T) of this dependency object in the visual tree.
        /// </summary>
        /// <typeparam name="T">Type deriving from DependencyObject</typeparam>
        /// <param name="dependencyObject">DependencyObject whose ancestors are to be obtained.</param>
        /// <returns>Enumerable collection of ancestors (of Type T)</returns>
        public static IEnumerable<T> GetAncestorsOfType<T>(this DependencyObject dependencyObject) where T : DependencyObject
        {
            return dependencyObject.GetAncestors().OfType<T>();
        }

        /// <summary>
        /// Gets the ancestors of this dependency object in the visual tree.
        /// </summary>
        /// <param name="dependencyObject">DependencyObject whose ancestors are to be obtained.</param>
        /// <returns>Enumerable collection of ancestors</returns>
        public static IEnumerable<DependencyObject> GetAncestors(this DependencyObject dependencyObject)
        {
            var parent = VisualTreeHelper.GetParent(dependencyObject);

            while (parent != null)
            {
                yield return parent;
                parent = VisualTreeHelper.GetParent(parent);
            }
        }

        /// <summary>
        /// Checks if this dependency object is present in the Visual Tree 
        /// of the current window.
        /// </summary>
        /// <param name="dependencyObject">DependencyObject</param>
        /// <returns>True if present, otherwise False</returns>
        public static bool IsInVisualTree(this DependencyObject dependencyObject)
        {
            return Window.Current.Content != null &&
                dependencyObject.GetAncestors().Contains(Window.Current.Content);
        }
    }
}
