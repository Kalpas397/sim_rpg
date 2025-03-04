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
    [SerializeField] private float minRotateAngleX = 0.0f;  // カメラのx軸回転の制限 最小値
    [SerializeField] private float maxRotateAngleX = 50.0f; // カメラのx軸回転の制限 最大値

    void Start()
    {
        pastPos = targetObject.transform.position;
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
        Vector3 angle = new Vector3(
            Input.GetAxis("Mouse X") * this.rotateSpeed,
            Input.GetAxis("Mouse Y") * this.rotateSpeed,
            0
            );
        this.mainCamera.transform.RotateAround(this.targetObject.transform.position, Vector3.up, angle.x);
        // Debug.Log(this.mainCamera.transform.RotateAround);
        this.mainCamera.transform.RotateAround(this.targetObject.transform.position, transform.right, angle.y);
    }
}
