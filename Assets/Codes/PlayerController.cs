using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;  // InputSystemに必要

public class PlayerController : MonoBehaviour
{
    @Level2 _inputActions;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var move = _inputActions.Player.Move.ReadValue<Vector2>();
        var v = new Vector3();
        v.x = move.x;
        v.z = move.y;
        var rb = GetComponent<Rigidbody>();
        rb.AddForce(v);
    }

    void Awake()
    {
        _inputActions = new @Level2();
        _inputActions.Enable();
    }
}
