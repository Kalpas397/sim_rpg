using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPositionGenerater : MonoBehaviour
{
    [Header("RandomPosition Settings")]
    [SerializeField] private Transform rangeA;
    [SerializeField] private Transform rangeB;
    [SerializeField] private float checkInterval = 3f;  // Groundにヒットした後の待機時間(秒)

    [Header("SphereCast Settings")]
    [SerializeField] private float _sphereRadius = 0.5f; // 球の半径
    [SerializeField] private float _maxDistance = 10f;  // 最大距離
    private RaycastHit _hit;

    private void Update()
    {
        // PerformSphereCast();
    }
    /*

    private IEnumerator CheckGroundLoop()
    {
        while (true)
        {
            Vector3 randomPosition;
            RaycastHit hit;

            // Groundが見つかるまで位置を再試行
            do
            {
                // ランダムな位置を生成（XZ平面）
                randomPosition = new Vector3(
                    Random.Range(rangeA, rangeB),
                    transform.position.y,
                    Random.Range(rangeA, rangeB)
                );

                // ランダムな位置から下方向にレイキャスト
            } while (!Physics.Raycast(randomPosition, Vector3.down, out hit) ||
                     LayerMask.LayerToName(hit.collider.gameObject.layer) != Ground);

            // Groundが見つかったら「ok」を出力
            Debug.Log("ok");

            // 3秒間待機
            yield return new WaitForSeconds(checkInterval);
        }
    }

    /// <summary>
    /// 球状のレイを下方向へ飛ばし直近のオブジェクトを調べる
    /// </summary>
    void PerformSphereCast()
    {
        // レイの起点と方向
        Vector3 origin = transform.position;
        Vector3 direction = Vector3.down;

        // SphereCastを実行
        bool isHit = Physics.SphereCast(origin, _sphereRadius, direction, out _hit, _maxDistance);

        if (isHit)
        {
            Debug.Log($"Hit object: {_hit.collider.gameObject.name}, Layer: {LayerMask.LayerToName(_hit.collider.gameObject.layer)}");
        }
        else
        {
            Debug.Log("No object _hit.");
        }
    }

    private void OnDrawGizmos()
    {
        // Gizmosで球状のレイを可視化
        Gizmos.color = Color.green;

        // シーンビューでの可視化
        Vector3 origin = transform.position;
        Vector3 endPoint = origin + Vector3.down * (_hit.collider != null ? _hit.distance : _maxDistance);
        Gizmos.DrawLine(origin, endPoint); // 線を描画
        Gizmos.DrawWireSphere(endPoint, _sphereRadius); // 球を描画
    }
    */
}
