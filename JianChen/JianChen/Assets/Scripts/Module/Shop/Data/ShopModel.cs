using System.Collections.Generic;
using FrameWork.JianChen.Core;
using UnityEngine;

namespace DataModel
{
    public class ShopModel : Model
    {
        private Dictionary<int, ShopBaseData> _shopBaseDatas;

        public ShopModel()
        {
            _shopBaseDatas=new Dictionary<int, ShopBaseData>();
        }

        public void SetShopMallDic(List<ShopBaseData> datas)
        {
            if (datas == null)
            {
                Debug.LogError("datas is null");
                return;
            }

            foreach (var v in datas)
            {
                //Debug.LogError(v.MallName);
                _shopBaseDatas.Add(v.MallId,v);
            }
            
            
        }

        public List<ShopBaseData> GetTargetShopItemList(MallType type)
        {
            List<ShopBaseData> tagetList=new List<ShopBaseData>();
            foreach (var v in _shopBaseDatas)
            {
                if (v.Value.MallType == type)
                {
                    tagetList.Add(v.Value);
                }
            }

            return tagetList;


        }



    }
    
    
}