using FrameWork.JianChen.Core;
using UnityEngine;

namespace FrameWork.JianChen.Interfaces
{
	public interface IEntitySystem  
	{

		GameObject Parent { get; set; }
		Camera ViewCamera { get; set; }
		string EntityName { get; set; }
		void LoadAssets();
		void UnloadAssets();
		void Init();
		void SendMessage(Message message);
		void SendViewMessage(Message message);
		void SendControllerMessage(Message message);
		void OnMessage(Message message);
		void RegisterEntity(Core.Entity entity);
		void UnregisterEntity(Core.Entity entity);
		void RegisterView(IEntityView view);
		T RegisterModel<T>() where T : IModel;
		void UnregisterModel(string name);
		void UnregisterView(IEntityView view);
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


