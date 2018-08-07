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
using System.Text.RegularExpressions;
using CompositionProToolkit.Win2d;
using CompositionProToolkit.Win2d.Core;
using CompositionProToolkit.Win2d.Geometry.Stroke;
using Microsoft.Graphics.Canvas;

namespace Win2dHelper.Win2d.Geometry.Stroke
{
    /// <summary>
    /// Abstract base class for Stroke Element
    /// </summary>
    internal abstract class AbstractCanvasStrokeElement : ICanvasStrokeElement
    {
        #region Properties

        /// <summary>
        /// Gets the Stroke data defining the Brush Element
        /// </summary>
        public string Data { get; protected set; }
        /// <summary>
        /// Gets the number of non-whitespace characters in 
        /// the Stroke Data
        /// </summary>
        public int ValidationCount { get; protected set; }

        #endregion

        #region APIs

        /// <summary>
        /// Initializes the Stroke Element with the given Capture
        /// </summary>
        /// <param name="match">Match object</param>
        public virtual void Initialize(Match match)
        {
            Data = match.Value;

            if (!match.Success)
            {
                return;
            }

            GetAttributes(match);

            // Update the validation count
            Validate();
        }

        /// <summary>
        /// Gets the number of non-whitespace characters in the data
        /// </summary>
        protected virtual void Validate()
        {
            ValidationCount = RegexFactory.ValidationRegex.Replace(Data, String.Empty).Length;
        }

        /// <summary>
        /// Creates the ICanvasStroke from the parsed data
        /// </summary>
        /// <returns>ICanvasStroke</returns>
        public abstract ICanvasStroke CreateStroke(ICanvasResourceCreator resourceCreator);

        #endregion

        #region Helpers

        /// <summary>
        /// Gets the Stroke Element Attributes from the Match
        /// </summary>
        /// <param name="match">Match object</param>
        protected abstract void GetAttributes(Match match);

        #endregion
    }
}
