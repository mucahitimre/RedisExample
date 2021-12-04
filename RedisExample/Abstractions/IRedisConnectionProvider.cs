namespace RedisExample.Abstractions
{
    /// <summary>
    /// The redis connection provider
    /// </summary>
    public interface IRedisConnectionProvider
    {
        /// <summary>
        /// The get redis configuration
        /// </summary>
        /// <returns></returns>
        ConfigurationOptions GetRedisConfiguration(bool allowAdmin = false);

        /// <summary>
        /// The opens redis link
        /// </summary>
        /// <param name="database"></param>
        /// <returns></returns>
        ConnectionMultiplexer OpenConnection(out IDatabase database, bool allowAdmin = false);
    }
}
