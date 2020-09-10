using System;
using System.Collections.Generic;

[Serializable]
public class DialogData
{
	public int DialogItemId; // 编号
	public string RoleName; // 角色名字
	public string DialogContent; // 对话内容
	public int DialogSetp; // 对话步骤
	public DialogEventType DialogEvent; // 对话事件
	public string DialogEventStr;
	public int DubbingId; // 任务Id

}

public enum DialogEventType
{
	None=0,
	ShowEmotion=1,//显示表情
	TransferToOther,//传送到其他地方
	GetAward,//获得奖励
	
	
}