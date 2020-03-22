using System.Data.Common;

namespace IDDDCommon.Port.Adapter.Persistence
{
    public abstract class DatabaseProvider
    {
        /// <summary>
        /// DatabaseのFactoryを取得する。
        /// </summary>
        protected abstract DbProviderFactory Factory { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected abstract string GetConnectionString();
        public virtual void Dispose() {}
    }
}