using Core;
using UnityEngine;


/*--------脚本描述-----------

电子邮箱：
	1607388033@qq.com
作者:
	暗沉
描述:
	过场渐变界面

-----------------------*/

namespace RPGGame
{
    public class UIPanel_DarkScreen : UIBase
    {
        private Animator anim;

        public override void UIAwake()
        {
            base.UIAwake();
            InitUIBase(EUIType.Fade, EUIMode.Normal, EUILucenyType.ImPenetrable, false);

            UIComponent UIComponent = panelGameObject.GetComponent<UIComponent>();
            GameObject T_DarkScreen = UIComponent.Get<GameObject>("T_DarkScreen");
            anim = T_DarkScreen.GetComponent<Animator>();

            ConfigEvent.EventFadeUI.AddEventListener<bool>(FadeOut);
        }

        public void FadeOut(bool isFadeOut)
        {
            string str = isFadeOut ? "fadeOut" : "fadeIn";
            anim.SetTrigger(str);
        }
    }
}
