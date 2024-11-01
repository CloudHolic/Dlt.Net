using Dlt.Net.Utils;

namespace Dlt.Net.Payloads.Arguments;

public abstract class Argument
{
    #region Private Fields

    private const int TypeInfoLength = 4;

    #region Masks & Shifts

    private const int BaseTypeMask = 0b00000000000000000110011111110000;

    private const int TypeLengthMask = 0b00000000000000000000000000001111;

    private const int VariableInfoMask = 0b00000000000000000000100000000000;

    private const int FixedPointMask = 0b00000000000000000001000000000000;

    private const int EncodingMask = 0b00000000000000111000000000000000;

    #endregion

    #endregion

    #region Properties

    public ArgumentType Type { get; }

    public int Length { get; }

    public int ArgumentLength => Length + TypeInfoLength;

    #endregion

    protected Argument(ArgumentType type, int length = 0)
    {
        Type = type;

        Length = type switch
        {
            ArgumentType.Boolean or ArgumentType.Int8 or ArgumentType.Uint8 => 1,
            ArgumentType.Int16 or ArgumentType.Uint16 => 2,
            ArgumentType.Int32 or ArgumentType.Uint32 or ArgumentType.Float32 => 4,
            ArgumentType.Int64 or ArgumentType.Uint64 or ArgumentType.Float64 => 8,
            ArgumentType.StringAscii or ArgumentType.StringUtf8 or ArgumentType.Raw => length + 2,
            _ => 0
        };
    }

    public static Argument CreateArgument(byte[] data, bool msbFirst)
    {
        var typeInfo = BitConverter.ToUInt32(data.GetBytes(0, 4, !msbFirst));

        return (typeInfo & BaseTypeMask) switch
        {
            TypeInfo.Bool => BoolArgument.CreateBoolArgument(data[4..], msbFirst),

            TypeInfo.Signed =>
                SignedIntArgument.CreateIntArgument(data[4..], (typeInfo & TypeLengthMask) switch
                {
                    TypeInfo.Length8 => 1,
                    TypeInfo.Length16 => 2,
                    TypeInfo.Length32 => 4,
                    TypeInfo.Length64 => 8,
                    _ => throw new ArgumentOutOfRangeException(nameof(typeInfo))
                }, msbFirst),

            TypeInfo.Unsigned =>
                UnsignedIntArgument.CreateUintArgument(data[4..], (typeInfo & TypeLengthMask) switch
                {
                    TypeInfo.Length8 => 1,
                    TypeInfo.Length16 => 2,
                    TypeInfo.Length32 => 4,
                    TypeInfo.Length64 => 8,
                    _ => throw new ArgumentOutOfRangeException(nameof(typeInfo))
                }, msbFirst),

            TypeInfo.Float =>
                DoubleArgument.CreateDoubleArgument(data[4..], (typeInfo & TypeLengthMask) switch
                {
                    TypeInfo.Length32 => 4,
                    TypeInfo.Length64 => 8,
                    _ => throw new ArgumentOutOfRangeException(nameof(typeInfo))
                }, msbFirst),

            TypeInfo.String =>
                StringArgument.CreateStringArgument(data[4..], (typeInfo & EncodingMask) switch
                {
                    TypeInfo.StringAscii => true,
                    TypeInfo.StringUtf8 => false,
                    _ => throw new ArgumentOutOfRangeException(nameof(typeInfo))
                }, msbFirst),

            TypeInfo.Raw => RawArgument.CreateRawArgument(data[4..], msbFirst),

            _ => throw new NotSupportedException($"Unsupported TypeInfo: {typeInfo}")
        };
    }

    public abstract override string ToString();
}
