namespace Dlt.Net.Tests;

public class Tests
{
    private byte[] _dltData;

    [SetUp]
    public void Setup()
    {
        _dltData = new byte[]{};
    }

    [Test]
    public void Test1()
    {
        var dltMsg = DltMessage.CreateMessage(_dltData);
    }
}