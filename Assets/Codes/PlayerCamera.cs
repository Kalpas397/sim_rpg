using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject targetObject;

    Vector3 currentPos;//現在のカメラ位置
    Vector3 pastPos;//過去のカメラ位置
    Vector3 diff;//移動距離

    [SerializeField] private float rotateSpeed = 1.0f;  // 回転スピード
    private Vector3 lastMousePosition;
    private Vector3 newCameraAngle = new Vector3(0, 0, 0);

    float currentVerticalAngle = 0.0f;
    [SerializeField] private float minRotateAngleX = 0.0f;  // カメラのx軸回転の制限 最小値
    [SerializeField] private float maxRotateAngleX = 50.0f; // カメラのx軸回転の制限 最大値

    void Start()
    {
        pastPos = targetObject.transform.position;
        rotateCamera();
    }

    void Update()
    {
        movingCamera();

        if (Input.GetMouseButtonDown(0))
        {
            newCameraAngle = mainCamera.transform.localEulerAngles;
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            rotateCamera();
        }
    }

    private void movingCamera()
    {
        //------カメラの移動------

        //プレイヤーの現在地の取得
        currentPos = targetObject.transform.position;

        diff = currentPos - pastPos;

        transform.position = Vector3.Lerp(transform.position, transform.position + diff, 1.0f);//カメラをプレイヤーの移動差分だけうごかすよ

        pastPos = currentPos;
    }

    private void rotateCamera()
    {
        // スワイプ入力受け取り
        float swipeInputX = Input.GetAxis("Mouse X") * this.rotateSpeed;
        float swipeInputY = Input.GetAxis("Mouse Y") * this.rotateSpeed * -1.0f;
        
        // カメラの角度を変化
        this.mainCamera.transform.RotateAround(this.targetObject.transform.position, Vector3.up, swipeInputX);

        // 垂直回転の範囲を制限
        float newVerticalAngle = currentVerticalAngle + swipeInputY;
        newVerticalAngle = Mathf.Clamp(newVerticalAngle, minRotateAngleX, maxRotateAngleX);
        float verticalRotation = newVerticalAngle - currentVerticalAngle;
        currentVerticalAngle = newVerticalAngle;

        // 縦の軸回転を適用
        this.mainCamera.transform.RotateAround(this.targetObject.transform.position, transform.right, verticalRotation);

        // ターゲットの方向を向かせる
        this.mainCamera.transform.LookAt(this.targetObject.transform.position);
    }
}
