using System;
using FrameWork.JianChen.Core;

public class ShopModule : ModuleBase
{
    ShopPanel _shoppanel;

    public override void Init()
    {
        _shoppanel = new ShopPanel();
        _shoppanel.Init(this);
        _shoppanel.Show(0);
    }

    public override void OnShow(float delay)
    {
        base.OnShow(delay);
        _shoppanel.Show(0);
    }

    public override void OnMessage(Message message)
    {
        string name = message.Name;
        object[] body = message.Params;
        switch (name)
        {

        }
    }
}
