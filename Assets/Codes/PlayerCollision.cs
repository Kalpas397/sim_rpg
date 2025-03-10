using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private Transform saucer;  // 受け皿オブジェクト
    // [SerializeField] private Transform[] fruits;
    [SerializeField] private int maxFruitCount = 100;
    [SerializeField] private int fruitCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Fruit"))
        {
            if (fruitCount < maxFruitCount)
            {
                fruitCount++;
                StackFruits(collision.gameObject);
            }
            
        }
    }

    private void StackFruits(GameObject fruit)
    {
        fruit.transform.SetParent(transform);
        fruit.transform.localPosition = new Vector3(0, fruitCount + 0.5f, 0);
        fruit.GetComponent<SphereCollider>().enabled = false;
    }
}
