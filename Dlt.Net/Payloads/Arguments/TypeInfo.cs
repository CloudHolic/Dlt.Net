namespace Dlt.Net.Payloads.Arguments;

public static class TypeInfo
{
    public const uint Length8 = 0b00000000000000000000000000000001;
    public const uint Length16 = 0b00000000000000000000000000000010;
    public const uint Length32 = 0b00000000000000000000000000000011;
    public const uint Length64 = 0b00000000000000000000000000000100;
    public const uint Length128 = 0b00000000000000000000000000000101;

    public const uint Bool = 0b00000000000000000000000000010000;
    public const uint Signed = 0b00000000000000000000000000100000;
    public const uint Unsigned = 0b00000000000000000000000001000000;
    public const uint Float = 0b00000000000000000000000010000000;
    public const uint Array = 0b00000000000000000000000100000000;
    public const uint String = 0b00000000000000000000001000000000;
    public const uint Raw = 0b00000000000000000000010000000000;
    public const uint VariableInfo = 0b00000000000000000000100000000000;
    public const uint FixedPoint = 0b00000000000000000001000000000000;
    public const uint TraceInfo = 0b00000000000000000010000000000000;
    public const uint Struct = 0b00000000000000000100000000000000;
    public const uint StringAscii = 0b00000000000000000000000000000000;
    public const uint StringUtf8 = 0b00000000000000001000000000000000;
}

public enum ArgumentType
{
    Boolean,

    Int8,
    Int16,
    Int32,
    Int64,
    Uint8,
    Uint16,
    Uint32,
    Uint64,

    Float32,
    Float64,

    StringAscii,
    StringUtf8,
    Raw
}