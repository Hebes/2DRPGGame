using Cysharp.Threading.Tasks;
using System.Collections.Generic;

/*--------脚本描述-----------

电子邮箱：
	1607388033@qq.com
作者:
	暗沉
描述:
	初始化游戏

-----------------------*/

namespace Core
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
            SwitchInitGameProcess(EInitGameProcess.FSMInitCore).Forget();
        }

        private async UniTaskVoid SwitchInitGameProcess(EInitGameProcess initGameProcess)
        {
            switch (initGameProcess)
            {
                case EInitGameProcess.FSMInitCore: await FSMInitCore(); break;
                    //case EInitGameProcess.FSMInitManagerCore: FSMInitManagerCore(); break;
                    //case EInitGameProcess.FSMInitModel: FSMInitModel(); break;
                    //case EInitGameProcess.FSMInitUI: FSMInitUI(); break;
                    //case EInitGameProcess.FSMEnterGame: FSMEnterGame().Forget(); break;
            }
        }

        private async UniTask FSMInitCore()
        {
            List<ICore> _initHs = new List<ICore>();
            _initHs.Add(new DebugManager());//日志管理
            _initHs.Add(new EventManager());//事件管理
            _initHs.Add(new DataManager());//事件管理
            _initHs.Add(new ResourceManager());//加载管理
            _initHs.Add(new UIManager());//UI管理

            foreach (var init in _initHs)
            {
                init.ICroeInit();
                await UniTask.Yield();
            }
            SwitchInitGameProcess(EInitGameProcess.FSMInitManagerCore);
        }
    }
}
