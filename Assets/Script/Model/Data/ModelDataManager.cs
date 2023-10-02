using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class ModelDataManager : IModelInit
    {
        private Dictionary<string, GameObject> prefabDic;

        public async UniTask Init()
        {
            prefabDic = new Dictionary<string, GameObject>();
            await UniTask.Yield();
        }


    }
}
