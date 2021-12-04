namespace RedisExample
{
    /// <summary>
    /// The validator
    /// </summary>
    public class Validator
    {
        /// <summary>
        /// The check
        /// </summary>
        /// <param name="name"></param>
        /// <param name="method"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Check(string name, Func<bool> method, Exception exception = null)
        {
            if (method.Invoke())
            {
                if (exception != null)
                {
                    throw exception;    
                }

                throw new ArgumentNullException(name);
            }
        }

        /// <summary>
        /// The null check
        /// </summary>
        /// <param name="name"></param>
        /// <param name="object"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void NullCheck(string name, object @object)
        {
            if (@object == null)
            {
                throw new ArgumentNullException(name);
            }
        }
    }
}
