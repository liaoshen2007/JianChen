using System.Collections.Generic;
using Common;
using FrameWork.JianChen.Core;
using FrameWork.JianChen.Core.Event;
using UnityEngine;

namespace DataModel
{
    public class PropModel : Model
    {

        public List<UserGrid> EquipGrids;
        public List<UserGrid> ConsumeGrids;
        private List<UserEquipData> hasWearEquipDatas;

        public List<UserEquipData> HasWearEquipDatas//只显示可读数据！
        {
            get { return hasWearEquipDatas; }
            //set { hasWearEquipDatas = value; }
        }

        private Dictionary<int, EquipBaseData> _equipBaseData;
        private Dictionary<int, UserEquipData> _userEquipData;

        //这里可以拓展玩家数据相关的方法！
        public PropModel()
        {
            EquipGrids=new List<UserGrid>();
            ConsumeGrids=new List<UserGrid>();
            hasWearEquipDatas=new List<UserEquipData>();
            _equipBaseData=new Dictionary<int, EquipBaseData>();
            _userEquipData=new Dictionary<int, UserEquipData>();
        }

        public void SetUserPropData(List<UserGrid> allgrids)
        {
            EquipGrids.Clear();
            ConsumeGrids.Clear();
            hasWearEquipDatas.Clear();

            foreach (var v in allgrids)
            {
                if (v.GripType==GripType.Equip)
                {
                    EquipGrids.Add(v);
                }
                else if(v.GripType==GripType.Consume)
                {
                    ConsumeGrids.Add(v);
                }
                
            }

            //SetWearEquipData();
            SetUserEquipDic(GlobalData.PlayerData.PlayerVo.UserEquipDatas);
        }
        
        public void SetUserEquipDic(List<UserEquipData> datas)
        {
            if (datas==null)
            {
                Debug.LogError("UserEquipData is null");
                return;
            }
            
            foreach (var v in datas)
            {
                if (v.HasWear == 1)
                {
                    hasWearEquipDatas.Add(v);
                }
                
                if (!_userEquipData.ContainsKey(v.EquipEntityId))
                {
                    _userEquipData.Add(v.EquipEntityId,v);
                }
                else
                {
                    _userEquipData[v.EquipEntityId] = v;
                }
            }
        }

        public int GetEquipKey()
        {
            int idx= 1;
            
            //找一个10000之内不存在的数字！
            for (int i = 1; i < 10000; i++)
            {
                if (!_userEquipData.ContainsKey(i))
                {
                    idx = i;
                    break;
                }
            }

            return idx;

        }
        
        

        public void InitEquipBaseData(List<EquipBaseData> data)
        {
            _equipBaseData.Clear();
            foreach (var v in data)
            {
//                Debug.LogError(" "+v.EquipDescription);
                _equipBaseData.Add(v.EquipId,v);
            }
            
        }

        public Dictionary<int, EquipBaseData> GetEquipBaseData()
        {
            if (_equipBaseData.Count == 0)
            {
                Debug.LogError("why _equipBaseData 0?");
                ConfigDataManager.LoadPlayRuleDataById<EquipBaseData>("EquipBaseRule", data =>
                {
                    InitEquipBaseData(data);
            
            
                });
                return _equipBaseData;
            }
            else
            {
                return _equipBaseData;
            }
            
            

        }
        
        

        //todo 更新道具数据,一般是怪物爆道具的时候已经随机调节好属性了！并且重载一个输入List参数的Update函数
        public void UpdateUserEquipdata(UserEquipData userEquipData)
        {
            if (GlobalData.PlayerData.PlayerVo.UserEquipDatas != null)
            {
                //这个属于改造！
                bool hasContain = false;
                foreach (var v in GlobalData.PlayerData.PlayerVo.UserEquipDatas)
                {
                    if (v.EquipEntityId == userEquipData.EquipEntityId)
                    {
                        v.EquipBaseId = userEquipData.EquipBaseId;
                        v.ExtraAtk = userEquipData.ExtraAtk;
                        v.ExtraDef = userEquipData.ExtraDef;
                        v.ExtraHp = userEquipData.ExtraHp;
                        v.ExtraMP = userEquipData.ExtraMP;
                        
                        //同时要触发背包格子的数据改变！
                        v.GridPos = userEquipData.GridPos;
                        UpdateUserGrid(v.GridPos,v.EquipEntityId);
                        v.ExtraCriRate = userEquipData.ExtraCriRate;
                        v.ExtraHitRate = userEquipData.ExtraHitRate;
                        v.ExtraMoveSpd = userEquipData.ExtraMoveSpd;
                        v.ExtraAtkSpeed = userEquipData.ExtraAtkSpeed;
                        v.ExtraAtkRange = userEquipData.ExtraAtkRange;
                        v.LevelUpTimes = userEquipData.LevelUpTimes;
                        v.HasStrengthenTimes = userEquipData.HasStrengthenTimes;
                        hasContain = true;
                        break;
                    }
                }

                if (hasContain==false)
                {

                    var userEquipGrid = UseOneGrid(userEquipData);
                    GlobalData.PlayerData.PlayerVo.UserEquipDatas.Add(userEquipGrid);
                }
                
            }
            else
            {
                GlobalData.PlayerData.PlayerVo.UserEquipDatas=new List<UserEquipData>();
                var userEquipGrid = UseOneGrid(userEquipData);
                GlobalData.PlayerData.PlayerVo.UserEquipDatas.Add(userEquipGrid);
            }

            //背包数据链要跟着改变！
            SetUserPropData(GlobalData.PlayerData.PlayerVo.UserBagGrids);
            
            EventDispatcher.TriggerEvent(EventConst.UpdateGrid);
            
            
        }

