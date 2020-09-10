using System;
using FrameWork.JianChen.Core;
using Assets.Scripts.Common;
using FrameWork.JianChen.Interfaces;

public class EquipmentPanel : ReturnablePanel
{
    EquipmentController _equipmentmoduleController;

    public override void Init(IModule module)
    {
        base.Init(module);
        var viewScript = (EquipmentView)InstantiateView<EquipmentView>("Equipment/Prefabs/EquipmentView");
        _equipmentmoduleController = new EquipmentController();
        _equipmentmoduleController.View = viewScript;
        RegisterView(viewScript);
        RegisterController(_equipmentmoduleController);
        _equipmentmoduleController.Start();
    }

    public override void Show(float delay)
    {
        base.Show(delay);
        Main.ChangeMenu(MainUIDisplayState.ShowTopBar);
        ShowBackBtn();
    }

    public override void Hide()
    {
        base.Hide();
    }
}
