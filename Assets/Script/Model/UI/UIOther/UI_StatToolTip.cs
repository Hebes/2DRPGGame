using Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    I统计工具提示

-----------------------*/

namespace RPGGame
{
    public class UI_StatToolTip : UI_ToolTip
    {
        [SerializeField] private TextMeshProUGUI description;

        private void Awake()
        {
            ConfigEvent.EventStatToolTipShow.EventAdd<string>(ShowStatToolTip);
            ConfigEvent.EventStatToolTipClose.EventAdd(HideStatToolTip);
            gameObject.SetActive(false);
        }
        public void ShowStatToolTip(string _text)
        {
            description.text = _text;
            AdjustPosition();

            gameObject.SetActive(true);
        }

        public void HideStatToolTip()
        {
            description.text = "";
            gameObject.SetActive(false); 
        }
    }
}
