using Assets.Scripts.Framework.JianChen.Service;
using System.Collections.Generic;
using FrameWork.JianChen.Interfaces;
using UnityEngine;

namespace game.main
{
    public class PopupManager : MonoBehaviour
    {
        private static PopupManager _instance;

        private static Window _currentWindow;
        private static Stack<Window>  _popupWindows=new Stack<Window>();

        private Transform _windowLayer;

        private void Awake()
        {
            _instance = this;
            _windowLayer = transform;
            DontDestroyOnLoad(gameObject);
        }

        public static bool IsOnBack()
        {
            return _popupWindows.Count > 0;
        }

        /// <summary>
        /// 安卓返回键处理
        /// </summary>
        public static void OnBackKeyPress()
        {
            //_currentWindow.Close();
            if (_popupWindows.Count == 0)
                return;
            Window closeWindow = _popupWindows.Peek();
            closeWindow.Close();
        }

        /// <summary>
        /// 关闭最后一个窗口
        /// </summary>
        public static void CloseLastWindow()
        {
            _currentWindow.Close();
        }

        /// <summary>
        /// 把一个窗口加到舞台
        /// </summary>
        /// <param name="windowName">窗口的prefab路径 在Window类中</param>
        public static Window ShowWindow(string windowName)
        {
            GameObject window = InitPopupPrefab(windowName);
            Popup(window);

            Window win = window.GetComponent<Window>();
            win.OnOpen();
            _popupWindows.Push(win);
            return win;
        }

        public static T ShowWindow<T>(string windowName, IModule module = null) where T : Window
        {
            GameObject window = InitPopupPrefab(windowName);
            Popup(window);
            var win = window.AddScriptComponent<T>();
            win.Container = module;
            win.AssetName = windowName;
            win.OnOpen();
            _popupWindows.Push(win);
            return win;
        }
        
        public static T ShowWindow<T>(string windowName) where T : Window
        {
            GameObject window = InitPopupPrefab(windowName);
            Popup(window);
            var win = window.AddScriptComponent<T>();
            win.OnOpen();
            _popupWindows.Push(win);
            return win;
        }

        public static void Popup(GameObject go)
        {
            go.transform.SetParent(_instance._windowLayer, false);
            RectTransform rect = go.GetComponent<RectTransform>();
            rect.anchoredPosition = Vector2.zero;
        }

        public static void CloseWindow(Window win)
        {
            DestroyImmediate(win.transform.gameObject);
            Resources.UnloadAsset(win);
            _popupWindows.Pop();
        }

        public static GameObject InitPopupPrefab(string prefabPath)
        {
            return Instantiate(ResourceManager.Load<GameObject>("module/" + prefabPath));
        }

        /// <summary>
        /// 通用提示窗口
        /// </summary>
        /// <param name="content">提示内容</param>
        /// <param name="title">标题</param>
        /// <param name="okBtnText">确定按钮文本</param>
        /// <returns></returns>
        public static AlertWindow ShowAlertWindow(string content, string title = "提 示", string okBtnText = "确 定")
        {
            AlertWindow win = ShowWindow<AlertWindow>(Constants.AlertWindowPath);
            win.Content = content;
            win.Title = title;
            win.OkText = okBtnText;

            return win;
        }

        /// <summary>
        /// 通用确认窗口
        /// </summary>
        /// <param name="content">提示内容</param>
        /// <param name="title">标题</param>
        /// <param name="okBtnText">确定按钮文本</param>
        /// <param name="cancelBtnText">取消按钮文本</param>
        /// <returns></returns>
        public static ConfirmWindow ShowConfirmWindow(string content, string title = "提 示", string okBtnText = "确 定",
            string cancelBtnText = "取 消")
        {
            ConfirmWindow win = ShowWindow<ConfirmWindow>(Constants.ConfirmWindowPath);
            win.Content = content;
            win.Title = title;
            win.OkText = okBtnText;
            win.CancelText = cancelBtnText;

            return win;
        }
    }
}