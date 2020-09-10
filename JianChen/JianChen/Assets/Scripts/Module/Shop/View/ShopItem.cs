using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Framework.JianChen.Service;
using Common;
using DataModel;
using FrameWork.JianChen.Core.Event;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    private RawImage _mallIcon;
    private Text _mallName;
    private Text _mallInfo;
    private Button _buyMall;
    private ShopBaseData _shopBaseData;

    private void Awake()
    {
        _mallIcon = transform.GetRawImage("Frame/GoodsItem");
        _mallName = transform.GetText("GoodsNameBtm/Text");
        _mallInfo = transform.GetText("GoodsInfoBtm/Text");
        _buyMall = transform.GetButton("BuyBtn");
        _buyMall.onClick.AddListener(OnBuyClick);
    }

    public void SetData(ShopBaseData data)
    {
        _shopBaseData = data;
        _mallName.text = _shopBaseData.MallName;
        switch (_shopBaseData.MallType)
        {
            case MallType.Equip:
                var equipBase = GlobalData.PropModel.GetEquipBaseData()[_shopBaseData.ItemId];
                _mallIcon.texture=ResourceManager.Load<Texture>("Props/Equip/"+equipBase.EquipIcon);
                _mallInfo.text = $"   {equipBase.EquipType.ToString()}		 {_shopBaseData.Price}";
                break;
            case MallType.Consume:
                break;
            
        }
        


    }
    
    //todo 之后不可以购买有道具上限的装备！
    private void OnBuyClick()
    {
        Debug.Log("BuyItem"+_shopBaseData.ItemId);
        EventDispatcher.TriggerEvent<ShopBaseData>(EventConst.BuyItem,_shopBaseData);
    }



    
}
