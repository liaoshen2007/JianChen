using FrameWork.JianChen.Core;

namespace FrameWork.JianChen.Interfaces
{
    public interface IMessage
    {
        string Name { get; }

        object Body { get; set; }

        Message.MessageReciverType Type { get; set; }

        string ToString();
    }

}

