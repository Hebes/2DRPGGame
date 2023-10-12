using Core;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using Debug = Core.Debug;


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
        private Dictionary<string, Skill> skillDataDic; //技能
        private Dictionary<int, SkillInfo> skilDinfoDic; //技能


        public async UniTask Init()
        {
            Instance = this;
            //技能信息描述
            skilDinfoDic = new Dictionary<int, SkillInfo>();
            DataExpansion.InitData<SkillInfoData>(ConfigExcelData.skillInfoData);
            List<SkillInfoData> skillInfoDatasList = this.GetData<SkillInfoData>();
            foreach (SkillInfoData item in skillInfoDatasList)
            {
                SkillInfo skillInfo = new SkillInfo();
                skillInfo.skillID = item.skillID;
                skillInfo.name = item.name;
                skillInfo.skillCost = item.skillCost;
                skillInfo.shouldBeUnlocked = item.shouldBeUnlocked;
                skillInfo.shouldBeLocked = item.shouldBeLocked;
                skillInfo.isUnlocked = false;
                skilDinfoDic.Add(item.skillID, skillInfo);
            }
            //TODO 初始化完毕后续准备删除加载的数据

            //技能
            skillDataDic = new Dictionary<string, Skill>();
            //skillDataDic.Add(typeof(Blackhole_Skill).Name, new Blackhole_Skill());
            //skillDataDic.Add(typeof(Clone_Skill).Name, new Clone_Skill());
            //skillDataDic.Add(typeof(Crystal_Skill).Name, new Crystal_Skill());
            //skillDataDic.Add(typeof(Dash_Skill).Name, new Dash_Skill());
            //skillDataDic.Add(typeof(Dodge_Skill).Name, new Dodge_Skill());
            //skillDataDic.Add(typeof(Parry_Skill).Name, new Parry_Skill());
            //skillDataDic.Add(typeof(Sword_Skill).Name, new Sword_Skill());
            //foreach (Skill item in skillDataDic.Values)
            //{
            //    CoreMono.Instance.AwakeAddEvent(item.Awake);
            //    CoreMono.Instance.UpdateAddEvent(item.Update);
            //    item.CheckUnlock();//游戏加载检查技能是否解锁
            //}

            await UniTask.Yield();
        }

        /// <summary>
        /// 添加可以使用的技能
        /// </summary>
        private void AddSkill<T>() where T : Skill, new()
        {
            T t = new T();
            CoreMono.Instance.AwakeAddEvent(t.Awake);
            CoreMono.Instance.UpdateAddEvent(t.Update);
            skillDataDic.Add(typeof(T).Name, t);
        }

        /// <summary>
        /// 获取数据信息
        /// </summary>
        public SkillInfo GetSkillInfoOne(int key)
        {
            if (skilDinfoDic.TryGetValue(key, out SkillInfo skillInfo))
                return skillInfo;
            return null;
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
