using System.Text.Json;
using FluentAssertions;
using Xunit;
using YFinance.Implementation.Utils;

namespace YFinance.Tests.Unit.Utils;

public class DataParserTests
{
    private readonly DataParser _parser;

    public DataParserTests()
    {
        _parser = new DataParser();
    }

    [Fact]
    public void ParseDecimalArray_ValidData_ReturnsCorrectArray()
    {
        // Arrange
        var json = """{"values": [1.5, 2.7, 3.9]}""";
        var element = JsonDocument.Parse(json).RootElement;

        // Act
        var result = _parser.ParseDecimalArray(element, "values");

        // Assert
        result.Should().HaveCount(3);
        result[0].Should().Be(1.5m);
        result[1].Should().Be(2.7m);
        result[2].Should().Be(3.9m);
    }

    [Fact]
    public void ParseDecimalArray_WithNulls_ReturnsZeroForNulls()
    {
        // Arrange
        var json = """{"values": [1.5, null, 3.9]}""";
        var element = JsonDocument.Parse(json).RootElement;

        // Act
        var result = _parser.ParseDecimalArray(element, "values");

        // Assert
        result.Should().HaveCount(3);
        result[0].Should().Be(1.5m);
        result[1].Should().Be(0m);
        result[2].Should().Be(3.9m);
    }

    [Fact]
    public void ParseDecimalArray_EmptyArray_ReturnsEmptyArray()
    {
        // Arrange
        var json = """{"values": []}""";
        var element = JsonDocument.Parse(json).RootElement;

        // Act
        var result = _parser.ParseDecimalArray(element, "values");

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void ParseDecimalArray_MissingProperty_ReturnsEmptyArray()
    {
        // Arrange
        var json = """{"other": [1, 2, 3]}""";
        var element = JsonDocument.Parse(json).RootElement;

        // Act
        var result = _parser.ParseDecimalArray(element, "values");

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void ParseLongArray_ValidData_ReturnsCorrectArray()
    {
        // Arrange
        var json = """{"volumes": [1000, 2000, 3000]}""";
        var element = JsonDocument.Parse(json).RootElement;

        // Act
        var result = _parser.ParseLongArray(element, "volumes");

        // Assert
        result.Should().HaveCount(3);
        result[0].Should().Be(1000L);
        result[1].Should().Be(2000L);
        result[2].Should().Be(3000L);
    }

    [Fact]
    public void ParseLongArray_WithNulls_ReturnsZeroForNulls()
    {
        // Arrange
        var json = """{"volumes": [1000, null, 3000]}""";
        var element = JsonDocument.Parse(json).RootElement;

        // Act
        var result = _parser.ParseLongArray(element, "volumes");

        // Assert
        result.Should().HaveCount(3);
        result[0].Should().Be(1000L);
        result[1].Should().Be(0L);
        result[2].Should().Be(3000L);
    }

    [Fact]
    public void ParseArray_GenericType_WorksCorrectly()
    {
        // Arrange
        var json = """{"numbers": [10, 20, 30]}""";
        var element = JsonDocument.Parse(json).RootElement;

        // Act
        var result = _parser.ParseArray(element, "numbers", e => e.GetInt32(), 0);

        // Assert
        result.Should().HaveCount(3);
        result[0].Should().Be(10);
        result[1].Should().Be(20);
        result[2].Should().Be(30);
    }

    [Fact]
    public void ParseArray_WithDefaultValue_UsesDefaultForNulls()
    {
        // Arrange
        var json = """{"numbers": [10, null, 30]}""";
        var element = JsonDocument.Parse(json).RootElement;

        // Act
        var result = _parser.ParseArray(element, "numbers", e => e.GetInt32(), -1);

        // Assert
        result.Should().HaveCount(3);
        result[0].Should().Be(10);
        result[1].Should().Be(-1); // Default value
        result[2].Should().Be(30);
    }

    [Fact]
    public void UnixTimestampToDateTime_Seconds_ConvertsCorrectly()
    {
        // Arrange
        var timestamp = 1609459200L; // 2021-01-01 00:00:00 UTC

        // Act
        var result = _parser.UnixTimestampToDateTime(timestamp);

        // Assert
        result.Should().Be(new DateTime(2021, 1, 1, 0, 0, 0, DateTimeKind.Utc));
    }

    [Fact]
    public void UnixTimestampToDateTime_Milliseconds_ConvertsCorrectly()
    {
        // Arrange
        var timestamp = 1609459200000L; // 2021-01-01 00:00:00 UTC in milliseconds

        // Act
        var result = _parser.UnixTimestampToDateTime(timestamp);

        // Assert
        result.Should().Be(new DateTime(2021, 1, 1, 0, 0, 0, DateTimeKind.Utc));
    }

    [Fact]
    public void ExtractDecimal_DirectNumber_ReturnsValue()
    {
        // Arrange
        var json = """{"value": 123.45}""";
        var element = JsonDocument.Parse(json).RootElement.GetProperty("value");

        // Act
        var result = _parser.ExtractDecimal(element);

        // Assert
        result.Should().Be(123.45m);
    }

    [Fact]
    public void ExtractDecimal_RawFormat_ReturnsRawValue()
    {
        // Arrange
        var json = """{"value": {"raw": 123.45, "fmt": "123.45"}}""";
        var element = JsonDocument.Parse(json).RootElement.GetProperty("value");

        // Act
        var result = _parser.ExtractDecimal(element);

        // Assert
        result.Should().Be(123.45m);
    }

    [Fact]
    public void ExtractDecimal_StringNumber_ParsesCorrectly()
    {
        // Arrange
        var json = """{"value": "123.45"}""";
        var element = JsonDocument.Parse(json).RootElement.GetProperty("value");

        // Act
        var result = _parser.ExtractDecimal(element);

        // Assert
        result.Should().Be(123.45m);
    }

    [Fact]
    public void ExtractDecimal_Null_ReturnsNull()
    {
        // Arrange
        var json = """{"value": null}""";
        var element = JsonDocument.Parse(json).RootElement.GetProperty("value");

        // Act
        var result = _parser.ExtractDecimal(element);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void Parse_ValidJson_DeserializesCorrectly()
    {
        // Arrange
        var json = """{"Name": "Test", "Value": 123}""";

        // Act
        var result = _parser.Parse<TestModel>(json);

        // Assert
        result.Should().NotBeNull();
        result!.Name.Should().Be("Test");
        result.Value.Should().Be(123);
    }

    [Fact]
    public void Parse_EmptyString_ReturnsDefault()
    {
        // Arrange
        var json = "";

        // Act
        var result = _parser.Parse<TestModel>(json);

        // Assert
        result.Should().BeNull();
    }

    private class TestModel
    {
        public string? Name { get; set; }
        public int Value { get; set; }
    }
}
