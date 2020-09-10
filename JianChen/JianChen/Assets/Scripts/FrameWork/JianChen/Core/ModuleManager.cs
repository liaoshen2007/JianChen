using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Assets.Scripts.Framework.JianChen.Service;
using FrameWork.JianChen.Interfaces;
using UnityEngine;
//using cn.bmob.api;
//using cn.bmob.tools;
//using cn.bmob.io;
//using DataModel;

namespace FrameWork.JianChen.Core
{
    class ModuleManager : MonoBehaviour
    {
        private IModule _currentModule;
        private IModule _currentCommonModule;
        private List<string> _modulePath = new List<string>();
        public static float OffY;
        private GameObject _moduleParent;
        private GameObject _commonParent;
        private Dictionary<string, IModule> _modulesDic;
        private Dictionary<string, IModule> _commonModulesDic;

//        public static BmobUnity Bmob;

        private static ModuleManager _instance;

        public static ModuleManager Instance
        {
            get { return _instance; }
        }

        public void Awake()
        {
            _instance = this;
//            BmobDebug.Register(print);
//            Bmob = gameObject.GetComponent<BmobUnity>();
            _modulesDic = new Dictionary<string, IModule>();
            _commonModulesDic = new Dictionary<string, IModule>();
        }

        public GameObject InstantiatePrefab(string resUrl)
        {
            GameObject obj = Instantiate(ResourceManager.Load<GameObject>("module/"+resUrl));
            return obj;
        }

        public void DestroyObj(GameObject obj)
        {
            Destroy(obj);
        }

        public void SetOffY(float offY)
        {
            OffY = offY;
        }

        public void SetContainer(GameObject parent)
        {
            _moduleParent = parent;
        }

        /// <summary>
        /// 进入一个模块
        /// </summary>
        /// <param name="moduleName">名字</param>
        /// <param name="removePrev">是否移除上一个模块</param>
        /// <param name="hidePrev">是否隐藏上一个模块</param>
        /// <param name="paramObjects">传参</param>
        /// <returns></returns>
        public IModule EnterModule(string moduleName, bool removePrev = true, bool hidePrev = false,
            params object[] paramObjects)
        {
            if (_commonModulesDic.ContainsKey(moduleName))
            {
                return OpenCommonModule(moduleName, _commonParent, paramObjects);
            }

            if (_moduleParent == null)
            {
                throw new Exception("module parent null, do SetContainer");
            }
            if (removePrev && _currentModule != null)
            {
                Remove(_currentModule.ModuleName);
            }
            if (!removePrev && hidePrev && _currentModule != null)
            {
                Hide(_currentModule.ModuleName);
            }
            CheckPath(moduleName);
            _modulePath.Add(moduleName);
            return OpenModule(moduleName, paramObjects);
        }

//        /// <summary>
//        /// 跳转到其他模块
//        /// </summary>
//        /// <param name="data"></param>
//        /// <param name="removePrev"></param>
//        /// <param name="hidePrev"></param>
//        /// <returns></returns>
//        public IModule EnterModule(JumpData data, bool removePrev = false, bool hidePrev = true)
//        {
//            return EnterModule(data.Module, removePrev, hidePrev, data);
//        }
        
        private void RemoveModulePath(string moduleName)
        {
            var index = _modulePath.IndexOf(moduleName);
            if (index != -1)
            {
                _modulePath.RemoveAt(index);
            }
        }
        private void CheckPath(string moduleName)
        {
            try
            {
                var index = _modulePath.IndexOf(moduleName);
                if (index != -1)
                {
                    var num = _modulePath.Count - index;
                    _modulePath.RemoveRange(index, num);
                }
            }
            catch (Exception)
            {
                
            }
           
        }

