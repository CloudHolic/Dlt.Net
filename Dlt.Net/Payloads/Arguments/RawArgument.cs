using Dlt.Net.Utils;

namespace Dlt.Net.Payloads.Arguments;

public class RawArgument : Argument
{
    #region Properties

    public byte[] Data { get; }

    #endregion

    private RawArgument(byte[] payload, int length) : base(ArgumentType.Raw, length)
    {
        Data = payload;
    }

    public static RawArgument CreateRawArgument(byte[] data, bool msbFirst)
    {
        int length = BitConverter.ToUInt16(data.GetBytes(0, 2, !msbFirst));
        var rawData = data.GetBytes(2, length, !msbFirst);

        return new RawArgument(rawData, length);
    }

    public override string ToString() => BitConverter.ToString(Data).Replace("-", " ");
}
