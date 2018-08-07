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

using System.Numerics;
using Windows.UI;
using Windows.UI.Composition;

namespace CompositionProToolkit.Expressions.Templates
{
    /// <summary>
    /// Base Template class for DropShadow
    /// </summary>
    public abstract class DropShadowTemplate : ExpressionTemplate
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="name">Template name</param>
        internal DropShadowTemplate(string name) : base(name)
        {
            Type = typeof(DropShadow);
        }

        /// <summary>
        /// The radius of the Gaussian blur used to generate the shadow.
        /// </summary>
        public float BlurRadius { get; }
        /// <summary>
        /// The opacity of the shadow.
        /// </summary>
        public float Opacity { get; }
        /// <summary>
        /// Offset of the shadow relative to its SpriteVisual.
        /// </summary>
        public Vector3 Offset { get; }
        /// <summary>
        /// Color of the shadow
        /// </summary>
        public Color Color { get; }
    }

    /// <summary>
    /// Target Template for DropShadow. This provides a reference to
    /// the DropShadow object to which an Expression is connected.
    /// </summary>
    public sealed class DropShadowTarget : DropShadowTemplate
    {
        /// <summary>
        /// ctor
        /// </summary>
        public DropShadowTarget() : base(string.Empty)
        {

        }
    }

    /// <summary>
    /// Reference Template for DropShadow. When you use this object to define
    /// the reference in the Expression of any animation, you must call the 
    /// SetReference() method of the ExpressionAnimation to set the reference
    /// to actual DropShadow object.
    /// </summary>
    public sealed class DropShadowReference : DropShadowTemplate
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="referenceName">Reference name</param>
        public DropShadowReference(string referenceName) : base(referenceName)
        {

        }
    }
}
