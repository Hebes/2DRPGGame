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

            ConfigEvent.SkillUnLock.EventAdd<Skill, string>(SkillUnLock);
            await UniTask.Yield();
        }

        /// <summary>
        /// 技能解锁
        /// </summary>
        /// <param name="t"></param>
        /// <param name="skillName"></param>
        private void SkillUnLock(Skill t, string skillName)
        {
            if (skillDataDic.TryGetValue(t.GetType().Name, out Skill skillTemp))
                return;
            t.Awake();
            CoreMono.Instance.UpdateAddEvent(t.Update);
            skillDataDic.Add(t.GetType().Name, t);
            t.UnLockSkill(skillName);
            Debug.Log($"{skillName}技能解锁成功");
        }


        /// <summary>
        /// 确认技能是否解锁
        /// </summary>
        /// <returns></returns>
        public bool CheckSkillUnlock<T>()
        {
            if (skillDataDic.TryGetValue(typeof(T).Name, out Skill skillTemp))
                return true;
            return false;
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
        public void UnLockSkill<T>(string skillName) where T : Skill, new()
        {
            if (skillDataDic.TryGetValue(typeof(T).Name, out Skill skillTemp))
                return;
            T t = new T();
            t.Awake();
            CoreMono.Instance.UpdateAddEvent(t.Update);
            skillDataDic.Add(typeof(T).Name, t);
            t.UnLockSkill(skillName);
            Debug.Log("技能解锁成功");
        }

        /// <summary>
        /// 获取技能
        /// </summary>
        /// <typeparam name="T">技能类型</typeparam>
        /// <param name="skillName">技能名称</param>
        /// <returns>技能</returns>
        public T GetSkill<T>() where T : Skill
        {
            if (skillDataDic.TryGetValue(typeof(T).Name, out Skill skillTemp))
                return skillTemp as T;
            return null;
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
