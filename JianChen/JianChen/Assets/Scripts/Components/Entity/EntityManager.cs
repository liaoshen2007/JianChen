using System;
using System.Collections.Generic;
using System.Reflection;
using Assets.Scripts.Framework.JianChen.Service;
using FrameWork.JianChen.Core;
using FrameWork.JianChen.Interfaces;
using game.main;
using UnityEngine;

namespace Common
{
    public class EntityManager : MonoBehaviour
    {
        public enum GameModelType
        {
            MainRole,
            NPC,
            Friend,
            Enermy
        }
        
        private static EntityManager _instance;
        public static EntityManager Instance => _instance;
        private GameObject _entityParent;
        private EntityBase _curLeadingRole;
        private IEntitySystem _curEntity;
        private List<string> _entityPath=new List<string>();
        private Dictionary<string, IEntitySystem> _entityDic;
        
        const string PATH = "module/Entity/Prefabs/EntityManager";
        
        public static void Initialize()
        {
            if (_instance == null)
            {
                Instantiate(ResourceManager.Load<GameObject>(PATH));
            }
        }

        private void Awake()
        {
            _instance = this;
            _entityDic=new Dictionary<string, IEntitySystem>();
            DontDestroyOnLoad(this);
            SetContainer(this.gameObject);
        }
        
        public void SetContainer(GameObject parent)
        {
            _entityParent = parent;
        }

        public GameObject SpawnRoleModel(string roleName,Vector3 spawnPos)
        {
            GameObject obj = Instantiate(ResourceManager.Load<GameObject>("3DModel/"+roleName));
            return obj;
        }

        public GameObject SpawnSkillEffect(string effectName)
        {
            GameObject obj = Instantiate(ResourceManager.Load<GameObject>("3DModel/SkillEffect/Skill"+effectName));
            return obj;
        }

        public GameObject SpawnPoemClip()
        {
            GameObject obj = Instantiate(ResourceManager.Load<GameObject>("3DModel/PropModel/Prefabs/Poem"));
            return obj;
        }

        public GameObject SpawnDamageHud()
        {
            GameObject obj = Instantiate(ResourceManager.Load<GameObject>("3DModel/SkillEffect/DamageHudEffect"));
            return obj;
        }

        public IEntitySystem SpawnLeadingRolePool(string entityName,Vector3 spawnPos,bool replacePrev=true,params object[] paramObjects)
        {
            if (_curLeadingRole!=null&&_curLeadingRole.EntityName==entityName)
            {
                return _curLeadingRole;
            }

            if (_entityParent==null)
            {
                throw new Exception("Entity parent null, do SetContainer");
            }

            if (replacePrev&&_curLeadingRole!=null)
            {
                //todo
            }
            
            _entityPath.Add(entityName);


            return SpawnLeadingRoleEntity(entityName,paramObjects);
        }

        public IEntitySystem SpawnLeadingRoleEntity(string entityName, params object[] paramObjects)
        {
            IEntitySystem entity;
            if (_entityDic.ContainsKey(entityName))
            {
                Debug.Log("SHOW ENTITY+"+entityName);
                entity = _entityDic[entityName];
                entity.SetData(paramObjects);
                entity.LoadAssets();
                entity.OnShow(0);
                _curEntity = entity;
                return entity;
            }
            
            Debug.Log("OPEN ENTITY"+entityName);
            Assembly assembly = Assembly.GetExecutingAssembly(); // 获取当前程序集 
            object obj = assembly.CreateInstance(entityName + "EntitySystem"); // 
            entity = (IEntitySystem) obj;
            if (entity != null)
            {
                entity.EntityName = entityName;
                entity.Parent = _entityParent;
                entity.SetData(paramObjects);
                entity.LoadAssets();
                entity.Init();
                
                _curEntity = entity;
                _entityDic.Add(entityName, entity);
            }
            else
            {
                throw new Exception("module init fail");
            }
            return entity;
            
        }
        
        
    }
}