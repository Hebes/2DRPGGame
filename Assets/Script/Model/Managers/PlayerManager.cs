
using UnityEngine;
using Core;
using Debug = UnityEngine.Debug;

namespace RPGGame
{
    public class PlayerManager : SinglentMono<PlayerManager>, ISaveManager
    {
        public Player player;       //玩家
        public int currency;        //钱

        /// <summary>
        /// 有钱?
        /// </summary>
        /// <param name="_price"></param>
        /// <returns></returns>
        public bool HaveEnoughMoney(int _price)
        {
            if (_price > currency)
                return false;
            currency = currency - _price;
            return true;
        }

        public void LoadData(GameData _data)
        {
            this.currency = _data.currency;
        }

        public void SaveData(ref GameData _data)
        {
            _data.currency = this.currency;
        }
    }
}
