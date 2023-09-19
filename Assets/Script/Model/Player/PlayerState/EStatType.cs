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
    /// <summary> 玩家状态 </summary>
    public enum EStatType
    {
        /// <summary> 力量 </summary>
        strength,
        /// <summary> 敏捷 </summary>
        agility,
        /// <summary> 智力 </summary>
        intelegence,
        /// <summary> 活力 </summary>
        vitality,
        /// <summary> 伤害 </summary>
        damage,
        /// <summary> 暴击几率 </summary>
        critChance,
        /// <summary> 暴击 </summary>
        critPower,
        /// <summary> 生命 </summary>
        health,
        /// <summary> 护甲 </summary>
        armor,
        /// <summary> 闪避 </summary>
        evasion,
        /// <summary> 魔法恢复 </summary>
        magicRes,
        /// <summary> 火球伤害 </summary>
        fireDamage,
        /// <summary> 冰冻伤害 </summary>
        iceDamage,
        /// <summary> 雷电伤害 </summary>
        lightingDamage
    }
}
