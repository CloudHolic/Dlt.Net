using Dlt.Net.Utils;

namespace Dlt.Net.Payloads.Arguments;

public class SignedIntArgument : Argument
{
    #region Properties

    public long Data { get; }

    #endregion

    private SignedIntArgument(long payload, ArgumentType type) : base(type)
    {
        Data = payload;
    }

    public static SignedIntArgument CreateIntArgument(byte[] data, int length, bool msbFirst)
    {
        var rawData = data.GetBytes(0, length, !msbFirst);
        return length switch
        {
            1 => new SignedIntArgument(BitConverter.ToChar(rawData), ArgumentType.Int8),
            2 => new SignedIntArgument(BitConverter.ToInt16(rawData), ArgumentType.Int16),
            4 => new SignedIntArgument(BitConverter.ToInt32(rawData), ArgumentType.Int32),
            8 => new SignedIntArgument(BitConverter.ToInt64(rawData), ArgumentType.Int64),
            _ => throw new ArgumentOutOfRangeException(nameof(length), length, null)
        };
    }

    public override string ToString() => Data.ToString();
}
