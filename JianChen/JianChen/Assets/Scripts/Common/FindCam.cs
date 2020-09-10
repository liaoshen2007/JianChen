using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindCam : MonoBehaviour
{
//    public Vector3 Normal = Vector3.up;
//    private Transform _modelTransform;
//    Quaternion direction;

    private Transform refCamera;
    public bool reverFace = false;
    private Transform mRoot;


    private void Awake()
    {
//        _modelTransform = GameObject.FindWithTag("ModelCamera").transform;
//        direction = Quaternion.FromToRotation(new Vector3(0, 1, 0), Normal);
        refCamera = GameObject.FindWithTag("ModelCamera").transform;
        mRoot = transform;
    }


    //bug 这个地方可能会有问题，属于HUD面向摄像机的脚本
    private void LateUpdate()
    {
//        if (_modelTransform==null)
//        {
//            _modelTransform = GameObject.FindWithTag("ModelCamera").transform;
//        }
//        else
//        {
//            transform.rotation = _modelTransform.rotation * direction;
////            transform.rotation = _modelTransform.rotation;  
//        }

//        var transform1 = refCamera.transform;
        var rotation = refCamera.rotation;
        Vector3 targetPos =
            mRoot.position + rotation * (reverFace ? Vector3.back : Vector3.forward);
        Vector3 tarOrientation = rotation * Vector3.up;
        mRoot.LookAt(targetPos,tarOrientation);
        
        


    }
}