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
using System.Threading.Tasks;

namespace CompositionProToolkit.Controls
{
    internal interface ICacheHandler
    {
        /// <summary>
        /// Checks if this handler can cache the given object
        /// </summary>
        /// <param name="objectToCache">Object to cache</param>
        /// <returns>True if it can cache, otherwise False</returns>
        bool CanCache(object objectToCache);

        /// <summary>
        /// Caches the given Uri to the Application's ImageCache
        /// and returns the uri of the cached file.
        /// </summary>
        /// <param name="objectToCache">Object to cache</param>
        /// <param name="cacheFileName">Name of the cache file</param>
        /// <param name="progressHandler">Delegate for handling progress</param>
        /// <returns>Uri</returns>
        Task<Uri> GetCachedUriAsync(object objectToCache, string cacheFileName,
            CacheProgressHandler progressHandler = null);
    }
}
