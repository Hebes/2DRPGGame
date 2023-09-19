using UnityEngine;


/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    玩家状态

-----------------------*/

namespace RPGGame
{
    public class PlayerState
    {
        protected PlayerStateMachine stateMachine;
        protected Player player;

        protected Rigidbody2D rb;

        protected float xInput;
        protected float yInput;
        private string animBoolName;

        protected float stateTimer;     //状态定时器
        protected bool triggerCalled;   //攻击的触发

        public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
        {
            this.player = _player;
            this.stateMachine = _stateMachine;
            this.animBoolName = _animBoolName;
        }

        public virtual void Enter()
        {
            player.anim.SetBool(animBoolName, true);
            rb = player.rb;
            triggerCalled = false;
        }

        public virtual void Update()
        {
            stateTimer -= Time.deltaTime;
            //玩家移动输入
            xInput = Input.GetAxisRaw("Horizontal");
            yInput = Input.GetAxisRaw("Vertical");
            player.anim.SetFloat("yVelocity", rb.velocity.y);

        }

        public virtual void Exit()
        {
            player.anim.SetBool(animBoolName, false);
        }

        //动画完毕的触发,玩家攻击用的
        public virtual void AnimationFinishTrigger()
        {
            triggerCalled = true;
        }

    }
}
