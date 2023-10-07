using Core;
using UnityEngine;


/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    区域的声音

-----------------------*/

namespace RPGGame
{
    public class AreaSound : MonoBehaviour
    {
        [SerializeField] private int areaSoundIndex;


        private void OnTriggerEnter2D(Collider2D collision)
        {
            //if (collision.GetComponent<Player>() != null)
            //    ConfigEvent.PlayAudioSourceEvent.EventTriggerUniTask()
            //    ModelAudioManager.Instance.PlaySFX(areaSoundIndex, null);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            //if (collision.GetComponent<Player>() != null)
            //    ModelAudioManager.Instance.StopSFXWithTime(areaSoundIndex);
        }
    }
}