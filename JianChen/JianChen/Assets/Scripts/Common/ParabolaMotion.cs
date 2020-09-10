using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolaMotion : MonoBehaviour
{
  public float ShotSpeed = 6;
    private float time = 3;//代表从A点出发到B经过的时长
    //public Transform pointA;//点A
    public Vector3 pointB;//点B
    public float g = -10;//重力加速度
    // Use this for initialization
    private Vector3 speed;//初速度向量
    private Vector3 Gravity;//重力向量

    private Vector3 currentAngle;

    private float dTime = 0;
    private Vector3 offset;

    //随机抛物线运动
    public void SetRandomData(Vector3 startPos)
    {
        //pointA.position = startPos;
        pointB=new Vector3(startPos.x+Random.Range(-3,3),startPos.y,startPos.z+(Random.Range(-3,3)));
        dTime = 0;
        time = Vector3.Distance(startPos, pointB)/ShotSpeed;
        transform.position = startPos;//将物体置于A点
        //通过一个式子计算初速度
        speed = new Vector3((pointB.x - startPos.x) / time,
            (pointB.y - startPos.y) / time - 0.5f * g * time, (pointB.z - startPos.z) / time);
        Gravity = Vector3.zero;//重力初始速度为0
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        if (pointB==Vector3.zero)
        {
            return;
        }
        
        
        //todo 这个抛物线运动的算法效率不高，需要重构一下
        offset = (pointB - transform.position);
        //问题是要到到达点的时候停止！
        if (offset.sqrMagnitude > 0.01f && Vector3.Distance(pointB, transform.position) > 0.01)
        {
            Gravity.y = g * (dTime += Time.fixedDeltaTime);//v=at
            //模拟位移
            transform.position += (speed + Gravity) * Time.fixedDeltaTime;
            currentAngle.x = -Mathf.Atan((speed.y + Gravity.y) / speed.z) * Mathf.Rad2Deg;
            transform.eulerAngles = currentAngle;
        }
        
    }
}
