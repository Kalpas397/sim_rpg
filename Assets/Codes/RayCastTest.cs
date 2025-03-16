using UnityEngine;

public class RayCastTest : MonoBehaviour
{
    [Header("SphereCast Settings")]
    [SerializeField] private float sphereRadius = 0.5f; // 球の半径
    [SerializeField] private float maxDistance = 10f;  // 最大距離
    private RaycastHit hit;

    private void Update()
    {
        PerformSphereCast();
    }

    void PerformSphereCast()
    {
        // レイの起点と方向
        Vector3 origin = transform.position;
        Vector3 direction = Vector3.down;

        // SphereCastを実行
        bool isHit = Physics.SphereCast(origin, sphereRadius, direction, out hit, maxDistance);

        if (isHit)
        {
            Debug.Log($"Hit object: {hit.collider.gameObject.name}, Layer: {LayerMask.LayerToName(hit.collider.gameObject.layer)}");
        }
        else
        {
            Debug.Log("No object hit.");
        }
    }

    private void OnDrawGizmos()
    {
        // Gizmosで球状のレイを可視化
        Gizmos.color = Color.green;

        // シーンビューでの可視化
        Vector3 origin = transform.position;
        Vector3 endPoint = origin + Vector3.down * (hit.collider != null ? hit.distance : maxDistance);
        Gizmos.DrawLine(origin, endPoint); // 線を描画
        Gizmos.DrawWireSphere(endPoint, sphereRadius); // 球を描画
    }
}
