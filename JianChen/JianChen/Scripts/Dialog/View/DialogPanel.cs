using System;
using FrameWork.JianChen.Core;
using Assets.Scripts.Common;
using FrameWork.JianChen.Interfaces;

public class DialogPanel : ReturnablePanel
{
    DialogController _dialogmoduleController;

    public override void Init(IModule module)
    {
        base.Init(module);
        var viewScript = (DialogView)InstantiateView<DialogView>("ModuleName/Prefabs/DialogView");
        _dialogmoduleController = new DialogController();
        _dialogmoduleController.View = viewScript;
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
