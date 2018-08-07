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
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using CompositionProToolkit.Win2d.Core;
using Microsoft.Graphics.Canvas.Geometry;

namespace CompositionProToolkit.Win2d
{
    /// <summary>
    /// Defines extension method for CanvasPathBuilder
    /// </summary>
    public static class CanvasPathBuilderExtensions
    {
        /// <summary>
        /// Adds a line in the form of a cubic bezier. The control point of the quadratic bezier
        /// will be the endpoint of the line itself.
        /// </summary>
        /// <param name="pathBuilder">CanvasPathBuilder</param>
        /// <param name="end">Ending location of the line segment.</param>
        public static void AddLineAsQuadraticBezier(this CanvasPathBuilder pathBuilder, Vector2 end)
        {
            pathBuilder.AddQuadraticBezier(end, end);
        }

        /// <summary>
        /// Adds a line in the form of a cubic bezier. The two control points of the cubic bezier
        /// will be the endpoints of the line itself.
        /// </summary>
        /// <param name="pathBuilder">CanvasPathBuilder</param>
        /// <param name="start">Starting location of the line segment.</param>
        /// <param name="end">Ending location of the line segment.</param>
        public static void AddLineAsCubicBezier(this CanvasPathBuilder pathBuilder, Vector2 start, Vector2 end)
        {
            pathBuilder.AddCubicBezier(start, end, end);
        }

        /// <summary>
        /// Adds a circle figure to the path.
        /// </summary>
        /// <param name="pathBuilder">CanvasPathBuilder</param>
        /// <param name="center">Center location of the circle.</param>
        /// <param name="radius">Radius of the circle.</param>
        public static void AddCircleFigure(this CanvasPathBuilder pathBuilder, Vector2 center, float radius)
        {
            pathBuilder.AddEllipseFigure(center.X, center.Y, radius, radius);
        }

        /// <summary>
        /// Adds a circle figure to the path.
        /// </summary>
        /// <param name="pathBuilder">CanvasPathBuilder</param>
        /// <param name="x">X coordinate of the center location of the circle.</param>
        /// <param name="y">Y coordinate of the center location of the circle.</param>
        /// <param name="radius">Radius of the circle.</param>
        public static void AddCircleFigure(this CanvasPathBuilder pathBuilder, float x, float y, float radius)
        {
            pathBuilder.AddEllipseFigure(x, y, radius, radius);
        }

        /// <summary>
        /// Adds an ellipse figure to the path.
        /// </summary>
        /// <param name="pathBuilder">CanvasPathBuilder</param>
        /// <param name="center">Center location of the ellipse.</param>
        /// <param name="radiusX">Radius of the ellipse on the X-axis.</param>
        /// <param name="radiusY">Radius of the ellipse on the Y-axis.</param>
        public static void AddEllipseFigure(this CanvasPathBuilder pathBuilder, Vector2 center, float radiusX,
            float radiusY)
        {
            pathBuilder.AddEllipseFigure(center.X, center.Y, radiusX, radiusY);
        }

        /// <summary>
        /// Adds an ellipse figure to the path.
        /// </summary>
        /// <param name="pathBuilder">CanvasPathBuilder</param>
        /// <param name="x">X coordinate of the center location of the ellipse.</param>
        /// <param name="y">Y coordinate of the center location of the ellipse.</param>
        /// <param name="radiusX">Radius of the ellipse on the X-axis.</param>
        /// <param name="radiusY">Radius of the ellipse on the Y-axis.</param>
        public static void AddEllipseFigure(this CanvasPathBuilder pathBuilder, float x, float y,
            float radiusX, float radiusY)
        {
            // Sanitize the radiusX by taking the absolute value
            radiusX = Math.Abs(radiusX);

            // Sanitize the radiusY by taking the absolute value
            radiusY = Math.Abs(radiusY);

            try
            {
                pathBuilder.BeginFigure(x + radiusX, y);
            }
            catch (ArgumentException)
            {
                // An ArgumentException will be raised if another figure was already begun( and not ended)
                // before calling AddEllipseFigure() method.
                throw new InvalidOperationException("A call to CanvasPathBuilder.AddEllipseFigure occurred, " +
                                                    "when another figure was already begun. Please call CanvasPathBuilder.EndFigure method, " +
                                                    "before calling CanvasPathBuilder.AddEllipseFigure, to end the previous figure.");
            }

            // First Semi-Ellipse
            pathBuilder.AddArc(new Vector2(x - radiusX, y), radiusX, radiusY, Scalar.Pi, CanvasSweepDirection.Clockwise, CanvasArcSize.Large);
            // Second Semi-Ellipse
            pathBuilder.AddArc(new Vector2(x + radiusX, y), radiusX, radiusY, Scalar.Pi, CanvasSweepDirection.Clockwise, CanvasArcSize.Large);
            // End Figure
            pathBuilder.EndFigure(CanvasFigureLoop.Closed);
        }

