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
    // [SerializeField] private GameObject colliderPrefab;
    // [SerializeField] private LineEnclosedRange lineEnclosedRange;
    [SerializeField] private PolygonInsideChecker polygonInsideChecker;

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
                
                // GeneratePolygonCollider(vertexPositions);
                // GenerateColliderObject(vertexPositions);
                // ModifyMesh(colliderPrefab, vertexPositions);
                // StartCoroutine(lineEnclosedRange.AppearanceLineEnclosedRange(vertexPositions));
                Vector3[] polver = vertexPositions.ToArray();
                polygonInsideChecker.ModifyPolygon(polver);
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


    // PolygonColliderを生成する方法
    void GeneratePolygonCollider(List<Vector3> points3D)
    {
        // 新しい2Dオブジェクトを生成
        GameObject colliderObject = new GameObject("DynamicPolygonCollider2D");
        colliderObject.transform.position = Vector3.zero;

        // 横向きに回転させる
        colliderObject.transform.rotation = Quaternion.Euler(90, 0, 0); // X軸を90度回転

        // PolygonCollider2Dを追加
        PolygonCollider2D polygonCollider = colliderObject.AddComponent<PolygonCollider2D>();
        polygonCollider.isTrigger = true;

        // Vector3リストをVector2リストに変換
        List<Vector2> points2D = new List<Vector2>();
        foreach (Vector3 point in points3D)
        {
            points2D.Add(new Vector2(point.x, point.z));
        }

        // PolygonCollider2Dのポイントを設定
        polygonCollider.pathCount = 1; // 単一のポリゴンパス
        polygonCollider.SetPath(0, points2D.ToArray());
    }

    // MeshColliderを生成する方法
    void GenerateColliderObject(List<Vector3> points)
    {
        // 新しいGameObjectを生成
        GameObject colliderObject = new GameObject("DynamicColliderObject");
        
        // メッシュを生成
        Mesh mesh = new Mesh();
        mesh.vertices = points.ToArray();

        // 自動生成の三角形 (頂点数に応じて調整可能)
        List<int> triangles = new List<int>();
        for (int i = 0; i < points.Count - 2; i++)
        {
            triangles.Add(0);
            triangles.Add(i + 1);
            triangles.Add(i + 2);
        }
        mesh.triangles = triangles.ToArray();

        // メッシュの計算
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        // 新しいオブジェクトにMeshFilterとMeshColliderを追加
        MeshFilter meshFilter = colliderObject.AddComponent<MeshFilter>();
        MeshCollider meshCollider = colliderObject.AddComponent<MeshCollider>();

        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;

        // オブジェクトの位置を調整 (必要に応じて変更可能)
        colliderObject.transform.position = Vector3.zero;
    }

    // メッシュのプレハブを生成する方法
    void ModifyMesh(GameObject obj, List<Vector3> points3D)
    {
        if (obj == null)
        {
            Debug.LogError("対象オブジェクトが指定されていません！");
            return;
        }

        // MeshFilterとMeshColliderの取得
        MeshFilter meshFilter = obj.GetComponent<MeshFilter>();
        MeshCollider meshCollider = obj.GetComponent<MeshCollider>();

        if (meshFilter == null || meshCollider == null)
        {
            Debug.LogError("指定されたオブジェクトにMeshFilterまたはMeshColliderがありません！");
            return;
        }

        // メッシュ生成
        Mesh mesh = new Mesh();
        mesh.vertices = points3D.ToArray();

        // 三角形インデックス生成
        List<int> triangles = new List<int>();
        for (int i = 0; i < points3D.Count - 2; i++)
        {
            triangles.Add(0);
            triangles.Add(i + 1);
            triangles.Add(i + 2);
        }
        mesh.triangles = triangles.ToArray();

        // メッシュの再計算
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        // MeshFilterとMeshColliderに新しいメッシュを適用
        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;

        // オブジェクトの位置を変更（必要に応じて調整可能）
        obj.transform.position = Vector3.zero;
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
