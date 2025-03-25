using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーが線で敵を囲い攻撃するための処理
/// </summary>
public class PlayerDrawLine : MonoBehaviour
{
    [Header("LineRenderer Settings")]
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private float _touchThreshold = 10.0f; // 判定する距離の閾値 大きいほど各点の当たり判定がゆるくなる
    // プレイヤーの過去の座標
    // プレイヤーの座標の閾値

    [Header("Player Action Settings")]
    private float _nowDrawTime = 0f;
    private bool _canUseSkill = true; // スキルが使用可能か
    private bool _isUseSkill = false;   // スキルが使用されたか
    private bool _isCircleComplete = false; // 円が完成したか


    void Start()
    {
        
    }

    void Update()
    {
        // スキル使用中
        // 直前の座標と差異があるか
        _nowDrawTime += Time.deltaTime;
        if (_nowDrawTime > 0.25f)
        {
            // lineRendererのサイズを増やす
            _lineRenderer.positionCount += 1;
            // 新たな頂点の座標をプレイヤーの座標に設定
            _lineRenderer.SetPosition(
                _lineRenderer.positionCount - 1,
                this.transform.position
                );
            _nowDrawTime = 0f;
        }
    }

    /// <summary>
    /// 線に触れたか判定
    /// </summary>
    void CheckLineTouch()
    {
        int closestVertexIndex = -1;    // 最も近い頂点のインデックス

        // LineRendererの全ての頂点を探索
        for (int i = 0; i < _lineRenderer.positionCount; i++)
        {
            Vector3 vertexPosition = _lineRenderer.GetPosition(i);
        }
    }
}
