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

using System.Numerics;
using Windows.UI.Composition;

namespace CompositionProToolkit.Expressions.Templates
{
    /// <summary>
    /// Base Template class for ManipulationPropertySet
    /// </summary>
    public abstract class ManipulationPropertySetTemplate : ExpressionTemplate
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="name">Template name</param>
        internal ManipulationPropertySetTemplate(string name) : base(name)
        {
            Type = typeof(CompositionPropertySet);
        }

        /// <summary>
        /// The point about which rotation or scaling occurs. 
        /// </summary>
        public Vector3 CenterPoint { get; }
        /// <summary>
        /// The pan to apply on the ManipulationPropertySet
        /// </summary>
        public Vector3 Pan { get; }
        /// <summary>
        /// The scale to apply on the ManipulationPropertySet
        /// </summary>
        public Vector3 Scale { get; }
        /// <summary>
        /// The Translation to apply on the ManipulationPropertySet
        /// </summary>
        public Vector3 Translation { get; }
        /// <summary>
        /// The transformation matrix to apply on the ManipulationPropertySet
        /// </summary>
        public Matrix4x4 Matrix { get; }
    }

    /// <summary>
    /// Target Template for ManipulationPropertySet. This provides a reference to
    /// the ManipulationPropertySet object to which an Expression is connected.
    /// </summary>
    public sealed class ManipulationPropertySetTarget : ManipulationPropertySetTemplate
    {
        /// <summary>
        /// ctor
        /// </summary>
        public ManipulationPropertySetTarget() : base(string.Empty)
        {

        }
    }

    /// <summary>
    /// Reference Template for ManipulationPropertySet. When you use this object to define
    /// the reference in the Expression of any animation, you must call the 
    /// SetReference() method of the ExpressionAnimation to set the reference
    /// to actual ManipulationPropertySet object.
    /// </summary>
    public sealed class ManipulationPropertySetReference : ManipulationPropertySetTemplate
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="referenceName">Reference name</param>
        public ManipulationPropertySetReference(string referenceName) : base(referenceName)
        {

        }
    }
}
