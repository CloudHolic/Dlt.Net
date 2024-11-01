using Dlt.Net.Utils;

namespace Dlt.Net.Payloads.Arguments;

public class BoolArgument : Argument
{
    #region Properties

    public bool Data { get; }

    #endregion

    private BoolArgument(bool payload) : base(ArgumentType.Boolean)
    {
        Data = payload;
    }

    public static BoolArgument CreateBoolArgument(byte[] data, bool msbFirst)
    {
        var payload = BitConverter.ToBoolean(data.GetBytes(0, 1, !msbFirst));
        return new BoolArgument(payload);
    }

    public override string ToString() => Data.ToString();
}
