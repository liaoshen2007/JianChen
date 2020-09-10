using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FrameWork.JianChen.Core;

namespace game.main
{
    public class SkillView : Window
    {
        private Button m_CloseBtn = null;
        private LoopVerticalScrollRect _skillVerticalScrollRect;
        private List<SkillBaseData> _skillBaseDatas;

        void Awake()
        {
            m_CloseBtn = transform.Find("SkillPanel/CloseBtn").GetComponent<Button>();
            _skillVerticalScrollRect = transform.Find("SkillPanel/SkillList/SkillContent").GetComponent<LoopVerticalScrollRect>();
            _skillVerticalScrollRect.poolSize = 6;
            _skillVerticalScrollRect.prefabName = "Skill/Prefabs/SkillItem";
            _skillVerticalScrollRect.UpdateCallback = SkillDataListUpCallBack;

        }

        private void SkillDataListUpCallBack(GameObject gameobj, int index)
        {
            gameobj.GetComponent<SkillItem>().SetData(_skillBaseDatas[index]);
            
        }

        void Start()
        {
            InitUIEvent();
        }

        private void InitUIEvent()
        {
            m_CloseBtn.onClick.AddListener(OnCloseBtnClick);
        }

        public void SetData(List<SkillBaseData> vos)
        {
            _skillBaseDatas = vos;
            _skillVerticalScrollRect.RefillCells();
            _skillVerticalScrollRect.totalCount = vos.Count;
            _skillVerticalScrollRect.RefreshCells();

        }
        

        private void OnCloseBtnClick()
        {
            SendMessage(new Message(MessageConst.CMD_SKILL_CLOSE,Message.MessageReciverType.DEFAULT));
        }
        
        protected override void OpenAnimation()
        {
        }


        protected override void AddBgMask()
        {
        }
    } 

}

