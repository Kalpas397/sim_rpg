using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    LineRenderer line;
    [SerializeField] int count;  // 線の超点の数

    public LineRenderer lineRenderer;
    public Transform player; // プレイヤーのTransform
    public float touchThreshold = 0.2f; // 判定する距離の閾値
    private int lastGeneratedVertexIndex = -1; // 直近の頂点インデックス


    float currentTime = 0f;

    private bool isGenerated = false;   // 円が完成したか


    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGenerated)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                currentTime += Time.deltaTime;
                if (currentTime > 0.25f)
                {
                    count += 1;
                    line.positionCount = count;
                    line.SetPosition(count - 1, transform.position);
                    currentTime = 0f;
                }
            }

            // if (Input.GetKeyDown(KeyCode.C)) 
            DetectLineTouch();
        }
        
    }

    // public void AddNewVertex(Vector3 newVertexPosition)
    // {
    //     // 新しい頂点をLineRendererに追加
    //     int currentVertexCount = lineRenderer.positionCount;
    //     lineRenderer.positionCount = currentVertexCount + 1;
    //     lineRenderer.SetPosition(currentVertexCount, newVertexPosition);

    //     // 直近の頂点インデックスを更新
    //     lastGeneratedVertexIndex = currentVertexCount;
    // }

    void DetectLineTouch()
    {
        int closestVertexIndex = -1;    // 最も近い頂点インデックス
        float closestDistance = float.MaxValue; // 最短距離

        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            // 直近生成された頂点は判定から除外
            if (i == lastGeneratedVertexIndex)
            {
                continue;
            }

            Vector3 vertexPosition = lineRenderer.GetPosition(i);
            float distance = Vector3.Distance(player.position, vertexPosition);

            if (distance < touchThreshold && distance < closestDistance)
            {
                closestDistance = distance;
                closestVertexIndex = i;
            }
        }

        if (closestVertexIndex != -1)
        {
            
            Debug.Log($"触れた頂点: {closestVertexIndex}, 座標: {lineRenderer.GetPosition(closestVertexIndex)}");
            // 触れた頂点から最新の頂点までの座標を取得
            if (closestVertexIndex < lineRenderer.positionCount - 3)
            {
                List<Vector3> vertexPositions = GetVertexPositionsFromIndex(closestVertexIndex);
                Debug.Log($"触れた頂点から最新の座標リスト: {string.Join(", ", vertexPositions)}");
                isGenerated = true;
            }
            
        }
    }

    List<Vector3> GetVertexPositionsFromIndex(int startIndex)
    {
        List<Vector3> positions = new List<Vector3>();

        for (int i = startIndex; i < lineRenderer.positionCount; i++)
        {
            positions.Add(lineRenderer.GetPosition(i));
        }

        return positions;
    }

    

    



    
}