        /// <summary>
        /// Adds a n-sided polygon figure to the path.
        /// </summary>
        /// <param name="pathBuilder">CanvasPathBuilder</param>
        /// <param name="numSides">Number of sides of the polygon.</param>
        /// <param name="center">Center location of the polygon.</param>
        /// <param name="radius">Radius of the circle cirumscribing the polygon i.e. the distance
        /// of each of the vertices of the polygon from the center.</param>
        public static void AddPolygonFigure(this CanvasPathBuilder pathBuilder, int numSides, Vector2 center,
            float radius)
        {
            pathBuilder.AddPolygonFigure(numSides, center.X, center.Y, radius);
        }

        /// <summary>
        /// Adds a n-sided polygon figure to the path.
        /// </summary>
        /// <param name="pathBuilder">CanvasPathBuilder</param>
        /// <param name="numSides">Number of sides of the polygon.</param>
        /// <param name="x">X coordinate of the center location of the polygon.</param>
        /// <param name="y">Y coordinate of the center location of the polygon.</param>
        /// <param name="radius">Radius of the circle cirumscribing the polygon i.e. the distance
        /// of each of the vertices of the polygon from the center.</param>
        public static void AddPolygonFigure(this CanvasPathBuilder pathBuilder, int numSides, float x, float y,
            float radius)
        {
            // Sanitize the radius by taking the absolute value
            radius = Math.Abs(radius);

            if (numSides < 3)
            {
                throw new ArgumentException("A polygon should have at least 3 sides", nameof(numSides));
            }

            // Calculate the first vertex location based on the number of sides
            var angle = Scalar.TwoPi / numSides;
            var startAngle = numSides % 2 == 1 ? Scalar.PiByTwo : Scalar.PiByTwo - (angle / 2f);

            var startX = x + (float)(radius * Math.Cos(startAngle));
            var startY = y - (float)(radius * Math.Sin(startAngle));

            try
            {
                pathBuilder.BeginFigure(startX, startY);
            }
            catch (ArgumentException)
            {
                // An ArgumentException will be raised if another figure was already begun( and not ended)
                // before calling AddPolygonFigure() method.
                throw new InvalidOperationException("A call to CanvasPathBuilder.AddPolygonFigure occurred, " +
                                                    "when another figure was already begun. Please call CanvasPathBuilder.EndFigure method, " +
                                                    "before calling CanvasPathBuilder.AddPolygonFigure, to end the previous figure.");
            }

            // Add lines to the remaining vertices
            for (var i = 1; i < numSides; i++)
            {
                var posX = x + (float)(radius * Math.Cos(startAngle + i * angle));
                var posY = y - (float)(radius * Math.Sin(startAngle + i * angle));
                pathBuilder.AddLine(posX, posY);
            }

            // Add a line to the first vertex so that the lines join properly
            pathBuilder.AddLine(startX, startY);

            // End the Figure
            pathBuilder.EndFigure(CanvasFigureLoop.Closed);
        }

        /// <summary>
        /// Adds a Rectangle to the Path.
        /// </summary>
        /// <param name="pathBuilder">CanvasPathBuilder</param>
        /// <param name="x">X offset of the TopLeft corner of the Rectangle</param>
        /// <param name="y">Y offset of the TopLeft corner of the Rectangle</param>
        /// <param name="width">Width of the Rectangle</param>
        /// <param name="height">Height of the Rectangle</param>
        public static void AddRectangleFigure(this CanvasPathBuilder pathBuilder, float x, float y, float width,
            float height)
        {
            // Sanitize the width by taking the absolute value
            width = Math.Abs(width);
            // Sanitize the height by taking the absolute value
            height = Math.Abs(height);

            try
            {
                pathBuilder.BeginFigure(x, y);
            }
            catch (ArgumentException)
            {
                // An ArgumentException will be raised if another figure was already begun( and not ended)
                // before calling AddPolygonFigure() method.
                throw new InvalidOperationException("A call to CanvasPathBuilder.AddRectangleFigure occurred, " +
                                                    "when another figure was already begun. Please call CanvasPathBuilder.EndFigure method, " +
                                                    "before calling CanvasPathBuilder.AddRectangleFigure, to end the previous figure.");
            }

            // Top Side
            pathBuilder.AddLine(x + width, y);
            // Right Side
            pathBuilder.AddLine(x + width, y + height);
            // Bottom Side
            pathBuilder.AddLine(x, y + height);
            // Left Side
            pathBuilder.AddLine(x, y);

            // End the Figure
            pathBuilder.EndFigure(CanvasFigureLoop.Closed);
        }

        /// <summary>
        /// Adds a RoundedRectangle to the Path.
        /// </summary>
        /// <param name="pathBuilder">CanvasPathBuilder</param>
        /// <param name="x">X offset of the TopLeft corner of the RoundedRectangle</param>
        /// <param name="y">Y offset of the TopLeft corner of the RoundedRectangle</param>
        /// <param name="width">Width of the RoundedRectangle</param>
        /// <param name="height">Height of the RoundedRectangle</param>
        /// <param name="radiusX">Corner Radius on the x-axis</param>
        /// <param name="radiusY">Corner Radius on the y-axis</param>
        public static void AddRoundedRectangleFigure(this CanvasPathBuilder pathBuilder, float x, float y, float width,
            float height, float radiusX, float radiusY)
        {
            // Sanitize the width by taking the absolute value
            width = Math.Abs(width);
            // Sanitize the height by taking the absolute value
            height = Math.Abs(height);

            var rect = new CanvasRoundRect(x, y, width, height, radiusX, radiusY);
            pathBuilder.AddRoundedRectangleFigure(rect, true);
        }

