using Dlt.Net.Enums;
using Dlt.Net.Headers;
using Dlt.Net.Payloads;
using Dlt.Net.Utils;

namespace Dlt.Net;

public class DltMessage
{
    #region Properties

    #region Headers & Payload

    public DltStorageHeader StgHeader { get; }

    public DltStandardHeader StdHeader { get; }

    public DltExtendedHeader ExtHeader { get; }

    public IPayload Payload { get; }

    #endregion

    #region Expose Header / Payload properties

    public DateTime Time => TimeUtils.TimestampToDateTime(StgHeader.Seconds, StgHeader.Microseconds);

    public double? Timestamp => (double?)StdHeader.Timestamp / 10000;

    public int Count => StdHeader.MessageCounter;

    public string EcuId => StdHeader.EcuId ?? StgHeader.EcuId;

    public string AppId => ExtHeader.ApplicationId;

    public string CtxId => ExtHeader.ContextId;

    public uint? SessionId => StdHeader.SessionId;

    public MessageType Type => ExtHeader.MessageType;

    public MessageInfo SubType => ExtHeader.MessageTypeInfo;

    public bool IsVerbose => ExtHeader.Verbose;

    public int ArgumentsNum => ExtHeader.ArgumentsNumber;

    public string PayloadString => Payload.ToString();

    #endregion

    public byte[] RawData { get; }

    #endregion

    public DltMessage(DltStorageHeader stgHeader, DltStandardHeader stdHeader, DltExtendedHeader extHeader, IPayload payload, byte[] data)
    {
        StgHeader = stgHeader;
        StdHeader = stdHeader;
        ExtHeader = extHeader;
        Payload = payload;
        RawData = data;
    }

    public static DltMessage CreateMessage(byte[] data, bool existStorageHeader = true)
    {
        var (position, stgHeaderLength) = (0, 0);
        var stgHeader = new DltStorageHeader();

        if (existStorageHeader)
        {
            stgHeader = DltStorageHeader.CreateHeader(data);
            stgHeaderLength = stgHeader.HeaderLength;
            position += stgHeaderLength;
        }

        var stdHeader = DltStandardHeader.CreateHeader(data[position..]);
        position += stdHeader.HeaderLength;

        var extHeader = new DltExtendedHeader();
        var extHeaderLength = 0;
        if (stdHeader.UseExtendedHeader)
        {
            extHeader = DltExtendedHeader.CreateHeader(data[position..]);
            extHeaderLength = extHeader.HeaderLength;
            position += extHeaderLength;
        }

        IPayload payload = new NonVerbosePayload();
        if (stdHeader.TotalLength > stdHeader.HeaderLength + extHeaderLength)
        {
            if (extHeader is { IsEmpty: false, Verbose: true })
                payload = VerbosePayload.CreatePayload(data[position..(stdHeader.TotalLength + stgHeaderLength)],
                    stdHeader.MsbFirst, extHeader.ArgumentsNumber);
            else
                payload = NonVerbosePayload.CreatePayload(data[position..(stdHeader.TotalLength + stgHeaderLength)],
                    stdHeader.MsbFirst);
        }

        return new DltMessage(stgHeader, stdHeader, extHeader, payload, data[..(stdHeader.TotalLength + stgHeaderLength)]);
    }

    #region ToStrings

    public override string ToString() =>
        $"{Time} {(Timestamp == null ? "<None>" : Timestamp.ToString())} {Count} {EcuId} {AppId} {CtxId} " +
        $"{(SessionId == null ? "<None>" : SessionId.ToString())} {Type} {SubType.Value} " +
        $"{(IsVerbose ? "Verbose" : "NonVerbose")} {ArgumentsNum} {PayloadString}";

    #endregion
}
