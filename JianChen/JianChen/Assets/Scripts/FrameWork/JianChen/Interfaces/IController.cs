using FrameWork.JianChen.Core;

namespace FrameWork.JianChen.Interfaces
{
	public interface IController
	{
		IModule Container { set; get; }
		Panel Panel { set; get; }
		IEntitySystem EntityContainer { set; get; }
		Core.Entity Entity { set; get; }
		void OnMessage(Message message);
		void Start();
		void Init();
		void Destroy();
	}

}