        /// <summary>
        /// Adds a RoundedRectangle to the Path. (To be used internally)
        /// </summary>
        /// <param name="pathBuilder">CanvasPathBuilder</param>
        /// <param name="rect">CanvasRoundRect</param>
        /// <param name="raiseException">Flag to indicate whether exception should be raised</param>
        internal static void AddRoundedRectangleFigure(this CanvasPathBuilder pathBuilder, CanvasRoundRect rect,
            bool raiseException = false)
        {
            try
            {
                // Begin path
                pathBuilder.BeginFigure(rect.LeftTop);
            }
            catch (ArgumentException)
            {
                if (!raiseException)
                    return;

                // An ArgumentException will be raised if another figure was already begun( and not ended)
                // before calling AddPolygonFigure() method.
                throw new InvalidOperationException("A call to CanvasPathBuilder.AddRoundedRectangleFigure occurred, " +
                                                    "when another figure was already begun. Please call CanvasPathBuilder.EndFigure method, " +
                                                    "before calling CanvasPathBuilder.AddRoundedRectangleFigure, to end the previous figure.");
            }

            // Top line
            pathBuilder.AddLine(rect.RightTop);

            // Upper-right corners
            var radiusX = rect.TopRight.X - rect.RightTop.X;
            var radiusY = rect.TopRight.Y - rect.RightTop.Y;
            var center = new Vector2(rect.RightTop.X, rect.TopRight.Y);
            pathBuilder.AddArc(center, radiusX, radiusY, 3f * Scalar.PiByTwo, Scalar.PiByTwo);

            // Right line
            pathBuilder.AddLine(rect.BottomRight);

            // Lower-right corners
            radiusX = rect.BottomRight.X - rect.RightBottom.X;
            radiusY = rect.RightBottom.Y - rect.BottomRight.Y;
            center = new Vector2(rect.RightBottom.X, rect.BottomRight.Y);
            pathBuilder.AddArc(center, radiusX, radiusY, 0f, Scalar.PiByTwo);

            // Bottom line
            pathBuilder.AddLine(rect.LeftBottom);

            // Lower-left corners
            radiusX = rect.LeftBottom.X - rect.BottomLeft.X;
            radiusY = rect.LeftBottom.Y - rect.BottomLeft.Y;
            center = new Vector2(rect.LeftBottom.X, rect.BottomLeft.Y);
            pathBuilder.AddArc(center, radiusX, radiusY, Scalar.PiByTwo, Scalar.PiByTwo);

            // Left line
            pathBuilder.AddLine(rect.TopLeft);

            // Upper-left corners
            radiusX = rect.LeftTop.X - rect.TopLeft.X;
            radiusY = rect.TopLeft.Y - rect.LeftTop.Y;
            center = new Vector2(rect.LeftTop.X, rect.TopLeft.Y);
            pathBuilder.AddArc(center, radiusX, radiusY, 2f * Scalar.PiByTwo, Scalar.PiByTwo);

            // End path
            pathBuilder.EndFigure(CanvasFigureLoop.Closed);
        }

        /// <summary>
        /// Builds a path with the given collection of points.
        /// </summary>
        /// <param name="builder">CanvasPathBuilder</param>
        /// <param name="canvasFigureLoop">Specifies whether the figure is open or closed. 
        /// This affects the appearance of fills and strokes, as well as geometry operations.</param>
        /// <param name="points">Collection of Vector2 points on the path.</param>
        /// <returns>CanvasPathBuilder</returns>
        public static CanvasPathBuilder BuildPathWithLines(this CanvasPathBuilder builder,
            CanvasFigureLoop canvasFigureLoop, IEnumerable<Vector2> points)
        {
            var first = true;

            foreach (var point in points)
            {
                if (first)
                {
                    builder.BeginFigure(point);
                    first = false;
                }
                else
                {
                    builder.AddLine(point);
                }
            }

            builder.EndFigure(canvasFigureLoop);
            return builder;
        }

        /// <summary>
        /// Builds a path with the given collection of points in the (x, y) pattern.
        /// </summary>
        /// <param name="builder">CanvasPathBuilder</param>
        /// <param name="canvasFigureLoop">Specifies whether the figure is open or closed. 
        /// This affects the appearance of fills and strokes, as well as geometry operations.</param>
        /// <param name="nodes">Collection of points in the (x, y) pattern on the path.</param>
        /// <returns>CanvasPathBuilder</returns>
        public static CanvasPathBuilder BuildPathWithLines(this CanvasPathBuilder builder,
            CanvasFigureLoop canvasFigureLoop, IEnumerable<(float x, float y)> nodes)
        {
            var vectors = nodes.Select(n => new Vector2(n.x, n.y));
            return BuildPathWithLines(builder, canvasFigureLoop, vectors);
        }
    }
}
