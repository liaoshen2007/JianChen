using System;
using System.Collections.Generic;
using System.IO;
using Assets.Scripts.Framework.JianChen.Service;
using game.main;
using LitJson;
using UnityEditor;
using UnityEngine;

public class MapDataBuilder : MonoBehaviour
{
    //暂时主要编辑一个场景上的NPC数据，之后要拓展为可以编辑一个场景上的敌人数据和关卡Boss数据
    public MapData EditorMapData;
    private Transform _spawnPointTran;
    private Transform _npcRoot;
    private Transform _areaRoot;
    private string PrefabExt = ".prefab";

    private void Awake()
    {
        _spawnPointTran = transform.Find("SpawnPoint");
        _npcRoot = transform.Find("NpcRoot");
    }


    public void SaveJsonData()
    {
        Debug.LogError("saving mapdata " + EditorMapData.MapName);

        string jsondata = JsonMapper.ToJson(EditorMapData);
        string path = AssetLoader.GetMapDataPath(EditorMapData.MapName);
        StreamWriter sw = new StreamWriter(path);
        sw.Write(jsondata);
        sw.Close();
        sw.Dispose();

        Debug.LogError("save success " + EditorMapData.MapName);
    }

    public void SetMapData()
    {
        _spawnPointTran = transform.Find("SpawnPoint");
        _npcRoot = transform.Find("NpcRoot");
        _areaRoot = transform.Find("AreaRoot");
        if (EditorMapData.SpawnPointList == null)
        {
            EditorMapData.SpawnPointList = new List<SpawnPosValue>();
        }

        if (EditorMapData.NpcDataList == null)
        {
            EditorMapData.NpcDataList = new List<NPCData>();
        }

//        if (EditorMapData.ConnectMaps==null)
//        {
//            EditorMapData.NpcDataList=new List<NPCData>();
//        }
        EditorMapData.SpawnPointList.Clear();
        EditorMapData.NpcDataList.Clear();

        EditorMapData.AreaList?.Clear();


        for (int i = 0; i < _spawnPointTran.childCount; i++)
        {
            var point = _spawnPointTran.GetChild(i);
            SpawnPosValue pos = new SpawnPosValue();
            pos.PosX = point.localPosition.x;
            pos.PosY = point.localPosition.y;
            pos.PosZ = point.localPosition.z;
            EditorMapData.SpawnPointList.Add(pos);
        }
        
        for (int i = 0; i < _npcRoot.childCount; i++)
        {
            var npc = _npcRoot.GetChild(i);
            var npcentity = npc.GetComponent<NPCRoleSingleEntity>();
            npcentity.npcData.SpawnPos.PosX = npc.transform.localPosition.x;
            npcentity.npcData.SpawnPos.PosY = npc.transform.localPosition.y;
            npcentity.npcData.SpawnPos.PosZ = npc.transform.localPosition.z;                       
            npcentity.npcData.SpawnPos.AglX = npc.transform.localEulerAngles.x;
            npcentity.npcData.SpawnPos.AglY = npc.transform.localEulerAngles.y;
            npcentity.npcData.SpawnPos.AglZ = npc.transform.localEulerAngles.z;
            EditorMapData.NpcDataList.Add(npcentity.npcData); 
        }

        if (_areaRoot!=null)
        {
            for (int i = 0; i < _areaRoot.childCount; i++)
            {
                var area = _areaRoot.GetChild(i);
                var areaData = area.GetComponent<Area>();
                areaData.AreaData.EnemyDataList.Clear();
                for (int j = 0; j < areaData.transform.childCount; j++)
                {
                    var enemy = area.GetChild(j);
                    var enemyEntity = enemy.GetComponent<EnemyRoleSingleEntity>();
                    areaData.AreaData.EnemyDataList.Add(enemyEntity.Enemydata);
                    areaData.AreaData.EnemyDataList[j].SpawnPos.PosX = enemy.transform.position.x;
                    areaData.AreaData.EnemyDataList[j].SpawnPos.PosY = enemy.transform.position.y;
                    areaData.AreaData.EnemyDataList[j].SpawnPos.PosZ = enemy.transform.position.z;
                    areaData.AreaData.EnemyDataList[j].SpawnPos.AglX = enemy.transform.localEulerAngles.x;
                    areaData.AreaData.EnemyDataList[j].SpawnPos.AglY = enemy.transform.localEulerAngles.y;
                    areaData.AreaData.EnemyDataList[j].SpawnPos.AglZ = enemy.transform.localEulerAngles.z;
                }
                
                areaData.AreaData.AreaSpawnPos.PosX = area.transform.localPosition.x;
                areaData.AreaData.AreaSpawnPos.PosY = area.transform.localPosition.y;
                areaData.AreaData.AreaSpawnPos.PosZ = area.transform.localPosition.z;
                areaData.AreaData.AreaSpawnPos.AglX = area.transform.localEulerAngles.x;
                areaData.AreaData.AreaSpawnPos.AglY = area.transform.localEulerAngles.y;
                areaData.AreaData.AreaSpawnPos.AglZ = area.transform.localEulerAngles.z;
                
                
                EditorMapData.AreaList.Add(areaData.AreaData);
            }
            
            
        }
        
        
    }

