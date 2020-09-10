
   
public class EquipBaseData
{
	public int EquipId; // 装备Id
	public string EquipName; // 装备名称
	public EquipType EquipType; // 装备类型
	public string EquipDescription; // 装备描述
	public string EquipIcon; // 装备Icon
	public string EquipEffect; // 装备特效
	public GetType Gettype; // 获取途径
	public int Price; // 出售价格
	public int HP; // HP
	public int MP; // MP
	public int ATK; // 攻击力
	public int DEF; // 防御力
	public int SPD; // 攻击速度
	public int HIT; // 命中
	public int Cripercent; // 暴击率
	public int AtkRange; // 攻击范围
	public int MoveSpeed; // 移动速度
}

public enum EquipType
{
	Weapon=0,
	Cloth=1,
	Head=2,
	Shoe=3,
	
}

public enum GetType
{
	Shop=0,//商店购买
	Drop=1,//掉落
	
}
 
