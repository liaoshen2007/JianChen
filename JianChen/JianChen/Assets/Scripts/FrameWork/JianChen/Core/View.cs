using Assets.Scripts.Framework.JianChen.Service;

namespace FrameWork.JianChen.Core
{
    using UnityEngine;
    using Interfaces;

    public class View : MonoBehaviour, IView
    {
        private float _scaleFactor;
        private IModule _container;

        protected GameObject InstantiatePrefab(string resUrl)
        {
            return Instantiate(ResourceManager.Load<GameObject>("module/"+resUrl));
        }
        
        protected GameObject GetPrefab(string resUrl)
        {
            return ResourceManager.Load<GameObject>("module/"+resUrl);
        }
        
//        public float width
//        {
//            get {
//                _scaleFactor = gameObject.GetComponent<Canvas>().scaleFactor;
//                return Screen.width / _scaleFactor;
//            }
//        }
//        public float height
//        {
//            get {
//                _scaleFactor = gameObject.GetComponent<Canvas>().scaleFactor;
//                return Screen.height / _scaleFactor;
//            }
//            set { throw new System.NotImplementedException(); }
//        }
//
//        public float scale
//        {
//            get {
//                _scaleFactor = gameObject.GetComponent<Canvas>().scaleFactor;
//                return _scaleFactor;
//            }
//        }


        public virtual IModule Container
        {
            set
            {
                _container = value;
            }
        }
        public virtual void SendMessage(Message message)
        {
            _container.SendViewMessage(message);
        }

        public virtual void SendTopMessage(string type, string content, float time)
        {

        }

        public virtual void SendTopMessageLang(string type, string contentKey, float time)
        {

        }

        public virtual void Show(float delay = 0)
        {
            gameObject.Show();
        }

        public virtual void Hide()
        {
            gameObject.Hide();
        }

        public virtual void Destroy()
        {

        }
    }
}