    public void ReadJsonData()
    {
        //之后可以通过读json文件来给EditorMapData赋值。
        ConfigDataManager.LoadMapDataById<MapData>(EditorMapData.MapName, data =>
        {
            Debug.LogError(data.MapName+data.NpcDataList.Count+data.SpawnPointList.Count+data.ConnectMaps.Count);
            EditorMapData = data;
            SetNPCModelState();
        });
    }

    //自动生成NPC模型！
    private void SetNPCModelState()
    {
#if UNITY_EDITOR && !USE_BUNDLE
        _spawnPointTran = transform.Find("SpawnPoint");
        _npcRoot = transform.Find("NpcRoot");
        _areaRoot = transform.Find("AreaRoot");
        if (EditorMapData.SpawnPointList == null)
        {
            EditorMapData.SpawnPointList = new List<SpawnPosValue>();
        }

        if (EditorMapData.NpcDataList == null)
        {
            EditorMapData.NpcDataList = new List<NPCData>();
        }

        for (int i = 0; i < EditorMapData.SpawnPointList.Count; i++)
        {
            var newChild=new GameObject("Point"+i);
            newChild.transform.SetParent(_spawnPointTran,false);
            newChild.transform.localPosition=new Vector3((float)EditorMapData.SpawnPointList[i].PosX,(float)EditorMapData.SpawnPointList[i].PosY,(float)EditorMapData.SpawnPointList[i].PosZ);
        }
        
        
//        while (_spawnPointTran.childCount>0)
//        {
//            Destroy(_spawnPointTran.GetChild(0).gameObject);   
//        }
//        
//        while (_npcRoot.childCount>0)
//        {
//            Destroy(_npcRoot.GetChild(0).gameObject);             
//        }
//
//        if (_areaRoot!=null)
//        {
//            while (_areaRoot.childCount>0)
//            {
//                Destroy(_areaRoot.GetChild(0).gameObject);             
//            } 
//        }


        
        for (int i = 0; i < EditorMapData.NpcDataList.Count; i++)
        {
            var pathName = string.Format("3DModel/NPCRole/{0}/Prefab/{0}", EditorMapData.NpcDataList[i].AssetName);
            Debug.LogError( pathName.ToLower()+PrefabExt);
            var pathBundleNam = pathName.ToLower() + PrefabExt;
            string[] bundles = AssetDatabase.GetAssetPathsFromAssetBundle(pathBundleNam);
            Debug.LogError(bundles[0]);
            UnityEngine.Object obj =  AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(bundles[0]);  //Instantiate(ResourceManager.Load<GameObject>("3DModel/"+pathName));
            Debug.LogError(obj);
            GameObject gameobj = Instantiate(obj) as GameObject;
            gameobj.transform.SetParent(_npcRoot,false);
            NPCRoleSingleEntity npcentity = GetEntity<NPCRoleSingleEntity>(gameobj);
            npcentity.npcData = EditorMapData.NpcDataList[i];
            npcentity.transform.localPosition=new Vector3((float)npcentity.npcData.SpawnPos.PosX,(float)npcentity.npcData.SpawnPos.PosY,(float)npcentity.npcData.SpawnPos.PosZ);
            npcentity.transform.localEulerAngles=new Vector3((float)npcentity.npcData.SpawnPos.AglX,(float)npcentity.npcData.SpawnPos.AglY,(float)npcentity.npcData.SpawnPos.AglZ);

        }

        SetEnemyModelState();
        //AssetDatabase.Refresh();

#endif
    }

