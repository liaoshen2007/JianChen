using FrameWork.JianChen.Core;

namespace FrameWork.JianChen.Interfaces
{
	public interface IModel
	{
		void OnMessage(Message message);
		void Destroy();
	}
}
