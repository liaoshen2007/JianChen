using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using FrameWork.JianChen.Core;

namespace game.main
{
    public class ShopView : Window
    {
        private Button m_CloseBtn = null;
        private LoopVerticalScrollRect _shopVerticalScrollRect;
        private List<ShopBaseData> _shopBaseList;

        void Awake()
        {
            m_CloseBtn = transform.Find("ShopPanel/CloseBtn").GetComponent<Button>();
            _shopVerticalScrollRect = transform.Find("ShopPanel/GoodsList/GoosdContent")
                .GetComponent<LoopVerticalScrollRect>();
            _shopVerticalScrollRect.poolSize = 6;
            _shopVerticalScrollRect.prefabName = "Shop/Prefabs/ShopItem";
            _shopVerticalScrollRect.UpdateCallback = ShopListUpDateCallBack;

        }

        private void ShopListUpDateCallBack(GameObject gameobj, int index)
        {
            gameobj.GetComponent<ShopItem>().SetData(_shopBaseList[index]);
        }

        public void SetData(List<ShopBaseData> vos)
        {
            _shopBaseList = vos;
            _shopVerticalScrollRect.RefillCells();
            _shopVerticalScrollRect.totalCount = vos.Count;
            _shopVerticalScrollRect.RefreshCells();


        }

        void Start()
        {
            InitUIEvent();
        }

        private void InitUIEvent()
        {
            m_CloseBtn.onClick.AddListener(OnCloseBtnClick);
        }

        private void OnCloseBtnClick()
        {
            SendMessage(new Message(MessageConst.CMD_SHOP_CLOSE, Message.MessageReciverType.DEFAULT));
        }
        
        protected override void OpenAnimation()
        {
        }


        protected override void AddBgMask()
        {
        }
    }
}
