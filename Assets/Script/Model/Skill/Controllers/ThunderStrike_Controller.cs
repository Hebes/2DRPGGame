using UnityEngine;

/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    雷电控制器

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
