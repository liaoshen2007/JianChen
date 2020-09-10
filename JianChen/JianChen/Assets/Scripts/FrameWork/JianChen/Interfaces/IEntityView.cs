using FrameWork.JianChen.Core;

namespace FrameWork.JianChen.Interfaces
{
	public interface IEntityView
	{
		IEntitySystem Container { set; }
		void SendMessage(Message message);
		void Show(float delay = 0);
		void Hide();
		void ResetView();
		void Destroy();
	}

}