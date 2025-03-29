using System;   // Array.Resizeを使用
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    [SerializeField] private Transform saucer;  // 受け皿オブジェクト
    [SerializeField] private GameObject[] fruits;    // 拾ったフルーツを格納する配列
    [SerializeField] private int maxFruitCount = 100;
    [SerializeField] private int fruitCount = 0;
    [SerializeField] private bool isDamaged = false; // ダメージオブジェクトに触れているかのフラグ
    private Collider _collision;    // TriggerEnterで取得したコライダーを格納

    public int MaxFruitCount { get => maxFruitCount; set => maxFruitCount = value; }
    public int FruitCount { get => fruitCount; set => fruitCount = value; }

    // Start is called before the first frame update
    void Start()
    {
        Array.Resize(ref fruits, maxFruitCount);    // 配列の要素数を最大個数に変更
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerController.IsInvincible)
        {
            if (isDamaged)
            {
                DestroyFruits();    // フルーツ削除
                StartCoroutine(playerController.DamagedPlayer(_collision));
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        // フルーツに触れた処理
        if (collision.gameObject.CompareTag("Fruit"))
        {
            // 操作不可状態のときは拾えない　最大所持数以上は拾えない
            if (!playerController.IsNotControl && fruitCount < maxFruitCount)
            {
                StackFruits(collision.gameObject);
                fruitCount++;
            }
        }

        // 納品カゴ
        if (collision.gameObject.CompareTag("Goal"))
        {
            // スコア加算
            DestroyFruits();    // フルーツ削除
        }

        // ダメージオブジェクト
        if (collision.gameObject.CompareTag("Damage"))
        {
            _collision = collision;
            isDamaged = true;
        }
            
    }

    private void OnTriggerStay(Collider collision)
    {
        // ダメージオブジェクト
        if (collision.gameObject.CompareTag("Damage"))
        {
            isDamaged = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        // ダメージオブジェクト
        if (collision.gameObject.CompareTag("Damage"))
        {
            isDamaged = false;
        }
    }


    // 拾ったフルーツを積み上げる
    private void StackFruits(GameObject fruit)
    {
        fruits[fruitCount] = fruit;
        fruit.transform.SetParent(saucer.transform);
        fruit.transform.localPosition = new Vector3(0, fruitCount, 0);
        fruit.GetComponent<SphereCollider>().enabled = false;
    }

    // 所持している全てのフルーツを削除
    private void DestroyFruits()
    {
        if (fruitCount >= 1)
        {
            while (fruitCount > 0)
            {
                Debug.Log("fruit: " + fruitCount);
                Destroy(fruits[fruitCount - 1]);
                fruits[fruitCount - 1] = null;
                fruitCount -= 1;
            }
        }
    }
}