    //给生成敌人做预留的函数
#if UNITY_EDITOR && !USE_BUNDLE
    private void SetEnemyModelState()
    {
        if (_areaRoot==null)
        {
            return;
        }
        
        for (int i = 0; i < EditorMapData.AreaList.Count; i++)
        {
            var area = EditorMapData.AreaList[i];
            var newChild=new GameObject("area"+i);
            newChild.transform.SetParent(_areaRoot,false);
            newChild.AddComponent<Area>().AreaData = area;
            newChild.AddComponent<SphereCollider>().radius = area.Range;
            newChild.transform.localPosition=new Vector3((float)area.AreaSpawnPos.PosX,(float)area.AreaSpawnPos.PosY,(float)area.AreaSpawnPos.PosZ);
            newChild.transform.localEulerAngles=new Vector3((float)area.AreaSpawnPos.AglX,(float)area.AreaSpawnPos.AglY,(float)area.AreaSpawnPos.AglZ);
            for (int j = 0; j < area.EnemyDataList.Count; j++)
            {
                var pathName = string.Format("3DModel/EnemyRole/{0}/Prefab/{0}", area.EnemyDataList[i].AssetName);
                Debug.LogError( pathName.ToLower()+PrefabExt);
                var pathBundleNam = pathName.ToLower() + PrefabExt;
                string[] bundles = AssetDatabase.GetAssetPathsFromAssetBundle(pathBundleNam);
                Debug.LogError(bundles[0]);
                UnityEngine.Object obj =  AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(bundles[0]);  //Instantiate(ResourceManager.Load<GameObject>("3DModel/"+pathName));
                Debug.LogError(obj);
                GameObject gameobj = Instantiate(obj) as GameObject;
                gameobj.transform.SetParent(_areaRoot,false);
                EnemyRoleSingleEntity enemyentity = GetEntity<EnemyRoleSingleEntity>(gameobj);
                Debug.LogError(enemyentity);
                Debug.LogError(area.EnemyDataList[i].Name);
                enemyentity.Enemydata = area.EnemyDataList[i];
                enemyentity.transform.SetParent(newChild.transform,false);
                enemyentity.transform.position=new Vector3((float)area.EnemyDataList[i].SpawnPos.PosX,(float)area.EnemyDataList[i].SpawnPos.PosY,(float)area.EnemyDataList[i].SpawnPos.PosZ);
                enemyentity.transform.localEulerAngles=new Vector3((float)area.EnemyDataList[i].SpawnPos.AglX,(float)area.EnemyDataList[i].SpawnPos.AglY,(float)area.EnemyDataList[i].SpawnPos.AglZ);
                    
            }
            
        }
        
    }
#endif

    private T GetEntity<T>(GameObject gameObj)where T:MonoBehaviour
    {
        T objT = gameObj.GetComponent<T>();
        if (objT==null)
        {
            objT=gameObj.AddComponent<T>();
        }

        return objT;
    }
    
}

[Serializable]
public class MapData
{
    public int MapId;
    
    public string MapName;

    public List<SpawnPosValue> SpawnPointList;

    public List<NPCData> NpcDataList;

    
    //不应该是简单的EnemyData，要有一个Area作为包围区域！
    public List<AreaData> AreaList;

    public List<string> ConnectMaps;

    //之后还有传送门之类的list要拓展！
}


//之后要换成Int！精准到两位小数就可以
[Serializable]
public class SpawnPosValue
{
    public double PosX;
    public double PosY;
    public double PosZ;
    public double AglX;
    public double AglY;
    public double AglZ;
}