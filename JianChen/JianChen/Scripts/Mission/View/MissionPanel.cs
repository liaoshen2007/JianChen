using System;
using FrameWork.JianChen.Core;
using Assets.Scripts.Common;
using FrameWork.JianChen.Interfaces;

public class MissionPanel : ReturnablePanel
{
    MissionController _missionmoduleController;

    public override void Init(IModule module)
    {
        base.Init(module);
        var viewScript = (MissionView)InstantiateView<MissionView>("Mission/Prefabs/MissionView");
        _missionmoduleController = new MissionController();
        _missionmoduleController.View = viewScript;
        RegisterView(viewScript);
        RegisterController(_missionmoduleController);
        _missionmoduleController.Start();
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
