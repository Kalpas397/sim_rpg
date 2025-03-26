using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int a = 5, b = 2;
        float f = 0.5f;
        Debug.Log($"{a % b}");
        Debug.Log($"fは{f}です");
        Debug.Log($"{a, 3}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
