using Core;
using UnityEngine;

namespace RPGGame
{
    /// <summary>
    /// 技能管理器
    /// </summary>
    public class SkillManager : SinglentMono<SkillManager>
    {
        [HideInInspector] public Dash_Skill dash;           //冲刺技能
        [HideInInspector] public Clone_Skill clone;         //克隆技能
        [HideInInspector] public Sword_Skill sword;         //剑技能
        [HideInInspector] public Blackhole_Skill blackhole; //黑洞
        [HideInInspector] public Crystal_Skill crystal;     //水晶
        [HideInInspector] public Parry_Skill parry;         //格挡  
        [HideInInspector] public Dodge_Skill dodge;         //闪避

        protected override void Awake()
        {
            base.Awake();
            dash = GetComponent<Dash_Skill>();
            clone = GetComponent<Clone_Skill>();
            sword = GetComponent<Sword_Skill>();
            blackhole = GetComponent<Blackhole_Skill>();
            crystal = GetComponent<Crystal_Skill>();
            parry = GetComponent<Parry_Skill>();
            dodge = GetComponent<Dodge_Skill>();
        }
    }
}
