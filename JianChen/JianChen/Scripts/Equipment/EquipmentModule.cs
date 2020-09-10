using System;
using FrameWork.JianChen.Core;

public class EquipmentModule : ModuleBase
{
    EquipmentPanel _equipmentpanel;

    public override void Init()
    {
        _equipmentpanel = new EquipmentPanel();
        _equipmentpanel.Init(this);
        _equipmentpanel.Show(0);
    }

    public override void OnShow(float delay)
    {
        base.OnShow(delay);
        _equipmentpanel.Show(0);
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
