using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;  // InputSystemに必要

public class PlayerController : MonoBehaviour
{
    @Level2 _inputActions;
    Vector3 _initpos;   // 初期位置
    [SerializeField] private float moveSpeed = 3.0f;
    private Rigidbody rb;
    private Vector2 move;

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
    }

    void FixedUpdate()
    {
        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
    
        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * move.y + Camera.main.transform.right * move.x;
    
        // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
        rb.velocity = moveForward * moveSpeed + new Vector3(0, rb.velocity.y, 0);
    
        // キャラクターの向きを進行方向に
        if (moveForward != Vector3.zero) {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
    }

    void Awake()
    {
        _inputActions = new @Level2();
        _inputActions.Enable();
    }
}
