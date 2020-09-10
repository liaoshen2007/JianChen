using FrameWork.JianChen.Core;
using FrameWork.JianChen.Interfaces;

public class CreateRolePanel : Panel
{

    private CreateRoleController _createRoleController;
    
    public override void Init(IModule module)
    {
        base.Init(module);
        var createRoleView = (CreateRoleView)InstantiateView<CreateRoleView>("CreateRole/Prefabs/CreateRoleView");
        _createRoleController=new CreateRoleController();
        _createRoleController._CreateRoleView = createRoleView;
        RegisterView(createRoleView);
        RegisterController(_createRoleController);
        _createRoleController.Start();
    }

    public override void Hide()
    {
    }

    public override void Show(float delay)
    {
        
    }
}