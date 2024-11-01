namespace Dlt.Net.Payloads;

public interface IPayload
{
    #region Properties

    /// <summary>
    /// Human-readable payload string
    /// </summary>
    public string Payload { get; }

    /// <summary>
    /// All payload data of a Dlt message
    /// </summary>
    public byte[] PayloadBytes { get; }

    public bool IsEmpty { get; }

    #endregion

    public string ToString();
}