        //改变格子位置！
        public void UpdateUserGrid(int gridid,int entityId,int propnum=0,GripType gripType=GripType.Equip)
        {
            foreach (var v in GlobalData.PlayerData.PlayerVo.UserBagGrids)
            {
                if (v.GripType == gripType)
                {
                    if (gridid==v.GridId)
                    {
                        v.GridPropId = entityId;
                        v.PropNum = propnum;
                    }
                }  
            }
            
        }

        
        //获得一个道具的时候一定要先调用这个函数
        public UserEquipData UseOneGrid(UserEquipData userEquipData,int propnum=0,GripType gripType=GripType.Equip)
        {
            UserEquipData equipData = userEquipData;
            foreach (var v in GlobalData.PlayerData.PlayerVo.UserBagGrids)
            {
                if (v.GripType == gripType)
                {
                    if (v.GridPropId == 0)
                    {
                        v.GridPropId = userEquipData.EquipEntityId;
                        v.PropNum = propnum;
                        equipData.GridPos = v.GridId;
                        break;
                    }
                }  
            }

            return equipData;
        }

        public UserEquipData GetUserEquipData(int propEntityId)
        {
            if (GlobalData.PlayerData.PlayerVo.UserEquipDatas==null)
            {
                Debug.LogError("Error id:"+propEntityId);
                return null;
            }
            
            foreach (var v in GlobalData.PlayerData.PlayerVo.UserEquipDatas)
            {
                if (v.EquipEntityId==propEntityId)
                {
                    return v;
                }
                
            }

            return null;
        }

        public void SwapBagItem(int originalGridId,int targetGridId,GripType gripType=GripType.Equip)
        {
      //      Debug.LogError(originalGridId+" GridId"+targetGridId);
            //这里执行完交换算法后还要触发一次背包列表刷新，我的天！
            UserGrid originalGrid = new UserGrid()
            {
                GridId = 0,
                GridPropId = 0,
                GripType = gripType,
                PropNum = 0
            };
            UserGrid targetGrid = new UserGrid()            
            {
                GridId = 0,
                GridPropId = 0,
                GripType = gripType,
                PropNum = 0
            };

            //先赋值
            foreach (var v in GlobalData.PlayerData.PlayerVo.UserBagGrids)
            {
                if (v.GripType==gripType)
                {
                    if (v.GridId==originalGridId)
                    {
                        originalGrid.GridId = v.GridId;
                        originalGrid.PropNum = v.PropNum;
                        originalGrid.GridPropId = v.GridPropId;

                    }
                    else if(v.GridId==targetGridId)
                    {
                        targetGrid.GridId = v.GridId;
                        targetGrid.PropNum = v.PropNum;
                        targetGrid.GridPropId = v.GridPropId;
                    }   
                }
            }

//            if (originalGrid == null || targetGrid == null)
//            {
//                Debug.LogError("GridError:"+originalGridId+" "+targetGridId);
//            }
//            
            //然后交换！
            foreach (var v in GlobalData.PlayerData.PlayerVo.UserBagGrids)
            {
                if (v.GripType==gripType)
                {
                    if (v.GridId==originalGridId)
                    {
   //                     Debug.LogError(v.GridPropId+"Change GridId"+targetGrid.GridPropId);
                        v.GridPropId = targetGrid.GridPropId;
                    }
                    else if(v.GridId==targetGridId)
                    {
//                        Debug.LogError(v.GridPropId+"Change GridId"+originalGrid.GridPropId);
                        v.GridPropId = originalGrid.GridPropId;

                    }   
                }
            }
            
            SetUserPropData(GlobalData.PlayerData.PlayerVo.UserBagGrids);
            
            
            

        }

        public void ApplyOneEquip(UserGrid grid)
        {
            //首先要把已经穿戴上的此类型的HasWeard都置为0；
            var targetEquip = GetUserEquipData(grid.GridPropId);
            var targetBaseEquip = _equipBaseData[targetEquip.EquipBaseId];
            foreach (var v in GlobalData.PlayerData.PlayerVo.UserEquipDatas)
            {
                //先把已经穿戴上的同类道具卸下来
                if (v.HasWear==1&&_equipBaseData[v.EquipBaseId].EquipType == targetBaseEquip.EquipType)
                {
                    v.HasWear = 0;
                }
                
                //然后把目标的Equip设置为haswear=1;
                if (v.EquipEntityId==targetEquip.EquipEntityId&&v.HasWear==0)
                {
                    v.HasWear = 1;
                }
                
            }
            
            //然后刷新一下EquipGrids,这个Grid的GridEntity为0
            foreach (var v in GlobalData.PlayerData.PlayerVo.UserBagGrids)
            {
                if (v.GridId == grid.GridId)
                {
                    v.GridPropId = 0;
                }
            }


            SetUserPropData(GlobalData.PlayerData.PlayerVo.UserBagGrids);
            
            //不仅要刷新背包，还要刷新装备栏！
            EventDispatcher.TriggerEvent(EventConst.UpdateGrid);
            EventDispatcher.TriggerEvent(EventConst.UpdateEquipmentView);





            //triggerEvent!



        }

		
		

    }

    public class UserGrid
    {
        public int GridId;
        public int GridPropId;
        public GripType GripType;
        public int PropNum;
        

    }
    
    public enum GripType
    {
        Equip,
        Consume,
        
        
    }

}


