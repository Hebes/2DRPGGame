using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    场景加载管理

-----------------------*/

namespace Core
{
    public class CoreSceneManager : ICore
    {
        public static CoreSceneManager Instance;
        private ISceneLoad sceneLoad = new UnityLoadScene();
        public void ICroeInit()
        {
            Instance = this;
        }

        public async UniTask LoadSceneAsync(string sceneName, ELoadSceneModel loadSceneModel = ELoadSceneModel.Additive)
        {
            await sceneLoad.LoadSceneAsync(sceneName, loadSceneModel);
        }
        public async UniTask UnloadSceneAsync(string sceneName)
        {
            await sceneLoad.UnloadSceneAsync(sceneName);
        }
        public async UniTask ChangeSceneAsync(string oldSceneName, string newSceneName, ELoadSceneModel loadSceneModel = ELoadSceneModel.Additive)
        {
            await sceneLoad.ChangeSceneAsync(oldSceneName, newSceneName, loadSceneModel);
        }
    }
}
