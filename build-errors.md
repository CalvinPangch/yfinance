# Build Failure Summary

Branch: claude/fix-security-mjrem4hwy8i13ppr-qFXov
Commit: ca8f4791000271c0b91413537c7203d59cdd81f9
Workflow: CI
Failed Jobs: 2

## Failed Job: Build and Test

*Note: Log truncated, showing last 50000 characters (79584 characters omitted)*

```
tion.CollectingAssertionStrategy.ThrowIfAny(IDictionary`2 context)
2025-12-30T04:02:42.3853408Z    at FluentAssertions.Execution.AssertionScope.Dispose()
2025-12-30T04:02:42.3855199Z    at FluentAssertions.Specialized.ExceptionAssertions`1.ExceptionMessageAssertion.Execute(IEnumerable`1 messages, String expectation, String because, Object[] becauseArgs)
2025-12-30T04:02:42.3857464Z    at FluentAssertions.Specialized.ExceptionAssertions`1.ExceptionMessageAssertion.Execute(IEnumerable`1 messages, String expectation, String because, Object[] becauseArgs)
2025-12-30T04:02:42.3859511Z    at FluentAssertions.Specialized.ExceptionAssertions`1.WithMessage(String expectedWildcardPattern, String because, Object[] becauseArgs)
2025-12-30T04:02:42.3861971Z    at YFinance.NET.Tests.Unit.Utils.SymbolValidatorTests.ValidateAndThrow_UrlEncoded_ThrowsArgumentException(String symbol) in /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Utils/SymbolValidatorTests.cs:line 215
2025-12-30T04:02:42.3864095Z    at InvokeStub_SymbolValidatorTests.ValidateAndThrow_UrlEncoded_ThrowsArgumentException(Object, Span`1)
2025-12-30T04:02:42.3865997Z    at System.Reflection.MethodBaseInvoker.InvokeWithOneArg(Object obj, BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)
2025-12-30T04:02:42.3867851Z   Failed YFinance.NET.Tests.Unit.Utils.SymbolValidatorTests.ValidateAndThrow_UrlEncoded_ThrowsArgumentException(symbol: "%2E") [< 1 ms]
2025-12-30T04:02:42.3868992Z   Error Message:
2025-12-30T04:02:42.3870375Z    Expected exception message to match the equivalent of "Symbol contains URL-encoded characters*", but "Symbol contains invalid characters. Only alphanumeric characters and .-^=_ are allowed. (Parameter 'symbol')" does not.
2025-12-30T04:02:42.3871164Z 
2025-12-30T04:02:42.3871242Z   Stack Trace:
2025-12-30T04:02:42.3871578Z      at FluentAssertions.Execution.XUnit2TestFramework.Throw(String message)
2025-12-30T04:02:42.3872100Z    at FluentAssertions.Execution.TestFrameworkProvider.Throw(String message)
2025-12-30T04:02:42.3872712Z    at FluentAssertions.Execution.CollectingAssertionStrategy.ThrowIfAny(IDictionary`2 context)
2025-12-30T04:02:42.3873459Z    at FluentAssertions.Execution.AssertionScope.Dispose()
2025-12-30T04:02:42.3874266Z    at FluentAssertions.Specialized.ExceptionAssertions`1.ExceptionMessageAssertion.Execute(IEnumerable`1 messages, String expectation, String because, Object[] becauseArgs)
2025-12-30T04:02:42.3875838Z    at FluentAssertions.Specialized.ExceptionAssertions`1.ExceptionMessageAssertion.Execute(IEnumerable`1 messages, String expectation, String because, Object[] becauseArgs)
2025-12-30T04:02:42.3876902Z    at FluentAssertions.Specialized.ExceptionAssertions`1.WithMessage(String expectedWildcardPattern, String because, Object[] becauseArgs)
2025-12-30T04:02:42.3878207Z    at YFinance.NET.Tests.Unit.Utils.SymbolValidatorTests.ValidateAndThrow_UrlEncoded_ThrowsArgumentException(String symbol) in /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Utils/SymbolValidatorTests.cs:line 215
2025-12-30T04:02:42.3879401Z    at InvokeStub_SymbolValidatorTests.ValidateAndThrow_UrlEncoded_ThrowsArgumentException(Object, Span`1)
2025-12-30T04:02:42.3880281Z    at System.Reflection.MethodBaseInvoker.InvokeWithOneArg(Object obj, BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)
2025-12-30T04:02:42.3881202Z   Passed YFinance.NET.Tests.Unit.Utils.SymbolValidatorTests.ValidateAndThrow_ValidSymbol_DoesNotThrow [< 1 ms]
2025-12-30T04:02:42.3882205Z   Passed YFinance.NET.Tests.Unit.Scrapers.HistoryScraperTests.GetHistoryAsync_VariousIntervals_BuildsCorrectQueryParam(interval: OneHour, expectedInterval: "1h") [7 ms]
2025-12-30T04:02:42.3883206Z   Failed YFinance.NET.Tests.Unit.Utils.SymbolValidatorTests.Sanitize_OnlyInvalidCharacters_ReturnsEmpty [< 1 ms]
2025-12-30T04:02:42.3883715Z   Error Message:
2025-12-30T04:02:42.3883928Z    Expected result to be empty, but found "^".
2025-12-30T04:02:42.3884187Z   Stack Trace:
2025-12-30T04:02:42.3884691Z      at FluentAssertions.Execution.XUnit2TestFramework.Throw(String message)
2025-12-30T04:02:42.3885227Z    at FluentAssertions.Execution.TestFrameworkProvider.Throw(String message)
2025-12-30T04:02:42.3885803Z    at FluentAssertions.Execution.DefaultAssertionStrategy.HandleFailure(String message)
2025-12-30T04:02:42.3886386Z    at FluentAssertions.Execution.AssertionScope.FailWith(Func`1 failReasonFunc)
2025-12-30T04:02:42.3886918Z    at FluentAssertions.Execution.AssertionScope.FailWith(Func`1 failReasonFunc)
2025-12-30T04:02:42.3887472Z    at FluentAssertions.Execution.AssertionScope.FailWith(String message, Object[] args)
2025-12-30T04:02:42.3888103Z    at FluentAssertions.Primitives.StringAssertions`1.BeEmpty(String because, Object[] becauseArgs)
2025-12-30T04:02:42.3889175Z    at YFinance.NET.Tests.Unit.Utils.SymbolValidatorTests.Sanitize_OnlyInvalidCharacters_ReturnsEmpty() in /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Utils/SymbolValidatorTests.cs:line 403
2025-12-30T04:02:42.3890214Z    at System.Reflection.MethodBaseInvoker.InterpretedInvoke_Method(Object obj, IntPtr* args)
2025-12-30T04:02:42.3890866Z    at System.Reflection.MethodBaseInvoker.InvokeWithNoArgs(Object obj, BindingFlags invokeAttr)
2025-12-30T04:02:42.3891561Z   Passed YFinance.NET.Tests.Unit.Utils.SymbolValidatorTests.IsValid_ExactlyMaxLength_ReturnsTrue [< 1 ms]
2025-12-30T04:02:42.3892451Z   Failed YFinance.NET.Tests.Unit.Utils.SymbolValidatorTests.ValidateAndThrow_PathTraversal_ThrowsArgumentException(symbol: "AAPL/../admin") [1 ms]
2025-12-30T04:02:42.3893090Z   Error Message:
2025-12-30T04:02:42.3893874Z    Expected exception message to match the equivalent of "Symbol contains path traversal sequences*", but "Symbol contains invalid characters. Only alphanumeric characters and .-^=_ are allowed. (Parameter 'symbol')" does not.
2025-12-30T04:02:42.3894808Z 
2025-12-30T04:02:42.3894881Z   Stack Trace:
2025-12-30T04:02:42.3895202Z      at FluentAssertions.Execution.XUnit2TestFramework.Throw(String message)
2025-12-30T04:02:42.3895711Z    at FluentAssertions.Execution.TestFrameworkProvider.Throw(String message)
2025-12-30T04:02:42.3896487Z    at FluentAssertions.Execution.CollectingAssertionStrategy.ThrowIfAny(IDictionary`2 context)
2025-12-30T04:02:42.3897032Z    at FluentAssertions.Execution.AssertionScope.Dispose()
2025-12-30T04:02:42.3897928Z    at FluentAssertions.Specialized.ExceptionAssertions`1.ExceptionMessageAssertion.Execute(IEnumerable`1 messages, String expectation, String because, Object[] becauseArgs)
2025-12-30T04:02:42.3899117Z    at FluentAssertions.Specialized.ExceptionAssertions`1.ExceptionMessageAssertion.Execute(IEnumerable`1 messages, String expectation, String because, Object[] becauseArgs)
2025-12-30T04:02:42.3900219Z    at FluentAssertions.Specialized.ExceptionAssertions`1.WithMessage(String expectedWildcardPattern, String because, Object[] becauseArgs)
2025-12-30T04:02:42.3901527Z    at YFinance.NET.Tests.Unit.Utils.SymbolValidatorTests.ValidateAndThrow_PathTraversal_ThrowsArgumentException(String symbol) in /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Utils/SymbolValidatorTests.cs:line 201
2025-12-30T04:02:42.3902671Z    at System.Reflection.MethodBaseInvoker.InterpretedInvoke_Method(Object obj, IntPtr* args)
2025-12-30T04:02:42.3903424Z    at System.Reflection.MethodBaseInvoker.InvokeDirectByRefWithFewArgs(Object obj, Span`1 copyOfArgs, BindingFlags invokeAttr)
2025-12-30T04:02:42.3904988Z [xUnit.net 00:00:00.72]       Expected exception message to match the equivalent of "Symbol contains path traversal sequences*", but "Symbol contains invalid characters. Only alphanumeric characters and .-^=_ are allowed. (Parameter 'symbol')" does not.
2025-12-30T04:02:42.3905910Z [xUnit.net 00:00:00.72]       
2025-12-30T04:02:42.3906172Z [xUnit.net 00:00:00.72]       Stack Trace:
2025-12-30T04:02:42.3906684Z [xUnit.net 00:00:00.72]            at FluentAssertions.Execution.XUnit2TestFramework.Throw(String message)
2025-12-30T04:02:42.3907383Z [xUnit.net 00:00:00.72]            at FluentAssertions.Execution.TestFrameworkProvider.Throw(String message)
2025-12-30T04:02:42.3908181Z [xUnit.net 00:00:00.72]            at FluentAssertions.Execution.CollectingAssertionStrategy.ThrowIfAny(IDictionary`2 context)
2025-12-30T04:02:42.3908892Z [xUnit.net 00:00:00.72]            at FluentAssertions.Execution.AssertionScope.Dispose()
2025-12-30T04:02:42.3909920Z [xUnit.net 00:00:00.72]            at FluentAssertions.Specialized.ExceptionAssertions`1.ExceptionMessageAssertion.Execute(IEnumerable`1 messages, String expectation, String because, Object[] becauseArgs)
2025-12-30T04:02:42.3911333Z [xUnit.net 00:00:00.72]            at FluentAssertions.Specialized.ExceptionAssertions`1.ExceptionMessageAssertion.Execute(IEnumerable`1 messages, String expectation, String because, Object[] becauseArgs)
2025-12-30T04:02:42.3912599Z [xUnit.net 00:00:00.72]            at FluentAssertions.Specialized.ExceptionAssertions`1.WithMessage(String expectedWildcardPattern, String because, Object[] becauseArgs)
2025-12-30T04:02:42.3914209Z [xUnit.net 00:00:00.72]         /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Utils/SymbolValidatorTests.cs(201,0): at YFinance.NET.Tests.Unit.Utils.SymbolValidatorTests.ValidateAndThrow_PathTraversal_ThrowsArgumentException(String symbol)
2025-12-30T04:02:42.3915814Z [xUnit.net 00:00:00.72]            at InvokeStub_SymbolValidatorTests.ValidateAndThrow_PathTraversal_ThrowsArgumentException(Object, Span`1)
2025-12-30T04:02:42.3916937Z [xUnit.net 00:00:00.73]            at System.Reflection.MethodBaseInvoker.InvokeWithOneArg(Object obj, BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)
2025-12-30T04:02:42.3918083Z   Failed YFinance.NET.Tests.Unit.Utils.SymbolValidatorTests.ValidateAndThrow_PathTraversal_ThrowsArgumentException(symbol: "../../../etc/passwd") [< 1 ms]
2025-12-30T04:02:42.3918756Z   Error Message:
2025-12-30T04:02:42.3919539Z    Expected exception message to match the equivalent of "Symbol contains path traversal sequences*", but "Symbol contains invalid characters. Only alphanumeric characters and .-^=_ are allowed. (Parameter 'symbol')" does not.
2025-12-30T04:02:42.3920872Z 
2025-12-30T04:02:42.3921219Z   Stack Trace:
2025-12-30T04:02:42.3921855Z      at FluentAssertions.Execution.XUnit2TestFramework.Throw(String message)
2025-12-30T04:02:42.3923076Z    at FluentAssertions.Execution.TestFrameworkProvider.Throw(String message)
2025-12-30T04:02:42.3924216Z    at FluentAssertions.Execution.CollectingAssertionStrategy.ThrowIfAny(IDictionary`2 context)
2025-12-30T04:02:42.3925456Z    at FluentAssertions.Execution.AssertionScope.Dispose()
2025-12-30T04:02:42.3926968Z    at FluentAssertions.Specialized.ExceptionAssertions`1.ExceptionMessageAssertion.Execute(IEnumerable`1 messages, String expectation, String because, Object[] becauseArgs)
2025-12-30T04:02:42.3929266Z    at FluentAssertions.Specialized.ExceptionAssertions`1.ExceptionMessageAssertion.Execute(IEnumerable`1 messages, String expectation, String because, Object[] becauseArgs)
2025-12-30T04:02:42.3931372Z    at FluentAssertions.Specialized.ExceptionAssertions`1.WithMessage(String expectedWildcardPattern, String because, Object[] becauseArgs)
2025-12-30T04:02:42.3933933Z    at YFinance.NET.Tests.Unit.Utils.SymbolValidatorTests.ValidateAndThrow_PathTraversal_ThrowsArgumentException(String symbol) in /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Utils/SymbolValidatorTests.cs:line 201
2025-12-30T04:02:42.3936390Z    at InvokeStub_SymbolValidatorTests.ValidateAndThrow_PathTraversal_ThrowsArgumentException(Object, Span`1)
2025-12-30T04:02:42.3938110Z    at System.Reflection.MethodBaseInvoker.InvokeWithOneArg(Object obj, BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)
2025-12-30T04:02:42.3940294Z   Passed YFinance.NET.Tests.Unit.Scrapers.HistoryScraperTests.GetHistoryAsync_VariousIntervals_BuildsCorrectQueryParam(interval: FiveDays, expectedInterval: "5d") [2 ms]
2025-12-30T04:02:42.3942064Z   Passed YFinance.NET.Tests.Unit.Utils.SymbolValidatorTests.IsValid_TooLongSymbol_ReturnsFalse [< 1 ms]
2025-12-30T04:02:42.3943545Z   Passed YFinance.NET.Tests.Unit.Utils.SymbolValidatorTests.Sanitize_OnlyLeadingInvalidChars_ReturnsEmpty [< 1 ms]
2025-12-30T04:02:42.3945305Z   Passed YFinance.NET.Tests.Unit.Services.RateLimitServiceTests.IsRateLimited_StatusCode429_ReturnsTrue [< 1 ms]
2025-12-30T04:02:42.3947297Z   Passed YFinance.NET.Tests.Unit.Scrapers.HistoryScraperTests.GetHistoryAsync_VariousIntervals_BuildsCorrectQueryParam(interval: TwoMinutes, expectedInterval: "2m") [6 ms]
2025-12-30T04:02:42.3949742Z   Passed YFinance.NET.Tests.Unit.Scrapers.HistoryScraperTests.GetHistoryAsync_VariousIntervals_BuildsCorrectQueryParam(interval: SixtyMinutes, expectedInterval: "60m") [6 ms]
2025-12-30T04:02:42.3952140Z   Passed YFinance.NET.Tests.Unit.Scrapers.HistoryScraperTests.GetHistoryAsync_VariousIntervals_BuildsCorrectQueryParam(interval: FiveMinutes, expectedInterval: "5m") [8 ms]
2025-12-30T04:02:42.3954762Z   Passed YFinance.NET.Tests.Unit.Scrapers.HistoryScraperTests.GetHistoryAsync_VariousIntervals_BuildsCorrectQueryParam(interval: OneMinute, expectedInterval: "1m") [4 ms]
2025-12-30T04:02:42.3957149Z   Passed YFinance.NET.Tests.Unit.Scrapers.HistoryScraperTests.GetHistoryAsync_VariousIntervals_BuildsCorrectQueryParam(interval: OneDay, expectedInterval: "1d") [3 ms]
2025-12-30T04:02:42.3959450Z   Passed YFinance.NET.Tests.Unit.Scrapers.HistoryScraperTests.GetHistoryAsync_VariousIntervals_BuildsCorrectQueryParam(interval: NinetyMinutes, expectedInterval: "90m") [4 ms]
2025-12-30T04:02:42.3961502Z   Passed YFinance.NET.Tests.Unit.Scrapers.HoldersScraperTests.GetHoldersAsync_ValidResponse_ReturnsHoldersData [42 ms]
2025-12-30T04:02:42.3963161Z   Passed YFinance.NET.Tests.Unit.Scrapers.FundsScraperTests.Constructor_NullDataParser_ThrowsArgumentNullException [1 ms]
2025-12-30T04:02:42.3965418Z   Passed YFinance.NET.Tests.Unit.Scrapers.HistoryScraperTests.GetHistoryAsync_VariousIntervals_BuildsCorrectQueryParam(interval: ThirtyMinutes, expectedInterval: "30m") [2 ms]
2025-12-30T04:02:42.3967555Z   Passed YFinance.NET.Tests.Unit.Scrapers.HistoryScraperTests.GetHistoryAsync_WithStartEndDates_BuildsUnixTimestamps [4 ms]
2025-12-30T04:02:42.4047781Z [xUnit.net 00:00:00.77]     YFinance.NET.Tests.Unit.Scrapers.HistoryScraperTests.GetHistoryAsync_InvalidSymbol_ThrowsArgumentException(invalidSymbol: "   ") [FAIL]
2025-12-30T04:02:42.4049479Z [xUnit.net 00:00:00.77]       Assert.Throws() Failure: Exception type was not an exact match
2025-12-30T04:02:42.4050325Z [xUnit.net 00:00:00.77]       Expected: typeof(System.ArgumentException)
2025-12-30T04:02:42.4052289Z [xUnit.net 00:00:00.77]       Actual:   typeof(System.ArgumentNullException)
2025-12-30T04:02:42.4054052Z [xUnit.net 00:00:00.77]       ---- System.ArgumentNullException : Value cannot be null. (Parameter 'json')
2025-12-30T04:02:42.4112104Z [xUnit.net 00:00:00.77]       Stack Trace:
2025-12-30T04:02:42.4121511Z [xUnit.net 00:00:00.78]         /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/HistoryScraperTests.cs(380,0): at YFinance.NET.Tests.Unit.Scrapers.HistoryScraperTests.GetHistoryAsync_InvalidSymbol_ThrowsArgumentException(String invalidSymbol)
2025-12-30T04:02:42.4123813Z [xUnit.net 00:00:00.78]         --- End of stack trace from previous location ---
2025-12-30T04:02:42.4124951Z [xUnit.net 00:00:00.78]         ----- Inner Stack Trace -----
2025-12-30T04:02:42.4131731Z [xUnit.net 00:00:00.78]     YFinance.NET.Tests.Unit.Scrapers.HistoryScraperTests.GetHistoryAsync_InvalidSymbol_ThrowsArgumentException(invalidSymbol: null) [FAIL]
2025-12-30T04:02:42.4133405Z [xUnit.net 00:00:00.78]            at System.ArgumentNullException.Throw(String paramName)
2025-12-30T04:02:42.4134835Z [xUnit.net 00:00:00.78]            at System.ArgumentNullException.ThrowIfNull(Object argument, String paramName)
2025-12-30T04:02:42.4136210Z [xUnit.net 00:00:00.78]            at System.Text.Json.JsonDocument.Parse(String json, JsonDocumentOptions options)
2025-12-30T04:02:42.4138968Z [xUnit.net 00:00:00.78]         /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/HistoryScraper.cs(149,0): at YFinance.NET.Implementation.Scrapers.HistoryScraper.ParseHistoricalData(String symbol, String jsonResponse, HistoryRequest request)
2025-12-30T04:02:42.4142989Z [xUnit.net 00:00:00.78]         /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/HistoryScraper.cs(61,0): at YFinance.NET.Implementation.Scrapers.HistoryScraper.GetHistoryAsync(String symbol, HistoryRequest request, CancellationToken cancellationToken)
2025-12-30T04:02:42.4170268Z [xUnit.net 00:00:00.78]     YFinance.NET.Tests.Unit.Scrapers.HistoryScraperTests.GetHistoryAsync_InvalidSymbol_ThrowsArgumentException(invalidSymbol: "") [FAIL]
2025-12-30T04:02:42.4171953Z [xUnit.net 00:00:00.78]       Assert.Throws() Failure: Exception type was not an exact match
2025-12-30T04:02:42.4172959Z [xUnit.net 00:00:00.78]       Expected: typeof(System.ArgumentException)
2025-12-30T04:02:42.4173918Z [xUnit.net 00:00:00.78]       Actual:   typeof(System.ArgumentNullException)
2025-12-30T04:02:42.4175250Z [xUnit.net 00:00:00.78]       ---- System.ArgumentNullException : Value cannot be null. (Parameter 'json')
2025-12-30T04:02:42.4176177Z [xUnit.net 00:00:00.78]       Stack Trace:
2025-12-30T04:02:42.4178517Z [xUnit.net 00:00:00.78]         /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/HistoryScraperTests.cs(380,0): at YFinance.NET.Tests.Unit.Scrapers.HistoryScraperTests.GetHistoryAsync_InvalidSymbol_ThrowsArgumentException(String invalidSymbol)
2025-12-30T04:02:42.4180833Z [xUnit.net 00:00:00.78]         --- End of stack trace from previous location ---
2025-12-30T04:02:42.4181646Z [xUnit.net 00:00:00.78]         ----- Inner Stack Trace -----
2025-12-30T04:02:42.4182529Z [xUnit.net 00:00:00.78]            at System.ArgumentNullException.Throw(String paramName)
2025-12-30T04:02:42.4183742Z [xUnit.net 00:00:00.78]            at System.ArgumentNullException.ThrowIfNull(Object argument, String paramName)
2025-12-30T04:02:42.4185354Z [xUnit.net 00:00:00.78]            at System.Text.Json.JsonDocument.Parse(String json, JsonDocumentOptions options)
2025-12-30T04:02:42.4188381Z [xUnit.net 00:00:00.78]         /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/HistoryScraper.cs(149,0): at YFinance.NET.Implementation.Scrapers.HistoryScraper.ParseHistoricalData(String symbol, String jsonResponse, HistoryRequest request)
2025-12-30T04:02:42.4192507Z [xUnit.net 00:00:00.78]         /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/HistoryScraper.cs(61,0): at YFinance.NET.Implementation.Scrapers.HistoryScraper.GetHistoryAsync(String symbol, HistoryRequest request, CancellationToken cancellationToken)
2025-12-30T04:02:42.4195202Z [xUnit.net 00:00:00.78]       Assert.Throws() Failure: Exception type was not an exact match
2025-12-30T04:02:42.4196173Z [xUnit.net 00:00:00.78]       Expected: typeof(System.ArgumentException)
2025-12-30T04:02:42.4197009Z [xUnit.net 00:00:00.78]       Actual:   typeof(System.ArgumentNullException)
2025-12-30T04:02:42.4198082Z [xUnit.net 00:00:00.78]       ---- System.ArgumentNullException : Value cannot be null. (Parameter 'json')
2025-12-30T04:02:42.4198958Z [xUnit.net 00:00:00.78]       Stack Trace:
2025-12-30T04:02:42.4201265Z [xUnit.net 00:00:00.78]         /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/HistoryScraperTests.cs(380,0): at YFinance.NET.Tests.Unit.Scrapers.HistoryScraperTests.GetHistoryAsync_InvalidSymbol_ThrowsArgumentException(String invalidSymbol)
2025-12-30T04:02:42.4203535Z [xUnit.net 00:00:00.78]         --- End of stack trace from previous location ---
2025-12-30T04:02:42.4204713Z [xUnit.net 00:00:00.78]         ----- Inner Stack Trace -----
2025-12-30T04:02:42.4205637Z [xUnit.net 00:00:00.78]            at System.ArgumentNullException.Throw(String paramName)
2025-12-30T04:02:42.4206864Z [xUnit.net 00:00:00.78]            at System.ArgumentNullException.ThrowIfNull(Object argument, String paramName)
2025-12-30T04:02:42.4208371Z [xUnit.net 00:00:00.78]            at System.Text.Json.JsonDocument.Parse(String json, JsonDocumentOptions options)
2025-12-30T04:02:42.4211192Z [xUnit.net 00:00:00.78]         /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/HistoryScraper.cs(149,0): at YFinance.NET.Implementation.Scrapers.HistoryScraper.ParseHistoricalData(String symbol, String jsonResponse, HistoryRequest request)
2025-12-30T04:02:42.4215356Z [xUnit.net 00:00:00.78]         /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/HistoryScraper.cs(61,0): at YFinance.NET.Implementation.Scrapers.HistoryScraper.GetHistoryAsync(String symbol, HistoryRequest request, CancellationToken cancellationToken)
2025-12-30T04:02:42.4232910Z   Passed YFinance.NET.Tests.Unit.Scrapers.HistoryScraperTests.GetHistoryAsync_ValidResponse_ParsesCorrectly [14 ms]
2025-12-30T04:02:42.4236267Z   Passed YFinance.NET.Tests.Unit.TickerServiceTests.GetEsgAsync_DelegatesToEsgScraper [29 ms]
2025-12-30T04:02:42.4240674Z   Passed YFinance.NET.Tests.Unit.Scrapers.HistoryScraperTests.GetHistoryAsync_EmptyResponse_ReturnsEmptyData [5 ms]
2025-12-30T04:02:42.4242062Z   Passed YFinance.NET.Tests.Unit.TickerServiceTests.GetNewsAsync_DelegatesToNewsScraper [6 ms]
2025-12-30T04:02:42.4243632Z   Failed YFinance.NET.Tests.Unit.Scrapers.HistoryScraperTests.GetHistoryAsync_InvalidSymbol_ThrowsArgumentException(invalidSymbol: "   ") [1 ms]
2025-12-30T04:02:42.4245066Z   Error Message:
2025-12-30T04:02:42.4245527Z    Assert.Throws() Failure: Exception type was not an exact match
2025-12-30T04:02:42.4246742Z Expected: typeof(System.ArgumentException)
2025-12-30T04:02:42.4247455Z Actual:   typeof(System.ArgumentNullException)
2025-12-30T04:02:42.4248302Z ---- System.ArgumentNullException : Value cannot be null. (Parameter 'json')
2025-12-30T04:02:42.4249012Z   Stack Trace:
2025-12-30T04:02:42.4251047Z      at YFinance.NET.Tests.Unit.Scrapers.HistoryScraperTests.GetHistoryAsync_InvalidSymbol_ThrowsArgumentException(String invalidSymbol) in /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/HistoryScraperTests.cs:line 380
2025-12-30T04:02:42.4253075Z --- End of stack trace from previous location ---
2025-12-30T04:02:42.4253630Z ----- Inner Stack Trace -----
2025-12-30T04:02:42.4254571Z    at System.ArgumentNullException.Throw(String paramName)
2025-12-30T04:02:42.4255430Z    at System.ArgumentNullException.ThrowIfNull(Object argument, String paramName)
2025-12-30T04:02:42.4256424Z    at System.Text.Json.JsonDocument.Parse(String json, JsonDocumentOptions options)
2025-12-30T04:02:42.4258637Z    at YFinance.NET.Implementation.Scrapers.HistoryScraper.ParseHistoricalData(String symbol, String jsonResponse, HistoryRequest request) in /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/HistoryScraper.cs:line 149
2025-12-30T04:02:42.4262070Z    at YFinance.NET.Implementation.Scrapers.HistoryScraper.GetHistoryAsync(String symbol, HistoryRequest request, CancellationToken cancellationToken) in /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/HistoryScraper.cs:line 61
2025-12-30T04:02:42.4265078Z   Failed YFinance.NET.Tests.Unit.Scrapers.HistoryScraperTests.GetHistoryAsync_InvalidSymbol_ThrowsArgumentException(invalidSymbol: null) [7 ms]
2025-12-30T04:02:42.4266288Z   Error Message:
2025-12-30T04:02:42.4266795Z    Assert.Throws() Failure: Exception type was not an exact match
2025-12-30T04:02:42.4267520Z Expected: typeof(System.ArgumentException)
2025-12-30T04:02:42.4268154Z Actual:   typeof(System.ArgumentNullException)
2025-12-30T04:02:42.4269062Z ---- System.ArgumentNullException : Value cannot be null. (Parameter 'json')
2025-12-30T04:02:42.4269752Z   Stack Trace:
2025-12-30T04:02:42.4271390Z      at YFinance.NET.Tests.Unit.Scrapers.HistoryScraperTests.GetHistoryAsync_InvalidSymbol_ThrowsArgumentException(String invalidSymbol) in /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/HistoryScraperTests.cs:line 380
2025-12-30T04:02:42.4273483Z --- End of stack trace from previous location ---
2025-12-30T04:02:42.4274048Z ----- Inner Stack Trace -----
2025-12-30T04:02:42.4274847Z    at System.ArgumentNullException.Throw(String paramName)
2025-12-30T04:02:42.4275715Z    at System.ArgumentNullException.ThrowIfNull(Object argument, String paramName)
2025-12-30T04:02:42.4276765Z    at System.Text.Json.JsonDocument.Parse(String json, JsonDocumentOptions options)
2025-12-30T04:02:42.4278980Z    at YFinance.NET.Implementation.Scrapers.HistoryScraper.ParseHistoricalData(String symbol, String jsonResponse, HistoryRequest request) in /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/HistoryScraper.cs:line 149
2025-12-30T04:02:42.4282470Z    at YFinance.NET.Implementation.Scrapers.HistoryScraper.GetHistoryAsync(String symbol, HistoryRequest request, CancellationToken cancellationToken) in /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/HistoryScraper.cs:line 61
2025-12-30T04:02:42.4285161Z   Passed YFinance.NET.Tests.Unit.TickerServiceTests.GetQuoteAsync_DelegatesToQuoteScraper [9 ms]
2025-12-30T04:02:42.4286852Z   Failed YFinance.NET.Tests.Unit.Scrapers.HistoryScraperTests.GetHistoryAsync_InvalidSymbol_ThrowsArgumentException(invalidSymbol: "") [1 ms]
2025-12-30T04:02:42.4288114Z   Error Message:
2025-12-30T04:02:42.4288625Z    Assert.Throws() Failure: Exception type was not an exact match
2025-12-30T04:02:42.4289327Z Expected: typeof(System.ArgumentException)
2025-12-30T04:02:42.4289951Z Actual:   typeof(System.ArgumentNullException)
2025-12-30T04:02:42.4290772Z ---- System.ArgumentNullException : Value cannot be null. (Parameter 'json')
2025-12-30T04:02:42.4291474Z   Stack Trace:
2025-12-30T04:02:42.4293246Z      at YFinance.NET.Tests.Unit.Scrapers.HistoryScraperTests.GetHistoryAsync_InvalidSymbol_ThrowsArgumentException(String invalidSymbol) in /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/HistoryScraperTests.cs:line 380
2025-12-30T04:02:42.4309298Z --- End of stack trace from previous location ---
2025-12-30T04:02:42.4309934Z ----- Inner Stack Trace -----
2025-12-30T04:02:42.4310733Z    at System.ArgumentNullException.Throw(String paramName)
2025-12-30T04:02:42.4311631Z    at System.ArgumentNullException.ThrowIfNull(Object argument, String paramName)
2025-12-30T04:02:42.4312921Z    at System.Text.Json.JsonDocument.Parse(String json, JsonDocumentOptions options)
2025-12-30T04:02:42.4316118Z    at YFinance.NET.Implementation.Scrapers.HistoryScraper.ParseHistoricalData(String symbol, String jsonResponse, HistoryRequest request) in /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/HistoryScraper.cs:line 149
2025-12-30T04:02:42.4319542Z    at YFinance.NET.Implementation.Scrapers.HistoryScraper.GetHistoryAsync(String symbol, HistoryRequest request, CancellationToken cancellationToken) in /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/HistoryScraper.cs:line 61
2025-12-30T04:02:42.4376074Z   Passed YFinance.NET.Tests.Unit.TickerServiceTests.GetFundsDataAsync_DelegatesToFundsScraper [3 ms]
2025-12-30T04:02:42.4378811Z   Passed YFinance.NET.Tests.Unit.TickerServiceTests.GetUpgradesDowngradesAsync_DelegatesToAnalysisScraper [2 ms]
2025-12-30T04:02:42.4380524Z   Passed YFinance.NET.Tests.Unit.Scrapers.HistoryScraperTests.GetHistoryAsync_WithoutAdjustedClose_UsesRegularClose [12 ms]
2025-12-30T04:02:42.4382168Z   Passed YFinance.NET.Tests.Unit.Scrapers.FundsScraperTests.GetFundsDataAsync_ValidResponse_ParsesData [55 ms]
2025-12-30T04:02:42.4383753Z   Passed YFinance.NET.Tests.Unit.Scrapers.FundsScraperTests.Constructor_NullClient_ThrowsArgumentNullException [< 1 ms]
2025-12-30T04:02:42.4385547Z   Passed YFinance.NET.Tests.Unit.TickerServiceTests.GetCalendarAsync_DelegatesToCalendarScraper [6 ms]
2025-12-30T04:02:42.4387071Z   Passed YFinance.NET.Tests.Unit.Scrapers.HistoryScraperTests.GetHistoryAsync_WithStockSplits_IncludesSplitData [5 ms]
2025-12-30T04:02:42.4587338Z   Passed YFinance.NET.Tests.Unit.TickerServiceTests.GetRecommendationsAsync_DelegatesToAnalysisScraper [3 ms]
2025-12-30T04:02:42.4593988Z   Passed YFinance.NET.Tests.Unit.Scrapers.HistoryScraperTests.GetHistoryAsync_CancellationRequested_PropagatesToken [2 ms]
2025-12-30T04:02:42.4597069Z   Passed YFinance.NET.Tests.Unit.TickerServiceTests.GetSharesHistoryAsync_DelegatesToSharesScraper [7 ms]
2025-12-30T04:02:42.4598790Z   Passed YFinance.NET.Tests.Unit.Scrapers.SharesScraperTests.GetSharesHistoryAsync_ValidResponse_ReturnsEntries [19 ms]
2025-12-30T04:02:42.4600394Z   Passed YFinance.NET.Tests.Unit.Utils.DataParserTests.ParseLongArray_WithNulls_ReturnsZeroForNulls [1 ms]
2025-12-30T04:02:42.4601838Z   Passed YFinance.NET.Tests.Unit.Utils.DataParserTests.ParseLongArray_ValidData_ReturnsCorrectArray [< 1 ms]
2025-12-30T04:02:42.4603257Z   Passed YFinance.NET.Tests.Unit.Utils.DataParserTests.ExtractDecimal_RawFormat_ReturnsRawValue [< 1 ms]
2025-12-30T04:02:42.4647431Z   Passed YFinance.NET.Tests.Unit.Scrapers.HistoryScraperTests.GetHistoryAsync_WeeklyInterval_ResamplesToWeeklyBars [16 ms]
2025-12-30T04:02:42.4668661Z   Passed YFinance.NET.Tests.Unit.Utils.DataParserTests.ExtractDecimal_StringNumber_ParsesCorrectly [< 1 ms]
2025-12-30T04:02:42.4670079Z   Passed YFinance.NET.Tests.Unit.Utils.DataParserTests.ExtractDecimal_Null_ReturnsNull [< 1 ms]
2025-12-30T04:02:42.4671524Z   Passed YFinance.NET.Tests.Unit.Utils.DataParserTests.UnixTimestampToDateTime_Seconds_ConvertsCorrectly [< 1 ms]
2025-12-30T04:02:42.4673178Z   Passed YFinance.NET.Tests.Unit.Utils.DataParserTests.UnixTimestampToDateTime_Milliseconds_ConvertsCorrectly [< 1 ms]
2025-12-30T04:02:42.4675082Z   Passed YFinance.NET.Tests.Unit.Scrapers.HistoryScraperTests.GetHistoryAsync_AlwaysIncludesDividendsAndSplits [3 ms]
2025-12-30T04:02:42.4676807Z   Passed YFinance.NET.Tests.Unit.Scrapers.HistoryScraperTests.Constructor_NullClient_ThrowsArgumentNullException [1 ms]
2025-12-30T04:02:42.4678397Z   Passed YFinance.NET.Tests.Unit.Utils.DataParserTests.ParseArray_WithDefaultValue_UsesDefaultForNulls [4 ms]
2025-12-30T04:02:42.4796772Z   Passed YFinance.NET.Tests.Unit.Utils.DataParserTests.ParseDecimalArray_MissingProperty_ReturnsEmptyArray [< 1 ms]
2025-12-30T04:02:42.4798424Z   Passed YFinance.NET.Tests.Unit.Scrapers.HistoryScraperTests.GetHistoryAsync_InvalidRequest_ThrowsArgumentException [3 ms]
2025-12-30T04:02:42.4799813Z   Passed YFinance.NET.Tests.Unit.Utils.DataParserTests.Parse_ValidJson_DeserializesCorrectly [7 ms]
2025-12-30T04:02:42.4800574Z   Passed YFinance.NET.Tests.Unit.Scrapers.HistoryScraperTests.GetHistoryAsync_MissingTimezone_DefaultsToUTC [4 ms]
2025-12-30T04:02:42.4801377Z   Passed YFinance.NET.Tests.Unit.Utils.DataParserTests.ParseDecimalArray_WithNulls_ReturnsZeroForNulls [1 ms]
2025-12-30T04:02:42.4802164Z   Passed YFinance.NET.Tests.Unit.Utils.DataParserTests.ParseDecimalArray_EmptyArray_ReturnsEmptyArray [< 1 ms]
2025-12-30T04:02:42.4803032Z   Passed YFinance.NET.Tests.Unit.Scrapers.HistoryScraperTests.Constructor_NullDataParser_ThrowsArgumentNullException [1 ms]
2025-12-30T04:02:42.4803888Z   Passed YFinance.NET.Tests.Unit.Utils.DataParserTests.ParseDecimalArray_ValidData_ReturnsCorrectArray [< 1 ms]
2025-12-30T04:02:42.4804883Z   Passed YFinance.NET.Tests.Unit.Utils.DataParserTests.ParseArray_GenericType_WorksCorrectly [< 1 ms]
2025-12-30T04:02:43.9755171Z   Passed YFinance.NET.Tests.Unit.Utils.DataParserTests.Parse_EmptyString_ReturnsDefault [< 1 ms]
2025-12-30T04:02:43.9756793Z   Passed YFinance.NET.Tests.Unit.Scrapers.OptionsScraperTests.GetOptionChainAsync_WithExpirationDate_UsesDateQueryParam [27 ms]
2025-12-30T04:02:43.9758249Z   Passed YFinance.NET.Tests.Unit.Utils.DataParserTests.ExtractDecimal_DirectNumber_ReturnsValue [< 1 ms]
2025-12-30T04:02:43.9759584Z   Passed YFinance.NET.Tests.Unit.Scrapers.OptionsScraperTests.GetExpirationsAsync_ValidResponse_ReturnsDates [3 ms]
2025-12-30T04:02:43.9760984Z   Passed YFinance.NET.Tests.Unit.Scrapers.OptionsScraperTests.GetOptionChainAsync_ValidResponse_ReturnsChain [2 ms]
2025-12-30T04:02:43.9761847Z   Passed YFinance.NET.Tests.Unit.Services.RateLimitServiceTests.HandleRateLimitAsync_FirstRetry_Delays1Second [1 s]
2025-12-30T04:02:43.9762721Z   Passed YFinance.NET.Tests.Unit.Services.RateLimitServiceTests.IsRateLimited_RateLimitMixedCase_ReturnsTrue [< 1 ms]
2025-12-30T04:02:51.3573937Z   Passed YFinance.NET.Tests.Unit.Services.RateLimitServiceTests.HandleRateLimitAsync_FourthRetry_Delays8Seconds [8 s]
2025-12-30T04:02:52.8582747Z   Passed YFinance.NET.Tests.Unit.Services.RateLimitServiceTests.IsRateLimited_EmptyResponseBody_ReturnsFalse [< 1 ms]
2025-12-30T04:02:52.8584823Z   Passed YFinance.NET.Tests.Unit.Services.RateLimitServiceTests.IsRateLimited_ResponseContainsRateLimit_ReturnsTrue [< 1 ms]
2025-12-30T04:02:52.8586447Z   Passed YFinance.NET.Tests.Unit.Services.RateLimitServiceTests.IsRateLimited_CaseInsensitive_ReturnsTrue [< 1 ms]
2025-12-30T04:02:52.8587394Z   Passed YFinance.NET.Tests.Unit.Services.RateLimitServiceTests.HandleRateLimitAsync_FifthRetry_ThrowsRateLimitException [< 1 ms]
2025-12-30T04:02:52.8588280Z   Passed YFinance.NET.Tests.Unit.Services.RateLimitServiceTests.IsRateLimited_Status404_ReturnsFalse [< 1 ms]
2025-12-30T04:02:52.8589189Z   Passed YFinance.NET.Tests.Unit.Services.RateLimitServiceTests.IsRateLimited_ResponseContainsTooManyRequests_ReturnsTrue [< 1 ms]
2025-12-30T04:02:52.8590094Z   Passed YFinance.NET.Tests.Unit.Services.RateLimitServiceTests.IsRateLimited_NullResponseBody_ReturnsFalse [< 1 ms]
2025-12-30T04:02:52.8590920Z   Passed YFinance.NET.Tests.Unit.Services.RateLimitServiceTests.IsRateLimited_NormalResponse_ReturnsFalse [< 1 ms]
2025-12-30T04:02:55.3609127Z   Passed YFinance.NET.Tests.Unit.Services.RateLimitServiceTests.HandleRateLimitAsync_ThirdRetry_Delays4Seconds [4 s]
2025-12-30T04:02:56.8615868Z   Passed YFinance.NET.Tests.Unit.Services.RateLimitServiceTests.HandleRateLimitAsync_MaxRetries_ThrowsRateLimitException [< 1 ms]
2025-12-30T04:02:57.8718518Z [xUnit.net 00:00:16.24]   Finished:    YFinance.NET.Tests
2025-12-30T04:02:57.9273958Z   Passed YFinance.NET.Tests.Unit.Services.RateLimitServiceTests.HandleRateLimitAsync_SecondRetry_Delays2Seconds [2 s]
2025-12-30T04:02:57.9276095Z   Passed YFinance.NET.Tests.Unit.Services.RateLimitServiceTests.HandleRateLimitAsync_CancellationDuringDelay_ThrowsTaskCanceledException [503 ms]
2025-12-30T04:02:57.9277647Z   Passed YFinance.NET.Tests.Unit.Services.RateLimitServiceTests.HandleRateLimitAsync_CancellationRequested_ThrowsTaskCanceledException [< 1 ms]
2025-12-30T04:02:57.9755697Z Results File: /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/TestResults/unit-test-results.trx
2025-12-30T04:02:57.9758732Z 
2025-12-30T04:02:57.9796563Z Test Run Failed.
2025-12-30T04:02:57.9797000Z Total tests: 251
2025-12-30T04:02:57.9797371Z      Passed: 236
2025-12-30T04:02:57.9797742Z      Failed: 15
2025-12-30T04:02:57.9859324Z  Total time: 16.6996 Seconds
2025-12-30T04:02:57.9984699Z      1>Project "/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.sln" (1) is building "/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj" (3) on node 2 (VSTest target(s)).
2025-12-30T04:02:57.9985761Z      3>_VSTestConsole:
2025-12-30T04:02:57.9986133Z          MSB4181: The "VSTestTask" task returned false but did not log an error.
2025-12-30T04:02:58.0023366Z      3>Done Building Project "/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj" (VSTest target(s)) -- FAILED.
2025-12-30T04:02:58.0103413Z      1>Done Building Project "/home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.sln" (VSTest target(s)) -- FAILED.
2025-12-30T04:02:58.0230879Z 
2025-12-30T04:02:58.0232258Z Build FAILED.
2025-12-30T04:02:58.0232631Z     0 Warning(s)
2025-12-30T04:02:58.0232955Z     0 Error(s)
2025-12-30T04:02:58.0239931Z 
2025-12-30T04:02:58.0244818Z Time Elapsed 00:00:18.56
2025-12-30T04:02:58.0472367Z ##[error]Process completed with exit code 1.
2025-12-30T04:02:58.0553284Z ##[group]Run actions/upload-artifact@v4
2025-12-30T04:02:58.0553571Z with:
2025-12-30T04:02:58.0553765Z   name: test-results
2025-12-30T04:02:58.0553980Z   path: **/TestResults/*.trx

2025-12-30T04:02:58.0554220Z   retention-days: 30
2025-12-30T04:02:58.0554654Z   if-no-files-found: warn
2025-12-30T04:02:58.0554874Z   compression-level: 6
2025-12-30T04:02:58.0555082Z   overwrite: false
2025-12-30T04:02:58.0555299Z   include-hidden-files: false
2025-12-30T04:02:58.0555520Z env:
2025-12-30T04:02:58.0555695Z   DOTNET_ROOT: /usr/share/dotnet
2025-12-30T04:02:58.0555930Z ##[endgroup]
2025-12-30T04:02:58.3263144Z With the provided path, there will be 1 file uploaded
2025-12-30T04:02:58.3269328Z Artifact name is valid!
2025-12-30T04:02:58.3270315Z Root directory input is valid!
2025-12-30T04:02:58.4637302Z Beginning upload of artifact content to blob storage
2025-12-30T04:02:58.6325067Z Uploaded bytes 57399
2025-12-30T04:02:58.6702846Z Finished uploading artifact content to blob storage!
2025-12-30T04:02:58.6706735Z SHA256 digest of uploaded artifact zip is 3b5c779449fdb6dd6b9570c1f98b2a7eb5d45fa7a2a326bf3255a60bf0baf201
2025-12-30T04:02:58.6708552Z Finalizing artifact upload
2025-12-30T04:02:58.8082156Z Artifact test-results.zip successfully finalized. Artifact ID 4988550213
2025-12-30T04:02:58.8083532Z Artifact test-results has been successfully uploaded! Final size is 57399 bytes. Artifact ID is 4988550213
2025-12-30T04:02:58.8090001Z Artifact download URL: https://github.com/CalvinPangch/YFinance.NET/actions/runs/20588535708/artifacts/4988550213
2025-12-30T04:02:58.8219218Z ##[group]Run dorny/test-reporter@v1
2025-12-30T04:02:58.8219492Z with:
2025-12-30T04:02:58.8219682Z   name: Test Results
2025-12-30T04:02:58.8219897Z   path: **/TestResults/*.trx
2025-12-30T04:02:58.8220133Z   reporter: dotnet-trx
2025-12-30T04:02:58.8220341Z   fail-on-error: false
2025-12-30T04:02:58.8220565Z   path-replace-backslashes: false
2025-12-30T04:02:58.8220805Z   list-suites: all
2025-12-30T04:02:58.8220997Z   list-tests: all
2025-12-30T04:02:58.8221187Z   max-annotations: 10
2025-12-30T04:02:58.8221385Z   fail-on-empty: true
2025-12-30T04:02:58.8221585Z   only-summary: false
2025-12-30T04:02:58.8221885Z   token: ***
2025-12-30T04:02:58.8222063Z env:
2025-12-30T04:02:58.8222249Z   DOTNET_ROOT: /usr/share/dotnet
2025-12-30T04:02:58.8222490Z ##[endgroup]
2025-12-30T04:02:58.9388703Z Action was triggered by pull_request: using SHA from head of source branch
2025-12-30T04:02:58.9399411Z Check runs will be created with SHA=ca8f4791000271c0b91413537c7203d59cdd81f9
2025-12-30T04:02:58.9402967Z ##[group]Listing all files tracked by git
2025-12-30T04:02:58.9443547Z [command]/usr/bin/git ls-files -z
2025-12-30T04:02:58.9579624Z .github/workflows/ci.yml .github/workflows/claude-auto-fix.yml .github/workflows/claude-code-review.yml .github/workflows/claude.yml .gitignore CLAUDE.md PR_DESCRIPTION.md README.md SECURITY_FIX_SUMMARY.md YFinance.NET.Implementation/CalendarService.cs YFinance.NET.Implementation/Constants/YahooFinanceConstants.cs YFinance.NET.Implementation/DependencyInjection/ServiceCollectionExtensions.cs YFinance.NET.Implementation/DomainService.cs YFinance.NET.Implementation/IsinService.cs YFinance.NET.Implementation/LiveMarketService.cs YFinance.NET.Implementation/MarketService.cs YFinance.NET.Implementation/MultiTickerService.cs YFinance.NET.Implementation/Properties/AssemblyInfo.cs YFinance.NET.Implementation/Scrapers/AnalysisScraper.cs YFinance.NET.Implementation/Scrapers/CalendarScraper.cs YFinance.NET.Implementation/Scrapers/CalendarVisualizationScraper.cs YFinance.NET.Implementation/Scrapers/EarningsScraper.cs YFinance.NET.Implementation/Scrapers/EsgScraper.cs YFinance.NET.Implementation/Scrapers/FastInfoScraper.cs YFinance.NET.Implementation/Scrapers/FundamentalsScraper.cs YFinance.NET.Implementation/Scrapers/FundsScraper.cs YFinance.NET.Implementation/Scrapers/HistoryScraper.cs YFinance.NET.Implementation/Scrapers/HoldersScraper.cs YFinance.NET.Implementation/Scrapers/InfoScraper.cs YFinance.NET.Implementation/Scrapers/LookupScraper.cs YFinance.NET.Implementation/Scrapers/NewsScraper.cs YFinance.NET.Implementation/Scrapers/OptionsScraper.cs YFinance.NET.Implementation/Scrapers/QuoteScraper.cs YFinance.NET.Implementation/Scrapers/ScreenerScraper.cs YFinance.NET.Implementation/Scrapers/SearchScraper.cs YFinance.NET.Implementation/Scrapers/SharesScraper.cs YFinance.NET.Implementation/Services/CacheService.cs YFinance.NET.Implementation/Services/CookieService.cs YFinance.NET.Implementation/Services/RateLimitService.cs YFinance.NET.Implementation/TickerService.cs YFinance.NET.Implementation/Tickers.cs YFinance.NET.Implementation/Utils/DataParser.cs YFinance.NET.Implementation/Utils/JsonElementExtensions.cs YFinance.NET.Implementation/Utils/PriceRepair.cs YFinance.NET.Implementation/Utils/SymbolValidator.cs YFinance.NET.Implementation/Utils/TimezoneHelper.cs YFinance.NET.Implementation/YFinance.NET.Implementation.csproj YFinance.NET.Implementation/YahooFinanceClient.cs YFinance.NET.Interfaces/ICalendarService.cs YFinance.NET.Interfaces/IDomainService.cs YFinance.NET.Interfaces/IIsinService.cs YFinance.NET.Interfaces/ILiveMarketService.cs YFinance.NET.Interfaces/IMarketService.cs YFinance.NET.Interfaces/IMultiTickerService.cs YFinance.NET.Interfaces/ITickerService.cs YFinance.NET.Interfaces/IYahooFinanceClient.cs YFinance.NET.Interfaces/Scrapers/IAnalysisScraper.cs YFinance.NET.Interfaces/Scrapers/ICalendarScraper.cs YFinance.NET.Interfaces/Scrapers/ICalendarVisualizationScraper.cs YFinance.NET.Interfaces/Scrapers/IEarningsScraper.cs YFinance.NET.Interfaces/Scrapers/IEsgScraper.cs YFinance.NET.Interfaces/Scrapers/IFastInfoScraper.cs YFinance.NET.Interfaces/Scrapers/IFundamentalsScraper.cs YFinance.NET.Interfaces/Scrapers/IFundsScraper.cs YFinance.NET.Interfaces/Scrapers/IHistoryScraper.cs YFinance.NET.Interfaces/Scrapers/IHoldersScraper.cs YFinance.NET.Interfaces/Scrapers/IInfoScraper.cs YFinance.NET.Interfaces/Scrapers/ILookupScraper.cs YFinance.NET.Interfaces/Scrapers/INewsScraper.cs YFinance.NET.Interfaces/Scrapers/IOptionsScraper.cs YFinance.NET.Interfaces/Scrapers/IQuoteScraper.cs YFinance.NET.Interfaces/Scrapers/IScreenerScraper.cs YFinance.NET.Interfaces/Scrapers/ISearchScraper.cs YFinance.NET.Interfaces/Scrapers/ISharesScraper.cs YFinance.NET.Interfaces/Services/ICacheService.cs YFinance.NET.Interfaces/Services/ICookieService.cs YFinance.NET.Interfaces/Services/IRateLimitService.cs YFinance.NET.Interfaces/Utils/IDataParser.cs YFinance.NET.Interfaces/Utils/IPriceRepair.cs YFinance.NET.Interfaces/Utils/ISymbolValidator.cs YFinance.NET.Interfaces/Utils/ITimezoneHelper.cs YFinance.NET.Interfaces/YFinance.NET.Interfaces.csproj YFinance.NET.Models/ActionData.cs YFinance.NET.Models/ActionsData.cs YFinance.NET.Models/AnalystData.cs YFinance.NET.Models/CalendarData.cs YFinance.NET.Models/CalendarQuery.cs YFinance.NET.Models/CalendarRequest.cs YFinance.NET.Models/CalendarResult.cs YFinance.NET.Models/DomainData.cs YFinance.NET.Models/EarningsData.cs YFinance.NET.Models/Enums/Interval.cs YFinance.NET.Models/Enums/LookupType.cs YFinance.NET.Models/Enums/Period.cs YFinance.NET.Models/Enums/StatementType.cs YFinance.NET.Models/EsgData.cs YFinance.NET.Models/Exceptions/DataParsingException.cs YFinance.NET.Models/Exceptions/InvalidTickerException.cs YFinance.NET.Models/Exceptions/RateLimitException.cs YFinance.NET.Models/Exceptions/YahooFinanceException.cs YFinance.NET.Models/FastInfo.cs YFinance.NET.Models/FastInfoData.cs YFinance.NET.Models/FinancialStatement.cs YFinance.NET.Models/FundsData.cs YFinance.NET.Models/HistoricalData.cs YFinance.NET.Models/HistoryMetadata.cs YFinance.NET.Models/HolderData.cs YFinance.NET.Models/InfoData.cs YFinance.NET.Models/LivePriceData.cs YFinance.NET.Models/LookupResult.cs YFinance.NET.Models/MajorHoldersData.cs YFinance.NET.Models/NewsItem.cs YFinance.NET.Models/OptionsData.cs YFinance.NET.Models/QuoteData.cs YFinance.NET.Models/RecommendationData.cs YFinance.NET.Models/Requests/ActionsRequest.cs YFinance.NET.Models/Requests/EarningsDatesRequest.cs YFinance.NET.Models/Requests/HistoryRequest.cs YFinance.NET.Models/Requests/LookupRequest.cs YFinance.NET.Models/Requests/NewsRequest.cs YFinance.NET.Models/Requests/OptionChainRequest.cs YFinance.NET.Models/Requests/ScreenerRequest.cs YFinance.NET.Models/Requests/SearchRequest.cs YFinance.NET.Models/Requests/SharesHistoryRequest.cs YFinance.NET.Models/ScreenerPredefinedQueries.cs YFinance.NET.Models/ScreenerQuery.cs YFinance.NET.Models/ScreenerResult.cs YFinance.NET.Models/SearchResult.cs YFinance.NET.Models/SharesData.cs YFinance.NET.Models/YFinance.NET.Models.csproj YFinance.NET.Tests/Integration/TickerServiceIntegrationTests.cs YFinance.NET.Tests/TestFixtures/MockHttpMessageHandler.cs YFinance.NET.Tests/TestFixtures/MockYahooFinanceClient.cs YFinance.NET.Tests/TestFixtures/TestDataBuilder.cs YFinance.NET.Tests/Unit/MultiTickerServiceTests.cs YFinance.NET.Tests/Unit/Scrapers/AnalysisScraperTests.cs YFinance.NET.Tests/Unit/Scrapers/CalendarScraperTests.cs YFinance.NET.Tests/Unit/Scrapers/EarningsScraperTests.cs YFinance.NET.Tests/Unit/Scrapers/EsgScraperTests.cs YFinance.NET.Tests/Unit/Scrapers/FundamentalsScraperTests.cs YFinance.NET.Tests/Unit/Scrapers/FundsScraperTests.cs YFinance.NET.Tests/Unit/Scrapers/HistoryScraperTests.cs YFinance.NET.Tests/Unit/Scrapers/HoldersScraperTests.cs YFinance.NET.Tests/Unit/Scrapers/LookupScraperTests.cs YFinance.NET.Tests/Unit/Scrapers/NewsScraperTests.cs YFinance.NET.Tests/Unit/Scrapers/OptionsScraperTests.cs YFinance.NET.Tests/Unit/Scrapers/QuoteScraperTests.cs YFinance.NET.Tests/Unit/Scrapers/ScreenerScraperTests.cs YFinance.NET.Tests/Unit/Scrapers/SearchScraperTests.cs YFinance.NET.Tests/Unit/Scrapers/SharesScraperTests.cs YFinance.NET.Tests/Unit/Services/RateLimitServiceTests.cs YFinance.NET.Tests/Unit/TickerServiceTests.cs YFinance.NET.Tests/Unit/Utils/DataParserTests.cs YFinance.NET.Tests/Unit/Utils/SymbolValidatorTests.cs YFinance.NET.Tests/Unit/YahooFinanceClientTests.cs YFinance.NET.Tests/YFinance.NET.Tests.csproj YFinance.NET.sln build-errors.md build.log git-push-retry.sh global.json 
2025-12-30T04:02:58.9630840Z ##[endgroup]
2025-12-30T04:02:58.9631285Z Found 161 files tracked by GitHub
2025-12-30T04:02:58.9631831Z Using test report parser 'dotnet-trx'
2025-12-30T04:02:58.9671644Z ##[group]Creating test report Test Results
2025-12-30T04:02:58.9673904Z Processing test results for check run Test Results
2025-12-30T04:02:59.0438641Z Creating check run Test Results
2025-12-30T04:02:59.4728963Z Creating report summary
2025-12-30T04:02:59.4729993Z Generating check run summary
2025-12-30T04:02:59.4756235Z Creating annotations
2025-12-30T04:02:59.4764634Z Updating check run conclusion (success) and output
2025-12-30T04:02:59.9473364Z Check run create response: 200
2025-12-30T04:02:59.9474192Z Check run URL: https://api.github.com/repos/CalvinPangch/YFinance.NET/check-runs/59129545806
2025-12-30T04:02:59.9475311Z Check run HTML: https://github.com/CalvinPangch/YFinance.NET/runs/59129545806
2025-12-30T04:02:59.9482436Z ##[endgroup]
2025-12-30T04:02:59.9640516Z Post job cleanup.
2025-12-30T04:03:00.0583836Z [command]/usr/bin/git version
2025-12-30T04:03:00.0637007Z git version 2.52.0
2025-12-30T04:03:00.0684992Z Temporarily overriding HOME='/home/runner/work/_temp/58a49971-fa15-48b3-b6ed-87ee09868cfa' before making global git config changes
2025-12-30T04:03:00.0686450Z Adding repository directory to the temporary git global config as a safe directory
2025-12-30T04:03:00.0691883Z [command]/usr/bin/git config --global --add safe.directory /home/runner/work/YFinance.NET/YFinance.NET
2025-12-30T04:03:00.0732496Z [command]/usr/bin/git config --local --name-only --get-regexp core\.sshCommand
2025-12-30T04:03:00.0767332Z [command]/usr/bin/git submodule foreach --recursive sh -c "git config --local --name-only --get-regexp 'core\.sshCommand' && git config --local --unset-all 'core.sshCommand' || :"
2025-12-30T04:03:00.1022211Z [command]/usr/bin/git config --local --name-only --get-regexp http\.https\:\/\/github\.com\/\.extraheader
2025-12-30T04:03:00.1043998Z http.https://github.com/.extraheader
2025-12-30T04:03:00.1056836Z [command]/usr/bin/git config --local --unset-all http.https://github.com/.extraheader
2025-12-30T04:03:00.1087110Z [command]/usr/bin/git submodule foreach --recursive sh -c "git config --local --name-only --get-regexp 'http\.https\:\/\/github\.com\/\.extraheader' && git config --local --unset-all 'http.https://github.com/.extraheader' || :"
2025-12-30T04:03:00.1306279Z [command]/usr/bin/git config --local --name-only --get-regexp ^includeIf\.gitdir:
2025-12-30T04:03:00.1336839Z [command]/usr/bin/git submodule foreach --recursive git config --local --show-origin --name-only --get-regexp remote.origin.url
2025-12-30T04:03:00.1660363Z Cleaning up orphan processes
2025-12-30T04:03:00.1943784Z Terminate orphan process: pid (2344) (dotnet)
2025-12-30T04:03:00.1963182Z Terminate orphan process: pid (2345) (dotnet)
2025-12-30T04:03:00.1981756Z Terminate orphan process: pid (2346) (dotnet)
2025-12-30T04:03:00.2005040Z Terminate orphan process: pid (2462) (VBCSCompiler)

```

## Failed Job: Code Coverage

```
2025-12-30T04:02:12.8628487Z Current runner version: '2.330.0'
2025-12-30T04:02:12.8653267Z ##[group]Runner Image Provisioner
2025-12-30T04:02:12.8654029Z Hosted Compute Agent
2025-12-30T04:02:12.8654582Z Version: 20251211.462
2025-12-30T04:02:12.8655256Z Commit: 6cbad8c2bb55d58165063d031ccabf57e2d2db61
2025-12-30T04:02:12.8655926Z Build Date: 2025-12-11T16:28:49Z
2025-12-30T04:02:12.8656576Z Worker ID: {6bc68540-968a-4dd5-9b96-670be6e78c29}
2025-12-30T04:02:12.8657302Z ##[endgroup]
2025-12-30T04:02:12.8657846Z ##[group]Operating System
2025-12-30T04:02:12.8658395Z Ubuntu
2025-12-30T04:02:12.8658907Z 24.04.3
2025-12-30T04:02:12.8659336Z LTS
2025-12-30T04:02:12.8659783Z ##[endgroup]
2025-12-30T04:02:12.8660265Z ##[group]Runner Image
2025-12-30T04:02:12.8660971Z Image: ubuntu-24.04
2025-12-30T04:02:12.8661922Z Version: 20251215.174.1
2025-12-30T04:02:12.8662859Z Included Software: https://github.com/actions/runner-images/blob/ubuntu24/20251215.174/images/ubuntu/Ubuntu2404-Readme.md
2025-12-30T04:02:12.8664466Z Image Release: https://github.com/actions/runner-images/releases/tag/ubuntu24%2F20251215.174
2025-12-30T04:02:12.8665474Z ##[endgroup]
2025-12-30T04:02:12.8666730Z ##[group]GITHUB_TOKEN Permissions
2025-12-30T04:02:12.8668826Z Checks: write
2025-12-30T04:02:12.8669448Z Contents: read
2025-12-30T04:02:12.8669942Z Metadata: read
2025-12-30T04:02:12.8670396Z PullRequests: write
2025-12-30T04:02:12.8670949Z Statuses: write
2025-12-30T04:02:12.8671715Z ##[endgroup]
2025-12-30T04:02:12.8673872Z Secret source: Actions
2025-12-30T04:02:12.8674905Z Prepare workflow directory
2025-12-30T04:02:12.9009710Z Prepare all required actions
2025-12-30T04:02:12.9049906Z Getting action download info
2025-12-30T04:02:13.2780416Z Download action repository 'actions/checkout@v4' (SHA:34e114876b0b11c390a56381ad16ebd13914f8d5)
2025-12-30T04:02:13.6317976Z Download action repository 'actions/setup-dotnet@v4' (SHA:67a3573c9a986a3f9c594539f4ab511d57bb3ce9)
2025-12-30T04:02:14.1439528Z Download action repository 'codecov/codecov-action@v4' (SHA:b9fd7d16f6d7d1b5d2bec1a2887e65ceed900238)
2025-12-30T04:02:14.7290359Z Complete job name: Code Coverage
2025-12-30T04:02:14.8123516Z ##[group]Run actions/checkout@v4
2025-12-30T04:02:14.8124830Z with:
2025-12-30T04:02:14.8125615Z   repository: CalvinPangch/YFinance.NET
2025-12-30T04:02:14.8126950Z   token: ***
2025-12-30T04:02:14.8127673Z   ssh-strict: true
2025-12-30T04:02:14.8128422Z   ssh-user: git
2025-12-30T04:02:14.8129174Z   persist-credentials: true
2025-12-30T04:02:14.8130089Z   clean: true
2025-12-30T04:02:14.8130860Z   sparse-checkout-cone-mode: true
2025-12-30T04:02:14.8131985Z   fetch-depth: 1
2025-12-30T04:02:14.8132721Z   fetch-tags: false
2025-12-30T04:02:14.8133484Z   show-progress: true
2025-12-30T04:02:14.8134253Z   lfs: false
2025-12-30T04:02:14.8134967Z   submodules: false
2025-12-30T04:02:14.8135768Z   set-safe-directory: true
2025-12-30T04:02:14.8136876Z ##[endgroup]
2025-12-30T04:02:14.9305872Z Syncing repository: CalvinPangch/YFinance.NET
2025-12-30T04:02:14.9308546Z ##[group]Getting Git version info
2025-12-30T04:02:14.9310788Z Working directory is '/home/runner/work/YFinance.NET/YFinance.NET'
2025-12-30T04:02:14.9313620Z [command]/usr/bin/git version
2025-12-30T04:02:14.9449806Z git version 2.52.0
2025-12-30T04:02:14.9477394Z ##[endgroup]
2025-12-30T04:02:14.9494206Z Temporarily overriding HOME='/home/runner/work/_temp/31c9ac5b-02e5-4e8c-b3f2-5de217e3a486' before making global git config changes
2025-12-30T04:02:14.9499027Z Adding repository directory to the temporary git global config as a safe directory
2025-12-30T04:02:14.9503528Z [command]/usr/bin/git config --global --add safe.directory /home/runner/work/YFinance.NET/YFinance.NET
2025-12-30T04:02:14.9551733Z Deleting the contents of '/home/runner/work/YFinance.NET/YFinance.NET'
2025-12-30T04:02:14.9555128Z ##[group]Initializing the repository
2025-12-30T04:02:14.9559164Z [command]/usr/bin/git init /home/runner/work/YFinance.NET/YFinance.NET
2025-12-30T04:02:14.9695891Z hint: Using 'master' as the name for the initial branch. This default branch name
2025-12-30T04:02:14.9699521Z hint: will change to "main" in Git 3.0. To configure the initial branch name
2025-12-30T04:02:14.9702750Z hint: to use in all of your new repositories, which will suppress this warning,
2025-12-30T04:02:14.9704257Z hint: call:
2025-12-30T04:02:14.9704992Z hint:
2025-12-30T04:02:14.9705904Z hint: 	git config --global init.defaultBranch <name>
2025-12-30T04:02:14.9707521Z hint:
2025-12-30T04:02:14.9708595Z hint: Names commonly chosen instead of 'master' are 'main', 'trunk' and
2025-12-30T04:02:14.9710497Z hint: 'development'. The just-created branch can be renamed via this command:
2025-12-30T04:02:14.9712410Z hint:
2025-12-30T04:02:14.9713228Z hint: 	git branch -m <name>
2025-12-30T04:02:14.9714075Z hint:
2025-12-30T04:02:14.9715219Z hint: Disable this message with "git config set advice.defaultBranchName false"
2025-12-30T04:02:14.9717233Z Initialized empty Git repository in /home/runner/work/YFinance.NET/YFinance.NET/.git/
2025-12-30T04:02:14.9720474Z [command]/usr/bin/git remote add origin https://github.com/CalvinPangch/YFinance.NET
2025-12-30T04:02:14.9749973Z ##[endgroup]
2025-12-30T04:02:14.9751664Z ##[group]Disabling automatic garbage collection
2025-12-30T04:02:14.9753328Z [command]/usr/bin/git config --local gc.auto 0
2025-12-30T04:02:14.9781750Z ##[endgroup]
2025-12-30T04:02:14.9783014Z ##[group]Setting up auth
2025-12-30T04:02:14.9787699Z [command]/usr/bin/git config --local --name-only --get-regexp core\.sshCommand
2025-12-30T04:02:14.9817367Z [command]/usr/bin/git submodule foreach --recursive sh -c "git config --local --name-only --get-regexp 'core\.sshCommand' && git config --local --unset-all 'core.sshCommand' || :"
2025-12-30T04:02:15.0244934Z [command]/usr/bin/git config --local --name-only --get-regexp http\.https\:\/\/github\.com\/\.extraheader
2025-12-30T04:02:15.0278189Z [command]/usr/bin/git submodule foreach --recursive sh -c "git config --local --name-only --get-regexp 'http\.https\:\/\/github\.com\/\.extraheader' && git config --local --unset-all 'http.https://github.com/.extraheader' || :"
2025-12-30T04:02:15.0512407Z [command]/usr/bin/git config --local --name-only --get-regexp ^includeIf\.gitdir:
2025-12-30T04:02:15.0543317Z [command]/usr/bin/git submodule foreach --recursive git config --local --show-origin --name-only --get-regexp remote.origin.url
2025-12-30T04:02:15.0768870Z [command]/usr/bin/git config --local http.https://github.com/.extraheader AUTHORIZATION: basic ***
2025-12-30T04:02:15.0806713Z ##[endgroup]
2025-12-30T04:02:15.0815022Z ##[group]Fetching the repository
2025-12-30T04:02:15.0817755Z [command]/usr/bin/git -c protocol.version=2 fetch --no-tags --prune --no-recurse-submodules --depth=1 origin +4c92d17c03dd6b3dcd8d205aea181e29ff667a0e:refs/remotes/pull/4/merge
2025-12-30T04:02:15.4566277Z From https://github.com/CalvinPangch/YFinance.NET
2025-12-30T04:02:15.4567310Z  * [new ref]         4c92d17c03dd6b3dcd8d205aea181e29ff667a0e -> pull/4/merge
2025-12-30T04:02:15.4604176Z ##[endgroup]
2025-12-30T04:02:15.4605268Z ##[group]Determining the checkout info
2025-12-30T04:02:15.4606686Z ##[endgroup]
2025-12-30T04:02:15.4612060Z [command]/usr/bin/git sparse-checkout disable
2025-12-30T04:02:15.4657676Z [command]/usr/bin/git config --local --unset-all extensions.worktreeConfig
2025-12-30T04:02:15.4685070Z ##[group]Checking out the ref
2025-12-30T04:02:15.4689234Z [command]/usr/bin/git checkout --progress --force refs/remotes/pull/4/merge
2025-12-30T04:02:15.4815881Z Note: switching to 'refs/remotes/pull/4/merge'.
2025-12-30T04:02:15.4816758Z 
2025-12-30T04:02:15.4817214Z You are in 'detached HEAD' state. You can look around, make experimental
2025-12-30T04:02:15.4818315Z changes and commit them, and you can discard any commits you make in this
2025-12-30T04:02:15.4819501Z state without impacting any branches by switching back to a branch.
2025-12-30T04:02:15.4820245Z 
2025-12-30T04:02:15.4820818Z If you want to create a new branch to retain commits you create, you may
2025-12-30T04:02:15.4822079Z do so (now or later) by using -c with the switch command. Example:
2025-12-30T04:02:15.4823440Z 
2025-12-30T04:02:15.4823957Z   git switch -c <new-branch-name>
2025-12-30T04:02:15.4824422Z 
2025-12-30T04:02:15.4824742Z Or undo this operation with:
2025-12-30T04:02:15.4825119Z 
2025-12-30T04:02:15.4825355Z   git switch -
2025-12-30T04:02:15.4825854Z 
2025-12-30T04:02:15.4826442Z Turn off this advice by setting config variable advice.detachedHead to false
2025-12-30T04:02:15.4827106Z 
2025-12-30T04:02:15.4827816Z HEAD is now at 4c92d17 Merge ca8f4791000271c0b91413537c7203d59cdd81f9 into 06f9addc01c7a2bb4c339c037ebce9b43bf208cf
2025-12-30T04:02:15.4830586Z ##[endgroup]
2025-12-30T04:02:15.4866977Z [command]/usr/bin/git log -1 --format=%H
2025-12-30T04:02:15.4890383Z 4c92d17c03dd6b3dcd8d205aea181e29ff667a0e
2025-12-30T04:02:15.5127958Z ##[group]Run actions/setup-dotnet@v4
2025-12-30T04:02:15.5128473Z with:
2025-12-30T04:02:15.5128770Z   dotnet-version: 10.0.x
2025-12-30T04:02:15.5129266Z   cache: false
2025-12-30T04:02:15.5129579Z ##[endgroup]
2025-12-30T04:02:15.7009529Z [command]/home/runner/work/_actions/actions/setup-dotnet/v4/externals/install-dotnet.sh --skip-non-versioned-files --runtime dotnet --channel LTS
2025-12-30T04:02:16.2237093Z dotnet-install: .NET Core Runtime with version '10.0.1' is already installed.
2025-12-30T04:02:16.2268150Z [command]/home/runner/work/_actions/actions/setup-dotnet/v4/externals/install-dotnet.sh --skip-non-versioned-files --channel 10.0
2025-12-30T04:02:16.6224190Z dotnet-install: .NET Core SDK with version '10.0.101' is already installed.
2025-12-30T04:02:16.6466664Z ##[group]Run dotnet restore
2025-12-30T04:02:16.6467043Z [36;1mdotnet restore[0m
2025-12-30T04:02:16.6506497Z shell: /usr/bin/bash -e {0}
2025-12-30T04:02:16.6506814Z env:
2025-12-30T04:02:16.6507035Z   DOTNET_ROOT: /usr/share/dotnet
2025-12-30T04:02:16.6507308Z ##[endgroup]
2025-12-30T04:02:22.3567265Z   Determining projects to restore...
2025-12-30T04:02:23.3960767Z   Restored /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Models/YFinance.NET.Models.csproj (in 187 ms).
2025-12-30T04:02:25.7388238Z   Restored /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Interfaces/YFinance.NET.Interfaces.csproj (in 195 ms).
2025-12-30T04:02:25.7391604Z   Restored /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/YFinance.NET.Implementation.csproj (in 2.53 sec).
2025-12-30T04:02:25.9018462Z   Restored /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/YFinance.NET.Tests.csproj (in 2.72 sec).
2025-12-30T04:02:25.9445333Z ##[group]Run dotnet test --configuration Release --collect:"XPlat Code Coverage" --results-directory ./coverage
2025-12-30T04:02:25.9446184Z [36;1mdotnet test --configuration Release --collect:"XPlat Code Coverage" --results-directory ./coverage[0m
2025-12-30T04:02:25.9480003Z shell: /usr/bin/bash -e {0}
2025-12-30T04:02:25.9480270Z env:
2025-12-30T04:02:25.9480453Z   DOTNET_ROOT: /usr/share/dotnet
2025-12-30T04:02:25.9480698Z ##[endgroup]
2025-12-30T04:02:27.2371491Z   Determining projects to restore...
2025-12-30T04:02:27.8099549Z   All projects are up-to-date for restore.
2025-12-30T04:02:34.0675070Z   YFinance.NET.Models -> /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Models/bin/Release/net10.0/YFinance.NET.Models.dll
2025-12-30T04:02:34.3610716Z   YFinance.NET.Interfaces -> /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Interfaces/bin/Release/net10.0/YFinance.NET.Interfaces.dll
2025-12-30T04:02:35.9326366Z   YFinance.NET.Implementation -> /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/bin/Release/net10.0/YFinance.NET.Implementation.dll
2025-12-30T04:02:38.0622160Z   YFinance.NET.Tests -> /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/bin/Release/net10.0/YFinance.NET.Tests.dll
2025-12-30T04:02:38.0753005Z Test run for /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/bin/Release/net10.0/YFinance.NET.Tests.dll (.NETCoreApp,Version=v10.0)
2025-12-30T04:02:38.2020690Z VSTest version 18.0.1 (x64)
2025-12-30T04:02:38.2065392Z 
2025-12-30T04:02:38.6198741Z Starting test execution, please wait...
2025-12-30T04:02:38.6927651Z A total of 1 test files matched the specified pattern.
2025-12-30T04:02:41.0325227Z [xUnit.net 00:00:00.69]     YFinance.NET.Tests.Unit.Scrapers.HistoryScraperTests.GetHistoryAsync_InvalidSymbol_ThrowsArgumentException(invalidSymbol: "   ") [FAIL]
2025-12-30T04:02:41.0375667Z [xUnit.net 00:00:00.70]     YFinance.NET.Tests.Unit.Scrapers.HistoryScraperTests.GetHistoryAsync_InvalidSymbol_ThrowsArgumentException(invalidSymbol: null) [FAIL]
2025-12-30T04:02:41.0410267Z [xUnit.net 00:00:00.70]     YFinance.NET.Tests.Unit.Scrapers.HistoryScraperTests.GetHistoryAsync_InvalidSymbol_ThrowsArgumentException(invalidSymbol: "") [FAIL]
2025-12-30T04:02:41.0528987Z   Failed YFinance.NET.Tests.Unit.Scrapers.HistoryScraperTests.GetHistoryAsync_InvalidSymbol_ThrowsArgumentException(invalidSymbol: "   ") [4 ms]
2025-12-30T04:02:41.0530448Z   Error Message:
2025-12-30T04:02:41.0530938Z    Assert.Throws() Failure: Exception type was not an exact match
2025-12-30T04:02:41.0531922Z Expected: typeof(System.ArgumentException)
2025-12-30T04:02:41.0532506Z Actual:   typeof(System.ArgumentNullException)
2025-12-30T04:02:41.0533320Z ---- System.ArgumentNullException : Value cannot be null. (Parameter 'json')
2025-12-30T04:02:41.0542349Z   Stack Trace:
2025-12-30T04:02:41.0544161Z      at YFinance.NET.Tests.Unit.Scrapers.HistoryScraperTests.GetHistoryAsync_InvalidSymbol_ThrowsArgumentException(String invalidSymbol) in /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/HistoryScraperTests.cs:line 380
2025-12-30T04:02:41.0546225Z --- End of stack trace from previous location ---
2025-12-30T04:02:41.0546782Z ----- Inner Stack Trace -----
2025-12-30T04:02:41.0547317Z    at System.ArgumentNullException.Throw(String paramName)
2025-12-30T04:02:41.0548134Z    at System.ArgumentNullException.ThrowIfNull(Object argument, String paramName)
2025-12-30T04:02:41.0549107Z    at System.Text.Json.JsonDocument.Parse(String json, JsonDocumentOptions options)
2025-12-30T04:02:41.0551448Z    at YFinance.NET.Implementation.Scrapers.HistoryScraper.ParseHistoricalData(String symbol, String jsonResponse, HistoryRequest request) in /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/HistoryScraper.cs:line 149
2025-12-30T04:02:41.0555450Z    at YFinance.NET.Implementation.Scrapers.HistoryScraper.GetHistoryAsync(String symbol, HistoryRequest request, CancellationToken cancellationToken) in /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/HistoryScraper.cs:line 61
2025-12-30T04:02:41.0558325Z   Failed YFinance.NET.Tests.Unit.Scrapers.HistoryScraperTests.GetHistoryAsync_InvalidSymbol_ThrowsArgumentException(invalidSymbol: null) [1 ms]
2025-12-30T04:02:41.0559531Z   Error Message:
2025-12-30T04:02:41.0560025Z    Assert.Throws() Failure: Exception type was not an exact match
2025-12-30T04:02:41.0560705Z Expected: typeof(System.ArgumentException)
2025-12-30T04:02:41.0561541Z Actual:   typeof(System.ArgumentNullException)
2025-12-30T04:02:41.0562396Z ---- System.ArgumentNullException : Value cannot be null. (Parameter 'json')
2025-12-30T04:02:41.0563108Z   Stack Trace:
2025-12-30T04:02:41.0564881Z      at YFinance.NET.Tests.Unit.Scrapers.HistoryScraperTests.GetHistoryAsync_InvalidSymbol_ThrowsArgumentException(String invalidSymbol) in /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/HistoryScraperTests.cs:line 380
2025-12-30T04:02:41.0566921Z --- End of stack trace from previous location ---
2025-12-30T04:02:41.0567478Z ----- Inner Stack Trace -----
2025-12-30T04:02:41.0567999Z    at System.ArgumentNullException.Throw(String paramName)
2025-12-30T04:02:41.0568817Z    at System.ArgumentNullException.ThrowIfNull(Object argument, String paramName)
2025-12-30T04:02:41.0569758Z    at System.Text.Json.JsonDocument.Parse(String json, JsonDocumentOptions options)
2025-12-30T04:02:41.0572119Z    at YFinance.NET.Implementation.Scrapers.HistoryScraper.ParseHistoricalData(String symbol, String jsonResponse, HistoryRequest request) in /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/HistoryScraper.cs:line 149
2025-12-30T04:02:41.0575759Z    at YFinance.NET.Implementation.Scrapers.HistoryScraper.GetHistoryAsync(String symbol, HistoryRequest request, CancellationToken cancellationToken) in /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/HistoryScraper.cs:line 61
2025-12-30T04:02:41.0578453Z   Failed YFinance.NET.Tests.Unit.Scrapers.HistoryScraperTests.GetHistoryAsync_InvalidSymbol_ThrowsArgumentException(invalidSymbol: "") [1 ms]
2025-12-30T04:02:41.0579650Z   Error Message:
2025-12-30T04:02:41.0580132Z    Assert.Throws() Failure: Exception type was not an exact match
2025-12-30T04:02:41.0580799Z Expected: typeof(System.ArgumentException)
2025-12-30T04:02:41.0581645Z Actual:   typeof(System.ArgumentNullException)
2025-12-30T04:02:41.0582435Z ---- System.ArgumentNullException : Value cannot be null. (Parameter 'json')
2025-12-30T04:02:41.0583089Z   Stack Trace:
2025-12-30T04:02:41.0584823Z      at YFinance.NET.Tests.Unit.Scrapers.HistoryScraperTests.GetHistoryAsync_InvalidSymbol_ThrowsArgumentException(String invalidSymbol) in /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/HistoryScraperTests.cs:line 380
2025-12-30T04:02:41.0586828Z --- End of stack trace from previous location ---
2025-12-30T04:02:41.0587383Z ----- Inner Stack Trace -----
2025-12-30T04:02:41.0587920Z    at System.ArgumentNullException.Throw(String paramName)
2025-12-30T04:02:41.0588759Z    at System.ArgumentNullException.ThrowIfNull(Object argument, String paramName)
2025-12-30T04:02:41.0589752Z    at System.Text.Json.JsonDocument.Parse(String json, JsonDocumentOptions options)
2025-12-30T04:02:41.0592126Z    at YFinance.NET.Implementation.Scrapers.HistoryScraper.ParseHistoricalData(String symbol, String jsonResponse, HistoryRequest request) in /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/HistoryScraper.cs:line 149
2025-12-30T04:02:41.0595571Z    at YFinance.NET.Implementation.Scrapers.HistoryScraper.GetHistoryAsync(String symbol, HistoryRequest request, CancellationToken cancellationToken) in /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/HistoryScraper.cs:line 61
2025-12-30T04:02:41.0763185Z [xUnit.net 00:00:00.74]     YFinance.NET.Tests.Unit.Utils.SymbolValidatorTests.Sanitize_DangerousCharacters_RemovesCharacters(input: "AAPL?admin=true", expected: "AAPLadmintrue") [FAIL]
2025-12-30T04:02:41.0805137Z [xUnit.net 00:00:00.74]     YFinance.NET.Tests.Unit.Utils.SymbolValidatorTests.Sanitize_DangerousCharacters_RemovesCharacters(input: "AAPL&foo=bar", expected: "AAPLfoobar") [FAIL]
2025-12-30T04:02:41.0890125Z [xUnit.net 00:00:00.75]     YFinance.NET.Tests.Unit.Utils.SymbolValidatorTests.Sanitize_DangerousCharacters_RemovesCharacters(input: "AAPL/../../etc", expected: "AAPLetc") [FAIL]
2025-12-30T04:02:41.0892704Z   Failed YFinance.NET.Tests.Unit.Utils.SymbolValidatorTests.Sanitize_DangerousCharacters_RemovesCharacters(input: "AAPL?admin=true", expected: "AAPLadmintrue") [154 ms]
2025-12-30T04:02:41.0894070Z   Error Message:
2025-12-30T04:02:41.0894905Z    Expected result to be "AAPLadmintrue" with a length of 13, but "AAPLadmin=true" has a length of 14, differs near "=tr" (index 9).
2025-12-30T04:02:41.0895856Z   Stack Trace:
2025-12-30T04:02:41.0896450Z      at FluentAssertions.Execution.XUnit2TestFramework.Throw(String message)
2025-12-30T04:02:41.0897471Z    at FluentAssertions.Execution.TestFrameworkProvider.Throw(String message)
2025-12-30T04:02:41.0898539Z    at FluentAssertions.Execution.DefaultAssertionStrategy.HandleFailure(String message)
2025-12-30T04:02:41.0899591Z    at FluentAssertions.Execution.AssertionScope.FailWith(Func`1 failReasonFunc)
2025-12-30T04:02:41.0900581Z    at FluentAssertions.Execution.AssertionScope.FailWith(Func`1 failReasonFunc)
2025-12-30T04:02:41.0901947Z    at FluentAssertions.Primitives.StringEqualityValidator.ValidateAgainstLengthDifferences()
2025-12-30T04:02:41.0903447Z    at FluentAssertions.Primitives.StringValidator.Validate()
2025-12-30T04:02:41.0904599Z    at FluentAssertions.Primitives.StringAssertions`1.Be(String expected, String because, Object[] becauseArgs)
2025-12-30T04:02:41.0906927Z    at YFinance.NET.Tests.Unit.Utils.SymbolValidatorTests.Sanitize_DangerousCharacters_RemovesCharacters(String input, String expected) in /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Utils/SymbolValidatorTests.cs:line 316
2025-12-30T04:02:41.0909125Z    at System.Reflection.MethodBaseInvoker.InterpretedInvoke_Method(Object obj, IntPtr* args)
2025-12-30T04:02:41.0910576Z    at System.Reflection.MethodBaseInvoker.InvokeDirectByRefWithFewArgs(Object obj, Span`1 copyOfArgs, BindingFlags invokeAttr)
2025-12-30T04:02:41.0912753Z   Failed YFinance.NET.Tests.Unit.Utils.SymbolValidatorTests.Sanitize_DangerousCharacters_RemovesCharacters(input: "AAPL&foo=bar", expected: "AAPLfoobar") [3 ms]
2025-12-30T04:02:41.0914040Z   Error Message:
2025-12-30T04:02:41.0914833Z    Expected result to be "AAPLfoobar" with a length of 10, but "AAPLfoo=bar" has a length of 11, differs near "=ba" (index 7).
2025-12-30T04:02:41.0915545Z   Stack Trace:
2025-12-30T04:02:41.0915876Z      at FluentAssertions.Execution.XUnit2TestFramework.Throw(String message)
2025-12-30T04:02:41.0916420Z    at FluentAssertions.Execution.TestFrameworkProvider.Throw(String message)
2025-12-30T04:02:41.0917006Z    at FluentAssertions.Execution.DefaultAssertionStrategy.HandleFailure(String message)
2025-12-30T04:02:41.0917601Z    at FluentAssertions.Execution.AssertionScope.FailWith(Func`1 failReasonFunc)
2025-12-30T04:02:41.0918142Z    at FluentAssertions.Execution.AssertionScope.FailWith(Func`1 failReasonFunc)
2025-12-30T04:02:41.0918757Z    at FluentAssertions.Primitives.StringEqualityValidator.ValidateAgainstLengthDifferences()
2025-12-30T04:02:41.0919335Z    at FluentAssertions.Primitives.StringValidator.Validate()
2025-12-30T04:02:41.0919905Z    at FluentAssertions.Primitives.StringAssertions`1.Be(String expected, String because, Object[] becauseArgs)
2025-12-30T04:02:41.0921475Z    at YFinance.NET.Tests.Unit.Utils.SymbolValidatorTests.Sanitize_DangerousCharacters_RemovesCharacters(String input, String expected) in /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Utils/SymbolValidatorTests.cs:line 316
2025-12-30T04:02:41.0922711Z    at InvokeStub_SymbolValidatorTests.Sanitize_DangerousCharacters_RemovesCharacters(Object, Span`1)
2025-12-30T04:02:41.0923834Z    at System.Reflection.MethodBaseInvoker.InvokeWithFewArgs(Object obj, BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)
2025-12-30T04:02:41.1067883Z   Failed YFinance.NET.Tests.Unit.Utils.SymbolValidatorTests.Sanitize_DangerousCharacters_RemovesCharacters(input: "AAPL/../../etc", expected: "AAPLetc") [< 1 ms]
2025-12-30T04:02:41.1069739Z   Error Message:
2025-12-30T04:02:41.1106994Z    Expected result to be "AAPLetc" with a length of 7, but "AAPL....etc" has a length of 11, differs near "..." (index 4).
2025-12-30T04:02:41.1121820Z   Stack Trace:
2025-12-30T04:02:41.1152111Z      at FluentAssertions.Execution.XUnit2TestFramework.Throw(String message)
2025-12-30T04:02:41.1153338Z    at FluentAssertions.Execution.TestFrameworkProvider.Throw(String message)
2025-12-30T04:02:41.1154833Z    at FluentAssertions.Execution.DefaultAssertionStrategy.HandleFailure(String message)
2025-12-30T04:02:41.1156244Z    at FluentAssertions.Execution.AssertionScope.FailWith(Func`1 failReasonFunc)
2025-12-30T04:02:41.1157621Z    at FluentAssertions.Execution.AssertionScope.FailWith(Func`1 failReasonFunc)
2025-12-30T04:02:41.1159185Z    at FluentAssertions.Primitives.StringEqualityValidator.ValidateAgainstLengthDifferences()
2025-12-30T04:02:41.1160499Z    at FluentAssertions.Primitives.StringValidator.Validate()
2025-12-30T04:02:41.1162267Z    at FluentAssertions.Primitives.StringAssertions`1.Be(String expected, String because, Object[] becauseArgs)
2025-12-30T04:02:41.1164886Z    at YFinance.NET.Tests.Unit.Utils.SymbolValidatorTests.Sanitize_DangerousCharacters_RemovesCharacters(String input, String expected) in /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Utils/SymbolValidatorTests.cs:line 316
2025-12-30T04:02:41.1167842Z    at InvokeStub_SymbolValidatorTests.Sanitize_DangerousCharacters_RemovesCharacters(Object, Span`1)
2025-12-30T04:02:41.1169682Z    at System.Reflection.MethodBaseInvoker.InvokeWithFewArgs(Object obj, BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)
2025-12-30T04:02:41.2083685Z [xUnit.net 00:00:00.87]     YFinance.NET.Tests.Unit.Utils.SymbolValidatorTests.ValidateAndThrow_UrlEncoded_ThrowsArgumentException(symbol: "%2F") [FAIL]
2025-12-30T04:02:41.2234304Z   Failed YFinance.NET.Tests.Unit.Utils.SymbolValidatorTests.ValidateAndThrow_UrlEncoded_ThrowsArgumentException(symbol: "%2F") [7 ms]
2025-12-30T04:02:41.2236637Z [xUnit.net 00:00:00.87]     YFinance.NET.Tests.Unit.Utils.SymbolValidatorTests.ValidateAndThrow_UrlEncoded_ThrowsArgumentException(symbol: "AAPL%20TEST") [FAIL]
2025-12-30T04:02:41.2238974Z [xUnit.net 00:00:00.88]     YFinance.NET.Tests.Unit.Utils.SymbolValidatorTests.ValidateAndThrow_UrlEncoded_ThrowsArgumentException(symbol: "%2E") [FAIL]
2025-12-30T04:02:41.2240478Z   Error Message:
2025-12-30T04:02:41.2242409Z    Expected exception message to match the equivalent of "Symbol contains URL-encoded characters*", but "Symbol contains invalid characters. Only alphanumeric characters and .-^=_ are allowed. (Parameter 'symbol')" does not.
2025-12-30T04:02:41.2244157Z 
2025-12-30T04:02:41.2244464Z   Stack Trace:
2025-12-30T04:02:41.2245246Z      at FluentAssertions.Execution.XUnit2TestFramework.Throw(String message)
2025-12-30T04:02:41.2246755Z [xUnit.net 00:00:00.88]     YFinance.NET.Tests.Unit.Utils.SymbolValidatorTests.Sanitize_OnlyInvalidCharacters_ReturnsEmpty [FAIL]
2025-12-30T04:02:41.2248920Z [xUnit.net 00:00:00.89]     YFinance.NET.Tests.Unit.Utils.SymbolValidatorTests.ValidateAndThrow_PathTraversal_ThrowsArgumentException(symbol: "AAPL/../admin") [FAIL]
2025-12-30T04:02:41.2250685Z    at FluentAssertions.Execution.TestFrameworkProvider.Throw(String message)
2025-12-30T04:02:41.2252421Z    at FluentAssertions.Execution.CollectingAssertionStrategy.ThrowIfAny(IDictionary`2 context)
2025-12-30T04:02:41.2253621Z    at FluentAssertions.Execution.AssertionScope.Dispose()
2025-12-30T04:02:41.2255741Z    at FluentAssertions.Specialized.ExceptionAssertions`1.ExceptionMessageAssertion.Execute(IEnumerable`1 messages, String expectation, String because, Object[] becauseArgs)
2025-12-30T04:02:41.2258356Z    at FluentAssertions.Specialized.ExceptionAssertions`1.ExceptionMessageAssertion.Execute(IEnumerable`1 messages, String expectation, String because, Object[] becauseArgs)
2025-12-30T04:02:41.2260640Z    at FluentAssertions.Specialized.ExceptionAssertions`1.WithMessage(String expectedWildcardPattern, String because, Object[] becauseArgs)
2025-12-30T04:02:41.2263503Z    at YFinance.NET.Tests.Unit.Utils.SymbolValidatorTests.ValidateAndThrow_UrlEncoded_ThrowsArgumentException(String symbol) in /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Utils/SymbolValidatorTests.cs:line 215
2025-12-30T04:02:41.2266078Z    at System.Reflection.MethodBaseInvoker.InterpretedInvoke_Method(Object obj, IntPtr* args)
2025-12-30T04:02:41.2267821Z    at System.Reflection.MethodBaseInvoker.InvokeDirectByRefWithFewArgs(Object obj, Span`1 copyOfArgs, BindingFlags invokeAttr)
2025-12-30T04:02:41.2292408Z [xUnit.net 00:00:00.89]     YFinance.NET.Tests.Unit.Utils.SymbolValidatorTests.ValidateAndThrow_PathTraversal_ThrowsArgumentException(symbol: "../../../etc/passwd") [FAIL]
2025-12-30T04:02:41.2477410Z   Failed YFinance.NET.Tests.Unit.Utils.SymbolValidatorTests.ValidateAndThrow_UrlEncoded_ThrowsArgumentException(symbol: "AAPL%20TEST") [< 1 ms]
2025-12-30T04:02:41.2478894Z   Error Message:
2025-12-30T04:02:41.2480515Z    Expected exception message to match the equivalent of "Symbol contains URL-encoded characters*", but "Symbol contains invalid characters. Only alphanumeric characters and .-^=_ are allowed. (Parameter 'symbol')" does not.
2025-12-30T04:02:41.2482721Z 
2025-12-30T04:02:41.2483061Z   Stack Trace:
2025-12-30T04:02:41.2483863Z      at FluentAssertions.Execution.XUnit2TestFramework.Throw(String message)
2025-12-30T04:02:41.2484942Z    at FluentAssertions.Execution.TestFrameworkProvider.Throw(String message)
2025-12-30T04:02:41.2486185Z    at FluentAssertions.Execution.CollectingAssertionStrategy.ThrowIfAny(IDictionary`2 context)
2025-12-30T04:02:41.2487261Z    at FluentAssertions.Execution.AssertionScope.Dispose()
2025-12-30T04:02:41.2488819Z    at FluentAssertions.Specialized.ExceptionAssertions`1.ExceptionMessageAssertion.Execute(IEnumerable`1 messages, String expectation, String because, Object[] becauseArgs)
2025-12-30T04:02:41.2491253Z    at FluentAssertions.Specialized.ExceptionAssertions`1.ExceptionMessageAssertion.Execute(IEnumerable`1 messages, String expectation, String because, Object[] becauseArgs)
2025-12-30T04:02:41.2493341Z    at FluentAssertions.Specialized.ExceptionAssertions`1.WithMessage(String expectedWildcardPattern, String because, Object[] becauseArgs)
2025-12-30T04:02:41.2495843Z    at YFinance.NET.Tests.Unit.Utils.SymbolValidatorTests.ValidateAndThrow_UrlEncoded_ThrowsArgumentException(String symbol) in /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Utils/SymbolValidatorTests.cs:line 215
2025-12-30T04:02:41.2498189Z    at InvokeStub_SymbolValidatorTests.ValidateAndThrow_UrlEncoded_ThrowsArgumentException(Object, Span`1)
2025-12-30T04:02:41.2499961Z    at System.Reflection.MethodBaseInvoker.InvokeWithOneArg(Object obj, BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)
2025-12-30T04:02:41.2514391Z   Failed YFinance.NET.Tests.Unit.Utils.SymbolValidatorTests.ValidateAndThrow_UrlEncoded_ThrowsArgumentException(symbol: "%2E") [< 1 ms]
2025-12-30T04:02:41.2515471Z   Error Message:
2025-12-30T04:02:41.2516847Z    Expected exception message to match the equivalent of "Symbol contains URL-encoded characters*", but "Symbol contains invalid characters. Only alphanumeric characters and .-^=_ are allowed. (Parameter 'symbol')" does not.
2025-12-30T04:02:41.2518206Z 
2025-12-30T04:02:41.2518319Z   Stack Trace:
2025-12-30T04:02:41.2518839Z      at FluentAssertions.Execution.XUnit2TestFramework.Throw(String message)
2025-12-30T04:02:41.2519732Z    at FluentAssertions.Execution.TestFrameworkProvider.Throw(String message)
2025-12-30T04:02:41.2521002Z    at FluentAssertions.Execution.CollectingAssertionStrategy.ThrowIfAny(IDictionary`2 context)
2025-12-30T04:02:41.2522180Z    at FluentAssertions.Execution.AssertionScope.Dispose()
2025-12-30T04:02:41.2523590Z    at FluentAssertions.Specialized.ExceptionAssertions`1.ExceptionMessageAssertion.Execute(IEnumerable`1 messages, String expectation, String because, Object[] becauseArgs)
2025-12-30T04:02:41.2525721Z    at FluentAssertions.Specialized.ExceptionAssertions`1.ExceptionMessageAssertion.Execute(IEnumerable`1 messages, String expectation, String because, Object[] becauseArgs)
2025-12-30T04:02:41.2527698Z    at FluentAssertions.Specialized.ExceptionAssertions`1.WithMessage(String expectedWildcardPattern, String because, Object[] becauseArgs)
2025-12-30T04:02:41.2530030Z    at YFinance.NET.Tests.Unit.Utils.SymbolValidatorTests.ValidateAndThrow_UrlEncoded_ThrowsArgumentException(String symbol) in /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Utils/SymbolValidatorTests.cs:line 215
2025-12-30T04:02:41.2532383Z    at InvokeStub_SymbolValidatorTests.ValidateAndThrow_UrlEncoded_ThrowsArgumentException(Object, Span`1)
2025-12-30T04:02:41.2533930Z    at System.Reflection.MethodBaseInvoker.InvokeWithOneArg(Object obj, BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)
2025-12-30T04:02:41.2535527Z   Failed YFinance.NET.Tests.Unit.Utils.SymbolValidatorTests.Sanitize_OnlyInvalidCharacters_ReturnsEmpty [< 1 ms]
2025-12-30T04:02:41.2536402Z   Error Message:
2025-12-30T04:02:41.2536746Z    Expected result to be empty, but found "^".
2025-12-30T04:02:41.2537387Z   Stack Trace:
2025-12-30T04:02:41.2537901Z      at FluentAssertions.Execution.XUnit2TestFramework.Throw(String message)
2025-12-30T04:02:41.2538783Z    at FluentAssertions.Execution.TestFrameworkProvider.Throw(String message)
2025-12-30T04:02:41.2539761Z    at FluentAssertions.Execution.DefaultAssertionStrategy.HandleFailure(String message)
2025-12-30T04:02:41.2540768Z    at FluentAssertions.Execution.AssertionScope.FailWith(Func`1 failReasonFunc)
2025-12-30T04:02:41.2541834Z    at FluentAssertions.Execution.AssertionScope.FailWith(Func`1 failReasonFunc)
2025-12-30T04:02:41.2542789Z    at FluentAssertions.Execution.AssertionScope.FailWith(String message, Object[] args)
2025-12-30T04:02:41.2543865Z    at FluentAssertions.Primitives.StringAssertions`1.BeEmpty(String because, Object[] becauseArgs)
2025-12-30T04:02:41.2545728Z    at YFinance.NET.Tests.Unit.Utils.SymbolValidatorTests.Sanitize_OnlyInvalidCharacters_ReturnsEmpty() in /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Utils/SymbolValidatorTests.cs:line 403
2025-12-30T04:02:41.2547562Z    at System.Reflection.MethodBaseInvoker.InterpretedInvoke_Method(Object obj, IntPtr* args)
2025-12-30T04:02:41.2548666Z    at System.Reflection.MethodBaseInvoker.InvokeWithNoArgs(Object obj, BindingFlags invokeAttr)
2025-12-30T04:02:41.2550161Z   Failed YFinance.NET.Tests.Unit.Utils.SymbolValidatorTests.ValidateAndThrow_PathTraversal_ThrowsArgumentException(symbol: "AAPL/../admin") [1 ms]
2025-12-30T04:02:41.2551449Z   Error Message:
2025-12-30T04:02:41.2552820Z    Expected exception message to match the equivalent of "Symbol contains path traversal sequences*", but "Symbol contains invalid characters. Only alphanumeric characters and .-^=_ are allowed. (Parameter 'symbol')" does not.
2025-12-30T04:02:41.2554177Z 
2025-12-30T04:02:41.2554287Z   Stack Trace:
2025-12-30T04:02:41.2554804Z      at FluentAssertions.Execution.XUnit2TestFramework.Throw(String message)
2025-12-30T04:02:41.2555681Z    at FluentAssertions.Execution.TestFrameworkProvider.Throw(String message)
2025-12-30T04:02:41.2556713Z    at FluentAssertions.Execution.CollectingAssertionStrategy.ThrowIfAny(IDictionary`2 context)
2025-12-30T04:02:41.2557629Z    at FluentAssertions.Execution.AssertionScope.Dispose()
2025-12-30T04:02:41.2559052Z    at FluentAssertions.Specialized.ExceptionAssertions`1.ExceptionMessageAssertion.Execute(IEnumerable`1 messages, String expectation, String because, Object[] becauseArgs)
2025-12-30T04:02:41.2561452Z    at FluentAssertions.Specialized.ExceptionAssertions`1.ExceptionMessageAssertion.Execute(IEnumerable`1 messages, String expectation, String because, Object[] becauseArgs)
2025-12-30T04:02:41.2563388Z    at FluentAssertions.Specialized.ExceptionAssertions`1.WithMessage(String expectedWildcardPattern, String because, Object[] becauseArgs)
2025-12-30T04:02:41.2565709Z    at YFinance.NET.Tests.Unit.Utils.SymbolValidatorTests.ValidateAndThrow_PathTraversal_ThrowsArgumentException(String symbol) in /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Utils/SymbolValidatorTests.cs:line 201
2025-12-30T04:02:41.2567720Z    at System.Reflection.MethodBaseInvoker.InterpretedInvoke_Method(Object obj, IntPtr* args)
2025-12-30T04:02:41.2569040Z    at System.Reflection.MethodBaseInvoker.InvokeDirectByRefWithFewArgs(Object obj, Span`1 copyOfArgs, BindingFlags invokeAttr)
2025-12-30T04:02:41.2570791Z   Failed YFinance.NET.Tests.Unit.Utils.SymbolValidatorTests.ValidateAndThrow_PathTraversal_ThrowsArgumentException(symbol: "../../../etc/passwd") [< 1 ms]
2025-12-30T04:02:41.2572097Z   Error Message:
2025-12-30T04:02:41.2573486Z    Expected exception message to match the equivalent of "Symbol contains path traversal sequences*", but "Symbol contains invalid characters. Only alphanumeric characters and .-^=_ are allowed. (Parameter 'symbol')" does not.
2025-12-30T04:02:41.2574851Z 
2025-12-30T04:02:41.2574972Z   Stack Trace:
2025-12-30T04:02:41.2575492Z      at FluentAssertions.Execution.XUnit2TestFramework.Throw(String message)
2025-12-30T04:02:41.2576632Z    at FluentAssertions.Execution.TestFrameworkProvider.Throw(String message)
2025-12-30T04:02:41.2577659Z    at FluentAssertions.Execution.CollectingAssertionStrategy.ThrowIfAny(IDictionary`2 context)
2025-12-30T04:02:41.2578582Z    at FluentAssertions.Execution.AssertionScope.Dispose()
2025-12-30T04:02:41.2579973Z    at FluentAssertions.Specialized.ExceptionAssertions`1.ExceptionMessageAssertion.Execute(IEnumerable`1 messages, String expectation, String because, Object[] becauseArgs)
2025-12-30T04:02:41.2582303Z    at FluentAssertions.Specialized.ExceptionAssertions`1.ExceptionMessageAssertion.Execute(IEnumerable`1 messages, String expectation, String because, Object[] becauseArgs)
2025-12-30T04:02:41.2584207Z    at FluentAssertions.Specialized.ExceptionAssertions`1.WithMessage(String expectedWildcardPattern, String because, Object[] becauseArgs)
2025-12-30T04:02:41.2586526Z    at YFinance.NET.Tests.Unit.Utils.SymbolValidatorTests.ValidateAndThrow_PathTraversal_ThrowsArgumentException(String symbol) in /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Utils/SymbolValidatorTests.cs:line 201
2025-12-30T04:02:41.2588651Z    at InvokeStub_SymbolValidatorTests.ValidateAndThrow_PathTraversal_ThrowsArgumentException(Object, Span`1)
2025-12-30T04:02:41.2590219Z    at System.Reflection.MethodBaseInvoker.InvokeWithOneArg(Object obj, BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)
2025-12-30T04:02:41.3637096Z [xUnit.net 00:00:00.92]     YFinance.NET.Tests.Unit.Scrapers.QuoteScraperTests.GetQuoteAsync_InvalidSymbol_ThrowsArgumentException(symbol: "") [FAIL]
2025-12-30T04:02:41.3648370Z [xUnit.net 00:00:00.92]     YFinance.NET.Tests.Unit.Scrapers.QuoteScraperTests.GetQuoteAsync_InvalidSymbol_ThrowsArgumentException(symbol: "   ") [FAIL]
2025-12-30T04:02:41.3663228Z [xUnit.net 00:00:00.93]     YFinance.NET.Tests.Unit.Scrapers.QuoteScraperTests.GetQuoteAsync_InvalidSymbol_ThrowsArgumentException(symbol: null) [FAIL]
2025-12-30T04:02:41.3693705Z   Failed YFinance.NET.Tests.Unit.Scrapers.QuoteScraperTests.GetQuoteAsync_InvalidSymbol_ThrowsArgumentException(symbol: "") [1 ms]
2025-12-30T04:02:41.3694833Z   Error Message:
2025-12-30T04:02:41.3695326Z    Assert.Throws() Failure: Exception type was not an exact match
2025-12-30T04:02:41.3696036Z Expected: typeof(System.ArgumentException)
2025-12-30T04:02:41.3696655Z Actual:   typeof(System.ArgumentNullException)
2025-12-30T04:02:41.3697869Z ---- System.ArgumentNullException : Value cannot be null. (Parameter 'json')
2025-12-30T04:02:41.3698634Z   Stack Trace:
2025-12-30T04:02:41.3700320Z      at YFinance.NET.Tests.Unit.Scrapers.QuoteScraperTests.GetQuoteAsync_InvalidSymbol_ThrowsArgumentException(String symbol) in /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/QuoteScraperTests.cs:line 60
2025-12-30T04:02:41.3702469Z --- End of stack trace from previous location ---
2025-12-30T04:02:41.3703048Z ----- Inner Stack Trace -----
2025-12-30T04:02:41.3703573Z    at System.ArgumentNullException.Throw(String paramName)
2025-12-30T04:02:41.3704444Z    at System.ArgumentNullException.ThrowIfNull(Object argument, String paramName)
2025-12-30T04:02:41.3705467Z    at System.Text.Json.JsonDocument.Parse(String json, JsonDocumentOptions options)
2025-12-30T04:02:41.3707387Z    at YFinance.NET.Implementation.Scrapers.QuoteScraper.ParseQuoteData(String symbol, String jsonResponse) in /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/QuoteScraper.cs:line 77
2025-12-30T04:02:41.3710338Z    at YFinance.NET.Implementation.Scrapers.QuoteScraper.GetQuoteAsync(String symbol, CancellationToken cancellationToken) in /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/QuoteScraper.cs:line 46
2025-12-30T04:02:41.3713012Z   Failed YFinance.NET.Tests.Unit.Scrapers.QuoteScraperTests.GetQuoteAsync_InvalidSymbol_ThrowsArgumentException(symbol: "   ") [< 1 ms]
2025-12-30T04:02:41.3714146Z   Error Message:
2025-12-30T04:02:41.3714634Z    Assert.Throws() Failure: Exception type was not an exact match
2025-12-30T04:02:41.3715891Z Expected: typeof(System.ArgumentException)
2025-12-30T04:02:41.3716529Z Actual:   typeof(System.ArgumentNullException)
2025-12-30T04:02:41.3717346Z ---- System.ArgumentNullException : Value cannot be null. (Parameter 'json')
2025-12-30T04:02:41.3718011Z   Stack Trace:
2025-12-30T04:02:41.3719675Z      at YFinance.NET.Tests.Unit.Scrapers.QuoteScraperTests.GetQuoteAsync_InvalidSymbol_ThrowsArgumentException(String symbol) in /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/QuoteScraperTests.cs:line 60
2025-12-30T04:02:41.3721798Z --- End of stack trace from previous location ---
2025-12-30T04:02:41.3722360Z ----- Inner Stack Trace -----
2025-12-30T04:02:41.3722890Z    at System.ArgumentNullException.Throw(String paramName)
2025-12-30T04:02:41.3723714Z    at System.ArgumentNullException.ThrowIfNull(Object argument, String paramName)
2025-12-30T04:02:41.3724736Z    at System.Text.Json.JsonDocument.Parse(String json, JsonDocumentOptions options)
2025-12-30T04:02:41.3726739Z    at YFinance.NET.Implementation.Scrapers.QuoteScraper.ParseQuoteData(String symbol, String jsonResponse) in /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/QuoteScraper.cs:line 77
2025-12-30T04:02:41.3729565Z    at YFinance.NET.Implementation.Scrapers.QuoteScraper.GetQuoteAsync(String symbol, CancellationToken cancellationToken) in /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/QuoteScraper.cs:line 46
2025-12-30T04:02:41.3731439Z   Failed YFinance.NET.Tests.Unit.Scrapers.QuoteScraperTests.GetQuoteAsync_InvalidSymbol_ThrowsArgumentException(symbol: null) [1 ms]
2025-12-30T04:02:41.3732063Z   Error Message:
2025-12-30T04:02:41.3732343Z    Assert.Throws() Failure: Exception type was not an exact match
2025-12-30T04:02:41.3732720Z Expected: typeof(System.ArgumentException)
2025-12-30T04:02:41.3733050Z Actual:   typeof(System.ArgumentNullException)
2025-12-30T04:02:41.3733495Z ---- System.ArgumentNullException : Value cannot be null. (Parameter 'json')
2025-12-30T04:02:41.3733893Z   Stack Trace:
2025-12-30T04:02:41.3734912Z      at YFinance.NET.Tests.Unit.Scrapers.QuoteScraperTests.GetQuoteAsync_InvalidSymbol_ThrowsArgumentException(String symbol) in /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Tests/Unit/Scrapers/QuoteScraperTests.cs:line 60
2025-12-30T04:02:41.3735942Z --- End of stack trace from previous location ---
2025-12-30T04:02:41.3736546Z ----- Inner Stack Trace -----
2025-12-30T04:02:41.3736864Z    at System.ArgumentNullException.Throw(String paramName)
2025-12-30T04:02:41.3737333Z    at System.ArgumentNullException.ThrowIfNull(Object argument, String paramName)
2025-12-30T04:02:41.3737871Z    at System.Text.Json.JsonDocument.Parse(String json, JsonDocumentOptions options)
2025-12-30T04:02:41.3738906Z    at YFinance.NET.Implementation.Scrapers.QuoteScraper.ParseQuoteData(String symbol, String jsonResponse) in /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/QuoteScraper.cs:line 77
2025-12-30T04:02:41.3740478Z    at YFinance.NET.Implementation.Scrapers.QuoteScraper.GetQuoteAsync(String symbol, CancellationToken cancellationToken) in /home/runner/work/YFinance.NET/YFinance.NET/YFinance.NET.Implementation/Scrapers/QuoteScraper.cs:line 46
2025-12-30T04:02:57.0292527Z 
2025-12-30T04:02:57.0333584Z Failed!  - Failed:    15, Passed:   238, Skipped:     0, Total:   253, Duration: 15 s - YFinance.NET.Tests.dll (net10.0)
2025-12-30T04:02:57.2393543Z 
2025-12-30T04:02:57.2399188Z Attachments:
2025-12-30T04:02:57.2401824Z   /home/runner/work/YFinance.NET/YFinance.NET/coverage/adff0726-e2e2-48df-81ad-abae3543c9b4/coverage.cobertura.xml
2025-12-30T04:02:57.3023622Z ##[error]Process completed with exit code 1.
2025-12-30T04:02:57.3136593Z Post job cleanup.
2025-12-30T04:02:57.4087261Z [command]/usr/bin/git version
2025-12-30T04:02:57.4132898Z git version 2.52.0
2025-12-30T04:02:57.4174980Z Temporarily overriding HOME='/home/runner/work/_temp/3b5367e6-70c1-4e22-bf42-e5c7292a7f74' before making global git config changes
2025-12-30T04:02:57.4176094Z Adding repository directory to the temporary git global config as a safe directory
2025-12-30T04:02:57.4179954Z [command]/usr/bin/git config --global --add safe.directory /home/runner/work/YFinance.NET/YFinance.NET
2025-12-30T04:02:57.4212781Z [command]/usr/bin/git config --local --name-only --get-regexp core\.sshCommand
2025-12-30T04:02:57.4244318Z [command]/usr/bin/git submodule foreach --recursive sh -c "git config --local --name-only --get-regexp 'core\.sshCommand' && git config --local --unset-all 'core.sshCommand' || :"
2025-12-30T04:02:57.4473674Z [command]/usr/bin/git config --local --name-only --get-regexp http\.https\:\/\/github\.com\/\.extraheader
2025-12-30T04:02:57.4494913Z http.https://github.com/.extraheader
2025-12-30T04:02:57.4507040Z [command]/usr/bin/git config --local --unset-all http.https://github.com/.extraheader
2025-12-30T04:02:57.4537027Z [command]/usr/bin/git submodule foreach --recursive sh -c "git config --local --name-only --get-regexp 'http\.https\:\/\/github\.com\/\.extraheader' && git config --local --unset-all 'http.https://github.com/.extraheader' || :"
2025-12-30T04:02:57.4761337Z [command]/usr/bin/git config --local --name-only --get-regexp ^includeIf\.gitdir:
2025-12-30T04:02:57.4794056Z [command]/usr/bin/git submodule foreach --recursive git config --local --show-origin --name-only --get-regexp remote.origin.url
2025-12-30T04:02:57.5132973Z Cleaning up orphan processes
2025-12-30T04:02:57.5430128Z Terminate orphan process: pid (2332) (dotnet)
2025-12-30T04:02:57.5448451Z Terminate orphan process: pid (2333) (dotnet)
2025-12-30T04:02:57.5466141Z Terminate orphan process: pid (2334) (dotnet)
2025-12-30T04:02:57.5495270Z Terminate orphan process: pid (2504) (VBCSCompiler)

```

