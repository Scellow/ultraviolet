﻿using System;
using System.Runtime.InteropServices;
using Ultraviolet.Core;
using SDL_FingerID = System.Int64;
using SDL_TouchID = System.Int64;

#pragma warning disable 1591

namespace Ultraviolet.SDL2.Native
{
    [Preserve]
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_TouchFingerEvent
    {
        public UInt32 type;
        public UInt32 timestamp;
        public SDL_TouchID touchId;
        public SDL_FingerID fingerId;
        public Single x;
        public Single y;
        public Single dx;
        public Single dy;
        public Single pressure;
    }
}