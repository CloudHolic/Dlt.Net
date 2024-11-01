namespace Dlt.Net.Utils;

public static class ByteArrayExtensions
{
    public static byte[] GetBytes(this byte[] data, int offset, int count, bool littleEndian)
    {
        var endianFlip = BitConverter.IsLittleEndian != littleEndian;
        var split = data[offset..(offset + count)];
        if (endianFlip)
            Array.Reverse(split);

        return split;
    }
}
