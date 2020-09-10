using FrameWork.JianChen.Core;
using FrameWork.JianChen.Interfaces;

public class LoginPanel : Panel
{

    private LoginController _loginController;
    private LoadDataController loadDataControl;
    
    public override void Init(IModule module)
    {
        base.Init(module);
        var loginview=(LoginView)InstantiateView<LoginView>("Login/Prefabs/LoginModule");
        _loginController=new LoginController();
        _loginController._LoginView = loginview;
        
        loadDataControl = new LoadDataController();
        RegisterController(loadDataControl);
        
        RegisterView(loginview);
        RegisterController(_loginController);
        _loginController.Start();
    }

    public override void Hide()
    {
        ClientTimer.Instance.DelayCall(Destroy, 0.4f);
    }

    public override void Show(float delay=0.1f)
    {
    }
}