        public IModule OpenCommonModule(string moduleName, params object[] paramObjects)
        {
            return OpenCommonModule(moduleName, null, paramObjects);
        }
        public IModule OpenCommonModule(string moduleName, GameObject parent, params object[] paramObjects)
        {
            RemoveAllModule();//进入通用模块，移除其他所有模块
            _modulePath.Clear();
            _currentModule = null;

            if (parent == null)
            {
                if (_commonParent != null)
                {
                    parent = _commonParent;
                }
                else
                {
                    throw new Exception("no common parent");
                }
                
            }
            if (_currentCommonModule != null && _currentCommonModule.ModuleName != moduleName)
            {
                _currentCommonModule.OnHide();
            }

            IModule module;
            //模块不创建多个实例
            if (_commonModulesDic.ContainsKey(moduleName))
            {
                Debug.Log("SHOW COMMON MODULE " + moduleName);
                module = _commonModulesDic[moduleName];
                module.SetData(paramObjects);
                module.LoadAssets();
                module.OnShow(0);
                _currentCommonModule = module;
                return module;
            }
            Debug.Log("OPEN COMMON MODULE " + moduleName);
            Assembly assembly = Assembly.GetExecutingAssembly(); // 获取当前程序集 
            object obj = assembly.CreateInstance(moduleName + "Module"); // 
            module = (IModule)obj;
            if (module != null)
            {
                module.ModuleName = moduleName;
                module.Parent = parent;
                module.SetData(paramObjects);
                module.LoadAssets();
                module.Init();

                _currentCommonModule = module;
                _commonModulesDic.Add(moduleName, module);
                _commonParent = parent;
            }
            else
            {
                throw new Exception("module init fail");
            }
            return module;
        }
        


        public IModule OpenModule(string moduleName, params object[] paramObjects)
        {
            if (_commonModulesDic.ContainsKey(moduleName))
            {
                throw new Exception("Can not open a CommonModule, use OpenCommonModule");
            }
            IModule module;
            //模块不创建多个实例
            if (_modulesDic.ContainsKey(moduleName))
            {
                Debug.Log("SHOW MODULE " + moduleName);
                module = _modulesDic[moduleName];
                module.SetData(paramObjects);
                module.LoadAssets();
                module.OnShow(0);
                _currentModule = module;
                return module;
            }
            Debug.Log("OPEN MODULE " + moduleName);
            Assembly assembly = Assembly.GetExecutingAssembly(); // 获取当前程序集 
            object obj = assembly.CreateInstance(moduleName + "Module"); // 
            module = (IModule) obj;
            if (module != null)
            {
                module.ModuleName = moduleName;
                module.Parent = _moduleParent;
                module.SetData(paramObjects);
                module.LoadAssets();
                module.Init();
                
                _currentModule = module;
                _modulesDic.Add(moduleName, module);
            }
            else
            {
                throw new Exception("module init fail");
            }
            return module;
        }

        public void Remove(string moduleName)
        {
            if (_modulesDic.ContainsKey(moduleName))
            {
                IModule module = _modulesDic[moduleName];
                module.Remove(0);
                _modulesDic.Remove(moduleName);
                RemoveModulePath(moduleName);
                Debug.Log("ModuleManager Remove:" + moduleName);
            }
            else
            {
                if (_commonModulesDic.ContainsKey(moduleName))
                {
                    Debug.LogWarning("Common Module Can't be removed");
                }
                else
                {
                    Debug.LogWarning("module no found: " + moduleName);
                }
            }
        }

        private void Hide(string moduleName)
        {
            if (_modulesDic.ContainsKey(moduleName))
            {
                IModule module = _modulesDic[moduleName];
                module.OnHide();
                Debug.Log("ModuleManager Hide:" + moduleName);
            }
            else
            {
                throw new Exception("module no found: " + moduleName);
            }
        }

        public void GoBack()
        {
            if (_modulePath.Count > 1)
            {
                var curModuleName = _modulePath[_modulePath.Count - 1];
                var prevModuleName = _modulePath[_modulePath.Count - 2];
                _modulePath.RemoveAt(_modulePath.Count - 1);
                Remove(curModuleName);
                OpenModule(prevModuleName);
            }
            else
            {
                //GoHome();
                RemoveAllModule();
                _modulePath.Clear();
                _currentModule = null;
                if (_currentCommonModule != null)
                {
                    _currentCommonModule.OnShow(0);
                }
            }
        }
        

        /// <summary>
        /// 销毁除CommonModule以外所有Module
        /// </summary>
        private void RemoveAllModule()
        {
            while (_modulesDic.Keys.Count > 0)
            {
                var moduleName = _modulesDic.Keys.ElementAt(0);
                Remove(moduleName);
            }
        }

        /// <summary>
        /// 销毁所有module，包括CommonModule
        /// </summary>
        public void DestroyAllModule()
        {
            foreach (var module in _modulesDic)
            {
                module.Value.Remove(0);
            }

            foreach (var module in _commonModulesDic)
            {
                module.Value.Remove(0);
            }
            _modulesDic.Clear();
            _commonModulesDic.Clear();
        }

        public void SendGlobalMessage(string msg)
        {
            foreach (KeyValuePair<string, IModule> module in _modulesDic)
            {
                module.Value.SendMessage(new Message("global", Message.MessageReciverType.DEFAULT, msg));
            }
        }
    }

}
