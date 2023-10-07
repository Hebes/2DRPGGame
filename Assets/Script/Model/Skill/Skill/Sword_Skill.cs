using Core;
using UnityEngine;
using UnityEngine.UI;

/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    剑技能

-----------------------*/

namespace RPGGame
{
    public enum SwordType
    {
        /// <summary> 普通 </summary>
        Regular,
        /// <summary> 反弹 </summary>
        Bounce,
        /// <summary> 穿刺 </summary>
        Pierce,
        /// <summary> 旋转 </summary>
        Spin
    }

    public class Sword_Skill : Skill
    {
        public SwordType swordType = SwordType.Spin;

        //反弹的信息
        [SerializeField] private int bounceAmount;
        [SerializeField] private float bounceGravity;
        [SerializeField] private float bounceSpeed;

        //刺穿
        [SerializeField] private int pierceAmount;
        [SerializeField] private float pierceGravity;

        //旋转信息
        [SerializeField] private float hitCooldown = .35f;
        [SerializeField] private float maxTravelDistance = 7;
        [SerializeField] private float spinDuration = 2;
        [SerializeField] private float spinGravity = 1;

        //技能信息
        public bool swordUnlocked;
        private GameObject swordPrefab;
        private Vector2 launchForce;
        private float swordGravity;
        private float freezeTimeDuration;
        private float returnSpeed;

        //被动技能
        public bool timeStopUnlocked;
        public bool vulnerableUnlocked;



        private Vector2 finalDir;

        //瞄准点
        private int numberOfDots;
        private float spaceBeetwenDots;
        private GameObject dotPrefab;
        private Transform dotsParent;

        private GameObject[] dots;


        public override void Awake()
        {
            base.Awake();

            bounceAmount = 4;
            bounceGravity = 1f;
            bounceSpeed = 20f;

            pierceAmount = 2;
            pierceGravity = 1f;

            hitCooldown = 0.1f;
            maxTravelDistance = 7f;
            spinDuration = 2f;
            spinGravity = 1f;

            swordPrefab = LoadResExtension.Load<GameObject>(ConfigPrefab.prefabSword);
            launchForce = new Vector2(35f, 25f);
            swordGravity = 5f;
            freezeTimeDuration = 1.5f;
            returnSpeed = 15f;

            numberOfDots = 20;
            spaceBeetwenDots = 0.07f;
            dotPrefab = LoadResExtension.Load<GameObject>(ConfigPrefab.prefabAimDots);

            dotsParent = new GameObject("DotsParent").transform;

            GenereateDots();
            SetupGraivty();
        }

        public override void UnLockSkill(string skillName)
        {
            base.UnLockSkill(skillName);
            switch (skillName)
            {
                case ConfigSkill.SkillSword:
                    swordType = SwordType.Regular;
                    swordUnlocked = true;
                    break;
                case ConfigSkill.SkillBounceSword:
                    swordType = SwordType.Bounce;
                    break;
                case ConfigSkill.SkillSpinSword:
                    swordType = SwordType.Spin;
                    break;
                case ConfigSkill.SkillPierceSword:
                    swordType = SwordType.Pierce;
                    break;
                case ConfigSkill.SkillTimeStop:
                    timeStopUnlocked = true;
                    break;
                case ConfigSkill.SkillVulnurable:
                    vulnerableUnlocked = true;
                    break;

            }
        }

        public override void Update()
        {
            if (Input.GetKeyUp(KeyCode.Mouse1))
                finalDir = new Vector2(AimDirection().normalized.x * launchForce.x, AimDirection().normalized.y * launchForce.y);


            if (Input.GetKey(KeyCode.Mouse1))
            {
                for (int i = 0; i < dots.Length; i++)
                    dots[i].transform.position = DotsPosition(i * spaceBeetwenDots);
            }
        }
        private void SetupGraivty()
        {
            if (swordType == SwordType.Bounce)
                swordGravity = bounceGravity;
            else if (swordType == SwordType.Pierce)
                swordGravity = pierceGravity;
            else if (swordType == SwordType.Spin)
                swordGravity = spinGravity;
        }

        public void CreateSword()
        {
            GameObject newSword = GameObject.Instantiate(swordPrefab, Player.Instance.transform.position, Quaternion.identity);
            Sword_Skill_Controller newSwordScript = newSword.GetComponent<Sword_Skill_Controller>();
            switch (swordType)
            {
                case SwordType.Regular:
                    break;
                case SwordType.Bounce:
                    newSwordScript.SetupBounce(true, bounceAmount, bounceSpeed);
                    break;
                case SwordType.Pierce:
                    newSwordScript.SetupPierce(pierceAmount);
                    break;
                case SwordType.Spin:
                    newSwordScript.SetupSpin(true, maxTravelDistance, spinDuration, hitCooldown);
                    break;
                default:
                    break;
            }
            newSwordScript.SetupSword(finalDir, swordGravity, Player.Instance, freezeTimeDuration, returnSpeed);
            Player.Instance.AssignNewSword(newSword);
            DotsActive(false);
        }




        public override void CheckUnlock()
        {
            base.CheckUnlock();
        }


        //目标区域
        public Vector2 AimDirection()
        {
            Vector2 playerPosition = Player.Instance.transform.position;
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePosition - playerPosition;

            return direction;
        }
        public void DotsActive(bool _isActive)
        {
            for (int i = 0; i < dots.Length; i++)
                dots[i].SetActive(_isActive);
        }

        /// <summary>
        /// 生成瞄准点
        /// </summary>
        private void GenereateDots()
        {
            dots = new GameObject[numberOfDots];
            for (int i = 0; i < numberOfDots; i++)
            {
                dots[i] = GameObject.Instantiate(dotPrefab, Player.Instance.transform.position, Quaternion.identity, dotsParent);
                dots[i].SetActive(false);
            }
        }

        private Vector2 DotsPosition(float t)
        {
            Vector2 position = (Vector2)Player.Instance.transform.position + new Vector2(
                AimDirection().normalized.x * launchForce.x,
                AimDirection().normalized.y * launchForce.y) * t + .5f * (Physics2D.gravity * swordGravity) * (t * t);

            return position;
        }
    }
}