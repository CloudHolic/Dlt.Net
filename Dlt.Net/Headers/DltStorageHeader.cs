using Dlt.Net.Utils;
using System.Collections.Immutable;
using System.Text;

namespace Dlt.Net.Headers;

public class DltStorageHeader : DltHeader
{
    #region Private Fields

    private const int MinLength = 16;

    private static readonly ImmutableArray<byte> DltPattern = ImmutableArray.Create(new byte[] { 0x44, 0x4c, 0x54, 0x01 });   // "DLT" + 0x01

    #endregion

    #region Properties

    #region DltHeader

    public override int HeaderLength => MinLength;

    #endregion

    /// <summary>
    /// A seconds since 1970.01.01 (unix timestamp)
    /// </summary>
    public uint Seconds { get; }

    /// <summary>
    /// A microseconds of the second (between 0 ~ 1_000_000)
    /// </summary>
    public int Microseconds { get; }

    /// <summary>
    /// The ECU ID
    /// </summary>
    public string EcuId { get; }

    #endregion

    public DltStorageHeader() : base(true, 0) => EcuId = "";

    private DltStorageHeader(uint second, int microsecond, string ecuId, int length) : base(false, length)
    {
        Seconds = second;
        Microseconds = microsecond;
        EcuId = ecuId.Trim('\0');
    }

    public static DltStorageHeader CreateHeader(byte[] data)
    {
        // Validate argument
        if (data.Length < MinLength)
            throw new ArgumentException($"Storage header must be {MinLength} or more", nameof(data));

        var pattern = data[..4];
        if (!DltPattern.SequenceEqual(pattern))
            throw new ArgumentException($"Beginning of Storage header must be {DltPattern.ToString()}");

        // Parse bytes
        var second = BitConverter.ToUInt32(data.GetBytes(4, 4, true));
        var microsecond = BitConverter.ToInt32(data.GetBytes(8, 4, true));
        var ecuId = Encoding.ASCII.GetString(data.GetBytes(12, 4, true));

        return new DltStorageHeader(second, microsecond, ecuId, MinLength);
    }

    public override string ToString() =>
        $"StorageHeader(Seconds:{Seconds}, " +
        $"Microseconds:{Microseconds}, " +
        $"EcuId:{EcuId}";
}
