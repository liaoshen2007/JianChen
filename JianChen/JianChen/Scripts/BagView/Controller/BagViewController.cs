using System;
using FrameWork.JianChen.Core;

public class BagViewController : Controller
{

    public BagViewView View;

    public override void Start()
    {

    }

    public override void OnMessage(Message message)
    {
        string name = message.Name;
        object[] body = message.Params;
        switch (name)
        {

        }
    }

    public override void Destroy()
    {
        base.Destroy();
    }
}
