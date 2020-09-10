using System.Collections.Generic;
using FrameWork.JianChen.Interfaces;
using game.main;
using UnityEngine;

namespace Assets.Scripts.Framework.JianChen.Service
{
    public class PoolManager: MonoBehaviour
    {
        private Dictionary<string, Queue<IEntityView>> GameObjectDic;
        private static PoolManager _instance;
        public static PoolManager Instance => _instance;

        private void Awake()
        {
            //放到EntityManager的预制体下面！
            _instance = this;
            DontDestroyOnLoad(this);
            GameObjectDic=new Dictionary<string, Queue<IEntityView>>();
        }

        public bool CheckHasCacheEntity(string path)
        {
            if (GameObjectDic.ContainsKey(path))
            {
                if (GameObjectDic[path].Count>0)
                {
                    return true;
                }
            }

            return false;

        }

        public void RecoverEntity(string path,IEntityView recoverView)
        {
            if (GameObjectDic.ContainsKey(path))
            {
                GameObjectDic[path].Enqueue(recoverView);
            }
            else
            {
                var gameobjQuene = new Queue<IEntityView>();
                gameobjQuene.Enqueue(recoverView);
                GameObjectDic.Add(path,gameobjQuene);
            }
            recoverView.Hide();
        }

        public IEntityView TryGetEntity(string path)
        {
            if (CheckHasCacheEntity(path))
            {
                if (GameObjectDic[path].Count>0)
                {
                    var cacheview=GameObjectDic[path].Dequeue();
                    cacheview.ResetView();
                    cacheview.Show();
                    return cacheview;
                }
            }
            return null;
        }
        

        public void ClearPool()
        {
            //todo,隔一段时间就要清理一次对象池
        }



    }
}


