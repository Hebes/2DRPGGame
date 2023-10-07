/*--------脚本描述-----------
				
电子邮箱：
	1607388033@qq.com
作者:
	暗沉
描述:
    UI面板拓展类

-----------------------*/

namespace Core
{
    public static class CoreUIManagerExpansion
    {
        /// <summary>
        /// 显示面板
        /// </summary>
        /// <typeparam name="T">面板的脚本</typeparam>
        /// <param name="uiFormName">面板的名称</param>
        /// <returns></returns>
        public static T ShwoUIPanel<T>(this string uiFormName) where T : UIBase, new()
        {
            return CroeUIManager.Instance.ShwoUIPanel<T>(uiFormName);
        }

        /// <summary>
        /// 关闭面板
        /// </summary>
        /// <param name="uiFormName">面板的名称</param>
        public static void CloseUIPanel(this string uiFormName)
        {
            CroeUIManager.Instance.CloseUIForms(uiFormName);
        }

        /// <summary>
        /// 获取面板
        /// </summary>
        /// <typeparam name="T">面板的脚本</typeparam>
        /// <param name="uiFormName">面板的名称</param>
        /// <returns></returns>
        public static T GetUIPanl<T>(this string uiFormName) where T : UIBase
        {
            return CroeUIManager.Instance.GetUIPanl<T>(uiFormName);
        }
    }
}
