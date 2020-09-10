using System.Collections.Generic;
using Common;
using DataModel;
using FrameWork.JianChen.Core;
using FrameWork.JianChen.Core.Event;
using game.main;

public class BagViewController : Controller
{

    public BagViewView View;
    
    //这些等会都要放到model上去！


    public override void Start()
    {      
        EventDispatcher.AddEventListener(EventConst.UpdateGrid,UpdateGrid);
        EventDispatcher.AddEventListener<int,int>(EventConst.SwapBagItem,SwapBagItem);
        EventDispatcher.AddEventListener<UserGrid>(EventConst.ChooseBagItem,ChooseBagItem);
        View.SetData(GlobalData.PropModel.EquipGrids);
    }

    private void ChooseBagItem(UserGrid userGrid)
    {
        View.SetChoosePropInfo(userGrid);
    }

    private void SwapBagItem(int originalGridId,int targetGridId)
    {
        GlobalData.PropModel.SwapBagItem(originalGridId,targetGridId);
        //这里刷新一次就可以了，不需要Trigger!！
        
        View.SetData(GlobalData.PropModel.EquipGrids);
    }

    private void UpdateGrid()
    {
        View.SetData(GlobalData.PropModel.EquipGrids);
        
    }


    public override void OnMessage(Message message)
    {
        string name = message.Name;
        object[] body = message.Params;
        switch (name)
        {
            case MessageConst.CMD_BAGVIEW_APPLYPROP:
                UserGrid grid = (UserGrid)body[0];
                GlobalData.PropModel.ApplyOneEquip(grid);
                
                break;
        }
    }

    public override void Destroy()
    {
        base.Destroy();
        EventDispatcher.RemoveEventListener(EventConst.UpdateGrid,UpdateGrid);
        EventDispatcher.RemoveEventListener<int,int>(EventConst.SwapBagItem,SwapBagItem);
        EventDispatcher.RemoveEventListener<UserGrid>(EventConst.ChooseBagItem,ChooseBagItem);
    }
}
