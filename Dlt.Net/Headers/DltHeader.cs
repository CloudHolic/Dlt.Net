namespace Dlt.Net.Headers;

public abstract class DltHeader
{
    #region Properties
    public virtual int HeaderLength { get; }

    public virtual bool IsEmpty { get; }

    #endregion

    protected DltHeader(bool isEmpty, int length)
    {
        HeaderLength = length;
        IsEmpty = isEmpty;
    }

    public abstract override string ToString();
}
