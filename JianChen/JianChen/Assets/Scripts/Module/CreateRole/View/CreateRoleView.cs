using FrameWork.JianChen.Core;
using game.main;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CreateRoleView : View
{
	private InputField _rolename;
	private Dropdown _occupation;
	private ToggleGroup _sexual;
	private Dropdown _equip;
	private Button _button;
	private Toggle _male;
	private Toggle _female;

	private int _sexualSelect=0;
	 
	
	
	private void Awake()
	{
		_rolename = transform.Find("CreatPanel/PlayerName").GetComponent<InputField>();
		_occupation=transform.Find("CreatPanel/Occupation").GetComponent<Dropdown>();
		_sexual=transform.Find("CreatPanel/Sexual").GetComponent<ToggleGroup>();
		for (int i = 0; i < _sexual.transform.childCount; i++)
		{
			Toggle toggle = _sexual.transform.GetChild(i).GetComponent<Toggle>();
			toggle.onValueChanged.AddListener(SelectSexual);

		}
		
		_equip=transform.Find("CreatPanel/Equip").GetComponent<Dropdown>();
		_button = transform.Find("CreatPanel/Create").GetComponent<Button>();
		
        _button.onClick.AddListener(CreateRole);
		
	}

	private void SelectSexual(bool isOn)
	{
		if (isOn==false)
		{
			return;
		}

		string name = EventSystem.current.currentSelectedGameObject.name;
		switch (name)
		{
			case	"Male":
				_sexualSelect = 1;
				break;
			case 	"Female":
				_sexualSelect = 0;
				break;
			
		}
        Debug.LogError(name);

	}

	//点击创建角色
	private void CreateRole()
	{
		//以后可以正则表达式判断非法。现在可以只是判空，或者判断是否重名。这些可能要服务器来判断！
				
		Debug.LogError(_rolename.text);
		SendMessage(new Message(MessageConst.CMD_CREATEROLE,Message.MessageReciverType.CONTROLLER,new CreatRoleVo
		{
			RoleName = _rolename.text,
			Equip = _equip.value,
			Occupation = _occupation.value,
			Sexual = _sexualSelect
		}));
	}
}


