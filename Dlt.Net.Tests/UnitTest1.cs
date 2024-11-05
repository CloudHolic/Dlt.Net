namespace Dlt.Net.Tests;

public class Tests
{
    private byte[] _dltData1, _dltData2, _dltData3, _dltData4, _dltData5;

    [SetUp]
    public void Setup()
    {
        _dltData1 = new byte[]
        {
            0x44, 0x4c, 0x54, 0x01, 0xfe, 0xd1, 0x22, 0x67, 0xb8, 0x40, 0x0c, 0x20, 0x43, 0x43, 0x55, 0x32, 0x3d, 0x12, 0x20,
            0xa6, 0x43, 0x43, 0x55, 0x32, 0x20, 0x20, 0x04, 0xb9, 0x20, 0x05, 0x71, 0x33, 0x41, 0x0e, 0x41, 0x53, 0x4d, 0x20,
            0x4e, 0x54, 0x53, 0x4b, 0x20, 0x02, 0x20, 0x20, 0x12, 0x20, 0x53, 0x65, 0x74, 0x42, 0x75, 0x73, 0x4c, 0x6f, 0x61,
            0x64, 0x3a, 0x20, 0x46, 0x44, 0x42, 0x31, 0x3d, 0x20, 0x41, 0x20, 0x20, 0x20, 0x04, 0x20, 0x02, 0x20, 0x20, 0x08,
            0x20, 0x2c, 0x20, 0x46, 0x44, 0x42, 0x32, 0x3d, 0x20, 0x41, 0x20, 0x20, 0x20, 0x02, 0x20, 0x02, 0x20, 0x20, 0x07,
            0x20, 0x2c, 0x20, 0x46, 0x44, 0x4d, 0x3d, 0x20, 0x41, 0x20, 0x20, 0x20, 0x08, 0x20, 0x02, 0x20, 0x20, 0x08, 0x20,
            0x2c, 0x20, 0x46, 0x44, 0x50, 0x31, 0x3d, 0x20, 0x41, 0x20, 0x20, 0x20, 0x01, 0x20, 0x02, 0x20, 0x20, 0x08, 0x20,
            0x2c, 0x20, 0x46, 0x44, 0x50, 0x32, 0x3d, 0x20, 0x41, 0x20, 0x20, 0x20, 0x02, 0x20, 0x02, 0x20, 0x20, 0x07, 0x20,
            0x2c, 0x20, 0x46, 0x44, 0x43, 0x3d, 0x20, 0x41, 0x20, 0x20, 0x20, 0x01, 0x20, 0x02, 0x20, 0x20, 0x07, 0x20, 0x2c,
            0x20, 0x46, 0x44, 0x45, 0x3d, 0x20, 0x41, 0x20, 0x20, 0x20, 0x02
        };

        _dltData2 = new byte[]
        {
            0x44, 0x4c, 0x54, 0x01, 0xfe, 0xd1, 0x22, 0x67, 0xb8, 0x40, 0x0c, 0x20, 0x43, 0x43, 0x55, 0x32, 0x3d, 0xcb,
            0x20, 0x78, 0x43, 0x43, 0x55, 0x32, 0x20, 0x20, 0x03, 0xcc, 0x20, 0x05, 0x77, 0x2f, 0x31, 0x01, 0x41, 0x43, 
            0x53, 0x44, 0x56, 0x53, 0x49, 0x50, 0x20, 0x02, 0x20, 0x20, 0x58, 0x20, 0x64, 0x65, 0x6c, 0x69, 0x76, 0x65,
            0x72, 0x5f, 0x6e, 0x6f, 0x74, 0x69, 0x66, 0x69, 0x63, 0x61, 0x74, 0x69, 0x6f, 0x6e, 0x3a, 0x20, 0x45, 0x76,
            0x65, 0x6e, 0x74, 0x20, 0x5b, 0x31, 0x36, 0x38, 0x34, 0x2e, 0x30, 0x30, 0x30, 0x31, 0x2e, 0x61, 0x30, 0x30, 
            0x32, 0x5d, 0x20, 0x69, 0x73, 0x20, 0x6e, 0x6f, 0x74, 0x20, 0x72, 0x65, 0x67, 0x69, 0x73, 0x74, 0x65, 0x72, 
            0x65, 0x64, 0x2e, 0x20, 0x54, 0x68, 0x65, 0x20, 0x6d, 0x65, 0x73, 0x73, 0x61, 0x67, 0x65, 0x20, 0x69, 0x73, 
            0x20, 0x64, 0x72, 0x6f, 0x70, 0x70, 0x65, 0x64, 0x2e, 0x20
        };

        _dltData3 = new byte[]
        {
            0x44, 0x4c, 0x54, 0x01, 0xfe, 0xd1, 0x22, 0x67, 0x38, 0xa3, 0x06, 0x20, 0x43, 0x43, 0x55, 0x32, 0x35, 0x20, 0x20,
            0x51, 0x43, 0x43, 0x55, 0x32, 0x20, 0x01, 0x2e, 0x97, 0x26, 0x01, 0x44, 0x41, 0x31, 0x20, 0x44, 0x43, 0x31, 0x20,
            0x03, 0x20, 0x20, 0x20, 0x07, 0x01, 0x20, 0x4f, 0x54, 0x41, 0x20, 0x01, 0x20, 0x4f, 0x54, 0x41, 0x20, 0xff, 0xff,
            0x0e, 0x20, 0x4f, 0x54, 0x41, 0x20, 0x43, 0x6c, 0x69, 0x65, 0x6e, 0x74, 0x20, 0x41, 0x70, 0x70, 0x12, 0x20, 0x4f,
            0x54, 0x41, 0x20, 0x43, 0x6c, 0x69, 0x65, 0x6e, 0x74, 0x20, 0x4c, 0x6f, 0x67, 0x67, 0x69, 0x6e, 0x67, 0x72, 0x65,
            0x6d, 0x6f
        };

        _dltData4 = new byte[]
        {
            0x44, 0x4c, 0x54, 0x01, 0xfe, 0xd1, 0x22, 0x67, 0x50, 0x9f, 0x06, 0x20, 0x43, 0x43, 0x55, 0x32, 0x3d, 0x01,
            0x20, 0x65, 0x43, 0x43, 0x55, 0x32, 0x20, 0x20, 0x03, 0xd8, 0x20, 0x01, 0x2c, 0x9a, 0x41, 0x01, 0x44, 0x4c,
            0x54, 0x44, 0x49, 0x4e, 0x54, 0x4d, 0x20, 0x02, 0x20, 0x20, 0x45, 0x20, 0x41, 0x70, 0x70, 0x6c, 0x69, 0x63,
            0x61, 0x74, 0x69, 0x6f, 0x6e, 0x49, 0x44, 0x20, 0x27, 0x43, 0x53, 0x45, 0x27, 0x20, 0x72, 0x65, 0x67, 0x69,
            0x73, 0x74, 0x65, 0x72, 0x65, 0x64, 0x20, 0x66, 0x6f, 0x72, 0x20, 0x50, 0x49, 0x44, 0x20, 0x31, 0x36, 0x34,
            0x36, 0x2c, 0x20, 0x44, 0x65, 0x73, 0x63, 0x72, 0x69, 0x70, 0x74, 0x69, 0x6f, 0x6e, 0x3d, 0x43, 0x53, 0x45,
            0x20, 0x64, 0x6c, 0x74, 0x20, 0x6c, 0x6f, 0x67, 0x20, 0x44, 0x4c, 0x54, 0x01, 0xfe, 0xd1, 0x22, 0x67, 0x50,
            0x9f, 0x06, 0x20, 0x43, 0x43, 0x55
        };

        _dltData5 = new byte[]
        {
            0x44, 0x4c, 0x54, 0x01, 0xfe, 0xd1, 0x22, 0x67, 0x50, 0x9f, 0x06, 0x20, 0x43, 0x43, 0x55, 0x32, 0x3d, 0x01,
            0x20, 0x65, 0x43, 0x43, 0x55, 0x32, 0x20, 0x20, 0x03, 0xd8, 0x20, 0x01, 0x2c, 0x9a, 0x41, 0x01, 0x44, 0x4c,
            0x54, 0x44, 0x49, 0x4e, 0x54, 0x4d, 0x20, 0x02, 0x20, 0x20, 0x45, 0x20, 0x41, 0x70, 0x70, 0x6c, 0x69, 0x63,
            0x61, 0x74, 0x69, 0x6f, 0x6e, 0x49, 0x44, 0x20, 0x27, 0x43, 0x53, 0x45, 0x27, 0x20, 0x72, 0x65, 0x67, 0x69,
            0x73, 0x74, 0x65, 0x72, 0x65, 0x64, 0x20, 0x66, 0x6f, 0x72, 0x20, 0x50, 0x49, 0x44, 0x20, 0x31, 0x36, 0x34,
            0x36, 0x2c, 0x20, 0x44, 0x65, 0x73, 0x63, 0x72, 0x69, 0x70, 0x74, 0x69, 0x6f, 0x6e, 0x3d, 0x43, 0x53, 0x45,
            0x20, 0x64, 0x6c, 0x74, 0x20, 0x6c, 0x6f, 0x67, 0x20
        };
    }

    [Test]
    public void Test1()
    {
        DltMessage.TransformNullCharacter(ref _dltData1);
        var dltMsg = DltMessage.CreateMessage(_dltData1);

        //Assert.That(DltMessage.SplitMessageBytes(ref _dltData4, ref message));
        //var dltMessage = DltMessage.CreateMessage(_dltData4);
    }
}