using Core;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;


/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    技能管理器

-----------------------*/

namespace RPGGame
{
    public class ModelSkill : IModelInit, ISaveManager
    {
        public static ModelSkill Instance;
        private Dictionary<string, Skill> skillDataDic;


        public async UniTask Init()
        {
            Instance = this;
            skillDataDic = new Dictionary<string, Skill>();

            //TODO 加载数据
            //黑洞
            Blackhole_Skill blackhole_Skill = new Blackhole_Skill();
            skillDataDic.Add(typeof(Blackhole_Skill).Name, blackhole_Skill);

            //克隆
            Clone_Skill clone_Skill = new Clone_Skill();
            skillDataDic.Add(typeof(Clone_Skill).Name, clone_Skill);

            //克隆
            Crystal_Skill crystal_Skill = new Crystal_Skill();
            skillDataDic.Add(typeof(Crystal_Skill).Name, crystal_Skill);

            //克隆
            Dash_Skill dash_Skill = new Dash_Skill();
            skillDataDic.Add(typeof(Dash_Skill).Name, dash_Skill);

            //克隆
            Dodge_Skill dodge_Skill = new Dodge_Skill();
            skillDataDic.Add(typeof(Dodge_Skill).Name, dodge_Skill);

            //克隆
            Parry_Skill parry_Skill = new Parry_Skill();
            skillDataDic.Add(typeof(Parry_Skill).Name, parry_Skill);

            //克隆
            Sword_Skill sword_Skill = new Sword_Skill();
            skillDataDic.Add(typeof(Sword_Skill).Name, sword_Skill);

            //每个技能的初始化检查和生命周期监听
            foreach (Skill item in skillDataDic.Values)
            {
                CoreMono.Instance.AwakeAddEvent(item.Awake);
                CoreMono.Instance.UpdateAddEvent(item.Update);
                item.CheckUnlock();//游戏加载检查技能是否解锁
            }
            await UniTask.Yield();
        }


        /// <summary>
        /// 解锁技能
        /// </summary>
        /// <typeparam name="T">技能类型</typeparam>
        /// <param name="skill">总技能名称</param>
        /// <param name="skillName">子技能名称</param>
        public void UnLockSkill<T>(string skillName) where T : Skill
        {
            if (skillDataDic.TryGetValue(typeof(T).Name, out Skill skillTemp))
            {
                T t = skillTemp as T;
                t.UnLockSkill(skillName);
                return;
            }
            Debug.Error("没有此技能");
        }

        /// <summary>
        /// 获取技能
        /// </summary>
        /// <typeparam name="T">技能类型</typeparam>
        /// <param name="skillName">技能名称</param>
        /// <returns>技能</returns>
        public T GetSkill<T>() where T : Skill
        {
            Skill skillTemp = null;
            if (skillDataDic.TryGetValue(typeof(T).Name, out skillTemp))
                return skillTemp as T;
            return skillTemp as T;
        }


        //加载和存储
        public void LoadData(GameData _data)
        {
        }

        public void SaveData(ref GameData _data)
        {
        }
    }
}
