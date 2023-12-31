﻿using System.Collections.Generic;
using UnityEngine;


/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    状态

-----------------------*/

namespace RPGGame
{
    [System.Serializable]
    public class Stat
    {
        [SerializeField] private int baseValue;
        public List<int> modifiers;
        public int GetValue()
        {
            int finalValue = baseValue;

            foreach (int modifier in modifiers)
                finalValue += modifier;
            return finalValue;
        }

        /// <summary>
        /// 设置默认的值
        /// </summary>
        /// <param name="_value"></param>
        public void SetDefaultValue(int _value)
        {
            baseValue = _value;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="_modifier"></param>
        public void AddModifier(int _modifier)
        {
            modifiers.Add(_modifier);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="_modifier"></param>
        public void RemoveModifier(int _modifier)
        {
            modifiers.Remove(_modifier);
        }
    }
}
