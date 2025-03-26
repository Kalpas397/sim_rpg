using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 凹多角形の内外判定
/// </summary>
public class PolygonInsideChecker : MonoBehaviour
{
    public Vector3[] polygonVertices;   // 凹多角形の頂点リスト

    public Transform targetObject;  // 判定したいオブジェクトの位置

    void Update()
    {
        Vector2[] polygon2D = ProjectTo2D(polygonVertices);
        Vector2 targetPosition2D = new Vector2(targetObject.position.x, targetObject.position.z);

        bool isInside = IsPointInsidePolygon(targetPosition2D, polygon2D);
        Debug.Log("対象オブジェクトは多角形の内部か: " + isInside);

        // 各辺を描画
        for (int i = 0; i < polygonVertices.Length; i++)
        {
            Vector3 startPoint = polygonVertices[i];
            Vector3 endPoint = polygonVertices[(i + 1) % polygonVertices.Length]; // 最後の辺を閉じる
            Debug.DrawLine(startPoint, endPoint, Color.green);
        }
    }

    public void ModifyPolygon(Vector3[] polVer)
    {
        polygonVertices = polVer;
        // Vector2[] polygon2D = ProjectTo2D(polygonVertices);
        // Vector2 targetPosition2D = new Vector2(targetObject.position.x, targetObject.position.z);

        // bool isInside = IsPointInsidePolygon(targetPosition2D, polygon2D);
        // Debug.Log("対象オブジェクトは多角形の内部か: " + isInside);

        // // 各辺を描画
        // for (int i = 0; i < polygonVertices.Length; i++)
        // {
        //     Vector3 startPoint = polygonVertices[i];
        //     Vector3 endPoint = polygonVertices[(i + 1) % polygonVertices.Length]; // 最後の辺を閉じる
        //     Debug.DrawLine(startPoint, endPoint, Color.green);
        // }
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