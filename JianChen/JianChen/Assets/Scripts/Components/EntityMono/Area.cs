using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{

    public AreaData AreaData;
    
    //Area要做的事情有好多件：
    //首先判断是否有玩家Trigger，Trigger后要EventDispatch给Contoller，通知正向玩家的敌人要靠近玩家并且攻击他。
    //如果敌人因为追逐玩家而Exit Area，那么要隔断时间通知他返回Area中心点。
    //每隔段时间检测Area里的敌人是否低于某数量，如果是的话，就Event Controller通知生成新的敌人！
    

}
