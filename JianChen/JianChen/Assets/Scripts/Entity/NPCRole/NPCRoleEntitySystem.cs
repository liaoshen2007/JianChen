using System.Collections.Generic;
using DataModel;
using FrameWork.JianChen.Core;
using UnityEngine;

public class NPCRoleEntitySystem : EntityBase
{

	private NPCRoleGameEntity _npcRoleGameEntity;
	private Vector3 _spawnPos;
	
    
	public override void Init()
	{
		_npcRoleGameEntity=new NPCRoleGameEntity();
		_npcRoleGameEntity.Init(this);
		_npcRoleGameEntity.Show(0);
	}

	public override void OnMessage(Message message)
	{
		string name = message.Name;
		object[] body = message.Params;
		switch (name)
		{
			case MessageConst.CMD_NPCENTITY_CREATENPC:
				if (GlobalData.SceneData.CurMapData.NpcDataList.Count>0)
				{

					_npcRoleGameEntity.CreatNPCEntity(GlobalData.SceneData.CurMapData.NpcDataList);
				}
				else
				{
					Loading.instance.OnHideLoadingView();
				}
				break;
		}
	}

	public override void SetData(params object[] paramsObjects)
	{
		if (paramsObjects.Length>0)
		{
			Debug.Log("setnpcpos");
			_spawnPos = (Vector3) paramsObjects[0];
		}
	}


}