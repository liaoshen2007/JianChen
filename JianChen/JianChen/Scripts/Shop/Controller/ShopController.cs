using System;
using FrameWork.JianChen.Core;

public class ShopController : Controller
{

    public ShopView View;

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
