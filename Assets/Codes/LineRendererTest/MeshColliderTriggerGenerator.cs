using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class MeshColliderTriggerGenerator : MonoBehaviour
{
    [Header("ポリゴンの頂点リスト (2D座標 - X, Yのみ使用)")]
    public List<Vector2> vertices = new List<Vector2>(); // 頂点リスト（インスペクタで設定）

    private PolygonCollider2D polygonCollider;

    void Start()
    {
        // PolygonCollider2Dコンポーネントを取得
        polygonCollider = GetComponent<PolygonCollider2D>();

        // 頂点リストを適用してポリゴンを生成
        GeneratePolygonCollider();
    }

    void GeneratePolygonCollider()
    {
        if (vertices.Count < 3)
        {
            Debug.LogError("少なくとも3つの頂点が必要です！");
            return;
        }

        // PolygonCollider2Dのパスを設定
        polygonCollider.SetPath(0, vertices.ToArray());

        Debug.Log("PolygonCollider2Dが生成されました！");
    }
}