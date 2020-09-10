using System;
using System.Collections.Generic;
using Common;
using FrameWork.JianChen.Interfaces;
using UnityEngine;


namespace FrameWork.JianChen.Core
{
    public class Entity
    {
        private IEntitySystem _curentity;

        public IEntitySystem CurEntity
        {
            get { return _curentity; }
        }
        
        private readonly List<IController> _controllers=new List<IController>();
        private readonly List<IEntityView> _entityViews=new List<IEntityView>();
        
        private GameObject _container;
        private GameObject _showObject;
        
        private bool _isSimpleEntity = true;
        private List<string> _modelList = new List<string>();

        /// <summary>
        /// 设成简单Entity，没有生成容器
        /// </summary>
        public void SetComplexEntity()
        {
            if (_container != null)
            {
                throw new Exception("Please Set It before Init");
            }
            _isSimpleEntity = false;
        }
        
        public virtual void Init(IEntitySystem entity)
        {
            _curentity = entity;
            _curentity.RegisterEntity(this);
          
            if (!_isSimpleEntity)
            {
                var type = this.GetType();
                _container = new GameObject("" + type.Name);
                _container.transform.SetParent(_curentity.Parent.transform, false);
                
                
                _container.transform.localPosition=Vector3.zero;
                _container.transform.localScale=Vector3.one;
              
                _showObject = _container;
            }
        }
        
             /// <summary>
        /// 初始化View
        /// </summary>
        /// <typeparam name="T">View object的控制类</typeparam>
        /// <param name="resUrl">prefab path</param>
        /// <param name="mode">RenderMode</param>
        /// <param name="siblingIndex">显示层级</param>
        /// <returns></returns>
        public IEntityView InstantiateView<T>(string resUrl, int siblingIndex=-1,Vector3 spawnpos=default(Vector3)) where T:IEntityView
        {
            var gameObj= EntityManager.Instance.SpawnRoleModel(resUrl,Vector3.zero);
            if (_container == null)
            {

                gameObj.transform.SetParent(_curentity.Parent.transform, false);
                if (_showObject != null)
                {
                    var type = this.GetType();
                    _container = new GameObject("" + type.Name);
                    _container.transform.SetParent(_curentity.Parent.transform, false);
                    _showObject.transform.SetParent(_container.transform, false);
                    gameObj.transform.SetParent(_container.transform, false);
                    _showObject = _container;
                }
                else
                {
                    _showObject = gameObj;
                }
                gameObj.transform.localPosition=Vector3.zero;
                
            }
            else
            {
                gameObj.transform.SetParent(_container.transform, false);
            }
            
            gameObj.transform.localScale = new Vector3(1, 1, 1);
            if (siblingIndex != -1)
            {
                gameObj.transform.SetSiblingIndex(siblingIndex);
            }
           
            var tType = typeof(T);
            IEntityView vscript = (IEntityView)gameObj.AddComponent(tType);
            //gameObj.AddComponent();
            RegisterView(vscript);
            return vscript;
        }
        
        /// <summary>
        /// 注册controller
        /// </summary>
        /// <param name="controller"></param>
        public void RegisterController(IController controller)
        {
            controller.Entity = this;
            _curentity.RegisterController(controller);
            _controllers.Add(controller);
        }

        public void RegisterView(IEntityView view)
        {
            _curentity.RegisterView(view);
            _entityViews.Add(view);
        }

        public void RegisterModel<T>() where T : IModel
        {
            _curentity.RegisterModel<T>();
            _modelList.Add(typeof(T).ToString());
        }
        
        public void UnregisterModel()
        {
            for (int i = 0; i < _modelList.Count; i++)
            {
                _curentity.UnregisterModel(_modelList[i]);
            }
        }

        public void SetSiblingIndex(int index)
        {
            if (_showObject != null)
            {
                _showObject.transform.SetSiblingIndex(index);
            }
            else
            {
                throw new Exception("No View Objects");
            }
           
        }
        
        /// <summary>
        /// 获取已注册数据
        /// </summary>
        /// <typeparam name="T">model type</typeparam>
        /// <returns></returns>
        public T GetData<T>() where T:IModel
        {
            return _curentity.GetData<T>();
        }
        

        protected void SetToTop()
        {
            if (_showObject != null)
            {
                _showObject.transform.SetAsLastSibling();
            }
            else
            {
                throw new Exception("No View Objects");
            }
        }

        public virtual void Show(float delay)
        {
            if (_container != null)
            {
                _container.Show();
            }
            SetToTop();
        }

        public virtual void Hide()
        {
            if (_container != null)
            {
                _container.Hide();
            }
        }

      
        private void Unregister()
        {
            _controllers.Clear();
            _entityViews.Clear();
            UnregisterModel();
        }

        public virtual void Destroy()
        {
            Unregister();
            _curentity.Destroy(_showObject);
        }
        


    }
}