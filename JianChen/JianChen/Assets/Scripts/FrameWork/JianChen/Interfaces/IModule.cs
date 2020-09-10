using FrameWork.JianChen.Core;
using UnityEngine;

namespace FrameWork.JianChen.Interfaces
{
    public interface IModule  
    {

        GameObject Parent { get; set; }
        Camera ViewCamera { get; set; }
        string ModuleName { get; set; }
        void LoadAssets();
        void UnloadAssets();
        void Init();
        void SendMessage(Message message);
        void SendViewMessage(Message message);
        void SendControllerMessage(Message message);
        void OnMessage(Message message);
        void RegisterPanel(Panel panel);
        void UnregisterPanel(Panel panel);
        void RegisterView(IView view);
        T RegisterModel<T>() where T : IModel;
        void UnregisterModel(string name);
        void UnregisterView(IView view);
        void RegisterController(IController ctrl);
        void UnregisterController(IController ctrl);
        GameObject InstantiateView(string resUrl);
        void Destroy(GameObject obj);
        void OnHide();
        void SetData(params object[] paramObjects);
        void OnShow(float delay);
        void Remove(float delay);
        T GetData<T>();

    }
}


