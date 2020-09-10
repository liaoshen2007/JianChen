using System;
using Assets.Scripts.Framework.JianChen.Service;
using FrameWork.JianChen.Core;
using FrameWork.JianChen.Interfaces;
using UnityEngine;

public class SkillPlayer : EntityView
{
    public int SkillId;
    public string CreatorName;
    public SkillStage[] SkillList;


    //todo 尽早要构建好SkillData数据结构！
    public void PlayerEffect(string creatorName,int[] effectDamage,int recoverTime=0)
    {
        //effectDamage之后测试！
        

        CreatorName = creatorName;
        
        //暂时是单机，如果之后是MMO的话，那么我们就要根据字典里是否有这个名字来判断是否为玩家释放的特效！
        Debug.Log("CreatorName:"+CreatorName);

        if (SkillList.Length!=0)
        {
            for (int i = 0; i < SkillList.Length; i++)
            {
//                Debug.LogError("Play"+i);
                var skill = SkillList[i];
                //var damage=skill.skillobj.GetComponentInChildren<DamageEffectVo>();
                skill.DamageEffectVo.SetEffectValue(CreatorName,effectDamage[i]);
                ClientTimer.Instance.DelayCall(() =>
                {
                    skill.skillobj.SetActive(true);
                    ClientTimer.Instance.DelayCall(() =>
                    {
                        skill.skillobj.SetActive(false);
                    }, skill.HideTime);

                },skill.Timeinterval);

            }
        }
        
        //todo 需要加上一个播放完后的对象池回收的东西！
        //最复杂的地方还是在BUFF上面！

        //如果回收时间大于1，那么播放玩就要放到对象池！
        //todo 优化，这个recoverTime的时间放到特效本身去判断吧，不然靠程序写死是不正确的！
        if (recoverTime > 0)
        {
            ClientTimer.Instance.DelayCall(() =>
                {
                    Debug.Log("RecoverEntity:"+SkillId);
                    PoolManager.Instance.RecoverEntity(string.Format("3DModel/SkillEffect/Skill{0}", SkillId+""),this);
                },recoverTime);
        }



    }
    

    

}

[Serializable]
public class SkillStage
{

    public GameObject skillobj;//特效粒子和动画obj
    public SkillPlayerType SkillPlayerType;//特效技能类型
    public float Timeinterval=0;//延迟播放时间，实现多个特效的阶段性播放
    public AttackTtpe AttackTtpe;//攻击类型，是攻击，还是治疗，还是混合作用
    public bool TakeEffectIsSelf=false;//是否可以作用于本身
    //private int BaseApplyNum;//基础数值系数(攻击或者治疗)
    public DamageEffectVo DamageEffectVo;//技能效果数值和事件
    public Animator Ani;//动画帧

    public float HideTime=2;
    //public ParticleSystem ParticleSystem;

    //看看后续要不要加入Animator和partisystem的类型。



}

public enum SkillPlayerType
{
    Normal=0,
    AllRange=1,
    FlyLine=2,
    Buff=3
}

public enum AttackTtpe
{
    Attack,
    Cure,
    Mix
}