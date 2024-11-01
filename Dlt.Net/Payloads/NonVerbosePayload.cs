using Dlt.Net.Utils;

namespace Dlt.Net.Payloads;

public class NonVerbosePayload : IPayload
{
    #region Private Fields

    private const int MessageIdLength = 4;

    #endregion

    #region Properties

    #region IPayload

    public int HeaderLength => MessageIdLength + PayloadBytes.Length;

    #endregion

    /// <summary>
    /// Unique ID for a specific Dlt message
    /// </summary>
    public uint MessageId { get; }

    /// <summary>
    /// Human-readable payload string
    /// </summary>
    public string Payload { get; }

    /// <summary>
    /// All payload data of a Dlt message
    /// </summary>
    public byte[] PayloadBytes { get; }

    public bool IsEmpty { get; }

    #endregion

    public NonVerbosePayload()
    {
        PayloadBytes = Array.Empty<byte>();
        Payload = string.Empty;
        IsEmpty = true;
    }

    private NonVerbosePayload(uint messageId, byte[] data, bool msbFirst)
    {
        MessageId = messageId;

        if (BitConverter.IsLittleEndian == msbFirst)
            Array.Reverse(data);

        PayloadBytes = data;
        Payload = BitConverter.ToString(PayloadBytes).Replace("-", " ");
        IsEmpty = false;
    }

    public static NonVerbosePayload CreatePayload(byte[] data, bool msbFirst)
    {
        // Validate arguments
        if (data.Length < MessageIdLength)
            throw new ArgumentException($"Payload of Non-Verbose Mode must be {MessageIdLength} or more", nameof(data));

        // Parse bytes
        var messageId = BitConverter.ToUInt32(data.GetBytes(0, 4, !msbFirst));

        return new NonVerbosePayload(messageId, data[4..], msbFirst);
    }

    public override string ToString() => Payload;
}
