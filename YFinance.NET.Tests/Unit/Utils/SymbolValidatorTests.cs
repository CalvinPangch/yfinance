using Xunit;
using FluentAssertions;
using YFinance.NET.Implementation.Utils;

namespace YFinance.NET.Tests.Unit.Utils;

/// <summary>
/// Unit tests for SymbolValidator to ensure proper validation against URL injection attacks.
/// </summary>
public class SymbolValidatorTests
{
    private readonly SymbolValidator _validator = new();

    #region IsValid Tests

    [Theory]
    [InlineData("AAPL")]
    [InlineData("BRK.B")]
    [InlineData("BTC-USD")]
    [InlineData("^GSPC")]
    [InlineData("ES=F")]
    [InlineData("SYMBOL_NAME")]
    [InlineData("ABC123")]
    [InlineData("TEST.TO")]
    [InlineData("GOOG")]
    public void IsValid_ValidSymbols_ReturnsTrue(string symbol)
    {
        // Act
        var result = _validator.IsValid(symbol);

        // Assert
        result.Should().BeTrue($"'{symbol}' should be a valid symbol");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("\t")]
    [InlineData("\n")]
    public void IsValid_NullOrWhitespace_ReturnsFalse(string? symbol)
    {
        // Act
        var result = _validator.IsValid(symbol!);

        // Assert
        result.Should().BeFalse($"'{symbol}' should be invalid");
    }

    [Theory]
    [InlineData("../../../etc/passwd")]           // Path traversal
    [InlineData("..\\..\\windows\\system32")]     // Windows path traversal
    [InlineData("AAPL/../admin")]                 // Partial path traversal
    [InlineData("~/secret")]                      // Home directory
    [InlineData("AAPL/")]                         // Forward slash
    [InlineData("AAPL\\")]                        // Backslash
    [InlineData("%2F")]                           // URL-encoded slash
    [InlineData("%2E%2E%2F")]                     // URL-encoded ../
    [InlineData("AAPL%20")]                       // URL-encoded space
    [InlineData("%3C%3E")]                        // URL-encoded <>
    public void IsValid_PathTraversalAttempts_ReturnsFalse(string symbol)
    {
        // Act
        var result = _validator.IsValid(symbol);

        // Assert
        result.Should().BeFalse($"'{symbol}' should be rejected as path traversal attempt");
    }

    [Theory]
    [InlineData("AAPL?admin=true")]               // Query string injection
    [InlineData("AAPL#fragment")]                 // Fragment injection
    [InlineData("AAPL&foo=bar")]                  // Parameter injection
    [InlineData("AAPL:8080")]                     // Port injection
    [InlineData("AAPL;DROP TABLE")]               // Command injection
    [InlineData("AAPL@evil.com")]                 // Email-style injection
    [InlineData("AAPL<script>")]                  // XSS attempt
    [InlineData("AAPL>redirect")]                 // Redirect attempt
    [InlineData("AAPL\"quoted")]                  // Quote injection
    [InlineData("AAPL'quoted")]                   // Single quote injection
    [InlineData("AAPL(param)")]                   // Parentheses
    [InlineData("AAPL[array]")]                   // Brackets
    [InlineData("AAPL{json}")]                    // Braces
    [InlineData("AAPL|pipe")]                     // Pipe
    [InlineData("AAPL~tilde")]                    // Tilde
    [InlineData("AAPL`backtick")]                 // Backtick
    public void IsValid_InjectionAttempts_ReturnsFalse(string symbol)
    {
        // Act
        var result = _validator.IsValid(symbol);

        // Assert
        result.Should().BeFalse($"'{symbol}' should be rejected as injection attempt");
    }

