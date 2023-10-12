using TMPro;
using UnityEngine;


/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    提示基类

-----------------------*/


namespace RPGGame
{
    public class UI_ToolTip : MonoBehaviour
    {
        [SerializeField] private float xLimit = 960;
        [SerializeField] private float yLimit = 540;

        [SerializeField] private float xOffset = 150;
        [SerializeField] private float yOffset = 150;


        /// <summary>
        /// 调整位置
        /// </summary>
        public virtual void AdjustPosition()
        {
            Vector2 mousePosition = Input.mousePosition;

            float newXoffset = 0;
            float newYoffset = 0;

            if (mousePosition.x > xLimit)
                newXoffset = -xOffset;
            else
                newXoffset = xOffset;

            if (mousePosition.y > yLimit)
                newYoffset = -yOffset; 
            else
                newYoffset = yOffset;

            transform.position = new Vector2(mousePosition.x + newXoffset, mousePosition.y + newYoffset);
        }

        /// <summary>
        /// 调整字体大小
        /// </summary>
        /// <param name="_text"></param>
        public void AdjustFontSize(TextMeshProUGUI _text)
        {
            if (_text.text.Length > 12)
                _text.fontSize = (int)(_text.fontSize * .8f);
        }
    }
}
