using FrameWork.JianChen.Core;
using game.main;
using UnityEngine;
using UnityEngine.UI;

public class LoginView : View
{
	private Transform _loginPanel;
	private InputField _acountinput;
	private InputField _passwordinput;
	private Button _login;
	private Button _register;

	private Transform _characterCreation;
	private InputField _enterGameName;
	private Button _loadGameBtn;
	private Button _closeCreatBtn;
	
	private void Awake()
	{
		_loginPanel = transform.Find("LoginPanel");
		_acountinput = transform.Find("LoginPanel/Account").GetComponent<InputField>();
		_passwordinput = transform.Find("LoginPanel/Password").GetComponent<InputField>();
//		_login = transform.Find("LoginPanel/Login").GetComponent<Button>();
//		_register = transform.Find("LoginPanel/Register").GetComponent<Button>();
		
		_login = transform.Find("LoginRoot/LoadGame").GetComponent<Button>();
		_register = transform.Find("LoginRoot/NewGame").GetComponent<Button>();

		_characterCreation = transform.Find("LoginRoot/CreatCharacterName");
		_enterGameName = transform.Find("LoginRoot/CreatCharacterName/EnterGame/NameBtm/InputField").GetComponent<InputField>();
		_loadGameBtn = _characterCreation.GetButton("OkBtn");
		_closeCreatBtn = _characterCreation.GetButton("CloseBtn");
		
		_loadGameBtn.onClick.AddListener(OnNewGame);
		
		DefaultAccount();
		_login.onClick.AddListener(OnLoadClick);
		_register.onClick.AddListener(() =>
		{
			_characterCreation.gameObject.SetActive(true);
		});
		
		_closeCreatBtn.onClick.AddListener(() =>
		{
			_characterCreation.gameObject.SetActive(false);
		});
		
			
	}

	
	private void OnNewGame()
	{
		SendMessage(new Message(MessageConst.CMD_NEWGAMECREATER, Message.MessageReciverType.CONTROLLER,_enterGameName.text));
		
	}

	private void OnLoadClick()
	{
				SendMessage(new Message(MessageConst.CMD_LOGIN_DO_LOGIN, Message.MessageReciverType.CONTROLLER,
			"danboard", "danboard"));
	}

	public void DefaultAccount()
	{
		_acountinput.text = PlayerPrefs.GetString("username", "");
		_passwordinput.text = PlayerPrefs.GetString("password", "");
	}
	
	private void OnRegister()
	{
		SendMessage(new Message(MessageConst.CMD_REGISTER_OPEN, Message.MessageReciverType.CONTROLLER));
	}

	
	//	private void OnLoginClick()
//	{
//		if (_acountinput.text == "" || _passwordinput.text == "")
//		{
//			Debug.LogError("用户名密码不能为空！");
//			return;
//		}
//
//		SendMessage(new Message(MessageConst.CMD_LOGIN_DO_LOGIN, Message.MessageReciverType.CONTROLLER,
//			_acountinput.text, _passwordinput.text));
//	}
}


