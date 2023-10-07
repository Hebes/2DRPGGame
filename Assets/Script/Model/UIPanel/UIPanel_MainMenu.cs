using Core;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

/*--------脚本描述-----------

电子邮箱：
	1607388033@qq.com
作者:
	暗沉
描述:
	主菜单面板

-----------------------*/

namespace RPGGame
{
    public class UIPanel_MainMenu : UIBase
    {
        public override void UIAwake()
        {
            base.UIAwake();
            InitUIBase(EUIType.Normal, EUIMode.HideOther, EUILucenyType.Lucency, false);

            UIComponent UIComponent = panelGameObject.GetComponent<UIComponent>();
            GameObject T_Exitgame = UIComponent.Get<GameObject>("T_Exit game");
            GameObject T_Newgame = UIComponent.Get<GameObject>("T_New  game");
            GameObject T_Continue = UIComponent.Get<GameObject>("T_Continue ");

            ButtonOnClickAddListener(T_Exitgame.name, Exitgame);
            ButtonOnClickAddListener(T_Newgame.name, Newgame);
            ButtonOnClickAddListener(T_Continue.name, Continue);

            if (SaveManager.Instance.HasSavedData() == false)
                T_Continue.SetActive(false);
        }

        /// <summary>
        /// 继续游戏
        /// </summary>
        /// <param name="go"></param>
        private void Continue(GameObject go)
        {
            LoadSceneWithFadeEffect(1.5f).Forget();
        }

        /// <summary>
        /// 新游戏
        /// </summary>
        /// <param name="go"></param>
        private void Newgame(GameObject go)
        {
            SaveManager.Instance.DeleteSavedData();
            LoadSceneWithFadeEffect(1.5f).Forget();
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="go"></param>
        private void Exitgame(GameObject go)
        {
            Application.Quit();
        }

        /// <summary>
        /// 加载场景
        /// </summary>
        /// <param name="_delay"></param>
        /// <returns></returns>
        private async UniTask LoadSceneWithFadeEffect(float _delay)
        {
            ConfigEvent.EventFadeUI.EventTrigger(true);
            await UniTask.Delay(TimeSpan.FromSeconds(_delay), false);
            await ConfigScenes.unityMainScene.LoadSceneAsync(ELoadSceneModel.Single);

            //开启面板
            CoreUIManagerExpansion.ShwoUIPanel<UIPanel_SkillTree>(ConfigPrefab.prefabUIPanel_SkillTree);        //技能树
            CoreUIManagerExpansion.ShwoUIPanel<UIPanel_InGame>(ConfigPrefab.prefabUIPanel_InGame);              //快捷面板

            ConfigPrefab.prefabUIPanel_SkillTree.CloseUIPanel();        //技能树
            ConfigPrefab.prefabUIPanel_MainMenu.CloseUIPanel();          //主菜单面板
        }
    }
}
