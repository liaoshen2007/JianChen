using FrameWork.JianChen.Core;
using FrameWork.JianChen.Interfaces;
using game.main;

public class BagViewPanel : Panel
{
    BagViewController _bagviewmoduleController;

    public override void Init(IModule module)
    {
        base.Init(module);
        var viewScript = InstantiateWindow<BagViewView>("BagView/Prefabs/BagView");
        _bagviewmoduleController = new BagViewController();
        _bagviewmoduleController.View = viewScript;
        //RegisterView(viewScript);
        RegisterController(_bagviewmoduleController);
        _bagviewmoduleController.Start();
    }

    public override void Show(float delay)
    {
        base.Show(delay);
        //Main.ChangeMenu(MainUIDisplayState.ShowAll);
        //ShowBackBtn();
    }

    public override void Hide()
    {
        base.Hide();
    }
}
