namespace RedisExample.Abstractions
{
    /// <summary>
    /// The redis configuration
    /// </summary>
    public class RedisConfiguration
    {
        /// <summary>
        /// The redis setting name
        /// </summary>
        public const string RedisSettingName = "Redis";

        /// <summary>
        /// The redis host
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// The redis port
        /// </summary>
        public int Port { get; set; }
    }
}
