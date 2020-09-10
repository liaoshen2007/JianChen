using System.Collections.Generic;
using DataModel;
using FrameWork.JianChen.Core;
using UnityEngine;

public class EnemyRoleEntitySystem : EntityBase
{

	private EnemyRoleGameEntity _enemyRoleGameEntity;
//	private Vector3 _spawnPos;
	
    
	public override void Init()
	{
		_enemyRoleGameEntity=new EnemyRoleGameEntity();
		_enemyRoleGameEntity.Init(this);
		_enemyRoleGameEntity.Show(0);
	}

	public override void OnMessage(Message message)
	{
		string name = message.Name;
		object[] body = message.Params;
		switch (name)
		{
			case MessageConst.CMD_ENEMYENTITY_CREATEENEMY:
				if (GlobalData.SceneData.CurMapData.AreaList?.Count>0)
				{
					var scene = GameObject.FindGameObjectWithTag("EnvScene");
					var arearoot = scene.transform.Find("AreaRoot");
					foreach (var v in GlobalData.SceneData.CurMapData.AreaList)
					{
						var newChild=new GameObject("area"+v.AreaId);
						newChild.transform.SetParent(arearoot,false);
						newChild.AddComponent<Area>().AreaData = v;
						newChild.layer = 14;
						var collider=newChild.AddComponent<SphereCollider>();
						collider.radius = v.Range;
						collider.isTrigger = true;
						newChild.transform.localPosition=new Vector3((float)v.AreaSpawnPos.PosX,(float)v.AreaSpawnPos.PosY,(float)v.AreaSpawnPos.PosZ);
						newChild.transform.localEulerAngles=new Vector3((float)v.AreaSpawnPos.AglX,(float)v.AreaSpawnPos.AglY,(float)v.AreaSpawnPos.AglZ);
						_enemyRoleGameEntity.CreatEnemyEntity(v.EnemyDataList);
					}


				}


				break;
		}
	}

	public override void SetData(params object[] paramsObjects)
	{
		if (paramsObjects.Length>0)
		{
			Debug.Log("setEnemypos");
//			_spawnPos = (Vector3) paramsObjects[0];
		}
	}


}