using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraCon : MonoBehaviour
{
    //キャラクターのTransform
    public Transform charaLookAtPosition;
    //カメラの移動スピード
    //[SerializeField]
    //private float cameraMoveSpeed = 2f;
    //カメラの回転スピード
    [SerializeField]
    private float cameraRotateSpeed = 90f;
    //カメラのキャラクターからの相対位置を指定
    [SerializeField]
    private Vector3 basePos = new Vector3(0f, 0.35f, 0f);
    //[SerializeField]
    //float basePos_y = 0.35f;
    //カメラの角度を指定
    //[SerializeField]
    //private Vector3 baseRot = new Vector3(0f, 0f, 0f);
    [SerializeField]
    float baseRot_z = 0;
    //障害物とするレイヤー
    //[SerializeField]
    //LayerMask abstacleLayer;
    //[SerializeField]
    //float ContactPos = 1.3f;


    public void setTransform(Transform transform)
    {
        charaLookAtPosition = transform;
    }
    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        /*
        //通常のカメラ位置を計算
        var cameraPos = charaLookAtPosition.position +
            -charaLookAtPosition.forward * basePos.z +
            Vector3.up * basePos.y;
        //カメラの位置をキャラクターの後ろ側に移動させる
        transform.position = Vector3.Lerp(
            transform.position,
            cameraPos,
            cameraMoveSpeed * Time.deltaTime);
        */
        //カメラ位置をキャラクターに同期
        Vector3 dir = charaLookAtPosition.position;
        
        //カメラ位置のオフセット
        dir += basePos;
        //カメラ位置を設定
        transform.position = dir;
        //カメラ回転
        Quaternion q = charaLookAtPosition.rotation;
        q.x += baseRot_z;
        //q.z += baseRot_z;
        transform.localRotation = Quaternion.Lerp(
                                    transform.rotation,
                                    q,
                                    cameraRotateSpeed * Time.deltaTime);

         /*RaycastHit hit;
         //キャラクターとカメラの間に障害物があったら障害物の位置にカメラを移動させる
         if (Physics.Linecast(
             charaLookAtPosition.position,
             transform.position,
             out hit,
             abstacleLayer))
         {
             Vector3 dir = hit.point;
             dir.y *= ContactPos;
             transform.position = dir;

             Debug.Log("hitpoint:" + hit.point);
         }
         //レイを視覚的に確認
         Debug.DrawLine(
             charaLookAtPosition.position,
             transform.position,
             Color.red,
             0f,
             false);*/


        //　スピードを考慮しない場合はLookAtで出来る
        //transform.LookAt(charaTra.position);
        //　スピードを考慮する場合
        /*
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(
                charaLookAtPosition.position + baseRot - transform.position),
            cameraRotateSpeed * Time.deltaTime);
        */
    }
}