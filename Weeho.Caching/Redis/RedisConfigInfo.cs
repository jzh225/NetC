using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Weeho.Caching.Redis
{
    /// <summary>
    /// Class RedisConfigInfo. This class cannot be inherited.
    /// </summary>
    /// <seealso cref="System.Configuration.ConfigurationSection" />
    public sealed class RedisConfigInfo : ConfigurationSection
    {
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <returns>RedisConfigInfo.</returns>
        public static RedisConfigInfo GetConfig()
        {
            try
            {
                RedisConfigInfo section = (RedisConfigInfo)ConfigurationManager.GetSection("RedisConfig");
                return section;
            }
            catch (Exception)
            {

                return new RedisConfigInfo();
            }

        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <param name="sectionName">Name of the section.</param>
        /// <returns>RedisConfigInfo.</returns>
        /// <exception cref="System.Configuration.ConfigurationErrorsException">Section " + sectionName + " is not found.</exception>
        public static RedisConfigInfo GetConfig(string sectionName)
        {
            RedisConfigInfo section = (RedisConfigInfo)ConfigurationManager.GetSection("RedisConfig");
            if (section == null)
                throw new ConfigurationErrorsException("Section " + sectionName + " is not found.");
            return section;
        }
        /// <summary>
        /// 可写的Redis链接地址
        /// </summary>
        /// <value>The write server list.</value>
        [ConfigurationProperty("WriteServerList", IsRequired = false)]
        public string WriteServerList
        {
            get
            {
                return (string)base["WriteServerList"];
            }
            set
            {
                base["WriteServerList"] = value;
            }
        }


        /// <summary>
        /// 可读的Redis链接地址
        /// </summary>
        /// <value>The read server list.</value>
        [ConfigurationProperty("ReadServerList", IsRequired = false)]
        public string ReadServerList
        {
            get
            {
                return (string)base["ReadServerList"];
            }
            set
            {
                base["ReadServerList"] = value;
            }
        }


        /// <summary>
        /// 最大写链接数
        /// </summary>
        /// <value>The maximum size of the write pool.</value>
        [ConfigurationProperty("MaxWritePoolSize", IsRequired = false, DefaultValue = 5)]
        public int MaxWritePoolSize
        {
            get
            {
                int _maxWritePoolSize = (int)base["MaxWritePoolSize"];
                return _maxWritePoolSize > 0 ? _maxWritePoolSize : 5;
            }
            set
            {
                base["MaxWritePoolSize"] = value;
            }
        }


        /// <summary>
        /// 最大读链接数
        /// </summary>
        /// <value>The maximum size of the read pool.</value>
        [ConfigurationProperty("MaxReadPoolSize", IsRequired = false, DefaultValue = 5)]
        public int MaxReadPoolSize
        {
            get
            {
                int _maxReadPoolSize = (int)base["MaxReadPoolSize"];
                return _maxReadPoolSize > 0 ? _maxReadPoolSize : 5;
            }
            set
            {
                base["MaxReadPoolSize"] = value;
            }
        }


        /// <summary>
        /// 自动重启
        /// </summary>
        /// <value><c>true</c> if [automatic start]; otherwise, <c>false</c>.</value>
        [ConfigurationProperty("AutoStart", IsRequired = false, DefaultValue = true)]
        public bool AutoStart
        {
            get
            {
                return (bool)base["AutoStart"];
            }
            set
            {
                base["AutoStart"] = value;
            }
        }



        /// <summary>
        /// 本地缓存到期时间，单位:秒
        /// </summary>
        /// <value>The local cache time.</value>
        [ConfigurationProperty("LocalCacheTime", IsRequired = false, DefaultValue = 36000)]
        public int LocalCacheTime
        {
            get
            {
                return (int)base["LocalCacheTime"];
            }
            set
            {
                base["LocalCacheTime"] = value;
            }
        }


        /// <summary>
        /// 是否记录日志,该设置仅用于排查redis运行时出现的问题,如redis工作正常,请关闭该项
        /// </summary>
        /// <value><c>true</c> if [recorde log]; otherwise, <c>false</c>.</value>
        [ConfigurationProperty("RecordeLog", IsRequired = false, DefaultValue = false)]
        public bool RecordeLog
        {
            get
            {
                return (bool)base["RecordeLog"];
            }
            set
            {
                base["RecordeLog"] = value;
            }
        }

    }
}
