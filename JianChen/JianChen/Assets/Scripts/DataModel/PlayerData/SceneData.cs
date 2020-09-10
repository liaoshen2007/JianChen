using System;
using System.Collections.Generic;
using DataModel;
using FrameWork.JianChen.Core;
using UnityEngine;

namespace JianChen.Data
{
	public class SceneData : Model
	{
		public Dictionary<string, MapData> MapDataDic;
		public MapData CurMapData;

		//这里可以拓展玩家数据相关的方法！
		public SceneData()
		{
			MapDataDic=new Dictionary<string, MapData>();
		}

//		public void LoadData(MapData mapData)
//		{
//			if (!MapDataDic.ContainsKey(mapData.MapName))
//			{
//				MapDataDic.Add(mapData.MapName,mapData);
//			}	
//		}
		
		//todo 要在loading这里切换背景音乐！
		public void LoadData(string sceneName,Action action)
		{
			Debug.Log("JumpMap"+sceneName);
			if (!MapDataDic.ContainsKey(sceneName))
			{
				ConfigDataManager.LoadMapDataById<MapData>(sceneName, data =>
				{
					Debug.Log(data.MapName+":"+data.NpcDataList.Count+" "+data.SpawnPointList.Count+" "+data.ConnectMaps.Count);
					MapDataDic.Add(sceneName,data);
					CurMapData = data;
					action?.Invoke();
				});
			}
			else
			{
				CurMapData = MapDataDic[sceneName];
				action?.Invoke();
			}
		}
		
		public Vector3 SpawnPos(int index)
		{
			return new Vector3((float)CurMapData.SpawnPointList[index].PosX,(float)CurMapData.SpawnPointList[index].PosY,(float)CurMapData.SpawnPointList[index].PosZ);
		}
		

	}

}


