using System.Collections.Generic;
using Assets.Scripts.Framework.JianChen.Service;
using FrameWork.JianChen.Core;
using FrameWork.JianChen.Interfaces;
using UnityEngine;

public class EnemyRoleGameEntity : Entity
{

	private EnemyRoleEntityController _enemyRoleEntityController;	
    
	public override void Init(IEntitySystem entity)
	{
		SetComplexEntity();
		base.Init(entity);
		
		//InstantiateView可以拓展为传入位置！先读取json然后给生成的npc传入位置！
		
		_enemyRoleEntityController=new EnemyRoleEntityController();
		RegisterController(_enemyRoleEntityController);
		_enemyRoleEntityController.Start();
	}

	public void CreatEnemyEntity(List<EnemyData> list)
	{
		for (int i = 0; i < list.Count; i++)
		{
			//todo 之后要从npcdata的模型路径中加载模型
			//要知道NPCData的生成位置！
			var enemyroleEntityobj = (EnemyRoleSingleEntity)PoolManager.Instance.TryGetEntity(string.Format("EnemyRole/{0}/Prefab/{0}",list[i].AssetName));//"NPCRole/UnityGirl/Prefab/UnityGirl"
			if (enemyroleEntityobj==null)
			{
				enemyroleEntityobj =(EnemyRoleSingleEntity)InstantiateView<EnemyRoleSingleEntity>(string.Format("EnemyRole/{0}/Prefab/{0}",list[i].AssetName));	
			}
			//还要记录其旋转值！
			enemyroleEntityobj.SetEnemyData(list[i]); 
			enemyroleEntityobj.transform.position=new Vector3((float)list[i].SpawnPos.PosX,(float)list[i].SpawnPos.PosY,(float)list[i].SpawnPos.PosZ);
			enemyroleEntityobj.transform.localEulerAngles=new Vector3((float)list[i].SpawnPos.AglX,(float)list[i].SpawnPos.AglY,(float)list[i].SpawnPos.AglZ);
			_enemyRoleEntityController.EnemyRoleSingleEntities.Add(enemyroleEntityobj);
			RegisterView(enemyroleEntityobj);
		}
		Loading.instance.OnHideLoadingView();
		

		
	}
	
	public override void Hide()
	{
	}

	public override void Show(float delay)
	{
		Debug.Log("Enemy spawn Success!");
	}
}