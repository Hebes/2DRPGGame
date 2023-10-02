using TMPro;
using UnityEngine;


/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    文本弹出特效

-----------------------*/

namespace RPGGame
{
    public class PopUpTextFx : MonoBehaviour
    {
        private TextMeshPro myText;

        [SerializeField] private float speed = 1;                       //文字向上移动的速度 
        [SerializeField] private float desapearanceSpeed = 15;          //消失的速度    
        [SerializeField] private float colorDesapearanceSpeed = 2;      //透明的速度
        [SerializeField] private float lifeTime = 1;                    //文本存在的时间
        private float textTimer;                                        //计时器

        private void Awake()
        {
            myText = GetComponent<TextMeshPro>();
            textTimer = lifeTime;
        }

        void Update()
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y + 1), speed * Time.deltaTime);
            textTimer -= Time.deltaTime;

            if (textTimer < 0)
            {
                float alpha = myText.color.a - colorDesapearanceSpeed * Time.deltaTime;
                myText.color = new Color(myText.color.r, myText.color.g, myText.color.b, alpha);
                //加速上移时间
                if (myText.color.a < 50)
                    speed = desapearanceSpeed;
                //销毁物体
                if (myText.color.a <= 0)
                    Destroy(gameObject);
            }
        }
    }
}
