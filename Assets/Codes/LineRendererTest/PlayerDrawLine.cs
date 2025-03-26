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
        // test 線のリセット
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetLines();
            _isCircleComplete = false;
        }

        if (!_isCircleComplete)
        {

        
        // スキル使用中
        // 直前の座標と差異があるか
        DrawLine();
        CheckLineTouch();
        }
    }

    /// <summary>
    /// 一定時間ごとに線を引く
    /// </summary>
    void DrawLine()
    {
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
        float closestDistance = float.MaxValue; // 最短距離

        // LineRendererの全ての頂点を探索
        for (int i = 0; i < _lineRenderer.positionCount; i++)
        {
            // 各頂点を取得
            Vector3 vertexPosition = _lineRenderer.GetPosition(i);
            // プレイヤーと頂点の距離
            float distance = Vector3.Distance(this.transform.position, vertexPosition);

            // 距離が閾値未満か　かつ　プレイヤーと各頂点の距離が前回の距離と短いか
            if (distance < _touchThreshold && distance < closestDistance)
            {
                closestDistance = distance;
                // 最も近い頂点のインデックスを更新
                closestVertexIndex = i;
                // Debug.Log("i: " +  i + ", distance: " + distance);
            }
        }

        // 初期状態を除外
        if (closestVertexIndex != -1)
        {
            
            // Debug.Log($"触れた頂点: {closestVertexIndex}, 座標: {_lineRenderer.GetPosition(closestVertexIndex)}");
            
            // 最新の頂点より以前の頂点に触れたか判定
            if (closestVertexIndex < _lineRenderer.positionCount - 3)
            {
                Debug.Log("ワッ！！！！");
                _isCircleComplete = true;

                List<Vector3> vertexPositions = GetVertexPositionsFromIndex(closestVertexIndex);
                

                // テスト　頂点情報を番号で表示
                // for (int i = 0; i < vertexPositions.Count; i++)
                // {
                //     Debug.Log(vertexPositions);
                // }
                // Debug.Log($"触れた頂点から最新の座標リスト: {string.Join(", ", vertexPositions)}");
            }
            
        }
    }

    /// <summary>
    /// 触れた頂点から最新の頂点までをリストで取得
    /// </summary>
    /// <param name="startIndex">触れた頂点</param>
    /// <returns></returns>
    List<Vector3> GetVertexPositionsFromIndex(int startIndex)
    {
        List<Vector3> positions = new List<Vector3>();

        for (int i = startIndex; i < _lineRenderer.positionCount; i++)
        {
            positions.Add(_lineRenderer.GetPosition(i));
        }

        return positions;
    }

    /// <summary>
    /// 線のリセット
    /// </summary>
    void ResetLines()
    {
        // LineRendererのサイズを0にする
        _lineRenderer.positionCount = 0;
    }
}
