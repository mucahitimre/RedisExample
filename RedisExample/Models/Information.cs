namespace RedisExample.Models
{
    /// <summary>
    /// The redis information
    /// </summary>
    public class Information
    {
        /// <summary>
        /// The redis information
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="clientName"></param>
        /// <param name="operationCount"></param>
        /// <param name="timeoutMilliseconds"></param>
        /// <param name="isConnected"></param>
        /// <param name="version"></param>
        /// <param name="serverType"></param>
        /// <param name="features"></param>
        public Information(string configuration, string clientName, long operationCount, int timeoutMilliseconds, bool isConnected, Version version, ServerType serverType, RedisFeatures features)
        {
            Configuration = configuration;
            ClientName = clientName;
            OperationCount = operationCount;
            TimeoutMilliseconds = timeoutMilliseconds;
            IsConnected = isConnected;
            Version = version;
            ServerType = serverType;
            Features = features;
        }

        public string Configuration { get; }
        public string ClientName { get; }
        public long OperationCount { get; }
        public int TimeoutMilliseconds { get; }
        public bool IsConnected { get; }
        public Version Version { get; }
        public ServerType ServerType { get; }
        public RedisFeatures Features { get; }

        /// <summary>
        /// The equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return obj is Information info &&
                   Configuration == info.Configuration &&
                   ClientName == info.ClientName &&
                   OperationCount == info.OperationCount &&
                   TimeoutMilliseconds == info.TimeoutMilliseconds &&
                   IsConnected == info.IsConnected &&
                   EqualityComparer<Version>.Default.Equals(Version, info.Version) &&
                   ServerType == info.ServerType &&
                   EqualityComparer<RedisFeatures>.Default.Equals(Features, info.Features);
        }

        /// <summary>
        /// The hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Configuration, ClientName, OperationCount, TimeoutMilliseconds, IsConnected, Version, ServerType, Features);
        }
    }
}
