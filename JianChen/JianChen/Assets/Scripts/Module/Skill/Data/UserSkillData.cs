using System;


namespace DataModel
{
    public class UserSkillData : IComparable<UserSkillData>
    {
        //public int SkillEntityId;
        public int SkillId;
        public string SkillKeyPos;
        public int Level;//已经强化技能等级 
        
        
        
        public int CompareTo(UserSkillData other)
        {
            int result = 0;
        if (other.SkillId.CompareTo(SkillId)!=0)
        {
            result=-other.SkillId.CompareTo(SkillId);
        }

            return result;
        }
    }  

}

