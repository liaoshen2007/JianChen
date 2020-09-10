using System;
using System.Collections.Generic;

[Serializable]
public class AreaData
{
	public int AreaId;
	public List<EnemyData> EnemyDataList;
	public SpawnPosValue AreaSpawnPos;
	public int Range = 2;
	public int Type;//是否可以刷新的类型
	public int RefreshTime;//状态刷新时间

}

[Serializable]
public class EnemyData
{
	public int ID; // 编号
	public string Name; // NPC名字
	public string AssetName; // 资源名
	public int IsBoss;//是否为首领
	public int HP;//基础HP
	public int MP;//基础MP
	public int AISkill;//技能池
	public SpawnPosValue SpawnPos;//生成的位置
	public List<string> BaseDialogs;//需要的对话
	public string EventString;//战胜后的事件触发
	public List<AwardData> AwardData;//战利品
	public int Poem;//携带诗集;
	public int Sound;//基础语音
	public int NeedToRecord;//状态是否需要记录

}

[Serializable]
public class AwardData
{
	public int ResourceId;
	public ResourceType ResourceType;
	public int Num;
}

public enum ResourceType
{
	Item=0,
	Equip=1,
	Gold=2,
	Exp=3
}

