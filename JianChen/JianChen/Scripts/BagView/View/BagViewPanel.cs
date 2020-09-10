using System;
using FrameWork.JianChen.Core;
using Assets.Scripts.Common;
using FrameWork.JianChen.Interfaces;

public class BagViewPanel : ReturnablePanel
{
    BagViewController _bagviewmoduleController;

    public override void Init(IModule module)
    {
        base.Init(module);
        var viewScript = (BagViewView)InstantiateView<BagViewView>("BagView/Prefabs/BagViewView");
        _bagviewmoduleController = new BagViewController();
        _bagviewmoduleController.View = viewScript;
        RegisterView(viewScript);
        RegisterController(_bagviewmoduleController);
        _bagviewmoduleController.Start();
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
