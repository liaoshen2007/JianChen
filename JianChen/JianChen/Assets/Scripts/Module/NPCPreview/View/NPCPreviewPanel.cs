using Assets.Scripts.Common;
using Common;
using FrameWork.JianChen.Core.Event;
using FrameWork.JianChen.Interfaces;

public class NPCPreviewPanel : ReturnablePanel
{
    NPCPreviewController _npcpreviewmoduleController;
    private NPCData _npcData;

    public override void Init(IModule module)
    {
        base.Init(module);
        var viewScript = (NPCPreviewView)InstantiateView<NPCPreviewView>("NPCPreview/Prefabs/NPCPreview");
        _npcpreviewmoduleController = new NPCPreviewController();
        _npcpreviewmoduleController.View = viewScript;
        _npcpreviewmoduleController.NpcData = _npcData;
        RegisterView(viewScript);
        RegisterController(_npcpreviewmoduleController);
        _npcpreviewmoduleController.Start();
    }

    public void SetData(NPCData npcData)
    {
        _npcData= npcData;

    }
    
    public override void Show(float delay)
    {
        base.Show(delay);
        Main.ChangeMenu(MainUIDisplayState.ShowTopBar);
        ShowBackBtn();
    }

    public override void OnBackClick()
    {
        EventDispatcher.TriggerEvent(EventConst.SetCameState,FollowMainRole.CamState.FollowMainRole);
        base.OnBackClick();
    }

    public override void Hide()
    {
        base.Hide();
    }
}
