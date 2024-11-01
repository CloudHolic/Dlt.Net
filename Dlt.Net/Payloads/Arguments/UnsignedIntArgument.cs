using Dlt.Net.Utils;

namespace Dlt.Net.Payloads.Arguments;

public class UnsignedIntArgument : Argument
{
    #region Properties

    public ulong Data { get; }

    #endregion

    private UnsignedIntArgument(ulong payload, ArgumentType type) : base(type)
    {
        Data = payload;
    }

    public static UnsignedIntArgument CreateUintArgument(byte[] data, int length, bool msbFirst)
    {
        var rawData = data.GetBytes(0, length, !msbFirst);
        return length switch
        {
            1 => new UnsignedIntArgument(rawData[0], ArgumentType.Uint8),
            2 => new UnsignedIntArgument(BitConverter.ToUInt16(rawData), ArgumentType.Uint16),
            4 => new UnsignedIntArgument(BitConverter.ToUInt32(rawData), ArgumentType.Uint32),
            8 => new UnsignedIntArgument(BitConverter.ToUInt64(rawData), ArgumentType.Uint64),
            _ => throw new ArgumentOutOfRangeException(nameof(length), length, null)
        };
    }

    public override string ToString() => Data.ToString();
}
