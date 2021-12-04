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
        public ConfigurationOptions GetRedisConfiguration(bool allowAdmin = false)
        {
            var configuration = _configuration.GetSection(RedisConfiguration.RedisSettingName).Get<RedisConfiguration>();
            Validator.NullCheck(nameof(RedisConfiguration), configuration);

            var config = new ConfigurationOptions
            {
                EndPoints = { { configuration.Host, configuration.Port } },
                AllowAdmin = allowAdmin
            };

            return config;
        }

        /// <summary>
        /// The opens redis link
        /// </summary>
        /// <param name="database"></param>
        /// <returns></returns>
        public ConnectionMultiplexer OpenConnection(out IDatabase database, bool allowAdmin = false)
        {
            var connection = ConnectionMultiplexer.Connect(GetRedisConfiguration(allowAdmin));

            database = connection.GetDatabase();
            return connection;
        }
    }
}
