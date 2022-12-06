﻿using System.IO.Compression;

namespace Raspite.Library;

/// <summary>
/// Provides functionality to serialize bytes to a <see cref="Tag"/> and vice-versa.
/// </summary>
public static class NbtSerializer
{
    /// <summary>
    /// Converts a sequence of bytes into a <see cref="Tag"/>.
    /// </summary>
    /// <param name="source">The bytes to convert from.</param>
    /// <param name="options">Options to control the behavior of serializing.</param>
    /// <returns>A representation of the bytes into a <see cref="Tag"/>.</returns>
    public static async Task<Tag> SerializeAsync(byte[] source, BinaryOptions? options = null)
    {
        if (options?.Compression is Compression.GZip)
        {
            using var destination = new MemoryStream();

            await using var stream = new GZipStream(new MemoryStream(source), CompressionMode.Decompress);
            await stream.CopyToAsync(destination);

            source = destination.ToArray();
        }

        return new BinaryReader(source, options ?? new BinaryOptions()).Run();
    }

    /// <summary>
    /// Converts a <see cref="Tag"/> into sequence a of bytes.
    /// </summary>
    /// <param name="source">The tag to convert from.</param>
    /// <param name="options">Options to control the behavior of deserializing.</param>
    /// <returns>A representation of the <see cref="Tag"/> into bytes.</returns>
    public static async Task<byte[]> DeserializeAsync(Tag source, BinaryOptions? options = null)
    {
        var bytes = new BinaryWriter(source, options ?? new BinaryOptions()).Run();

        if (options?.Compression is Compression.GZip)
        {
            using var input = new MemoryStream(bytes);
            using var destination = new MemoryStream();

            await using var stream = new GZipStream(destination, CompressionMode.Compress);
            await input.CopyToAsync(stream);

            bytes = input.ToArray();
        }

        return bytes.ToArray();
    }
}