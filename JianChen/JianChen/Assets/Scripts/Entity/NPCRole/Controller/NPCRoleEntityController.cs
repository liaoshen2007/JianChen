using System.Collections.Generic;
using Assets.Scripts.Framework.JianChen.Service;
using Common;
using DataModel;
using FrameWork.JianChen.Core;
using FrameWork.JianChen.Core.Event;
using game.main;
using Module;
using UnityEngine;

public class NPCRoleEntityController : Controller
{

	//NPC属于list，应该要list控制！
	public NPCRoleSingleEntity EntityObj;
	public List<NPCRoleSingleEntity> NpcRoleSingleEntities;

	//开发步骤，地图加载完毕的时候会给controller发送一个消息，controller是从MapManager那里拿到NPC模型和数据的！
	
	
	
	public override void Start()
	{
		//发送消息给GameMainUI，可以操作控制！  //初始化人物可以执行!
//		EntityObj.SetData(GlobalData.PlayerData.PlayerVo);
		NpcRoleSingleEntities=new List<NPCRoleSingleEntity>(); 
		EventDispatcher.AddEventListener(EventConst.LoadModel,LoadNpcModel);
		EventDispatcher.AddEventListener(EventConst.UnLoadModel,UnLoadNpcModel);
		EventDispatcher.AddEventListener<int>(EventConst.ClickNpc,NPConClick);
		EventDispatcher.AddEventListener<UserMissionVo>(EventConst.CanGetRewardFromNpc,CanGetRewardFromNpc);
	    
	}

	//不仅领奖，接受任务完成任务也需要。
	private void CanGetRewardFromNpc(UserMissionVo userMissionVo)
	{
		var canrewardNPC = GlobalData.MissionData.MissionRuleDic[userMissionVo.MissionId];
		for (int i = 0; i < NpcRoleSingleEntities.Count; i++)
		{
			
			if (NpcRoleSingleEntities[i].npcData.ID==canrewardNPC.GoalNPC)
			{
				//这里就可以改变NPC模型头上的灯泡之类的。
				Debug.Log("can get reward from this npc:"+canrewardNPC.GoalNPC);
				
				
			}
			
			
		}
		
		
		
	}


	//todo 之后完成任务的时候，要通知此模块的相关NPC，灯泡要变成完成状态，或者加载模型的时候就要把相关任务数据绑定好！尤其是可领取状态的NPC！复杂！
	
	
	private void NPConClick(int npcid)
	{
		NPCData targetNpc=new NPCData();
		for (int i = 0; i < NpcRoleSingleEntities.Count; i++)
		{
			if (NpcRoleSingleEntities[i].npcData.ID==npcid)
			{
				targetNpc = NpcRoleSingleEntities[i].npcData;
			}
		}

		if (targetNpc.ID!=0)
		{
			ModuleManager.Instance.EnterModule(ModuleConfig.MODULE_NPCPREVIEW,false,false,targetNpc);
		}
		else
		{
			FlowText.ShowMessage("无NPC对话");
		}


	}

	//todo 这里应该要更新所有当前地图的NPC头顶任务状态！
	public void UpdateRewardStateNPC()
	{
		
		
		
	}

	private void UnLoadNpcModel()
	{
		//加载地图之前要先卸载NPC模型！
		for (int i = 0; i < NpcRoleSingleEntities.Count; i++)
		{
//			Debug.LogError("destorynpc?"+i);
			PoolManager.Instance.RecoverEntity(string.Format("NPCRole/{0}/Prefab/{0}",
				NpcRoleSingleEntities[i].npcData.AssetName),NpcRoleSingleEntities[i]);
			
		}
		
		
	}

	private void LoadNpcModel()//List<NPCData> npcDatas
	{
		SendMessage(new Message(MessageConst.CMD_NPCENTITY_CREATENPC));
		
	}

	/// <summary>
	/// 处理View消息
	/// </summary>
	/// <param name="message"></param>
	public override void OnMessage(Message message)
	{
		string name = message.Name;
		object[] body = message.Params;
		switch (name)
		{

		}
	}

	

	public override void Destroy()
	{
		EventDispatcher.RemoveEventListener(EventConst.LoadModel,LoadNpcModel);
		EventDispatcher.RemoveEventListener(EventConst.UnLoadModel,UnLoadNpcModel);
		EventDispatcher.RemoveEventListener<int>(EventConst.ClickNpc,NPConClick);
	}
}
