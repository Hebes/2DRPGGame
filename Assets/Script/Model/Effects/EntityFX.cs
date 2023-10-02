﻿using System.Collections;
using TMPro;
using UnityEngine;

/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    实体特效

-----------------------*/

namespace RPGGame
{
    public class EntityFX : MonoBehaviour
    {
        protected SpriteRenderer sr;

        [Header("动画特效")]
        [SerializeField] private float flashDuration;           //闪光时间
        [SerializeField] private Material hitMat;               //伤害材质
        private Material originalMat;


        [Header("疾病的颜色效果")]
        [SerializeField] private Color[] igniteColor;
        [SerializeField] private Color[] chillColor;
        [SerializeField] private Color[] shockColor;

        [Header("疾病的粒子")]
        [SerializeField] private ParticleSystem igniteFx;
        [SerializeField] private ParticleSystem chillFx;
        [SerializeField] private ParticleSystem shockFx;

        [Header("伤害特效")]
        [SerializeField] private GameObject hitFx;
        [SerializeField] private GameObject criticalHitFx;



        protected virtual void Start()
        {
            sr = GetComponentInChildren<SpriteRenderer>();
            originalMat = sr.material;
        }


        public void MakeTransprent(bool _transprent)
        {
            sr.color = _transprent ? Color.clear : Color.white;
        }

        private IEnumerator FlashFX()
        {
            sr.material = hitMat;
            Color currentColor = sr.color;
            sr.color = Color.white;

            yield return new WaitForSeconds(flashDuration);

            sr.color = currentColor;
            sr.material = originalMat;
        }

        private void RedColorBlink()
        {
            if (sr.color != Color.white)
                sr.color = Color.white;
            else
                sr.color = Color.red;
        }

        private void CancelColorChange()
        {
            CancelInvoke();
            sr.color = Color.white;

            igniteFx.Stop();
            chillFx.Stop();
            shockFx.Stop();
        }


        public void IgniteFxFor(float _seconds)
        {
            igniteFx.Play();

            InvokeRepeating("IgniteColorFx", 0, .3f);
            Invoke("CancelColorChange", _seconds);
        }

        public void ChillFxFor(float _seconds)
        {
            chillFx.Play();
            InvokeRepeating("ChillColorFx", 0, .3f);
            Invoke("CancelColorChange", _seconds);
        }


        public void ShockFxFor(float _seconds)
        {
            shockFx.Play();
            InvokeRepeating("ShockColorFx", 0, .3f);
            Invoke("CancelColorChange", _seconds);
        }

        private void IgniteColorFx()
        {
            if (sr.color != igniteColor[0])
                sr.color = igniteColor[0];
            else
                sr.color = igniteColor[1];
        }
        private void ChillColorFx()
        {
            if (sr.color != chillColor[0])
                sr.color = chillColor[0];
            else
                sr.color = chillColor[1];
        }

        private void ShockColorFx()
        {
            if (sr.color != shockColor[0])
                sr.color = shockColor[0];
            else
                sr.color = shockColor[1];
        }

        public void CreateHitFx(Transform _target, bool _critical)
        {


            float zRotation = Random.Range(-90, 90);
            float xPosition = Random.Range(-.5f, .5f);
            float yPosition = Random.Range(-.5f, .5f);

            Vector3 hitFxRotaion = new Vector3(0, 0, zRotation);

            GameObject hitPrefab = hitFx;

            if (_critical)
            {
                hitPrefab = criticalHitFx;

                float yRotation = 0;
                zRotation = Random.Range(-45, 45);

                if (GetComponent<Entity>().facingDir == -1)
                    yRotation = 180;

                hitFxRotaion = new Vector3(0, yRotation, zRotation);

            }

            GameObject newHitFx = Instantiate(hitPrefab, _target.position + new Vector3(xPosition, yPosition), Quaternion.identity); // uncomment this if you want particle to follow target ,_target);
            newHitFx.transform.Rotate(hitFxRotaion);
            Destroy(newHitFx, .5f);
        }
    }
}
