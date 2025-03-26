using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 線を囲った際に発生するメッシュコライダーの処理
/// </summary>
public class LineEnclosedRange : MonoBehaviour
{
    [SerializeField] private MeshCollider meshCollider;
    [SerializeField] private MeshFilter meshFilter;

    // Start is called before the first frame update
    void Start()
    {
        if (meshCollider)
        {
            meshCollider.enabled = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 受け取った頂点リストを元にメッシュを成形
    void ModifyMesh(List<Vector3> points3D)
    {

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
        this.transform.position = Vector3.zero;
    }

    public IEnumerator AppearanceLineEnclosedRange(List<Vector3> points3D)
    {
        meshCollider.enabled = true;
        ModifyMesh(points3D);
        // yield return new WaitForSeconds(3f);
        // meshCollider.enabled = false;
        yield return null;
    }
}
