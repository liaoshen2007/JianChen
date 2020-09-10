using System;

namespace DataModel
{
    public class UserEquipData:IComparable<UserEquipData>
    {
        //之后这个EquipEntityId，如果是爆出来的，一定不能是在玩家数据中出现，所以，最好是先得出玩家当前的最大EquipEntityId，然后加1；
        public int EquipEntityId;
        public int EquipBaseId;
        public int HasWear;//是否穿戴上了
        public int GridPos;  //背包位置
        public int LevelUpTimes; //可升级次数
        public int HasStrengthenTimes;//已经强化次数
        public int ExtraHp;
        public int ExtraMP;
        public int ExtraAtk;
        public int ExtraDef;
        public int ExtraAtkSpeed;
        public int ExtraHitRate;
        public int ExtraCriRate;
        public int ExtraAtkRange;
        public int ExtraMoveSpd;
        
        //额外字段之后思考在填充吧！
        
        
        public int CompareTo(UserEquipData other)
        {
            int result = 0;
            if (other.ExtraAtk.CompareTo(ExtraAtk)!=0)
            {
                result=-other.ExtraAtk.CompareTo(ExtraAtk);
            }

            return result;
        }
    }
    
    

}
