using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// ゲームの進行状況、UIの管理
/// </summary>
    
public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerCollision playerCollision;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI textHoldNum;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCollision)
        {
            textHoldNum.text = playerCollision.FruitCount.ToString();
        }
        else
        {
            Debug.Log("PlayerCollisionアタッチされてないよ!");
        }
        
    }
}
