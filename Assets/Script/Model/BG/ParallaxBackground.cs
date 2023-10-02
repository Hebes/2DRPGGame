using UnityEngine;

/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    背景视差

-----------------------*/

namespace RPGGame
{
    public class ParallaxBackground : MonoBehaviour
    {
        [SerializeField] private float parallaxEffect = 1;//SkyBG1->1 CityBG2->0.9

        private float xPosition;
        private float length;

        void Start()
        {
            length = GetComponent<SpriteRenderer>().bounds.size.x;
            xPosition = transform.position.x;
        }

        void Update()
        {
            float distanceMoved = MainSceneManager.Instance.mainCam.transform.position.x * (1 - parallaxEffect);
            float distanceToMove = MainSceneManager.Instance.mainCam.transform.position.x * parallaxEffect;

            transform.position = new Vector3(xPosition + distanceToMove, transform.position.y);

            if (distanceMoved > xPosition + length)
                xPosition = xPosition + length;
            else if (distanceMoved < xPosition - length)
                xPosition = xPosition - length;
        }
    }
}
