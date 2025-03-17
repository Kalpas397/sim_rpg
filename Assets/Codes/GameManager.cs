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
    [SerializeField] private Timer timer;

    private bool _hasExecutedRthree = false; // 残り３秒のカウント表示が実行されたか

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI textHoldNum;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ThreeCountBegin());
    }

    // Update is called once per frame
    void Update()
    {
        CountPlayerHoldingFruits();
        
        // ラウンド終了残り３秒前の合図
        if (timer.NowTime <= 3f && !_hasExecutedRthree)
        {
            StartCoroutine(ThreeCountEnd());
            _hasExecutedRthree = true;
        }
        
    }

    // プレイヤーが所持しているフルーツの数をカウント
    void CountPlayerHoldingFruits()
    {
        if (playerCollision)
        {
            textHoldNum.text = playerCollision.FruitCount.ToString();
        }
    }

    // ラウンド開始前の3カウント
    IEnumerator ThreeCountBegin()
    {
        StartCoroutine(guideText.AppearTextPropertiesOverTime("3", 0.5f, false));
        yield return new WaitForSeconds(1f);
        StartCoroutine(guideText.AppearTextPropertiesOverTime("2", 0.5f, false));
        yield return new WaitForSeconds(1f);
        StartCoroutine(guideText.AppearTextPropertiesOverTime("1", 0.5f, false));
        yield return new WaitForSeconds(1f);
        StartCoroutine(guideText.AppearTextPropertiesOverTime("爆弾をゴールへ運ぼう", 3f, true));

        timer.IsStopTimer = false;
    }

    // ラウンド終了前の3カウント
    IEnumerator ThreeCountEnd()
    {
        StartCoroutine(guideText.AppearTextPropertiesOverTime("3", 0.5f, false));
        yield return new WaitForSeconds(1f);
        StartCoroutine(guideText.AppearTextPropertiesOverTime("2", 0.5f, false));
        yield return new WaitForSeconds(1f);
        StartCoroutine(guideText.AppearTextPropertiesOverTime("1", 0.5f, false));
        yield return new WaitForSeconds(1f);

        // ノルマを達成できていない場合はゲームオーバー
        StartCoroutine(guideText.AppearTextPropertiesOverTime("ラウンド終了", 3f, true));

        timer.IsStopTimer = true;
    }

}
