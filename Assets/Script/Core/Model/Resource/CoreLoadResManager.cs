using Cysharp.Threading.Tasks;

/*--------脚本描述-----------
				
电子邮箱：
	1607388033@qq.com
作者:
	暗沉
描述:
    资源加载

-----------------------*/

namespace Core
{
    /// <summary>
    /// 加载资源的方式
    /// </summary>
    public enum ELoadType
    {
        ReResources,
        YooAsset,
    }

    public class CoreLoadResManager : ICore
    {
        public static CoreLoadResManager Instance;
        private IResload iload;
        public void ICroeInit()
        {
            Instance = this;
            iload = new UnityResLoad();
        }

        public T Load<T>(string ResName) where T : UnityEngine.Object
        {
            return iload.Load<T>(ResName);
        }
        public UniTask<T> LoadAsync<T>(string assetName) where T : UnityEngine.Object
        {
            return iload.LoadAsync<T>(assetName);
        }

        //加载子资源对象
        public T LoadSub<T>(string location, string AssetName) where T : UnityEngine.Object
        {
            return iload.LoadSub<T>(location, AssetName);
        }
        public UniTask<T> LoadSubAsync<T>(string location, string AssetName) where T : UnityEngine.Object
        {
            return iload.LoadSubAsync<T>(location, AssetName);

        }

        //加载所有资源
        public T[] LoadAll<T>(string AssetName) where T : UnityEngine.Object
        {
            return iload.LoadAll<T>(AssetName);
        }
        public UniTask<T[]> LoadAllAsync<T>(string AssetName) where T : UnityEngine.Object
        {
            return iload.LoadAllAsync<T>(AssetName);
        }

        public void UnloadAssets()
        {
            iload.UnloadAssets();
        }


        public void ReleaseAsset(string AssetName = null)
        {
            iload.ReleaseAsset();
        }
    }
}
