using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPGGame
{
    public interface ISaveManager
    {
        void LoadData(GameData _data);
        void SaveData(ref GameData _data);
    }
}