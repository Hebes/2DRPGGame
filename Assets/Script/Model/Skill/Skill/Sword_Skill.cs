using UnityEngine;
using UnityEngine.UI;

namespace RPGGame
{
    public enum SwordType
    {
        Regular,
        Bounce,
        Pierce,
        Spin
    }

    /// <summary>
    /// 剑技能
    /// </summary>
    public class Sword_Skill : Skill
    {
        public SwordType swordType = SwordType.Regular;

        [Header("反弹的信息")]
        [SerializeField] private UI_SkillTreeSlot bounceUnlockButton;
        [SerializeField] private int bounceAmount;
        [SerializeField] private float bounceGravity;
        [SerializeField] private float bounceSpeed;

        [Header("Peirce info")]
        [SerializeField] private UI_SkillTreeSlot pierceUnlockButton;
        [SerializeField] private int pierceAmount;
        [SerializeField] private float pierceGravity;

        [Header("旋转信息")]
        [SerializeField] private UI_SkillTreeSlot spinUnlockButton;
        [SerializeField] private float hitCooldown = .35f;
        [SerializeField] private float maxTravelDistance = 7;
        [SerializeField] private float spinDuration = 2;
        [SerializeField] private float spinGravity = 1;

        [Header("技能信息")]
        [SerializeField] private UI_SkillTreeSlot swordUnlockButton;
        public bool swordUnlocked { get; private set; }
        [SerializeField] private GameObject swordPrefab;
        [SerializeField] private Vector2 launchForce;
        [SerializeField] private float swordGravity;
        [SerializeField] private float freezeTimeDuration;
        [SerializeField] private float returnSpeed;

        [Header("被动技能")]
        [SerializeField] private UI_SkillTreeSlot timeStopUnlockButton;
        public bool timeStopUnlocked { get; private set; }
        [SerializeField] private UI_SkillTreeSlot vulnerableUnlockButton;
        public bool vulnerableUnlocked { get; private set; }



        private Vector2 finalDir;

        [Header("瞄准点")]
        [SerializeField] private int numberOfDots;
        [SerializeField] private float spaceBeetwenDots;
        [SerializeField] private GameObject dotPrefab;
        [SerializeField] private Transform dotsParent;

        private GameObject[] dots;



        //生命周期
        protected override void Start()
        {
            base.Start();

            GenereateDots();
            SetupGraivty();

            swordUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockSword);
            bounceUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockBounceSword);
            pierceUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockPierceSword);
            spinUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockSpinSword);
            timeStopUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockTimeStop);
            vulnerableUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockVulnurable);
        }
        protected override void Update()
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
            GameObject newSword = Instantiate(swordPrefab, player.transform.position, transform.rotation);
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
            newSwordScript.SetupSword(finalDir, swordGravity, player, freezeTimeDuration, returnSpeed);
            player.AssignNewSword(newSword);
            DotsActive(false);
        }



        //解锁区域

        protected override void CheckUnlock()
        {
            UnlockSword();
            UnlockBounceSword();
            UnlockSpinSword();
            UnlockPierceSword();
            UnlockTimeStop();
            UnlockVulnurable();
        }
        private void UnlockTimeStop()
        {
            if (timeStopUnlockButton.unlocked)
                timeStopUnlocked = true;
        }

        private void UnlockVulnurable()
        {

            if (vulnerableUnlockButton.unlocked)
                vulnerableUnlocked = true;
        }

        private void UnlockSword()
        {
            if (swordUnlockButton.unlocked)
            {
                swordType = SwordType.Regular;
                swordUnlocked = true;
            }
        }

        private void UnlockBounceSword()
        {
            if (bounceUnlockButton.unlocked)
                swordType = SwordType.Bounce;
        }

        private void UnlockPierceSword()
        {
            if (pierceUnlockButton.unlocked)
                swordType = SwordType.Pierce;
        }

        private void UnlockSpinSword()
        {
            if (spinUnlockButton.unlocked)
                swordType = SwordType.Spin;
        }





        //目标区域
        public Vector2 AimDirection()
        {
            Vector2 playerPosition = player.transform.position;
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
                dots[i] = Instantiate(dotPrefab, player.transform.position, Quaternion.identity, dotsParent);
                dots[i].SetActive(false);
            }
        }

        private Vector2 DotsPosition(float t)
        {
            Vector2 position = (Vector2)player.transform.position + new Vector2(
                AimDirection().normalized.x * launchForce.x,
                AimDirection().normalized.y * launchForce.y) * t + .5f * (Physics2D.gravity * swordGravity) * (t * t);

            return position;
        }
    }
}