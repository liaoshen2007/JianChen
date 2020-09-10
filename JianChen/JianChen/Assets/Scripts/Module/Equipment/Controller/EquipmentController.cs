using System;
using Common;
using DataModel;
using FrameWork.JianChen.Core;
using FrameWork.JianChen.Core.Event;
using game.main;

public class EquipmentController : Controller
{

    public EquipmentView View;

    public override void Start()
    {
        EventDispatcher.AddEventListener(EventConst.UpdateEquipmentView,UpdateEquipView);
        View.SetData(GlobalData.PropModel.HasWearEquipDatas);
    }

    private void UpdateEquipView()
    {
        //刷新装备栏！希望一切OK！
        View.SetData(GlobalData.PropModel.HasWearEquipDatas);
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
        EventDispatcher.RemoveEventListener(EventConst.UpdateEquipmentView,UpdateEquipView);
        base.Destroy();
    }
}
