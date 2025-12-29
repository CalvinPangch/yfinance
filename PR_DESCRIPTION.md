# 🔒 Security Fix: Prevent URL Injection Vulnerability in Symbol Parameters

**Severity**: HIGH
**Impact**: High security improvement, no breaking changes
**Issue**: CWE-22 (Path Traversal), CWE-20 (Improper Input Validation)

---

## 📋 Summary

This PR fixes a critical URL injection vulnerability where ticker symbols were directly interpolated into URLs without proper validation. Attackers could potentially manipulate API endpoints through specially crafted symbol strings containing path traversal sequences or URL-encoded characters.

## 🚨 Vulnerability Details

### Before (Vulnerable)
```csharp
var endpoint = $"/v8/finance/chart/{symbol}";  // No validation!
```

**Attack Vectors**:
- Path Traversal: `../../../etc/passwd`, `..\windows\system32`
- URL-Encoded Injection: `%2F`, `%2E%2E%2F`
- Query String Injection: `AAPL?admin=true`
- Special Characters: `/`, `\`, `%`, `?`, `#`, `&`, `:`, `;`, `@`, `<`, `>`

### After (Secure)
```csharp
_symbolValidator.ValidateAndThrow(symbol, nameof(symbol));  // ✅ Validated!
var endpoint = $"/v8/finance/chart/{symbol}";
```

## 🛡️ Solution Implemented

### 1. Created SymbolValidator Security Utility

**New Files**:
- `YFinance.NET.Interfaces/Utils/ISymbolValidator.cs` - Interface definition
- `YFinance.NET.Implementation/Utils/SymbolValidator.cs` - Secure implementation
- `YFinance.NET.Tests/Unit/Utils/SymbolValidatorTests.cs` - 40+ comprehensive tests

**Security Features**:
- ✅ **Whitelist Validation**: Only alphanumeric characters and `.-^=_` allowed
- ✅ **Length Constraints**: 1-50 characters enforced
- ✅ **Path Traversal Detection**: Blocks `..`, `./`, `.\\`, `~/`
- ✅ **URL Encoding Detection**: Rejects percent-encoded sequences (`%XX`)
- ✅ **Special Character Blocking**: Prevents injection attempts
- ✅ **ReDoS Protection**: 100ms regex timeout prevents regex denial-of-service
- ✅ **Sanitization Method**: Safe fallback for user input processing
- ✅ **Clear Error Messages**: Developers get specific validation failure feedback

### 2. Updated Scrapers with Validation

Applied security validation to **5 critical scrapers** (7 methods total):

| Scraper | Methods Updated | Lines Changed |
|---------|----------------|---------------|
| `HistoryScraper.cs` | 2 | Constructor + 2 methods |
| `QuoteScraper.cs` | 1 | Constructor + 1 method |
| `FundamentalsScraper.cs` | 1 | Constructor + 1 method |
| `InfoScraper.cs` | 1 | Constructor + 1 method |
| `OptionsScraper.cs` | 2 | Constructor + 2 methods |

**Change Pattern**:
```csharp
// Before
if (string.IsNullOrWhiteSpace(symbol))
    throw new ArgumentException("Symbol cannot be null or whitespace.", nameof(symbol));

// After
_symbolValidator.ValidateAndThrow(symbol, nameof(symbol));
```

### 3. Dependency Injection Setup

**Modified**: `ServiceCollectionExtensions.cs`
```csharp
services.AddSingleton<ISymbolValidator, SymbolValidator>();
```

### 4. Comprehensive Testing

**Created**: `SymbolValidatorTests.cs` with **40+ unit tests**

**Test Coverage**:
- ✅ Valid symbol formats (9 tests)
- ✅ Null/whitespace handling (5 tests)
- ✅ Path traversal prevention (10 tests)
- ✅ Injection attack blocking (14 tests)
- ✅ Length validation (4 tests)
- ✅ Character validation (6 tests)
- ✅ Sanitization logic (13 tests)
- ✅ Edge cases (4 tests)

## ✅ Valid Symbols (Still Work)

These legitimate ticker symbols continue to work perfectly:
- `AAPL` - Standard stocks
- `BRK.B` - Share class notation
- `BTC-USD` - Currency pairs
- `^GSPC` - Index symbols
- `ES=F` - Futures contracts
- `SYMBOL_NAME` - Underscores allowed

## 🚫 Blocked Symbols (Security Threats)

The validator now correctly rejects:
- `../../../etc/passwd` - Path traversal attack
- `AAPL?admin=true` - Query parameter injection
- `%2F` - URL-encoded characters
- `AAPL/test` - Directory traversal
- `AAPL<script>` - XSS attempts
- `AAPL;DROP` - Command injection
- `.AAPL` - Invalid start character
- Symbols > 50 characters - Length violations

## 📊 Changes Summary

| Category | Count |
|----------|-------|
| **Files Added** | 3 (Interface, Implementation, Tests) |
| **Files Modified** | 6 (5 Scrapers + DI Registration) |
| **Documentation** | 1 (SECURITY_FIX_SUMMARY.md) |
| **Lines Added** | 865+ |
| **Unit Tests** | 40+ |
| **Breaking Changes** | 0 |

## 🔍 Security Benefits

1. **Prevents URL Manipulation**: Attackers cannot inject path traversal sequences
2. **Blocks Encoded Attacks**: URL-encoded injection attempts are detected and rejected
3. **Whitelist Approach**: Only explicitly allowed characters are permitted (secure by default)
4. **Defense in Depth**: Multiple validation layers (length, characters, patterns, encoding)
5. **Clear Error Messages**: Developers get actionable feedback on validation failures
6. **Performance Optimized**: Regex timeout prevents ReDoS attacks
7. **Comprehensive Testing**: 40+ unit tests covering edge cases and attack vectors
8. **Zero Breaking Changes**: All valid symbols continue to work

## 🧪 Testing

### Unit Tests
- ✅ All 40+ new unit tests pass
- ✅ Covers all attack vectors
- ✅ Edge cases validated
- ✅ Sanitization logic verified

### Integration Testing
No breaking changes expected. All existing integration tests using valid symbols will continue to pass.

## 📚 Documentation

See [`SECURITY_FIX_SUMMARY.md`](./SECURITY_FIX_SUMMARY.md) for complete technical details, attack vectors prevented, and migration notes.

## 🔗 References

- **OWASP**: [Path Traversal Attack](https://owasp.org/www-community/attacks/Path_Traversal_Attack)
- **CWE-22**: Improper Limitation of a Pathname to a Restricted Directory
- **CWE-20**: Improper Input Validation

## ✅ Checklist

- [x] Symbol validation interface created
- [x] Secure implementation with comprehensive checks
- [x] Registered in DI container
- [x] Applied to all critical scrapers
- [x] 40+ unit tests written and passing
- [x] Follows project conventions (CLAUDE.md)
- [x] XML documentation added
- [x] Error messages are clear and actionable
- [x] Security documentation created
- [x] No breaking changes for valid symbols

## 🎯 Impact

- **Security**: ⬆️ HIGH improvement
- **Breaking Changes**: ✅ NONE
- **Performance**: ➡️ Negligible (single validation check)
- **Maintainability**: ⬆️ Improved (centralized validation)

---

**Ready for Review** 🚀

This PR significantly improves the security posture of YFinance.NET by preventing URL injection attacks through symbol parameters, following security best practices with comprehensive test coverage.
