using UnityEngine;

/*--------�ű�����-----------

�������䣺
    1607388033@qq.com
����:
    ����
����:
    �׵������

-----------------------*/

namespace RPGGame
{

    public class ThunderStrike_Controller : MonoBehaviour
    {
        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Enemy>() != null)
            {
                PlayerStats playerStats = ModelPlayerManager.Instance.player.GetComponent<PlayerStats>();
                EnemyStats enemyTarget = collision.GetComponent<EnemyStats>();
                playerStats.DoMagicalDamage(enemyTarget);
            }
        }
    }
}
