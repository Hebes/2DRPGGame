using Cysharp.Threading.Tasks;

/*--------脚本描述-----------
				
电子邮箱：
	1607388033@qq.com
作者:
	暗沉
描述:
    场景加载拓展类

-----------------------*/

namespace Core
{
    public static class SceneExpansion
    {
        public static async UniTask LoadSceneAsync(this string sceneName, ELoadSceneModel loadSceneModel = ELoadSceneModel.Additive)
        {
            await CoreSceneManager.Instance.LoadSceneAsync(sceneName, loadSceneModel);
        }
        public static async UniTask UnloadSceneAsync(this string sceneName)
        {
            await CoreSceneManager.Instance.UnloadSceneAsync(sceneName);
        }
        public static async UniTask ChangeSceneAsync(this string oldSceneName, string newSceneName, ELoadSceneModel loadSceneModel = ELoadSceneModel.Additive)
        {
            await CoreSceneManager.Instance.ChangeSceneAsync(oldSceneName, newSceneName, loadSceneModel);
        }
    }
}
