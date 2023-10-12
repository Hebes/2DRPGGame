using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core
{
    public class UnityResLoad : IResload
    {
        public T Load<T>(string AssetName) where T : UnityEngine.Object
        {
            T t = Resources.Load<T>(AssetName);
            if (t == null)
                Debug.Error($"资源为空:{AssetName}");
            return t;
        }
        public async UniTask<T> LoadAsync<T>(string AssetName) where T : UnityEngine.Object
        {
            ResourceRequest t = Resources.LoadAsync<T>(AssetName);
            await t.ToUniTask();
            if (t.isDone == false)
                Debug.Error($"资源为空{AssetName}");
            return t.asset as T;
        }

        public T[] LoadAll<T>(string AssetName) where T : UnityEngine.Object
        {
            T[] values = Resources.LoadAll<T>(AssetName);
            if (values == null && values.Length <= 0)
                Debug.Error($"资源为空{AssetName}");
            return values;
        }

        public UniTask<T[]> LoadAllAsync<T>(string location) where T : UnityEngine.Object
        {

            return default;
        }


        public T LoadSub<T>(string location, string ResName) where T : UnityEngine.Object
        {
            return null;
        }

        public UniTask<T> LoadSubAsync<T>(string location, string ResName) where T : UnityEngine.Object
        {
            return default;
        }

        public void ReleaseAsset(string ResName = null)
        {
        }

        public void UnloadAssets()
        {
            Resources.UnloadUnusedAssets();
        }
    }
}
