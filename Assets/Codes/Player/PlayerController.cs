using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;  // InputSystemに必要

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerCollision playerCollision;
    @Level2 _inputActions;
    Vector3 _initpos;   // 初期位置
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private float minSpeed = 3.0f;
    private Rigidbody rb;
    private Vector2 move;
    [SerializeField] private Animator anim;
    [SerializeField] private bool isInvincible = false; // 無敵フラグ
    [SerializeField] private bool isNotControl = false; // 操作不可フラグ

    public bool IsInvincible { get => isInvincible; set => isInvincible = value; }
    public bool IsNotControl { get => isNotControl; set => isNotControl = value; }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _initpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // InputSystemから上下左右の操作を受け取る
        move = _inputActions.Player.Move.ReadValue<Vector2>();

        // マップ外落下から復帰
        if (transform.position.y < -10.0f)
        {
            transform.position = _initpos;
        }

        var velocity = rb.velocity;
        velocity.y = 0.0f;

        float animSpeed = velocity.magnitude;
        animSpeed = Mathf.Pow(animSpeed, 0.25f);
        // Debug.Log("animSpeed: " + animSpeed);

        // アニメーション
        if (anim)
        {
            anim.SetFloat("Speed", animSpeed);
            // if (animSpeed > 0)
            // {
            //     anim.SetFloat("Speed", animSpeed);
            // }
            // else if (animSpeed < 0)
            // {
            //     anim.SetFloat("Speed", animSpeed);
            // }
        }
    }

    void FixedUpdate()
    {
        // 被ダメージ時は移動不可
        if (!isNotControl)
        {
            RunningPlayer();
        }
        
    }

    void Awake()
    {
        _inputActions = new @Level2();
        _inputActions.Enable();
    }

    private void RunningPlayer()
    {
        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
    
        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * move.y + Camera.main.transform.right * move.x;
    
        // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
        // rb.velocity = moveForward * moveSpeed + new Vector3(0, rb.velocity.y, 0);
        rb.velocity = moveForward * CalcCurrentSpeed() + new Vector3(0, rb.velocity.y, 0);
    
        // キャラクターの向きを進行方向に
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
    }

    // 所持数による移動速度の計算
    private float CalcCurrentSpeed()
    {
        // 最低速度は最大速度の半分まで
        float speedFactor = 1f - (playerCollision.FruitCount / (float)playerCollision.MaxFruitCount) / 2f;
        float currentSpeed = moveSpeed * Mathf.Max(minSpeed / moveSpeed, speedFactor);
        // Debug.Log("currentSpeed: "+ currentSpeed);
        return currentSpeed;
    }

    // プレイヤーの被弾時の挙動
    public IEnumerator DamagedPlayer(Collider collision)
    {
        if (anim)
        {
            anim.SetBool("isDamaged", true);
        }

        // 対象オブジェクトへ向く
        var velocity = rb.velocity;
        rb.velocity = new Vector3(0, 0, 0);

        Vector3 direction = collision.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
        // あとでx,y軸は回転しないようにする
        
        isNotControl = true;    // 操作不可状態にする
        isInvincible = true;    // 無敵にする

        // 後方へ飛ばす
        if (rb != null)
        {
            Vector3 backwardDirection = -transform.forward;
            Vector3 forceDirection = backwardDirection * 5f + Vector3.up * 5f;
            rb.AddForce(forceDirection, ForceMode.Impulse);
        }

        Debug.Log("Damaged!");

        yield return new WaitForSeconds(0.5f);
        if (anim)
        {
            anim.SetBool("isDamaged", false);
        }

        yield return new WaitForSeconds(1);
        isNotControl = false;   // 操作不可状態を解除

        yield return new WaitForSeconds(3);
        isInvincible = false;   // 無敵状態を解除
    }

    
}
