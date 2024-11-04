using Dlt.Net.Utils;
using System.Text;

namespace Dlt.Net.Headers;

public class DltStandardHeader : DltHeader
{
    #region Private Fields

    private const int MinLength = 4;

    #region Masks & Shifts

    private const int UseExtendedHeaderMask = 0b00000001;

    private const int MsbFirstMask = 0b00000010;

    private const int WithEcuIdMask = 0b00000100;

    private const int WithSessionIdMask = 0b00001000;

    private const int WithTimestampMask = 0b00010000;

    private const int VersionNumberMask = 0b11100000;

    private const int VersionNumberShift = 5;

    #endregion

    #endregion

    #region Properties

    #region DltHeader

    public override int HeaderLength => MinLength + new[] { WithEcuId, WithSessionId, WithTimestamp }.Count(x => x) * 4;

    #endregion

    /// <summary>
    ///  If set, the Extended Header is transmitted
    /// </summary>
    public bool UseExtendedHeader { get; }

    /// <summary>
    /// If set, the payload data is in big endian format, else in little endian format
    /// </summary>
    public bool MsbFirst { get; }

    /// <summary>
    /// Version number of Dlt Data protocol
    /// </summary>
    public int VersionNumber { get; }

    /// <summary>
    /// Continuous number of message
    /// </summary>
    public int MessageCounter { get; }

    /// <summary>
    /// Length of the complete message in bytes
    /// </summary>
    public int TotalLength { get; }

    /// <summary>
    /// Unique address of sender
    /// </summary>
    public string? EcuId { get; }

    /// <summary>
    /// Session number
    /// </summary>
    public uint? SessionId { get; }

    /// <summary>
    /// Continuous time / ticks from the ECU at the moment the message is sent to Dlt
    /// </summary>
    public uint? Timestamp { get; }

    #endregion

    public DltStandardHeader() : base(true, 0)
    {

    }

    private DltStandardHeader(bool useExtHeader, bool msbFirst, int version, int messageCounter, int totalLength, string? ecuId, uint? sessionId, uint? timestamp, int length)
        : base(false, length)
    {
        UseExtendedHeader = useExtHeader;
        MsbFirst = msbFirst;
        VersionNumber = version;
        MessageCounter = messageCounter;
        TotalLength = totalLength;
        EcuId = ecuId?.Trim('\0');
        SessionId = sessionId;
        Timestamp = timestamp;
    }

    public static DltStandardHeader CreateHeader(byte[] data)
    {
        // Validate argument
        if (data.Length < MinLength)
            throw new ArgumentException($"Standard header must be {MinLength} or more", nameof(data));

        // Get Header type
        var headerTypeByte = data[0];
        var useExtendedHeader = Convert.ToBoolean(headerTypeByte & UseExtendedHeaderMask);
        var msbFirst = Convert.ToBoolean(headerTypeByte & MsbFirstMask);
        var withEcuId = Convert.ToBoolean(headerTypeByte & WithEcuIdMask);
        var withSessionId = Convert.ToBoolean(headerTypeByte & WithSessionIdMask);
        var withTimestamp = Convert.ToBoolean(headerTypeByte & WithTimestampMask);
        var version = (headerTypeByte & VersionNumberMask) >> VersionNumberShift;

        // Validate data
        var headerLength = MinLength;
        if (withEcuId)
            headerLength += 4;
        if (withSessionId)
            headerLength += 4;
        if (withTimestamp)
            headerLength += 4;
        if (data.Length < headerLength)
            throw new ArgumentException($"Standard header with " +
                                        $"WEID={withEcuId} WSID={withSessionId} WTMS={withTimestamp}" +
                                        $" must be {headerLength} or more", nameof(data));

        // Parse bytes
        int messageCounter = data.GetBytes(1, 1, false)[0];
        int totalLength = BitConverter.ToUInt16(data.GetBytes(2, 2, false));

        var offset = 4;

        string? ecuId = null;
        if (withEcuId)
        {
            ecuId = Encoding.ASCII.GetString(data.GetBytes(offset, 4, true));
            offset += 4;
        }

        uint? sessionId = null;
        if (withSessionId)
        {
            sessionId = BitConverter.ToUInt32(data.GetBytes(offset, 4, false));
            offset += 4;
        }

        uint? timestamp = null;
        if (withTimestamp)
        {
            timestamp = BitConverter.ToUInt32(data.GetBytes(offset, 4, false));
            offset += 4;
        }

        return new DltStandardHeader(useExtendedHeader, msbFirst, version, messageCounter, totalLength, ecuId, sessionId, timestamp, offset);
    }

    public static int GetMessageLength(byte[] data) => BitConverter.ToUInt16(data.GetBytes(2, 2, false));

    public override string ToString()
    {
        var result = $"StandardHeader(UseExtendedHeader:{UseExtendedHeader}, " +
                     $"MsbFirst:{MsbFirst}, " +
                     $"VersionNumber:{VersionNumber}, " +
                     $"MessageCounter:{MessageCounter}, " +
                     $"Length:{TotalLength}";

        if (WithEcuId)
            result += $", EcuId:{EcuId}";
        if (WithSessionId)
            result += $", SessionId:{SessionId}";
        if (WithTimestamp)
            result += $", Timestamp:{Timestamp}";

        result += ")";
        return result;
    }

    #region Private Methods

    private bool WithEcuId => EcuId != null;

    private bool WithSessionId => SessionId != null;

    private bool WithTimestamp => Timestamp != null;

    #endregion
}
