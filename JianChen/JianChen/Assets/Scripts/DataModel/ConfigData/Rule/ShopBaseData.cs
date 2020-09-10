//
// Auto Generated Code By excel2json
//

// Generate From ShopBaseData.xlsx
public class ShopBaseData
{
	public int MallId; // 商品Id
	public MallType MallType; // 商品类型
	public string MallName; // 商品名称
	public MoneyType MoneyType; // 货币类型
	public int ItemId; // 道具Id
	public int Price; // 价格
	public int MaxTimes; // 购买上限
	public string UseDesc; // 用途
}
// End of Auto Generated Code

public enum MallType
{
	Equip=0,//装备
	Consume=1,//消耗类道具
}

public enum MoneyType
{
	Gold=0,//金币
	Gem=1,//钻石
	
}