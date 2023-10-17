using Core;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Debug = UnityEngine.Debug;

/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    UI模块

-----------------------*/

namespace RPGGame
{
    public class ModelUI : IModelInit, ISaveManager
    {

        public static ModelUI Instance;

        [SerializeField] private UI_VolumeSlider[] volumeSettings;

        public async UniTask Init()
        {
            Instance = this;
            CoreMono.Instance.UpdateAddEvent(OnUpdate);
            await UniTask.Yield();
        }



        void OnUpdate()
        {
            Debug.Log("继续了");
            //角色界面
            if (Input.GetKeyDown(KeyCode.C))
                SwitchWithKeyTo<UIPanel_Character>(ConfigPrefab.prefabUIPanel_Character);

            //职业界面
            if (Input.GetKeyDown(KeyCode.B))
                SwitchWithKeyTo<UIPanel_Craft>(ConfigPrefab.prefabUIPanel_Craft);

            //技能树界面
            if (Input.GetKeyDown(KeyCode.K))
                SwitchWithKeyTo<UIPanel_SkillTree>(ConfigPrefab.prefabUIPanel_SkillTree);

            //设置界面
            if (Input.GetKeyDown(KeyCode.O))
                SwitchWithKeyTo<UIPanel_Options>(ConfigPrefab.prefabUIPanel_Options);
        }




        /// <summary>
        /// 开启或关闭指定的界面
        /// </summary>
        /// <typeparam name="T">界面的类</typeparam>
        /// <param name="UIPanelName">界面的名称</param>
        public void SwitchWithKeyTo<T>(string UIPanelName) where T : UIBase, new()
        {
            //选择的UI界面是开启状态
            T t = UIPanelName.GetUIPanl<T>();
            if (t != null && t.ChakckUIPanelOpen())
            {
                ConfigPrefab.prefabUIPanel_InGame.CloseUIPanel();
                ConfigPrefab.prefabUIPanel_InGame.ShwoUIPanel<UIPanel_InGame>();//开启InGame界面
                GameManager.Instance?.PauseGame(false);
                return;
            }

            //选择的UI界面是关闭状态,就执行下面的代码
            UIPanelName.CloseUIPanel();
            UISwitch<T>(UIPanelName);
        }

        /// <summary>
        /// UI界面切换
        /// </summary>
        /// <typeparam name="T">UI的类</typeparam>
        /// <param name="UIPanelName">UI面板名称</param>
        public void UISwitch<T>(string UIPanelName, bool PauseGame = true) where T : UIBase, new()
        {
            ConfigEvent.EventPlayAudioSource.EventTrigger(ConfigAudio.mp3sfx_click, EAudioSourceType.SFX, false);
            CoreUIManagerExpansion.ShwoUIPanel<T>(UIPanelName);
            GameManager.Instance?.PauseGame(PauseGame);
        }

        /// <summary>
        /// 玩家死亡触发
        /// </summary>
        public async UniTaskVoid SwitchOnEndScreen()
        {
            ConfigEvent.EventFadeUI.EventTrigger(true);
            await UniTask.Delay(System.TimeSpan.FromSeconds(2.5f), false);
            CoreUIManagerExpansion.ShwoUIPanel<UIPanel_RestartGame>(ConfigPrefab.prefabUIPanel_RestartGame);
        }

        //加载保存数据
        public void LoadData(GameData _data)
        {
            //foreach (KeyValuePair<string, float> pair in _data.volumeSettings)
            //{
            //    foreach (UI_VolumeSlider item in volumeSettings)
            //    {
            //        if (item.parametr == pair.Key)
            //            item.LoadSlider(pair.Value);
            //    }
            //}
        }

        public void SaveData(ref GameData _data)
        {
            //_data.volumeSettings.Clear();

            //foreach (UI_VolumeSlider item in volumeSettings)
            //{
            //    _data.volumeSettings.Add(item.parametr, item.slider.value);
            //}
        }
    }
}