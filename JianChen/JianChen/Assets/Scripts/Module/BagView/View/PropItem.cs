using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Framework.JianChen.Service;
using Common;
using DataModel;
using FrameWork.JianChen.Core.Event;
using Module;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PropItem : MonoBehaviour,IPointerClickHandler
{
    private RawImage _propIcon;
    private Text _numTxt;
    private Transform _iconTran;
    public UserGrid PropGrid;
    private PropIconMono _propIconMono;
    
    void Awake()
    {
        _iconTran = transform.Find("IconBg");
        _propIcon = transform.GetRawImage("IconBg/Prop");
        _propIconMono = _propIcon.GetComponent<PropIconMono>();
        _numTxt = transform.GetText("IconBg/Num");

    }

    public void SetData(UserGrid grid)
    {
        PropGrid = grid;
        _iconTran.gameObject.SetActive(grid.GridPropId != 0);
        if (grid.GridPropId != 0)
        {
            if (grid.GripType == GripType.Equip)
            {
                //要获取Grid所携带的PropEntity信息，暂时先用遍历的办法，之后要换另一种数据结构！尽量用map！
                var userEquip = GlobalData.PropModel.GetUserEquipData(grid.GridPropId);
                var equipBase = GlobalData.PropModel.GetEquipBaseData()[userEquip.EquipBaseId];
                _numTxt.gameObject.SetActive(grid.PropNum>0);
                _numTxt.text = grid.PropNum + "";

                _propIcon.texture = ResourceManager.Load<Texture>("Props/Equip/"+equipBase.EquipIcon);
                _propIconMono.SetData(grid,transform.parent.transform.parent.transform.parent);

            }
            
            
        }


    }


    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.LogError("ClickItem"+PropGrid.GridId);
        EventDispatcher.TriggerEvent(EventConst.ChooseBagItem,PropGrid);
    }
}
