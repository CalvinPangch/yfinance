using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using Google.Protobuf;
using YFinance.NET.Interfaces;
using YFinance.NET.Models;

namespace YFinance.NET.Implementation;

/// <summary>
/// WebSocket client for Yahoo Finance live pricing.
/// </summary>
public class LiveMarketService : ILiveMarketService
{
    private const string StreamUrl = "wss://streamer.finance.yahoo.com/?version=2";
    private readonly ClientWebSocket _socket = new();

    public async Task ListenAsync(IEnumerable<string> symbols, Func<LivePriceData, Task>? onMessage = null, CancellationToken cancellationToken = default)
    {
        var symbolList = symbols?.Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => s.Trim()).Distinct().ToList()
            ?? throw new ArgumentNullException(nameof(symbols));

        if (symbolList.Count == 0)
            return;

        await _socket.ConnectAsync(new Uri(StreamUrl), cancellationToken).ConfigureAwait(false);

        var subscribeMessage = JsonSerializer.Serialize(new { subscribe = symbolList });
        var subscribeBytes = Encoding.UTF8.GetBytes(subscribeMessage);
        await _socket.SendAsync(new ArraySegment<byte>(subscribeBytes), WebSocketMessageType.Text, true, cancellationToken)
            .ConfigureAwait(false);

        var buffer = new byte[8192];
        while (!cancellationToken.IsCancellationRequested && _socket.State == WebSocketState.Open)
        {
            var result = await _socket.ReceiveAsync(buffer, cancellationToken).ConfigureAwait(false);
            if (result.MessageType == WebSocketMessageType.Close)
                break;

            var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
            if (!TryParseMessage(message, out var data))
                continue;

            if (onMessage != null)
                await onMessage(data).ConfigureAwait(false);
        }
    }

    private static bool TryParseMessage(string jsonMessage, out LivePriceData data)
    {
        data = new LivePriceData();

        using var document = JsonDocument.Parse(jsonMessage);
        var root = document.RootElement;

        if (!root.TryGetProperty("message", out var messageElement) || messageElement.ValueKind != JsonValueKind.String)
            return false;

        var base64 = messageElement.GetString();
        if (string.IsNullOrWhiteSpace(base64))
            return false;

        var bytes = Convert.FromBase64String(base64);
        data = ParsePricingPayload(bytes);
        return true;
    }

    private static LivePriceData ParsePricingPayload(byte[] payload)
    {
        var reader = new CodedInputStream(payload);
        var data = new LivePriceData();

        uint tag;
        while ((tag = reader.ReadTag()) != 0)
        {
            switch (tag)
            {
                case 10: data.Id = reader.ReadString(); break;
                case 21: data.Price = reader.ReadFloat(); break;
                case 24: data.Time = reader.ReadSInt64(); break;
                case 34: data.Currency = reader.ReadString(); break;
                case 42: data.Exchange = reader.ReadString(); break;
                case 48: data.QuoteType = reader.ReadInt32(); break;
                case 56: data.MarketHours = reader.ReadInt32(); break;
                case 69: data.ChangePercent = reader.ReadFloat(); break;
                case 72: data.DayVolume = reader.ReadSInt64(); break;
                case 85: data.DayHigh = reader.ReadFloat(); break;
                case 93: data.DayLow = reader.ReadFloat(); break;
                case 101: data.Change = reader.ReadFloat(); break;
                case 106: data.ShortName = reader.ReadString(); break;
                case 112: data.ExpireDate = reader.ReadSInt64(); break;
                case 125: data.OpenPrice = reader.ReadFloat(); break;
                case 133: data.PreviousClose = reader.ReadFloat(); break;
                case 141: data.StrikePrice = reader.ReadFloat(); break;
                case 146: data.UnderlyingSymbol = reader.ReadString(); break;
                case 152: data.OpenInterest = reader.ReadSInt64(); break;
                case 160: data.OptionsType = reader.ReadSInt64(); break;
                case 168: data.MiniOption = reader.ReadSInt64(); break;
                case 176: data.LastSize = reader.ReadSInt64(); break;
                case 189: data.Bid = reader.ReadFloat(); break;
                case 192: data.BidSize = reader.ReadSInt64(); break;
                case 205: data.Ask = reader.ReadFloat(); break;
                case 208: data.AskSize = reader.ReadSInt64(); break;
                case 216: data.PriceHint = reader.ReadSInt64(); break;
                case 224: data.Volume24Hr = reader.ReadSInt64(); break;
                case 232: data.VolumeAllCurrencies = reader.ReadSInt64(); break;
                case 242: data.FromCurrency = reader.ReadString(); break;
                case 250: data.LastMarket = reader.ReadString(); break;
                case 257: data.CirculatingSupply = reader.ReadDouble(); break;
                case 265: data.MarketCap = reader.ReadDouble(); break;
                default:
                    reader.SkipLastField();
                    break;
            }
        }

        return data;
    }
}
