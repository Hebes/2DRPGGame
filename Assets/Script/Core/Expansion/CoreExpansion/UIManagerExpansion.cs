﻿/*--------脚本描述-----------
				
电子邮箱：
	1607388033@qq.com
作者:
	暗沉
描述:
    Ui面板拓展类

-----------------------*/

namespace Core
{
    public static class UIManagerExpansion
    {
        public static T ShwoUIPanel<T>(this string uiFormName) where T : UIBase, new()
        {
            return CroeUIManager.Instance.ShwoUIPanel<T>(uiFormName);
        }
        public static void CloseUIPanel(this string uiFormName)
        {
            CroeUIManager.Instance.CloseUIForms(uiFormName);
        }
        public static T GetUIPanl<T>(this string uiFormName) where T : UIBase
        {
            return CroeUIManager.Instance.GetUIPanl<T>(uiFormName);
        }
    }
}
