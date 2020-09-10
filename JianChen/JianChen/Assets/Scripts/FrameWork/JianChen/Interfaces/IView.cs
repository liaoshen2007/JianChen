using FrameWork.JianChen.Core;

namespace FrameWork.JianChen.Interfaces
{
	public interface IView
	{
		IModule Container { set; }
		void SendMessage(Message message);
		void Show(float delay = 0);
		void Hide();
		void Destroy();
	}

}