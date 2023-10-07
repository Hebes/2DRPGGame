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
                case EInitGameProcess.FSMInitManagerCore: FSMInitManagerCore().Forget(); break;
                //case EInitGameProcess.FSMInitModel: FSMInitModel(); break;
                //case EInitGameProcess.FSMInitUI: FSMInitUI(); break;
                case EInitGameProcess.FSMEnterGame: FSMEnterGame().Forget(); break;
            }
        }

        private async UniTask FSMInitManagerCore()
        {
            List<IModelInit> _initHs = new List<IModelInit>();
            _initHs.Add(new ModelEffectManager());
            _initHs.Add(new ModelAudioManager());
            _initHs.Add(new ModelUIManager());
            _initHs.Add(new SaveManager());
            _initHs.Add(new ModelDataManager());
            _initHs.Add(new ModelSkillManager());

            foreach (var init in _initHs)
                await init.Init();
            SwitchInitGameProcess(EInitGameProcess.FSMEnterGame);
        }

        private async UniTask FSMEnterGame()
        {
            await UniTask.Yield();

            //显示UI  TODO后续要修改到其他地方
            //第一梯队生成的
            ConfigPrefab.prefabUIPanel_DarkScreen.ShwoUIPanel<UIPanel_DarkScreen>();
            //第二梯队生成的
            ConfigPrefab.prefabUIPanel_RestartGame.ShwoUIPanel<UIPanel_RestartGame>();    //中心游戏
            ConfigPrefab.prefabUIPanel_Character.ShwoUIPanel<UIPanel_Character>();        //角色
            ConfigPrefab.prefabUIPanel_Options.ShwoUIPanel<UIPanel_Options>();            //设置
            ConfigPrefab.prefabUIPanel_Craft.ShwoUIPanel<UIPanel_Craft>();                //工艺界面
            ConfigPrefab.prefabUIPanel_MainMenu.ShwoUIPanel<UIPanel_MainMenu>();          //主菜单面板

            ConfigPrefab.prefabUIPanel_DarkScreen.CloseUIPanel();
            ConfigPrefab.prefabUIPanel_RestartGame.CloseUIPanel();    //中心游戏
            ConfigPrefab.prefabUIPanel_Character.CloseUIPanel();        //角色
            ConfigPrefab.prefabUIPanel_Options.CloseUIPanel();            //设置
            ConfigPrefab.prefabUIPanel_Craft.CloseUIPanel();                //工艺界面
            
        }

        private async UniTask FSMInitCore()
        {
            List<ICore> _initHs = new List<ICore>();
            _initHs.Add(new CoreDebugManager());//日志管理
            _initHs.Add(new CoreMonoManager());//Mono管理
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
