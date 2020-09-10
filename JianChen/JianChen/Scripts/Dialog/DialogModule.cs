using System;
using FrameWork.JianChen.Core;

public class DialogModule : ModuleBase
{
    DialogPanel _dialogpanel;

    public override void Init()
    {
        _dialogpanel = new DialogPanel();
        _dialogpanel.Init(this);
        _dialogpanel.Show(0);
    }

    public override void OnShow(float delay)
    {
        base.OnShow(delay);
        _dialogpanel.Show(0);
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
