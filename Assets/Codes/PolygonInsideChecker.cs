using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 凹多角形の内外判定
/// </summary>
public class PolygonInsideChecker : MonoBehaviour
{
    public Vector3[] _polygonVertices;   // 凹多角形の頂点リスト

    // public Transform targetObject;  // 判定したいオブジェクトの位置
    [SerializeField] private RandomPositionGenerater _rpg;

    void Update()
    {
        // 各辺を描画
        for (int i = 0; i < _polygonVertices.Length; i++)
        {
            Vector3 startPoint = _polygonVertices[i];
            Vector3 endPoint = _polygonVertices[(i + 1) % _polygonVertices.Length]; // 最後の辺を閉じる
            Debug.DrawLine(startPoint, endPoint, Color.green);
        }
    }

    /// <summary>
    /// PlayerDrawLine.csにて線が囲まれた際に呼び出される
    /// </summary>
    /// <param name="vertices">囲いを構成する頂点の配列</param>
    public void ModifyPolygon(Vector3[] vertices)
    {
        // 生成する凹多角形の形を更新
        _polygonVertices = vertices;

        Vector2[] polygon2D = ProjectTo2D(_polygonVertices);

        if (_rpg.Zombies != null)
        {
            // フィールドに存在する全てのゾンビが囲いの内側にいるか判定
            for (int i = 0; i < _rpg.Zombies.Count; i++)
            {
                Vector2 targetPosition2D = new Vector2(_rpg.Zombies[i].gameObject.transform.position.x, _rpg.Zombies[i].gameObject.transform.position.z);
                bool isInside = IsPointInsidePolygon(targetPosition2D, polygon2D);
                if (isInside)
                {
                    // 内側にいたゾンビを倒す
                    _rpg.Zombies[i].IsDefeat = true;
                    _rpg.Zombies[i] = null;
                }
            }
            // nullの要素をリストから削除
            _rpg.Zombies.RemoveAll(item => item == null);
        }
    }

    // Vector3配列をXZ平面に投影（2D化）
    Vector2[] ProjectTo2D(Vector3[] vertices)
    {
        Vector2[] projected = new Vector2[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            projected[i] = new Vector2(vertices[i].x, vertices[i].z);
        }
        return projected;
    }

    // Ray-Castingによる点の内包判定
    bool IsPointInsidePolygon(Vector2 point, Vector2[] polygon)
    {
        int intersectCount = 0;
        for (int i = 0; i < polygon.Length; i++)
        {
            Vector2 vertex1 = polygon[i];
            Vector2 vertex2 = polygon[(i + 1) % polygon.Length]; // 最後の辺を閉じる

            if ((point.y > vertex1.y && point.y <= vertex2.y) ||
                (point.y > vertex2.y && point.y <= vertex1.y))
            {
                float slope = (vertex2.x - vertex1.x) / (vertex2.y - vertex1.y);
                float intersectX = vertex1.x + slope * (point.y - vertex1.y);
                if (point.x < intersectX)
                {
                    intersectCount++;
                }
            }
        }
        return (intersectCount % 2) == 1; // 奇数なら内部
    }
}