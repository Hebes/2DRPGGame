using UnityEngine;


/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    残影特效

-----------------------*/

namespace RPGGame
{
    /// <summary>
    /// 残影特效
    /// </summary>
    public class AfterImageFX : MonoBehaviour
    {
        private SpriteRenderer sr;
        private float colorLooseRate;

        /// <summary>
        /// 图片设置
        /// </summary>
        /// <param name="_loosingSpeed">图片a通道的速度</param>
        /// <param name="_spriteImage">图片</param>
        public void SetupAfterImage(float _loosingSpeed, Sprite _spriteImage)
        {
            sr = GetComponent<SpriteRenderer>();
            sr.sprite = _spriteImage;
            colorLooseRate = _loosingSpeed;
        }

        
        private void Update()
        {
            //残影消失
            float alpha = sr.color.a - colorLooseRate * Time.deltaTime;
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);

            if (sr.color.a <= 0)
                Destroy(gameObject);
        }
    }
}
