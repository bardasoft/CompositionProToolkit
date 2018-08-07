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

using System.Text.RegularExpressions;
using Microsoft.Graphics.Canvas;

namespace CompositionProToolkit.Win2d.Geometry.Stroke
{
    /// <summary>
    /// Interface for Stroke Element
    /// </summary>
    internal interface ICanvasStrokeElement
    {
        #region Properties

        /// <summary>
        /// Gets the Stroke data defining the Brush Element
        /// </summary>
        string Data { get; }
        /// <summary>
        /// Gets the number of non-whitespace characters in 
        /// the Stroke Data
        /// </summary>
        int ValidationCount { get; }

        #endregion

        #region APIs

        /// <summary>
        /// Initializes the Stroke Element with the given Capture
        /// </summary>
        /// <param name="match">Match object</param>
        void Initialize(Match match);

        /// <summary>
        /// Creates the ICanvasStroke from the parsed data
        /// </summary>
        /// <returns>ICanvasStroke</returns>
        ICanvasStroke CreateStroke(ICanvasResourceCreator resourceCreator);

        #endregion
    }
}
