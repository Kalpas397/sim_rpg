using System.Collections.Generic;
using UnityEngine;

public class MeshColliderTriggerGenerator : MonoBehaviour
{
    public List<Vector3> vertices; // Vector3型の頂点リスト

    void Start()
    {
        // メッシュを生成
        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();

        // 自動生成の三角形 (シンプルな例として調整が必要)
        List<int> triangles = new List<int>();
        for (int i = 0; i < vertices.Count - 2; i++)
        {
            triangles.Add(0);
            triangles.Add(i + 1);
            triangles.Add(i + 2);
        }
        mesh.triangles = triangles.ToArray();

        // メッシュオブジェクトの設定
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        // MeshColliderに適用
        MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;
    }
}