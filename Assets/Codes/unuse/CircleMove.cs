using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MoveAreaのScale x, zの値をrangeに指定することで移動範囲の可視化が可能
/// 選択中のUnitを移動させるスクリプトにする予定
/// </summary>

public class CircleMove : MonoBehaviour
{
    
    [SerializeField] private Test_UnitStatus operationUnit; // 操作対象ユニットの選択
    [SerializeField] private Transform moveArea;
    // [SerializeField] private float range = 5.0f; // インスペクタで半径を指定


    private Vector3 startPos;

    void Start()
    {
        startPos = operationUnit.transform.position; // 初期位置を保存
        SetVisibleMoveRange();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal"); // 矢印キーの横方向入力
        float moveZ = Input.GetAxis("Vertical"); // 矢印キーの縦方向入力

        // float radius = (range - 1) / 2;
        float radius = (operationUnit.MoveRange - 1) / 2;

        Vector3 direction = new Vector3(moveX, 0, moveZ);
        Vector3 newPosition = operationUnit.transform.position + direction * Time.deltaTime * 10.0f; // 速度を調整

        // 初期位置からのオフセットを計算
        Vector3 offset = newPosition - startPos;

        // 半径内に収まるように制限
        if (offset.magnitude > radius)
        {
            offset = offset.normalized * radius;
            newPosition = startPos + offset;
        }

        operationUnit.transform.position = newPosition;

        // SetVisibleMoveRange();
    }

    void SetVisibleMoveRange()
    {
        moveArea.transform.position = new Vector3(operationUnit.transform.position.x, moveArea.transform.position.y, operationUnit.transform.position.z);
        moveArea.localScale = new Vector3(operationUnit.MoveRange, moveArea.localScale.y, operationUnit.MoveRange);
    }
}
