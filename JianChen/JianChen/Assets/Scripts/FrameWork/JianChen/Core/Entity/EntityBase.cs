using System;
using System.Collections.Generic;
using System.Reflection;
using FrameWork.JianChen.Interfaces;
using game.main;
using UnityEngine;

namespace FrameWork.JianChen.Core
{
    public class EntityBase : IEntitySystem
    {
        public GameObject Parent { get; set; }
        public Camera ViewCamera { get; set; }
        public string EntityName { get; set; }
        private readonly Dictionary<string, IController> _controllerDic = new Dictionary<string, IController>();
        private readonly Dictionary<string, IModel> _modelsDic = new Dictionary<string, IModel>();
        
        private List<Entity> _entityList=new List<Entity>();
        private GameObject _container;
        
        public void LoadAssets()
        {
            Debug.Log("todo");
        }

        public void UnloadAssets()
        {
            Debug.Log("todo");
        }

        public virtual void Init()
        {

        }

        public void SendMessage(Message message)
        {
            switch (message.Type)
            {
                case Message.MessageReciverType.CONTROLLER:
                    SendToControllers(message);
                    break;
                case Message.MessageReciverType.MODEL:
                    SendToModels(message);
                    break;
                case Message.MessageReciverType.DEFAULT:
                    OnMessage(message);
                    SendToControllers(message);
                    SendToModels(message);
                    break;
            }
        }
        
        private void SendToControllers(Message message)
        {
            var buffer = new List<string>(_controllerDic.Keys);
            if (_controllerDic.Count > 0)
            {
                foreach (string keys in buffer)
                {
                    IController value;
                    _controllerDic.TryGetValue(keys, out value);
                    if (value != null)
                    {
                        value.OnMessage(message);
                    }
                }
                /*foreach (var item in _controllerDic)
                {
                item.Value.OnMessage(message);
                }*/
            }
            
        }

        private void SendToModels(Message message)
        {
            foreach (var model in _modelsDic)
            {
                if(model.Value!=null)
                    model.Value.OnMessage(message);
            }
           
        }
        
        public void SendViewMessage(Message message)
        {
            SendMessage(message);
        }

        public void SendControllerMessage(Message message)
        {
            SendMessage(message);
        }

        public virtual void OnMessage(Message message)
        {
            
        }

        public void RegisterEntity(Entity entity)
        {
            if (_entityList == null)
            {
                _entityList = new List<Entity>();
            }
            _entityList.Add(entity);
        }

        public void UnregisterEntity(Entity entity)
        {
            if (_entityList != null)
            {
                _entityList.Remove(entity);
            }
        }

        public void RegisterView(IEntityView view)
        {
            view.Container = this;
        }

        public T RegisterModel<T>() where T : IModel
        {
            var type = typeof(T);
            var cname = type.ToString();

            IModel model = null;
            if (!_modelsDic.ContainsKey(cname))
            {
                Assembly assembly = Assembly.GetExecutingAssembly(); // 获取当前程序集 
                object obj = assembly.CreateInstance(type.ToString()); // 
                model = (IModel)obj;
                _modelsDic.Add(cname, model);
            }
            else
            {
                model = _modelsDic[cname];
            }

            return (T) model;
        }

        public void UnregisterModel(string name)
        {
//            var cname = model.GetType().ToString();
            if (_modelsDic.ContainsKey(name))
            {
                _modelsDic.Remove(name);
            }
        }

        public void UnregisterView(IEntityView view)
        {
            view.Container = null;
        }

        public void RegisterController(IController ctrl)
        {
            ctrl.EntityContainer = this;
            var cname = ctrl.ToString()+ctrl.GetHashCode();
            if (!_controllerDic.ContainsKey(cname))
            {
                _controllerDic.Add(cname, ctrl);
            }
            ctrl.Init();
        }

        public void UnregisterController(IController ctrl)
        {
            ctrl.Container = null;
            var cname = ctrl.ToString() + ctrl.GetHashCode();
            if (_controllerDic.ContainsKey(cname))
            {
                _controllerDic.Remove(cname);
            }
        }

        public GameObject InstantiateView(string resUrl)
        {
            return ModuleManager.Instance.InstantiatePrefab(resUrl);
        }

        public void Destroy(GameObject obj)
        {
            ModuleManager.Instance.DestroyObj(obj);
        }

        public void OnHide()
        {
            if (_container != null)
            {
                _container.Hide();
            }
        }

        public virtual void SetData(params object[] paramsObjects)
        {
            
        }

        public void OnShow(float delay)
        {
            _container.transform.SetAsLastSibling();
            if (_container != null)
            {
                _container.Show();
            }
        }

        public void Remove(float delay)
        {
            var temp = new List<IController>();
            
            foreach (var item in _controllerDic)
            {
                temp.Add(item.Value);
            }

            for (int i = temp.Count - 1; i >= 0; i--)
            {
                temp[i].Destroy();
            }
            
            foreach (var item in _modelsDic)
            {
                item.Value.Destroy();
            }
            if (_entityList != null)
            {
                foreach (var entity in _entityList)
                {                    
                    entity.Destroy();
                }
                _entityList.Clear();
            }
            if(_container!=null)
                Destroy(_container);
           
            _controllerDic.Clear();
            _modelsDic.Clear();
            
            UnloadAssets();

            Resources.UnloadUnusedAssets();
        }

        public T GetData<T>()
        {
            var cname = typeof(T).ToString();
            if (_modelsDic.ContainsKey(cname))
            {
                return (T)_modelsDic[cname];
            }
            else
            {
                throw new Exception("No this Type data");
            }
        }
    }
}
