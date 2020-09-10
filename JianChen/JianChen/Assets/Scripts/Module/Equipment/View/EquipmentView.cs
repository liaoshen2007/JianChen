using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.Scripts.Framework.JianChen.Service;
using DataModel;
using FrameWork.JianChen.Core;
using Module;

namespace game.main
{
    public class EquipmentView : Window
    {
        private Button m_CloseBtn = null;
        private Transform m_EquipItemList = null;

        void Awake()
        {
            m_CloseBtn = transform.Find("EquipPanel/CloseBtn").GetComponent<Button>();
            m_EquipItemList = transform.Find("EquipPanel/EquipItemList").GetComponent<Transform>();
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
            SendMessage(new Message(MessageConst.CMD_EQUIMENT_CLOSE,Message.MessageReciverType.DEFAULT));
        }

        public void SetData(List<UserEquipData> vos)
        {
            //先全部隐藏第二个节点！
            //先遍历一遍显示有该装备的IconSlot;
            for (int i = 0; i < m_EquipItemList.childCount; i++)
            {
                var equip = m_EquipItemList.GetChild(i).Find("Icon");
                equip.gameObject.SetActive(false);
            }

            foreach (var v in vos)
            {
                var equipBase = GlobalData.PropModel.GetEquipBaseData()[v.EquipBaseId];
                SetEquipData(equipBase);
            }
            
            
            
            
            
        }

        public void SetEquipData(EquipBaseData data)
        {
            RawImage icon = null;
            switch (data.EquipType)
            {
                case EquipType.Head:
                    icon= m_EquipItemList.Find("Headgear/Icon").GetRawImage();
                    icon.gameObject.SetActive(true);
                    break;
                case EquipType.Weapon:
                    icon = m_EquipItemList.Find("RightHand/Icon").GetRawImage();
                    icon.gameObject.SetActive(true);
                    break;
                case EquipType.Cloth:
                    icon = m_EquipItemList.Find("Armor/Icon").GetRawImage();
                    icon.gameObject.SetActive(true);
                    break;
                case EquipType.Shoe:
                    icon = m_EquipItemList.Find("Shoe/Icon").GetRawImage();
                    icon.gameObject.SetActive(true);
                    break;
            }

            if (icon!=null)
            {
                icon.texture=ResourceManager.Load<Texture>("Props/Equip/"+data.EquipIcon);
            }

            
        }
        
    
        protected override void OpenAnimation()
        {
        }


        protected override void AddBgMask()
        {
        }
    }

}

