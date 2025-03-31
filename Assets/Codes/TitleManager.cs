using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textScoreNum;

    // Start is called before the first frame update
    void Start()
    {
        textScoreNum.text = Statics.defeatEnemyNum.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
