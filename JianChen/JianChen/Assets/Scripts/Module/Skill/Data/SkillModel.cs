using System.Collections.Generic;
using Common;
using FrameWork.JianChen.Core;
using FrameWork.JianChen.Core.Event;
using UnityEngine;

namespace DataModel
{
    public class SkillModel : Model
    {

        public Dictionary<int, SkillBaseData> SkillBaseDataDic;

        public SkillModel()
        {
            SkillBaseDataDic=new Dictionary<int, SkillBaseData>();
        }

        public void InitSkillBaseData(List<SkillBaseData> data)
        {
            if (data == null)
            {
                Debug.LogError("datas is null");
                return;
            }

            foreach (var v in data)
            {
                //Debug.LogError(v.SkillName);
                SkillBaseDataDic.Add(v.SkillId,v);
            }
            
            
        }

        public List<SkillBaseData> GetTargetSkillListItem(Occupation occupation)
        {
            var targetSkillListItem = new List<SkillBaseData>();
            foreach (var v in SkillBaseDataDic)
            {
                if (v.Value.Occupation == occupation)
                {
                    targetSkillListItem.Add(v.Value);
                }
                
            }

            return targetSkillListItem;

        }

        public void SetSkillKeyMap(int skillIdx,string skillKeyMap)
        {
            if (GlobalData.PlayerData.PlayerVo.UserSkillDatas==null)
            {
                Debug.LogError("Error the data!");
                return;
            }
            
            //先清空掉之前的Icon数据
            foreach (var v in GlobalData.PlayerData.PlayerVo.UserSkillDatas)
            {
                if (v.SkillKeyPos == skillKeyMap)
                {
                    v.SkillKeyPos = "";
                }
                
                if (v.SkillId == skillIdx)
                {
                    v.SkillKeyPos = skillKeyMap;
                }
            }
            
            //再赋值最新的KeyMap数据
//            foreach (var v in GlobalData.PlayerData.PlayerVo.UserSkillDatas)
//            {
//                if (v.SkillId == skillIdx)
//                {
//                    v.SkillKeyPos = skillKeyMap;
//                }
//            }
            
            
            
            
            
        }
        

        public int GetSkillKey()
        {
            int idx= 1;
            
            //找一个10000之内不存在的数字！
            for (int i = 1; i < 1000; i++)
            {
                if (!SkillBaseDataDic.ContainsKey(i))
                {
                    idx = i;
                    break;
                }
            }

            return idx;

        }
		
		

    }



}


