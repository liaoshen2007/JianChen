using System;
using FrameWork.JianChen.Interfaces;

namespace FrameWork.JianChen.Core
{
    public class Controller : IController
    {
        private IModule _container;
        public virtual IModule Container
        {
            set
            {
                _container = value;
            }

            get
            {
                return _container;
            }
        }

        private Panel _panel;
        public Panel Panel
        {
            set
            {
                _panel = value;
            }

            get
            {
                return _panel;
            }
        }

        //Controller层需要修改的！
        public IEntitySystem EntityContainer { get; set; }

        public Entity Entity { get; set; }

        protected Controller()
        {
            //_container = module;
        }
        public virtual void SendMessage(Message message)
        {
            if (_container != null)
            {
                _container.SendControllerMessage(message);
            }


            if (EntityContainer!=null)
            {
                EntityContainer.SendControllerMessage(message);
            }

            if (_container==null&&EntityContainer==null)
            {
                throw new Exception(this.GetType()+" didn't registered");
            }
            
        }
        public virtual void OnMessage(Message message)
        {

        }

        /// <summary>
        /// 注册后自动执行
        /// </summary>
        public virtual void Init()
        {
            
        }

        /// <summary>
        /// 需手动启动
        /// </summary>
        public virtual void Start()
        {
            
        }

        public virtual void Destroy()
        {
            if(_container != null)
                _container.UnregisterController(this);
            
            _container = null;
            _panel = null;
        }

        /// <summary>
        /// 获取已注册数据
        /// </summary>
        /// <typeparam name="T">model type</typeparam>
        /// <returns></returns>
        public T GetData<T>() where T:IModel
        {
            if(_container==null) throw new Exception(this.GetType()+" didn't registered");
            return _container.GetData<T>();
        }
    }

}
