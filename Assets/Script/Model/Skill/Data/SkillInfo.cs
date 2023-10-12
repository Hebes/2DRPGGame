using System.Collections.Generic;

/*--------脚本描述-----------

电子邮箱：
	1607388033@qq.com
作者:
	暗沉
描述:
	技能信息

-----------------------*/

namespace RPGGame
{
    public class SkillInfo
    {
        public int skillID;                         //技能id
        public string name;                         //技能名称
        public string skillDescription;             //技能描述
        public int skillCost;                       //学习技能消耗的点数
        public List<int> shouldBeUnlocked;          //前置技能解锁
        public List<int> shouldBeLocked;            //同级技能二选一
        public bool isUnlocked;                     //是否解锁
    }
}
