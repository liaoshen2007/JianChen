using System;
using Common;
using FrameWork.JianChen.Core.Event;
using game.main;
using UnityEngine;
using UnityEngine.UI;

public class RegisterWindow : Window {

	private InputField _userNameInput;
	private InputField _passwordInput;
	private InputField _ensurePassword;
	private InputField _email;

	private void Awake()
	{
		_userNameInput = transform.Find("LoginPanel/Account").GetComponent<InputField>();
		_passwordInput = transform.Find("LoginPanel/Password").GetComponent<InputField>();
		_ensurePassword = transform.Find("LoginPanel/EnsurePassword").GetComponent<InputField>();
		_email = transform.Find("LoginPanel/Email").GetComponent<InputField>();
		Button okBtn = transform.Find("LoginPanel/Register").GetComponent<Button>();
		okBtn.onClick.AddListener(OnOkBtnClick);
	}

	private void OnOkBtnClick()
	{
		if (String.IsNullOrEmpty(_userNameInput.text))
		{
			Debug.LogError("用户名不为空");
			return;
		}
		
		if (_passwordInput.text.Length < 6)
		{
			Debug.LogError("密码长度不小于6位");
			return;
		}

		if (_passwordInput.text!=_ensurePassword.text)
		{
			Debug.LogError("重复密码错误");
			return;
		}
		
		EventDispatcher.TriggerEvent(EventConst.Register,_userNameInput.text, _passwordInput.text,_email.text);
		//Close();
	}

	public override void Close()
	{
		base.Close();
		
	}
}