    [Theory]
    [InlineData("A")]
    [InlineData("AB")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    public void IsValid_ValidLength_ReturnsTrue(string symbol)
    {
        // Act
        var result = _validator.IsValid(symbol);

        // Assert
        result.Should().BeTrue($"'{symbol}' has valid length");
    }

    [Fact]
    public void IsValid_TooLongSymbol_ReturnsFalse()
    {
        // Arrange - 51 characters (max is 50)
        var symbol = new string('A', 51);

        // Act
        var result = _validator.IsValid(symbol);

        // Assert
        result.Should().BeFalse("Symbol exceeds maximum length of 50 characters");
    }

    [Theory]
    [InlineData(".AAPL")]                         // Starts with period
    [InlineData("-AAPL")]                         // Starts with hyphen
    public void IsValid_InvalidStartCharacter_ReturnsFalse(string symbol)
    {
        // Act
        var result = _validator.IsValid(symbol);

        // Assert
        result.Should().BeFalse($"'{symbol}' starts with invalid character");
    }

    [Theory]
    [InlineData("AAPL ")]                         // Trailing space
    [InlineData(" AAPL")]                         // Leading space
    [InlineData("AA PL")]                         // Middle space
    [InlineData("AAPL\t")]                        // Tab character
    [InlineData("AAPL\r")]                        // Carriage return
    [InlineData("AAPL\n")]                        // Newline
    public void IsValid_WhitespaceCharacters_ReturnsFalse(string symbol)
    {
        // Act
        var result = _validator.IsValid(symbol);

        // Assert
        result.Should().BeFalse($"'{symbol}' contains whitespace");
    }

    #endregion

    #region ValidateAndThrow Tests

    [Fact]
    public void ValidateAndThrow_ValidSymbol_DoesNotThrow()
    {
        // Act
        var act = () => _validator.ValidateAndThrow("AAPL");

        // Assert
        act.Should().NotThrow();
    }

    [Theory]
    [InlineData(null, "Symbol cannot be null or whitespace.")]
    [InlineData("", "Symbol cannot be null or whitespace.")]
    [InlineData("   ", "Symbol cannot be null or whitespace.")]
    public void ValidateAndThrow_NullOrWhitespace_ThrowsArgumentException(string? symbol, string expectedMessage)
    {
        // Act
        var act = () => _validator.ValidateAndThrow(symbol!);

        // Assert
        act.Should().Throw<ArgumentException>()
           .WithMessage($"{expectedMessage}*");
    }

    [Fact]
    public void ValidateAndThrow_TooLongSymbol_ThrowsArgumentException()
    {
        // Arrange
        var symbol = new string('A', 51);

        // Act
        var act = () => _validator.ValidateAndThrow(symbol);

        // Assert
        act.Should().Throw<ArgumentException>()
           .WithMessage("Symbol length must be between 1 and 50 characters.*");
    }

    [Theory]
    [InlineData("../../../etc/passwd")]
    [InlineData("AAPL/../admin")]
    public void ValidateAndThrow_PathTraversal_ThrowsArgumentException(string symbol)
    {
        // Act
        var act = () => _validator.ValidateAndThrow(symbol);

        // Assert
        act.Should().Throw<ArgumentException>()
           .WithMessage("Symbol contains path traversal sequences*");
    }

    [Theory]
    [InlineData("%2F")]
    [InlineData("%2E")]
    [InlineData("AAPL%20TEST")]
    public void ValidateAndThrow_UrlEncoded_ThrowsArgumentException(string symbol)
    {
        // Act
        var act = () => _validator.ValidateAndThrow(symbol);

        // Assert
        act.Should().Throw<ArgumentException>()
           .WithMessage("Symbol contains URL-encoded characters*");
    }

    [Theory]
    [InlineData("AAPL?test")]
    [InlineData("AAPL&test")]
    [InlineData("AAPL/test")]
    public void ValidateAndThrow_InvalidCharacters_ThrowsArgumentException(string symbol)
    {
        // Act
        var act = () => _validator.ValidateAndThrow(symbol);

        // Assert
        act.Should().Throw<ArgumentException>()
           .WithMessage("Symbol contains invalid characters*");
    }

    [Theory]
    [InlineData(".AAPL")]
    [InlineData("-AAPL")]
    public void ValidateAndThrow_InvalidStartCharacter_ThrowsArgumentException(string symbol)
    {
        // Act
        var act = () => _validator.ValidateAndThrow(symbol);

        // Assert
        act.Should().Throw<ArgumentException>()
           .WithMessage("Symbol cannot start with '.' or '-' characters.*");
    }

    [Fact]
    public void ValidateAndThrow_CustomParameterName_IncludesInException()
    {
        // Act
        var act = () => _validator.ValidateAndThrow("", "customParam");

        // Assert
        act.Should().Throw<ArgumentException>()
           .And.ParamName.Should().Be("customParam");
    }

    #endregion

    #region Sanitize Tests

    [Theory]
    [InlineData("AAPL", "AAPL")]
    [InlineData("BRK.B", "BRK.B")]
    [InlineData("BTC-USD", "BTC-USD")]
    [InlineData("^GSPC", "^GSPC")]
    [InlineData("ES=F", "ES=F")]
    [InlineData("SYMBOL_NAME", "SYMBOL_NAME")]
    public void Sanitize_ValidSymbols_ReturnsUnchanged(string input, string expected)
    {
        // Act
        var result = _validator.Sanitize(input);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(null, "")]
    [InlineData("", "")]
    [InlineData("   ", "")]
    public void Sanitize_NullOrWhitespace_ReturnsEmpty(string? input, string expected)
    {
        // Act
        var result = _validator.Sanitize(input!);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("  AAPL  ", "AAPL")]
    [InlineData("\tAAPL\t", "AAPL")]
    [InlineData(" BRK.B ", "BRK.B")]
    public void Sanitize_WithWhitespace_TrimsWhitespace(string input, string expected)
    {
        // Act
        var result = _validator.Sanitize(input);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("AAPL?admin=true", "AAPLadmintrue")]
    [InlineData("AAPL/../../etc", "AAPLetc")]
    [InlineData("AAPL<script>", "AAPLscript")]
    [InlineData("AAPL&foo=bar", "AAPLfoobar")]
    [InlineData("AAPL:8080", "AAPL8080")]
    [InlineData("AAPL;DROP", "AAPLDROP")]
    public void Sanitize_DangerousCharacters_RemovesCharacters(string input, string expected)
    {
        // Act
        var result = _validator.Sanitize(input);

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void Sanitize_TooLongSymbol_TruncatesToMaxLength()
    {
        // Arrange - 60 characters
        var input = new string('A', 60);
        var expected = new string('A', 50); // Should be truncated to 50

        // Act
        var result = _validator.Sanitize(input);

        // Assert
        result.Should().Be(expected);
        result.Length.Should().Be(50);
    }

    [Theory]
    [InlineData(".AAPL", "AAPL")]
    [InlineData("-AAPL", "AAPL")]
    [InlineData("..AAPL", "AAPL")]
    [InlineData("--AAPL", "AAPL")]
    [InlineData(".-AAPL", "AAPL")]
    public void Sanitize_InvalidStartCharacters_RemovesLeadingChars(string input, string expected)
    {
        // Act
        var result = _validator.Sanitize(input);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("A!B@C#D$E", "ABCDE")]
    [InlineData("TEST(123)", "TEST123")]
    [InlineData("SYM[BOL]", "SYMBOL")]
    [InlineData("FOO{BAR}", "FOOBAR")]
    public void Sanitize_SpecialCharacters_KeepsOnlyValid(string input, string expected)
    {
        // Act
        var result = _validator.Sanitize(input);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("AAPL123", "AAPL123")]
    [InlineData("123AAPL", "123AAPL")]
    [InlineData("A1B2C3", "A1B2C3")]
    public void Sanitize_Alphanumeric_Preserved(string input, string expected)
    {
        // Act
        var result = _validator.Sanitize(input);

        // Assert
        result.Should().Be(expected);
    }

    #endregion

    #region Edge Cases

    [Fact]
    public void IsValid_ExactlyMaxLength_ReturnsTrue()
    {
        // Arrange - Exactly 50 characters
        var symbol = new string('A', 50);

        // Act
        var result = _validator.IsValid(symbol);

        // Assert
        result.Should().BeTrue("Symbol is exactly max length");
    }

    [Fact]
    public void Sanitize_OnlyInvalidCharacters_ReturnsEmpty()
    {
        // Arrange
        var input = "!@#$%^&*()[]{}";

        // Act
        var result = _validator.Sanitize(input);

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void Sanitize_OnlyLeadingInvalidChars_ReturnsEmpty()
    {
        // Arrange
        var input = "...---";

        // Act
        var result = _validator.Sanitize(input);

        // Assert
        result.Should().BeEmpty();
    }

    #endregion
}
