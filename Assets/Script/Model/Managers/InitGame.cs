using Core;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;

/*--------脚本描述-----------

电子邮箱：
	1607388033@qq.com
作者:
	暗沉
描述:
	初始化游戏

-----------------------*/

namespace RPGGame
{
    /// <summary> 游戏流程 </summary>
    public enum EInitGameProcess
    {
        /// <summary> 初始化框架基础核心 </summary>
        FSMInitCore,
        /// <summary> 初始化框架管理核心 </summary>
        FSMInitManagerCore,
        /// <summary> 初始化数据 </summary>
        FSMInitModel,
        /// <summary> 初始化UI </summary>
        FSMInitUI,
        /// <summary> 进入游戏 </summary>
        FSMEnterGame,
    }

    public class InitGame
    {

        public void Start()
        {
            SwitchInitGameProcess(EInitGameProcess.FSMInitCore);
        }

        private void SwitchInitGameProcess(EInitGameProcess initGameProcess) 
        {
            switch (initGameProcess)
            {
                case EInitGameProcess.FSMInitCore: FSMInitCore().Forget(); break;
                case EInitGameProcess.FSMInitManagerCore: FSMInitManagerCore(); break;
                //case EInitGameProcess.FSMInitModel: FSMInitModel(); break;
                //case EInitGameProcess.FSMInitUI: FSMInitUI(); break;
                case EInitGameProcess.FSMEnterGame: FSMEnterGame().Forget(); break;
            }
        }

        private async UniTask FSMInitManagerCore()
        {
            List<IModelInit> _initHs = new List<IModelInit>();
            _initHs.Add(new ModelEffectManager());//特效管理

            foreach (var init in _initHs)
                await init.Init();
            SwitchInitGameProcess(EInitGameProcess.FSMEnterGame);
        }

        private async UniTask FSMEnterGame()
        {
            Debug.Log("加载场景");
            await ConfigScenes.unityMainScene.LoadSceneAsync(ELoadSceneModel.Single);
        }

        private async UniTask FSMInitCore()
        {
            List<ICore> _initHs = new List<ICore>();
            _initHs.Add(new CoreDebugManager());//日志管理
            _initHs.Add(new CoreEventManager());//事件管理
            _initHs.Add(new CoreDataManager());//事件管理
            _initHs.Add(new CoreLoadResManager());//加载管理
            _initHs.Add(new CroeUIManager());//UI管理
            _initHs.Add(new CoreSceneManager());//场景管理

            foreach (var init in _initHs)
            {
                init.ICroeInit();
                await UniTask.Yield();
            }
             SwitchInitGameProcess(EInitGameProcess.FSMInitManagerCore);
        }
    }
}
