using Dlt.Net.Enums;
using Dlt.Net.Utils;
using System.Text;

namespace Dlt.Net.Headers;

public class DltExtendedHeader : DltHeader
{
    #region Private Fields

    private const int MinLength = 10;

    #region Masks & Shifts

    private const int VerboseMask = 0b00000001;

    private const int MessageTypeMask = 0b00001110;

    private const int MessageTypeInfoMask = 0b11110000;

    private const int MessageTypeShift = 1;

    private const int MessageTypeInfoShift = 4;

    #endregion

    #endregion

    #region Properties

    #region DltHeader

    public override int HeaderLength => MinLength;

    #endregion

    /// <summary>
    /// If set, a description of the transmitted data is provided within the payload.
    /// If not set, this information will be given within a file.
    /// </summary>
    public bool Verbose { get; }

    /// <summary>
    /// A field describes the transmitted Dlt message
    /// </summary>
    public MessageType MessageType { get; }

    /// <summary>
    /// A field depends on the Message Type
    /// </summary>
    public MessageInfo MessageTypeInfo { get; }

    /// <summary>
    /// Number of arguments in the message payload
    /// </summary>
    public int ArgumentsNumber { get; }

    /// <summary>
    /// Number / ID of application
    /// </summary>
    public string ApplicationId { get; }

    /// <summary>
    /// Unique ID of logging / tracing context
    /// </summary>
    public string ContextId { get; }

    #endregion

    public DltExtendedHeader() : base(true, 0)
    {
        MessageTypeInfo = new EmptyMessageInfo();
        ApplicationId = ContextId = "";
    }

    private DltExtendedHeader(bool verbose, MessageType messageType, MessageInfo messageTypeInfo, int argumentsNumber, string appId, string ctxId, int length) : base(false, length)
    {
        Verbose = verbose;
        MessageType = messageType;
        MessageTypeInfo = messageTypeInfo;
        ArgumentsNumber = argumentsNumber;
        ApplicationId = appId.Trim('\0');
        ContextId = ctxId.Trim('\0');
    }

    public static DltExtendedHeader CreateHeader(byte[] data)
    {
        // Validate argument
        if (data.Length < MinLength)
            throw new ArgumentException($"Extended header must be {MinLength} or more", nameof(data));

        // Parse bytes
        var messageInfoByte = data[0];
        var verbose = Convert.ToBoolean(messageInfoByte & VerboseMask);
        var messageType = (MessageType)((messageInfoByte & MessageTypeMask) >> MessageTypeShift);
        var messageTypeInfoValue = (messageInfoByte & MessageTypeInfoMask) >> MessageTypeInfoShift;
        MessageInfo messageTypeInfo = messageType switch
        {
            MessageType.Log => (MessageLogInfo)messageTypeInfoValue,
            MessageType.AppTrace => (MessageTraceInfo)messageTypeInfoValue,
            MessageType.NetworkTrace => (MessageBusInfo)messageTypeInfoValue,
            MessageType.Control => (MessageControlInfo)messageTypeInfoValue,
            _ => new EmptyMessageInfo()
        };

        int argumentsNumber = data.GetBytes(1, 1, false)[0];
        var appId = Encoding.ASCII.GetString(data.GetBytes(2, 4, true));
        var ctxId = Encoding.ASCII.GetString(data.GetBytes(6, 4, true));

        return new DltExtendedHeader(verbose, messageType, messageTypeInfo, argumentsNumber, appId, ctxId, MinLength);
    }

    public override string ToString() =>
        $"ExtendedHeader(Verbose:{Verbose}, " +
        $"MessageType:{MessageType}, " +
        $"MessageTypeInfo:{MessageTypeInfo.Value}, " +
        $"NumberOfArguments:{ArgumentsNumber}, " +
        $"AppId:{ApplicationId}, " +
        $"ContextId:{ContextId})";
}
