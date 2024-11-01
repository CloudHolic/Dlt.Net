using Dlt.Net.Payloads.Arguments;

namespace Dlt.Net.Payloads;

public class VerbosePayload : IPayload
{
    #region Properties

    public List<Argument> Arguments { get; }

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

    public VerbosePayload()
    {
        Payload = string.Empty;
        PayloadBytes = Array.Empty<byte>();
        Arguments = new List<Argument>();
        IsEmpty = true;
    }

    private VerbosePayload(byte[] data, List<Argument> arguments)
    {
        PayloadBytes = data;
        Arguments = arguments;
        IsEmpty = false;
        Payload = string.Join("", arguments);
    }

    public static VerbosePayload CreatePayload(byte[] data, bool msbFirst, int argumentsNumber)
    {
        var arguments = new List<Argument>();
        for (var (i, offset) = (0, 0); i < argumentsNumber; i++)
        {
            var arg = Argument.CreateArgument(data[offset..], msbFirst);
            arguments.Add(arg);
            offset += arg.ArgumentLength;
        }

        return new VerbosePayload(data, arguments);
    }

    public override string ToString() => Payload;
}
