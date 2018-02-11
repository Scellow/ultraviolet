﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Ultraviolet.Audio;
using Ultraviolet.BASS.Native;
using Ultraviolet.Core;
using Ultraviolet.Platform;

namespace Ultraviolet.BASS.Audio
{
    /// <summary>
    /// Represents the BASS implementation of the Song class.
    /// </summary>
    public sealed partial class BASSSong : Song
    {
        /// <summary>
        /// Initializes a new instance of the BASSSong class.
        /// </summary>
        /// <param name="uv">The Ultraviolet context.</param>
        /// <param name="file">The path to the file from which to stream the song.</param>
        public BASSSong(UltravioletContext uv, String file)
            : base(uv)
        {
            Contract.RequireNotEmpty(file, nameof(file));

            this.file = file;

            var stream = CreateStream(BASSNative.BASS_STREAM_DECODE);
            
            this.tags = GetTags(stream, out this.name, out this.artist);
            this.duration = GetDuration(stream);

            if (!BASSNative.StreamFree(stream))
                throw new BASSException();
        }

        /// <inheritdoc/>
        public override SongTagCollection Tags
        {
            get
            {
                Contract.EnsureNotDisposed(this, Disposed);

                return tags;
            }
        }

        /// <inheritdoc/>
        public override String Name
        {
            get
            {
                Contract.EnsureNotDisposed(this, Disposed);

                return name;
            }
        }

        /// <inheritdoc/>
        public override String Artist
        {
            get
            {
                Contract.EnsureNotDisposed(this, Disposed);

                return artist;
            }
        }

        /// <inheritdoc/>
        public override TimeSpan Duration
        {
            get
            {
                Contract.EnsureNotDisposed(this, Disposed);

                return duration;
            }
        }

        /// <summary>
        /// Creates a BASS stream that represents the song.
        /// </summary>
        /// <param name="flags">The flags to apply to the stream that is created.</param>
        /// <returns>The handle to the BASS stream that was created.</returns>
        internal UInt32 CreateStream(UInt32 flags)
        {
            if (FileSystemService.Source == null)
            {
                var stream = BASSNative.StreamCreateFile(file, flags);
                if (!BASSUtil.IsValidHandle(stream))
                    throw new BASSException();

                return stream;
            }
            else
            {
                if (instanceManager == null)
                {
                    instanceManager = new BASSSongInstanceManager(file);
                }
                return instanceManager.CreateInstance(flags);
            }
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                SafeDispose.DisposeRef(ref instanceManager);
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Gets the duration of the specified stream.
        /// </summary>
        private static TimeSpan GetDuration(UInt32 stream)
        {
            return TimeSpan.FromSeconds(BASSUtil.GetDurationInSeconds(stream));
        }

        /// <summary>
        /// Reads any supported tags which are contained by the specified stream.
        /// </summary>
        private static SongTagCollection GetTags(UInt32 stream, out String name, out String artist)
        {
            var result = new SongTagCollection();
            var tags = new Dictionary<String, String>();

            if (ReadID3TagsFromStream(stream, out tags) ||
                ReadOggTagsFromStream(stream, out tags))
            {
                foreach (var tag in tags)
                    result.Add(tag.Key, tag.Value);
            }

            name = 
                result["name"]?.Value ?? 
                result["title"]?.Value;

            artist = 
                result["artist"]?.Value;

            return result;
        }

        /// <summary>
        /// Attempts to read ID3 tags from the specified stream.
        /// </summary>
        private static unsafe Boolean ReadID3TagsFromStream(UInt32 handle, out Dictionary<String, String> tags)
        {
            var error = 0;

            tags = null;

            var ptr = BASSNative.ChannelGetTags(handle, BASSNative.BASS_TAG_ID3);
            if (ptr == null)
            {
                error = BASSNative.ErrorGetCode();
                if (error != BASSNative.BASS_ERROR_NOTAVAIL)
                    throw new BASSException(error);

                return false;
            }

            var data = Marshal.PtrToStructure<TAG_ID3>((IntPtr)ptr).ToMarshalledStruct();

            tags = new Dictionary<String, String>();
            tags["title"] = data.title;
            tags["artist"] = data.artist;
            tags["album"] = data.album;
            tags["year"] = data.year;
            tags["comment"] = data.comment;

            return true;
        }
        
        /// <summary>
        /// Attempts to read OGG tags from the specified stream.
        /// </summary>
        private static unsafe Boolean ReadOggTagsFromStream(UInt32 handle, out Dictionary<String, String> tags)
        {
            tags = null;

            var ptr = (Byte*)BASSNative.ChannelGetTags(handle, BASSNative.BASS_TAG_OGG);
            if (ptr == null)
            {
                var error = BASSNative.ErrorGetCode();
                if (error != BASSNative.BASS_ERROR_NOTAVAIL)
                    throw new BASSException(error);

                return false;
            }
            else
            {
                tags = new Dictionary<String, String>();

                var str = String.Empty;
                var strptr = default(Byte*);

                for (strptr = ptr; *strptr != 0; strptr += str.Length + 1)
                {
                    str = Marshal.PtrToStringAnsi((IntPtr)strptr);

                    var strparts = str.Split('=');
                    var key = strparts[0];
                    var val = strparts[1];

                    tags[key] = val;
                }

                return true;
            }
        }

        // The file from which to stream the song.
        private readonly String file;
        private readonly SongTagCollection tags;
        private readonly String name;
        private readonly String artist;
        private readonly TimeSpan duration;

        // The instance manager used when we can't read files directly from the file system using BASS.
        private BASSSongInstanceManager instanceManager;
    }
}
