using System;
using FrameWork.JianChen.Core;
using Assets.Scripts.Common;
using FrameWork.JianChen.Interfaces;

public class DialogPanel : ReturnablePanel
{
    DialogController _dialogmoduleController;
    public int MissionId;
    public NPCData NpcData;
    public int DialogState = 0;

    public override void Init(IModule module)
    {
        base.Init(module);
        var viewScript = (DialogView)InstantiateView<DialogView>("Dialog/Prefabs/DialogView");
        _dialogmoduleController = new DialogController();
        _dialogmoduleController.CurMissionId = MissionId;
        _dialogmoduleController.CurNpcData = NpcData;
        _dialogmoduleController.CurDialogstate = DialogState;
        _dialogmoduleController.View = viewScript;
        RegisterView(viewScript);
        RegisterController(_dialogmoduleController);
        _dialogmoduleController.Start();
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
