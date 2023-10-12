using Core;
using UnityEngine;


/*--------脚本描述-----------

电子邮箱：
	1607388033@qq.com
作者:
	暗沉
描述:
	再试一次界面

-----------------------*/

namespace RPGGame
{
    public class UIPanel_RestartGame:UIBase
    {
        public override void UIAwake()
        {
            base.UIAwake();
            InitUIBase(EUIType.Fade, EUIMode.Normal, EUILucenyType.Lucency, false);

            UIComponent UIComponent = panelGameObject.GetComponent<UIComponent>();
            GameObject  T_RestartGameBtn = UIComponent.Get<GameObject>("T_RestartGameBtn");
            ButtonOnClickAddListener(T_RestartGameBtn.name, RestartGameBtn);
        }

        /// <summary>
        /// 重新开始
        /// </summary>
        /// <param name="go"></param>
        private void RestartGameBtn(GameObject go)
        {
            ConfigPrefab.prefabUIPanel_InGame.CloseUIPanel();
            CloseUIForm();
            GameManager.Instance.RestartScene();
        }
    }
}
