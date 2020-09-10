using System;
using Common;
using DataModel;
using FrameWork.JianChen.Core;
using FrameWork.JianChen.Core.Event;
using game.main;
using UnityEngine;

public class ShopController : Controller
{

    public ShopView View;

    public override void Start()
    {
        EventDispatcher.AddEventListener<ShopBaseData>(EventConst.BuyItem,BuyItem);
        View.SetData(GlobalData.ShopModel.GetTargetShopItemList(MallType.Equip));
    }

    private void BuyItem(ShopBaseData data)
    {
        //买了道具后要记得刷新扣掉费用的金币和刷新道具
        switch (data.MallType)
        {
            case MallType.Equip:
                var newEquipMallItem=new UserEquipData()
                {
                    EquipEntityId=GlobalData.PropModel.GetEquipKey(),
                    EquipBaseId = data.ItemId,
                    HasWear = 0,
                    ExtraAtk = 0,
                    ExtraDef = 0,
                    ExtraHp = 0,
                    ExtraMP = 0,
                
                    //同时要触发背包格子的数据改变！
                    ExtraCriRate = 0,
                    ExtraHitRate = 0,
                    ExtraMoveSpd = 0,
                    ExtraAtkSpeed = 0,
                    ExtraAtkRange = 0,
                    LevelUpTimes = 7,
                    HasStrengthenTimes = 0,
                    
                    
                };
                
                Debug.Log("Add new prop!"+newEquipMallItem.EquipBaseId);
                GlobalData.PropModel.UpdateUserEquipdata(newEquipMallItem);
                
                
                break;
            case MallType.Consume:
                break;
        }
        
        

        
        
    }

    public override void OnMessage(Message message)
    {
        string name = message.Name;
        object[] body = message.Params;
        switch (name)
        {

        }
    }

    public override void Destroy()
    {
        base.Destroy();
    }
}
