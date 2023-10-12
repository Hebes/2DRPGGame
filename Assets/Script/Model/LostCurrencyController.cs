using UnityEngine;

/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    拾取货币

-----------------------*/

namespace RPGGame
{
    public class LostCurrencyController : MonoBehaviour
    {
        public int currency;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Player>() != null)
            {
                Debug.Log("捡到货币了");
                ModelData.Instance.currency += currency;
                Destroy(this.gameObject);
            }
        }
    }
}
