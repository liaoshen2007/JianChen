//
// Auto Generated Code By excel2json
//

// Generate From MissionRule.xlsx
public class MissionRule
{
	public int MissionId; // 任务Id
	public string MissionName; // 任务名称 
	public int MissionType; // 任务类型
	//public string TaskDetail;//任务细节
	public string TargetDetail;//任务目标
	public string Award; // 奖励
	public string MissionDesc; // 任务描述
	public string JumpTo; // 跳转，接收任务后可以立即跳转到哪张地图
	public int Weight; // 权重
	public int Progress; // 完成目标
	public int Level; //等级限制
	public int GoalNPC; //领奖NPC！
	public int PreTask;  //前置任务

}

public class TaskDetail
{
	public int TaskDetailtype;
	
	//detailid 可以是npcid，也可以是怪物id，或者是物品id，甚至是地图id。这个比较复杂，但是可以当成是弹性的id
	public int DetailId;
	public int TargetNum;

}
// End of Auto Generated Code

