using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace game.main
{
    public class AwardWindow : Window
    {
        private Transform _contentRoot;
        private Text _title;
        private Button _okBtn;

        private List<AwardData> _awardDatas;
        
        private void Awake()
        {
            _title = transform.GetText("AwardWindowBG/Title/Text");
            _contentRoot = transform.Find("AwardWindowBG/AwardList/Content");
            _okBtn = transform.GetButton("AwardWindowBG/OKBtn");

            _okBtn.onClick.AddListener(() =>
            {
                this.Close();
            });

        }

        public void SetData(List<AwardData> awardList,string title="奖励")
        {
            _title.text = title;

            _awardDatas = awardList;

            for (int i = 0; i < awardList.Count; i++)
            {
                var go = InstantiatePrefab("GameMain/Prefabs/AwardItem");   
                go.transform.SetParent(_contentRoot,false);
                
                UpdateAwardItem(go,i);
            }



        }

        private void UpdateAwardItem(GameObject go, int index)
        {
            
            go.GetComponent<AwardItem>().SetData(_awardDatas[index]);
            
            //todo 之后可以做点击Icon弹出物品描述
            
        }
        
        
        
    }
    
    
}