using Core;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;


/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    特效管理

-----------------------*/

namespace RPGGame
{
    public class ModelEffect : IModelInit
    {
        public GameObject popUpTextPrefab;      //文本特效预制体
        public GameObject prefabDustFX;         //走路灰尘特效预制体


        public async UniTask Init()
        {
            //加载资源
            popUpTextPrefab = await ConfigPrefab.prefabPopUpText.LoadAsync<GameObject>();
            //prefabDustFX = await ConfigPrefab.prefabDustFX.LoadAsync<GameObject>();
            //事件监听
            ConfigEvent.EventEffectPopUpText.EventAdd<string, Vector3>(EffectPopUpTextEvent);
        }

        /// <summary>
        /// 文本弹出特效
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void EffectPopUpTextEvent(string text, Vector3 position)
        {
            float randomX = Random.Range(-1, 1);
            float randomY = Random.Range(1.5f, 3);
            Vector3 positionOffset = new Vector3(randomX, randomY, 0);
            GameObject newText = GameObject.Instantiate(popUpTextPrefab, position + positionOffset, Quaternion.identity);
            newText.GetComponent<TextMeshPro>().text = text;
        }

        ///// <summary>
        ///// 走路灰尘特效
        ///// </summary>
        //private void EffectPlayDustFXEvent(Vector3 position)
        //{
        //    GameObject prefabDustFX = GameObject.Instantiate(popUpTextPrefab, position, Quaternion.identity);
        //}
    }
}
