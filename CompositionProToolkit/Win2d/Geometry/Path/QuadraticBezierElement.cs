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
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using CompositionProToolkit.Win2d.Common;
using CompositionProToolkit.Win2d.Core;
using Microsoft.Graphics.Canvas.Geometry;

namespace CompositionProToolkit.Win2d.Geometry.Path
{
    /// <summary>
    /// Class representing the Quadratic Bezier Element in a Path Geometry
    /// </summary>
    internal class QuadraticBezierElement : AbstractPathElement
    {
        #region Fields

        private float _x1;
        private float _y1;
        private float _x;
        private float _y;
        private Vector2 _absoluteControlPoint;

        #endregion

        #region Construction / Initialization

        /// <summary>
        /// ctor
        /// </summary>
        public QuadraticBezierElement()
        {
            _x1 = _y1 = 0;
            _x = _y = 0;
            _absoluteControlPoint = Vector2.Zero;
        }

        #endregion

        #region APIs

        /// <summary>
        /// Adds the Path Element to the Path.
        /// </summary>
        /// <param name="pathBuilder">CanvasPathBuilder object</param>
        /// <param name="currentPoint">The last active location in the Path before adding 
        /// the Path Element</param>
        /// <param name="lastElement">The previous PathElement in the Path.</param>
        /// <param name="logger">For logging purpose. To log the set of CanvasPathBuilder 
        /// commands, used for creating the CanvasGeometry, in string format.</param>
        /// <returns>The latest location in the Path after adding the Path Element</returns>
        public override Vector2 CreatePath(CanvasPathBuilder pathBuilder, Vector2 currentPoint, 
            ref ICanvasPathElement lastElement, StringBuilder logger)
        {
            // Calculate coordinates
            var controlPoint = new Vector2(_x1, _y1);
            var point = new Vector2(_x, _y);

            if (IsRelative)
            {
                controlPoint += currentPoint;
                point += currentPoint;
            }

            // Save the absolute control point so that it can be used by the following
            // SmoothQuadraticBezierElement (if any)
            _absoluteControlPoint = controlPoint;

            // Execute command
            pathBuilder.AddQuadraticBezier(controlPoint, point);

            // Log command
            logger?.Append($"{Indent}pathBuilder.AddQuadraticBezier(new Vector2({controlPoint.X}, {controlPoint.Y})");
            logger?.AppendLine($", new Vector2({point.X}, {point.Y}));");

            // Set Last Element
            lastElement = this;
            // Return current point
            return point;
        }

        /// <summary>
        /// Gets the Control Point of this Quadratic Bezier
        /// </summary>
        /// <returns>Vector2</returns>
        public Vector2 GetControlPoint()
        {
            return _absoluteControlPoint;
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Get the Regex for extracting Path Element Attributes
        /// </summary>
        /// <returns></returns>
        protected override Regex GetAttributesRegex()
        {
            return RegexFactory.GetAttributesRegex(PathElementType.QuadraticBezier);
        }

        /// <summary>
        /// Gets the Path Element Attributes from the Match
        /// </summary>
        /// <param name="match">Match object</param>
        protected override void GetAttributes(Match match)
        {
            Single.TryParse(match.Groups["X1"].Value, out _x1);
            Single.TryParse(match.Groups["Y1"].Value, out _y1);
            Single.TryParse(match.Groups["X"].Value, out _x);
            Single.TryParse(match.Groups["Y"].Value, out _y);
        }

        #endregion
    }
}
