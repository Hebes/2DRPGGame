using UnityEngine;


/*--------�ű�����-----------

�������䣺
    1607388033@qq.com
����:
    ����
����:
    ���������

-----------------------*/

namespace RPGGame
{
    public class AreaSound : MonoBehaviour
    {
        [SerializeField] private int areaSoundIndex;


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Player>() != null)
                AudioManager.Instance.PlaySFX(areaSoundIndex, null);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.GetComponent<Player>() != null)
                AudioManager.Instance.StopSFXWithTime(areaSoundIndex);
        }
    }
}