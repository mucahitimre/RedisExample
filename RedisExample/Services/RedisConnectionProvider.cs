namespace RedisExample.Services
{
    /// <summary>
    /// The redis connection provider 
    /// </summary>
    public class RedisConnectionProvider : IRedisConnectionProvider
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// The redis connection provider
        /// </summary>
        /// <param name="configuration"></param>
        public RedisConnectionProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// The get redis configuration
        /// </summary>
        /// <returns></returns>
        public ConfigurationOptions GetRedisConfiguration()
        {
            var configuration = _configuration.GetSection(RedisConfiguration.RedisSettingName).Get<RedisConfiguration>();

            var config = new ConfigurationOptions
            {
                EndPoints = { { configuration.Host, configuration.Port } }
            };

            return config;
        }

        /// <summary>
        /// The opens redis link
        /// </summary>
        /// <param name="database"></param>
        /// <returns></returns>
        public ConnectionMultiplexer OpenConnection(out IDatabase database)
        {
            var connection = ConnectionMultiplexer.Connect(GetRedisConfiguration());

            database = connection.GetDatabase();
            return connection;
        }
    }
}
