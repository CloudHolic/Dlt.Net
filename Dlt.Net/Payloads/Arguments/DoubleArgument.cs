using Dlt.Net.Utils;
using System.Globalization;

namespace Dlt.Net.Payloads.Arguments;

public class DoubleArgument : Argument
{
    #region Properties

    public double Data { get; }

    #endregion

    private DoubleArgument(double payload, ArgumentType type) : base(type)
    {
        Data = payload;
    }

    public static DoubleArgument CreateDoubleArgument(byte[] data, int length, bool msbFirst)
    {
        var rawData = data.GetBytes(0, length, !msbFirst);

        return length switch
        {
            4 => new DoubleArgument(BitConverter.ToSingle(rawData), ArgumentType.Float32),
            8 => new DoubleArgument(BitConverter.ToDouble(rawData), ArgumentType.Float64),
            _ => throw new ArgumentOutOfRangeException(nameof(length), length, null)
        };
    }

    public override string ToString() => Data.ToString(CultureInfo.CurrentCulture);
}
