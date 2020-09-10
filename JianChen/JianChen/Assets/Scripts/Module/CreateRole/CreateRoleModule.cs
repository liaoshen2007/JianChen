using FrameWork.JianChen.Core;

public class CreateRoleModule : ModuleBase
{
    private CreateRolePanel _createRolePanel;
    
    public override void Init()
    {
        _createRolePanel=new CreateRolePanel();
        _createRolePanel.Init(this);
        _createRolePanel.Show(0);
        
    }

    public override void OnMessage(Message message)
    {
        string name = message.Name;
        object[] body = message.Params;
        switch (name)
        {
        }
    }

    public void Destroy()
    {
        _createRolePanel.Destroy();
        
    }
}