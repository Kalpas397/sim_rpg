using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゾンビの処理
/// </summary>
public class Zombie : MonoBehaviour
{
    [SerializeField] private bool isDefeat = false; // 倒されたか
    private bool isDefeatExecuted = false;    // 倒れた判定が実行済みか

    public bool IsDefeat { get => isDefeat; set => isDefeat = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDefeatExecuted)
        {
            if (isDefeat)
            {
                Debug.Log("「やられた～！」");
                isDefeatExecuted = true;
                Destroy (gameObject, 1f);
            }
        }
    }
}
