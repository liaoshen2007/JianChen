using System;
using FrameWork.JianChen.Core;
using Module;

public class BagViewModule : ModuleBase
{
    BagViewPanel _bagviewpanel;

    public override void Init()
    {
        _bagviewpanel = new BagViewPanel();
        _bagviewpanel.Init(this);
        _bagviewpanel.Show(0);
    }

    public override void OnShow(float delay)
    {
        base.OnShow(delay);
        _bagviewpanel.Show(0);
    }

    public override void OnMessage(Message message)
    {
        string name = message.Name;
        object[] body = message.Params;
        switch (name)
        {
            case MessageConst.CMD_BAGIVEW_CLOSE:
                ModuleManager.Instance.GoBack();
                break;
            
        }
    }
}
