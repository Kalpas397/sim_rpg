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
    [SerializeField] private bool isDamaged = false; // ダメージフラグ ダメージオブジェクトに触れているか
    // [SerializeField] private bool isInvincible = false; // 無敵フラグ
    private Collider _collision;    // TriggerEnterで取得したコライダーを格納


    
    public bool IsDamaged { get => isDamaged; set => isDamaged = value; }
    // public bool IsInvincible { get => isInvincible; set => isInvincible = value; }

    // Start is called before the first frame update
    void Start()
    {
        fruits = new GameObject[100];
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerController.IsInvincible)
        {
            if (isDamaged)
            {
                StartCoroutine(playerController.DamagedPlayer(_collision));
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        // フルーツに触れた処理
        if (collision.gameObject.CompareTag("Fruit"))
        {
            if (fruitCount < maxFruitCount)
            {
                StackFruits(collision.gameObject);
                fruitCount++;
            }
        }

        // 納品カゴ
        if (collision.gameObject.CompareTag("Goal"))
        {
            // スコア加算
            DestroyFruits();
        }

        // ダメージオブジェクト
        if (collision.gameObject.CompareTag("Damage"))
        {
            _collision = collision;
            isDamaged = true;

            // フルーツ削除
            DestroyFruits();
            
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
