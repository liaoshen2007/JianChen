using System.Collections.Generic;
using Assets.Scripts.Framework.JianChen.Service;
using FrameWork.JianChen.Core;
using FrameWork.JianChen.Interfaces;
using UnityEngine;

public class NPCRoleGameEntity : Entity
{

	private NPCRoleEntityController _npcRoleEntityController;	
    
	public override void Init(IEntitySystem entity)
	{
		SetComplexEntity();
		base.Init(entity);
		
		//InstantiateView可以拓展为传入位置！先读取json然后给生成的npc传入位置！
		
		_npcRoleEntityController=new NPCRoleEntityController();
		RegisterController(_npcRoleEntityController);
		_npcRoleEntityController.Start();
	}

	public void CreatNPCEntity(List<NPCData> list)
	{
		for (int i = 0; i < list.Count; i++)
		{
			//todo 之后要从npcdata的模型路径中加载模型
			//要知道NPCData的生成位置！
			var npcroleEntityobj = (NPCRoleSingleEntity)PoolManager.Instance.TryGetEntity(string.Format("NPCRole/{0}/Prefab/{0}",list[i].AssetName));//"NPCRole/UnityGirl/Prefab/UnityGirl"
			if (npcroleEntityobj==null)
			{
				npcroleEntityobj =(NPCRoleSingleEntity)InstantiateView<NPCRoleSingleEntity>(string.Format("NPCRole/{0}/Prefab/{0}",list[i].AssetName));	
			}
			//还要记录其旋转值！
			npcroleEntityobj.SetNpcData(list[i]); 
			npcroleEntityobj.transform.localPosition=new Vector3((float)list[i].SpawnPos.PosX,(float)list[i].SpawnPos.PosY,(float)list[i].SpawnPos.PosZ);
			npcroleEntityobj.transform.localEulerAngles=new Vector3((float)list[i].SpawnPos.AglX,(float)list[i].SpawnPos.AglY,(float)list[i].SpawnPos.AglZ);
			_npcRoleEntityController.NpcRoleSingleEntities.Add(npcroleEntityobj);
			RegisterView(npcroleEntityobj);
		}
		
		_npcRoleEntityController.UpdateRewardStateNPC();
		Loading.instance.OnHideLoadingView();
		

		
	}
	
	public override void Hide()
	{
	}

	public override void Show(float delay)
	{
		Debug.Log("Success spawn npc!");
	}
}