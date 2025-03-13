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
    [SerializeField] private GuideText guideText;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI textHoldNum;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ThreeCount());
    }

    // Update is called once per frame
    void Update()
    {
        CountPlayerHoldingFruits();
        
    }

    // プレイヤーが所持しているフルーツの数をカウント
    void CountPlayerHoldingFruits()
    {
        if (playerCollision)
        {
            textHoldNum.text = playerCollision.FruitCount.ToString();
        }
    }

    IEnumerator ThreeCount()
    {
        StartCoroutine(guideText.AppearTextPropertiesOverTime("3", 0.5f, false));
        yield return new WaitForSeconds(1f);
        StartCoroutine(guideText.AppearTextPropertiesOverTime("2", 0.5f, false));
        yield return new WaitForSeconds(1f);
        StartCoroutine(guideText.AppearTextPropertiesOverTime("1", 0.5f, false));
        yield return new WaitForSeconds(1f);
        StartCoroutine(guideText.AppearTextPropertiesOverTime("うんこうんこうんこ！！！", 3f, true));
    }
}
