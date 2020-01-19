using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class RedisHelper
    {
        public static IRedisClientsManager clientManager = new PooledRedisClientManager(new string[] { "127.0.0.1:6379" });
        public static IRedisClient redisClient = clientManager.GetClient();
        /// <summary>
        /// 进队
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void EnQueue(string key,string value)
        {
            redisClient.EnqueueItemOnList(key, value);
        }
        /// <summary>
        /// 出队
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string DeQueue(string key)
        {
            return redisClient.DequeueItemFromList(key);
        }
        /// <summary>
        /// 获取队列中的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetQueueCount(string key)
        {
            return redisClient.GetListCount(key);
        }
    }
}
