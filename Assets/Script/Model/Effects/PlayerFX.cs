using Cinemachine;
using UnityEngine;
using static Cinemachine.CinemachineImpulseDefinition;


/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    玩家特效

-----------------------*/

namespace RPGGame
{
    public class PlayerFX : EntityFX
    {
        [Header("屏幕抖动特效")]
        [SerializeField] private float shakeMultiplier = 0.2f; //抖动倍数
        public Vector3 shakeSwordImpact;                        //手剑抖动
        public Vector3 shakeHighDamage;                         //受伤抖动
        private CinemachineImpulseSource screenShake;           //屏幕抖动

        [Header("图片特效")]
        [SerializeField] private GameObject afterImagePrefab;   //冲刺图像预制体
        [SerializeField] private float colorLooseRate;          //颜色消失时间
        [SerializeField] private float afterImageCooldown;      // 冲刺冷却时间
        private float afterImageCooldownTimer;                  //冲刺计时器

        [Space] 
        [SerializeField] private ParticleSystem dustFx;         //走路的灰尘特效

        protected override void Start()
        {
            base.Start();
            screenShake = GetComponent<CinemachineImpulseSource>();
        }

        private void Update()
        {
            afterImageCooldownTimer -= Time.deltaTime;
        }

        /// <summary>
        /// 屏幕抖动
        /// </summary>
        /// <param name="_shakePower"></param>
        public void ScreenShake(Vector3 _shakePower)
        {
            screenShake.m_DefaultVelocity = new Vector3(_shakePower.x * ModelPlayerManager.Instance.player.facingDir, _shakePower.y) * shakeMultiplier;
            screenShake.GenerateImpulse();
        }

        /// <summary>
        /// 创建参详图片
        /// </summary>
        public void CreateAfterImage()
        {
            if (afterImageCooldownTimer < 0)
            {
                afterImageCooldownTimer = afterImageCooldown;
                GameObject newAfterImage = Instantiate(afterImagePrefab, transform.position, transform.rotation);
                newAfterImage.GetComponent<AfterImageFX>().SetupAfterImage(colorLooseRate, sr.sprite);
            }
        }

        /// <summary>
        /// 播放走路灰尘特效
        /// </summary>
        public void PlayDustFX()
        {
            if (dustFx != null)
                dustFx.Play();
        }
    }
}
