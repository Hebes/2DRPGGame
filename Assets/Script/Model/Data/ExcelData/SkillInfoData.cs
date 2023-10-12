using Core;
using System;
using System.Collections.Generic;

[Serializable]
public class SkillInfoData : IData
{
	public int       	skillID;
	public string    	name;
	public string    	skillDescription;
	public int       	skillCost;
	public List<int> 	shouldBeUnlocked;
	public List<int> 	shouldBeLocked;
    public int GetId()
    {
		return skillID;
    }
}
