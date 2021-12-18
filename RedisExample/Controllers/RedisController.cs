namespace RedisExample.Controllers
{
    /// <summary>
    /// The redis controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RedisController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IRedisConnectionProvider _connectionProvider;

        /// <summary>
        /// The redis controller
        /// </summary>
        public RedisController(
            IConfiguration configuration,
            IRedisConnectionProvider redisConnectionProvider)
        {
            _configuration = configuration;
            _connectionProvider = redisConnectionProvider;
        }

        /// <summary>
        /// The set string
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SetString")]
        public bool SetString(string key, string data)
        {
            Validator.Check(nameof(key), () => string.IsNullOrEmpty(key));
            Validator.Check(nameof(key), () => key.Contains(" "), new Exception("key cannot contain spaces"));
            Validator.Check(nameof(data), () => string.IsNullOrEmpty(data));

            using (var session = _connectionProvider.OpenConnection(out var database))
            {
                database.StringSetAsync(key, data);
            }

            return true;
        }

        /// <summary>
        /// The get string
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetString/{key}")]
        public string GetString(string key)
        {
            Validator.Check(nameof(key), () => string.IsNullOrWhiteSpace(key));

            using (var session = _connectionProvider.OpenConnection(out var database))
            {
                var data = database.StringGetAsync(key).Result;
                if (!string.IsNullOrEmpty(data))
                {
                    return data;
                }

                throw new KeyNotFoundException();
            }
        }

        /// <summary>
        /// The get info
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetInfo")]
        public Information GetInfo()
        {
            using (var session = _connectionProvider.OpenConnection(out var database))
            {
                var config = _connectionProvider.GetRedisConfiguration(true);
                var server = session.GetServer(session.GetEndPoints().First());
                var data = new Information(
                    session.Configuration,
                    session.ClientName,
                    session.OperationCount,
                    session.TimeoutMilliseconds,
                    session.IsConnected,
                    server.Version,
                    server.ServerType,
                    server.Features
                );

                return data;
            }
        }


        /// <summary>
        /// Delete string value
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteString")]
        public bool DeleteString(string key)
        {
            Validator.Check(nameof(key), () => string.IsNullOrWhiteSpace(key));

            using (var session = _connectionProvider.OpenConnection(out var database))
            {
                return database.KeyDeleteAsync(key).Result;
            }
        }

        /// <summary>
        /// The get all values
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllValues")]
        public async Task<IDictionary<string, string>> GetValuesAsync()
        {
            var redisConfig = _configuration.GetSection(RedisConfiguration.RedisSettingName).Get<RedisConfiguration>();
            using (var session = _connectionProvider.OpenConnection(out var database))
            {
                var server = session.GetServer(redisConfig.Host, redisConfig.Port);

                var data = server.KeysAsync(pattern: "*").GetAsyncEnumerator();

                var list = new Dictionary<string, string>();
                while (await data.MoveNextAsync())
                {
                    list.Add(data.Current, string.Empty);
                }

                foreach (var item in list)
                {
                    try
                    {
                        var value = database.StringGet(item.Key);
                        if (value.HasValue)
                        {
                            list[item.Key] = value;
                        }
                    }
                    catch (Exception)
                    {
                        // log..
                        continue;
                    }
                }

                return list;
            }
        }

        /// <summary>
        /// the delete all keys
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteAllKeys")]
        public bool DeleteAllKeys()
        {
            var redisConfig = _configuration.GetSection(RedisConfiguration.RedisSettingName).Get<RedisConfiguration>();
            using (var session = _connectionProvider.OpenConnection(out var database, true))
            {
                var server = session.GetServer(redisConfig.Host, redisConfig.Port);

                server.FlushDatabase();
            }

            return true;
        }
    }
}
