namespace Raspite.Tests;

public sealed class NbtSerializerTests
{
    [Fact]
    private async Task Serializing_Little_None_Outputs_CorrectTags()
    {
        // Arrange
        var source = new byte[]
        {
            8, 4, 0, 78, 97, 109, 101, 7, 0, 82, 97, 115, 112, 105, 116, 101
        };

        var expected = new NbtTag.String()
        {
            Name = "Name",
            Value = "Raspite"
        };

        // Act
        var actual = await NbtSerializer.SerializeAsync(source, new NbtSerializerOptions()
        {
            Endianness = Endianness.Little,
            Compression = Compression.None
        }) as NbtTag.String;

        // Assert
        Assert.Equal(expected.Value, actual?.Value);
        Assert.Equal(expected.Name, actual?.Name);
    }

    [Fact]
    private async Task Deserializing_Little_None_Outputs_CorrectBytes()
    {
        // Arrange
        var source = new NbtTag.String()
        {
            Name = "Name",
            Value = "Raspite"
        };

        var expected = new byte[]
        {
            8, 4, 0, 78, 97, 109, 101, 7, 0, 82, 97, 115, 112, 105, 116, 101
        };

        // Act
        var actual = await NbtSerializer.DeserializeAsync(source, new NbtSerializerOptions()
        {
            Endianness = Endianness.Little,
            Compression = Compression.None
        });

        // Assert
        Assert.Equal(expected, actual);
    }
}