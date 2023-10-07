using Core;
using System.Collections;
using UnityEngine;


/*--------脚本描述-----------

电子邮箱：
	1607388033@qq.com
作者:
	暗沉
描述:
	实体类型，玩家和怪物的公共数据

-----------------------*/

namespace RPGGame
{
    public class Entity : MonoBehaviour
    {
        //组件
        [HideInInspector] public Animator anim;                         //动画组件
        [HideInInspector] public Rigidbody2D rb;                        //刚体组件
        [HideInInspector] public SpriteRenderer sr;                     //图片渲染
        [HideInInspector] public CharacterStats stats;                  //状态机
        [HideInInspector] public CapsuleCollider2D cd;                  //胶囊碰撞器2D

        //击退配置
        [Header("击退配置")]
        [SerializeField] protected Vector2 knockbackPower;              //击退概率
        [SerializeField] protected float knockbackDuration;             //击退持续时间
        protected bool isKnocked;                                       //是否被击退

        //碰撞信息
        [Header("碰撞信息")]
        public Transform attackCheck;                                   //攻击检查
        public float attackCheckRadius = 0.8f;                          //攻击检测半径
        [SerializeField] protected Transform groundCheck;               //地面检测
        [SerializeField] protected float groundCheckDistance = 0.15f;   //地面检测距离
        [SerializeField] protected Transform wallCheck;                 //墙壁检测
        [SerializeField] protected float wallCheckDistance = 0.05f;     //墙壁检测距离
        [SerializeField] protected LayerMask whatIsGround;              //检测层级

        [HideInInspector] public int knockbackDir;
        [HideInInspector] public int facingDir = 1;                     //面对的方向
        protected bool facingRight = true;

        //生命周期
        protected virtual void Awake()
        {
            //获取组件
            sr = GetComponentInChildren<SpriteRenderer>();
            anim = GetComponentInChildren<Animator>();
            rb = GetComponent<Rigidbody2D>();
            stats = GetComponent<CharacterStats>();
            cd = GetComponent<CapsuleCollider2D>();
        }
        protected virtual void Start()
        {
        }
        protected virtual void Update()
        {

        }



        //其他
        public virtual void SlowEntityBy(float _slowPercentage, float _slowDuration) { }
        protected virtual void SetupZeroKnockbackPower() { }
        protected virtual void ReturnDefaultSpeed() => anim.speed = 1;//返回默认速度
        public virtual void DamageImpact() => StartCoroutine("HitKnockback");//损伤的影响
        public void SetupKnockbackPower(Vector2 _knockbackpower) => knockbackPower = _knockbackpower;//设置击退的概率
        public virtual void Die() { }//死亡

        /// <summary>
        /// 设置击退方向
        /// </summary>
        /// <param name="_damageDirection"></param>
        public virtual void SetupKnockbackDir(Transform _damageDirection)
        {
            if (_damageDirection.position.x > transform.position.x)
                knockbackDir = -1;
            else if (_damageDirection.position.x < transform.position.x)
                knockbackDir = 1;
        }

        /// <summary>
        /// 击中时导致击退
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerator HitKnockback()
        {
            isKnocked = true;
            rb.velocity = new Vector2(knockbackPower.x * knockbackDir, knockbackPower.y);
            yield return new WaitForSeconds(knockbackDuration);
            isKnocked = false;
            SetupZeroKnockbackPower();
        }




        /// <summary>
        /// 移动
        /// </summary>
        public void SetZeroVelocity()
        {
            if (isKnocked) return;//如果被击退的话，直接跳过
            rb.velocity = new Vector2(0, 0);
        }
        public void SetVelocity(float _xVelocity, float _yVelocity)
        {
            if (isKnocked) 
                return;
            rb.velocity = new Vector2(_xVelocity, _yVelocity);
            FlipController(_xVelocity);
        }



        //碰撞
        public virtual bool IsGroundDetected()
        {
            bool isGroundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
            //if (isGroundDetected)
            //    Debug.Log("检测到地面");
            return isGroundDetected;
        }
        public virtual bool IsWallDetected()
        {
            bool isWallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);
            //if (isWallDetected)
            //    Debug.Log("检测到墙");
            return isWallDetected;
        }
        protected virtual void OnDrawGizmos()
        {
            Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
            Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
            Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
        }



        //角色等翻转
        public virtual void Flip()
        {
            facingDir = facingDir * -1;
            facingRight = !facingRight;
            transform.Rotate(0, 180, 0);
            ConfigEvent.EventPlayerFlipUI.EventTrigger();
        }
        public virtual void FlipController(float _x)
        {
            if (_x > 0 && !facingRight)
                Flip();
            else if (_x < 0 && facingRight)
                Flip();
        }
    }
}
