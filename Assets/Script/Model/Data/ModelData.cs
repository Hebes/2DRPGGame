using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;


/*--------脚本描述-----------

电子邮箱：
	1607388033@qq.com
作者:
	暗沉
描述:
	模块数据初始化

-----------------------*/

namespace RPGGame
{
    public class ModelData : IModelInit, ISaveManager
    {
        public static ModelData Instance;
        public int currency = 2000;        //钱 -> 灵魂
        private Dictionary<string, GameObject> prefabDic;

        public async UniTask Init()
        {
            Instance = this;
            prefabDic = new Dictionary<string, GameObject>();
            await UniTask.Yield();
        }


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
