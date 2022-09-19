// =============================================================================
// MIT License
// 
// Copyright (c) 2018 Valeriya Pudova (hww.github.io)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// =============================================================================

using System;

namespace XiSubsystem
{

    /// <summary>
    /// Structure for initialization of a subsystem.
    /// </summary>
    public struct SystemConfig
    {
        /// <summary> Subsystem name </summary>
        public string name;

        /// <summary> Called for each sub-system to initialize </summary>
        public Action preInitialize;

        /// <summary> Called after all sub-system preInitialize was called </summary>
        public Action initialize;

        /// <summary> Called for each entry </summary>
        public Action<object> initializeObject;

        /// <summary> Called for each sub-system- to de-initialize </summary>
        public Action deInitialize;

        /// <summary> Object type for this Subsystem </summary>
        public Type entryType;
    }
}