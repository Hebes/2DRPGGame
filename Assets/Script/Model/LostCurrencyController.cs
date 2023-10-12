using RPGGame;
using UnityEngine;

namespace RPGGame
{
    public class LostCurrencyController : MonoBehaviour
    {
        public int currency;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Player>() != null)
            {
                Debug.Log("Picked up currency");
                ModelData.Instance.currency += currency;
                Destroy(this.gameObject);
            }
        }
    }
}
