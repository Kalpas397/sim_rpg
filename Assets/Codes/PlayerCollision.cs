using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private Transform saucer;  // 受け皿オブジェクト
    [SerializeField] private GameObject[] fruits;    // 拾ったフルーツを格納する配列
    [SerializeField] private int maxFruitCount = 100;
    [SerializeField] private int fruitCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        fruits = new GameObject[100];
    }

    // Update is called once per frame
    void Update()
    {
        
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

        // ダメージオブジェクト
    }

    private void StackFruits(GameObject fruit)
    {
        fruits[fruitCount] = fruit;
        fruit.transform.SetParent(transform);
        fruit.transform.localPosition = new Vector3(0, fruitCount+ 1.0f + 0.5f, 0);
        fruit.GetComponent<SphereCollider>().enabled = false;
    }
}
