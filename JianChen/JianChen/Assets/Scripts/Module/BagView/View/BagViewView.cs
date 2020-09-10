using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using DataModel;
using FrameWork.JianChen.Core;
using UnityEngine.EventSystems;


namespace game.main
{
    public class BagViewView : Window
    {
        private Text m_Text = null;
        private Button m_CloseBtn = null;
        private LoopVerticalScrollRect _BagScrollRect;
        private Text _GoldNum;

        private List<UserGrid> _userGrids;
        private Transform _proInfoView;
        private Text _propDesc;
        private Button _applyPropBtn;
        private Button _strengthenBtn;

        private UserGrid _curGrid;

        void Awake()
        {
            m_Text = transform.Find("BagPanel/GoldBtm/Text").GetComponent<Text>();
            m_CloseBtn = transform.Find("BagPanel/CloseBtn").GetComponent<Button>();
            _proInfoView = transform.Find("BagPanel/PropInfo");
            _propDesc = transform.Find("BagPanel/PropInfo/Desc").GetText();
            _applyPropBtn = transform.Find("BagPanel/PropInfo/Apply").GetButton();
            _strengthenBtn = transform.Find("BagPanel/PropInfo/Better").GetButton();
            _applyPropBtn.onClick.AddListener(ApplyPropOnClick);

            _GoldNum = transform.Find("BagPanel/GoldBtm/Text").GetText();
            
            _BagScrollRect = transform.Find("BagPanel/BagList/BagContent").GetComponent<LoopVerticalScrollRect>();
            _BagScrollRect.poolSize = 6;
            _BagScrollRect.prefabName = "BagView/Prefabs/PropItem";
            _BagScrollRect.UpdateCallback = UpdatePropList;

        }

        private void ApplyPropOnClick()
        {
            //将当前道具的HasWear设置为1！
            if (_curGrid != null && _curGrid.GridPropId != 0)
            {
                SendMessage(new Message(MessageConst.CMD_BAGVIEW_APPLYPROP,Message.MessageReciverType.CONTROLLER,_curGrid));
            }
            

            
        }

        public void SetChoosePropInfo(UserGrid chooseGrid)
        {
            _proInfoView.gameObject.SetActive(true);
            _curGrid = chooseGrid;
            var userEquip = GlobalData.PropModel.GetUserEquipData(chooseGrid.GridPropId);
            var equipBase = GlobalData.PropModel.GetEquipBaseData()[userEquip.EquipBaseId];
            _propDesc.text = equipBase.EquipDescription;


        }
        
        
        private void UpdatePropList(GameObject gameobj, int index)
        {

            if (index<_userGrids.Count&&index>=0)
            {
                gameobj.GetComponent<PropItem>().SetData(_userGrids[index]);
            }
            else
            {
                Debug.LogError(index); 
            }


        }

        void Start()
        {
            InitUIEvent();
        }

        private void InitUIEvent()
        {
            m_CloseBtn.onClick.AddListener(OnCloseBtnClick);
        }

        public void SetData(List<UserGrid> grids)
        {
            //暂时先展示装备的格子！
            SetGoldNum(GlobalData.PlayerData.PlayerVo.Gold);
            _userGrids = grids;
            _BagScrollRect.RefillCells();
            _BagScrollRect.totalCount = grids.Count;

            _BagScrollRect.RefreshCells();            

        }

        public void SetGoldNum(int goldNum)
        {
            _GoldNum.text = goldNum + "";
        }

        private void OnCloseBtnClick()
        {
            SendMessage(new Message(MessageConst.CMD_BAGIVEW_CLOSE,Message.MessageReciverType.DEFAULT));
        }
        

        
        protected override void OpenAnimation()
        {
        }


        protected override void AddBgMask()
        {
        }


    }

}

