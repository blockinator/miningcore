using System.Collections.Generic;

namespace Miningcore.Blockchain
{
    public static class DevDonation
    {
        public const decimal Percent = 0.1m;

        public static readonly Dictionary<string, string> Addresses = new Dictionary<string, string>
        {
            { "BTC", "1Jtnju5EuWFs5QZNmp8g5JYhQHqRjwzw78" },
            { "BCH", "qpxcm3r90y6cedvazm4phwr82m3ywwn66gzwllq63l" },
            { "BTG", "GMrmWonMsBuz9J2EX28S3s3eHxbChxZU3t" },
            { "DASH", "XmGYLq6YFpMc6EMpsNHDsuMFGd37RMTaqA" },
            { "DOGE", "DDrA5dZTjjnyYPxT23wmG5X5sxqt7XNMQe" },
            { "DGB", "DMRrNu6JNky332ZsdLdNdo4WXGzmVQzCjn" },
            { "ETC", "0xBCB57A44dCD7b4B4834EF509eCE271BF27eB0ccB" },
            { "ETH", "0x745F2Bc9570B8C8DcD51249d7fdC2528f03efF1c" },
            { "LTC", "LKF12Fi92zuxDhpHLe7gSWBtTdJbcULa85" },
            { "RVN", "RGgnrqs29owWj7QfQAWJj74ugVB93aypYK" },
            { "XMR", "44c7umSm7TyXxKch9q4R5QfoTAf663A8yEFfJbxmxUJ1JCWq2kFu33oAAydrgNDQA8619rSQhZaFV3ScpESWCfcQB3Fqc6w" },
            { "ZEC", "t1eM9SymZUnJi1fw8PY3LU9SPmetBp4y4WS" }
        };
    }

    public static class CoinMetaData
    {
        public const string BlockHeightPH = "$height$";
        public const string BlockHashPH = "$hash$";
    }
}
