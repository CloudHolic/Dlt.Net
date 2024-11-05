using Dlt.Net.Utils;
using System.Text;

namespace Dlt.Net.Payloads.Arguments;

public class StringArgument : Argument
{
    #region Properties

    public string Data { get; }

    #endregion

    private StringArgument(string payload, ArgumentType type, int length) : base(type, length)
    {
        Data = payload.Trim('\0').Replace('\0', ' ');
    }

    public static StringArgument CreateStringArgument(byte[] data, bool isAscii, bool msbFirst)
    {
        int length = BitConverter.ToUInt16(data.GetBytes(0, 2, !msbFirst));
        var rawData = data.GetBytes(2, length, !msbFirst);

        return isAscii switch
        {
            true => new StringArgument(Encoding.ASCII.GetString(rawData), ArgumentType.StringAscii, length),
            false => new StringArgument(Encoding.UTF8.GetString(rawData), ArgumentType.StringUtf8, length)
        };
    }

    public override string ToString() => Data;
}
