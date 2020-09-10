using System.Collections.Generic;
using Assets.Scripts.Framework.JianChen.Service;
using Common;
using DataModel;
using FrameWork.JianChen.Core;
using FrameWork.JianChen.Core.Event;
using UnityEngine;

public class EnemyRoleEntityController : Controller
{

	//NPC属于list，应该要list控制！
	//public EnemyRoleSingleEntity EntityObj;
	public List<EnemyRoleSingleEntity> EnemyRoleSingleEntities;

	//开发步骤，地图加载完毕的时候会给controller发送一个消息，controller是从MapManager那里拿到NPC模型和数据的！
	
	
	
	public override void Start()
	{
		//发送消息给GameMainUI，可以操作控制！  //初始化人物可以执行!
//		EntityObj.SetData(GlobalData.PlayerData.PlayerVo);
		EnemyRoleSingleEntities=new List<EnemyRoleSingleEntity>(); 
		EventDispatcher.AddEventListener(EventConst.LoadModel,LoadEnemyModel);
		EventDispatcher.AddEventListener(EventConst.UnLoadModel,UnLoadEnemyModel);
		EventDispatcher.AddEventListener<int>(EventConst.ClickEnemy,EnemyonClick);
		EventDispatcher.AddEventListener<Transform,Area>(EventConst.HasPlayerEnter,HasPlayerEnter);
		EventDispatcher.AddEventListener<Transform,Area>(EventConst.GiveUpTargetAndBack,HasPlayerLevel);
		//EventDispatcher.AddEventListener(EventConst.HasBeenAttacked,HasBeenAttacked);
	}



	//玩家离开了区域！
	private void HasPlayerLevel(Transform tran, Area area)
	{
		Debug.Log("tranenter:"+tran.name);
		var areadata = area.AreaData;
		
		//这个算法不好，需要优化
		foreach (var v in EnemyRoleSingleEntities)
		{	
			foreach (var a in areadata.EnemyDataList)
			{
				if (v.Enemydata.ID==a.ID)
				{
					v.CallBacKtoOri(tran);
				}
			
			}
		}
		EventDispatcher.TriggerEvent(EventConst.ShowBattleTipsView,false);
	}
	
	//todo 明天的任务 要通过areaId来查找Enemy群组，然后再锁定玩家攻击它们！！
	private void HasPlayerEnter(Transform tran, Area area)
	{
		Debug.Log("tranenter:"+tran.name);
		var areadata = area.AreaData;
		
		//这个算法不好，需要优化
		foreach (var v in EnemyRoleSingleEntities)
		{	
			foreach (var a in areadata.EnemyDataList)
			{
				if (v.Enemydata.ID==a.ID)
				{
					v.SetTarget(tran);
				}
			
			}
		}
		
		EventDispatcher.TriggerEvent(EventConst.ShowBattleTipsView,true);
	}


	//开始战斗
	private void EnemyonClick(int npcid)
	{
//		NPCData targetNpc=new NPCData();
//		for (int i = 0; i < NpcRoleSingleEntities.Count; i++)
//		{
//			if (NpcRoleSingleEntities[i].npcData.ID==npcid)
//			{
//				targetNpc = NpcRoleSingleEntities[i].npcData;
//			}
//		}
//
//		if (targetNpc.ID!=0)	
//		{
//			ModuleManager.Instance.EnterModule(ModuleConfig.MODULE_NPCPREVIEW,false,false,targetNpc);
//		}
//		else
//		{
//			FlowText.ShowMessage("无NPC对话");
//		}


	}

	private void UnLoadEnemyModel()
	{
		//加载地图之前要先卸载NPC模型！
		for (int i = 0; i < EnemyRoleSingleEntities.Count; i++)
		{
			
			PoolManager.Instance.RecoverEntity(string.Format("EnemyRole/{0}/Prefab/{0}",
				EnemyRoleSingleEntities[i].Enemydata.AssetName),EnemyRoleSingleEntities[i]);
			
		}
		
		
	}

	private void LoadEnemyModel()//List<NPCData> npcDatas
	{
		SendMessage(new Message(MessageConst.CMD_ENEMYENTITY_CREATEENEMY));
		
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
		EventDispatcher.RemoveEventListener(EventConst.LoadModel,LoadEnemyModel);
		EventDispatcher.RemoveEventListener(EventConst.UnLoadModel,UnLoadEnemyModel);
		EventDispatcher.RemoveEventListener<int>(EventConst.ClickEnemy,EnemyonClick);
		EventDispatcher.RemoveEventListener<Transform,Area>(EventConst.HasPlayerEnter,HasPlayerEnter);
		EventDispatcher.RemoveEventListener<Transform,Area>(EventConst.GiveUpTargetAndBack,HasPlayerLevel);
	}
}
