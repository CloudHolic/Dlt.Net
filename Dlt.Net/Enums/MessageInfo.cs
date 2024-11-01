using OneOf;

namespace Dlt.Net.Enums;

public record EmptyMessageInfo;

[GenerateOneOf]
public partial class MessageInfo : OneOfBase<MessageLogInfo, MessageTraceInfo, MessageBusInfo, MessageControlInfo, EmptyMessageInfo>
{
}
