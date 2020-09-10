
public class SkillBaseData
{
	public int SkillId; // 技能Id
	public Occupation Occupation; // 职业类型
	public SkillType SkillType; // 技能类型
	public string SkillName; // 技能名称
	public string SkillIcon; // 技能Icon
	public int UnLockLevel; // 解锁等级
	public string SkillDesc; // 技能描述
	//还差一个技能附带的Animation名称的字段！！
	
}

public enum Occupation
{
	All=0,
	Coder=1,
	Sporter=2
}

public enum SkillType
{
	PassiveSkill,
	ActiveAttack,
	Buff,
}
