using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPositionGenerater : MonoBehaviour
{
    [Header("RandomPosition Settings")]
    [SerializeField] private Transform rangeA;
    [SerializeField] private Transform rangeB;
    [SerializeField] private float checkInterval = 3f;  // Groundにヒットした後の待機時間(秒)

    [Header("RandomPosition Settings")]
    [SerializeField] private GameObject _objFruit;

    [Header("SphereCast Settings")]
    [SerializeField] private float _sphereRadius = 0.5f; // 球の半径
    [SerializeField] private float _maxDistance = 10f;  // 最大距離
    private RaycastHit _hit;

    void Start()
    {
        StartCoroutine(GroundCheck());
    }

    private void Update()
    {
        // PerformSphereCast();
    }

    IEnumerator GroundCheck()
    {
        while (true)
        {
            // ランダムな位置を生成（XZ平面）
            this.transform.position = new Vector3(
                Random.Range(rangeA.position.x, rangeB.position.x),
                transform.position.y,
                Random.Range(rangeA.position.z, rangeB.position.z)
            );
            // Debug.Log("pos: " + this.transform.position);

            // Debug.Log("isGround" + PerformSphereCast());

            if (PerformSphereCast())
            {
                // オブジェクト生成
                if (_objFruit)
                {
                    Instantiate(_objFruit, new Vector3(
                        this.transform.position.x,
                        1f,
                        this.transform.position.z),
                        Quaternion.identity
                    );
                }
                
            }

            yield return new WaitForSeconds (checkInterval);
        }
        
    }

    /// <summary>
    /// 球状のレイを下方向へ飛ばし直近のオブジェクトを調べる
    /// </summary>
    bool PerformSphereCast()
    {
        // レイの起点と方向
        Vector3 origin = transform.position;
        Vector3 direction = Vector3.down;

        // SphereCastを実行
        bool isHit = Physics.SphereCast(origin, _sphereRadius, direction, out _hit, _maxDistance);

        if (isHit)
        {
            // Debug.Log($"Hit object: {_hit.collider.gameObject.name}, Layer: {LayerMask.LayerToName(_hit.collider.gameObject.layer)}");
            if (_hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                return true;
            }
        }
        else
        {
            // Debug.Log("No object _hit.");
        }
        
        return false;
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
}
