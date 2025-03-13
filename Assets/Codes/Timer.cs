using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// カウントダウンタイマー
/// ゲームマネージャーが参照
/// </summary>

public class Timer : MonoBehaviour
{
    [SerializeField] private bool _isStopTimer = true;   // trueの時タイマーが停止
    [SerializeField] private float _nowTime = 0.0f; // 現在のタイム
    [SerializeField] private float _limitTime = 60.0f;  // 1ラウンドの制限時間

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI textTime;

    public bool IsStopTimer { get => _isStopTimer; set => _isStopTimer = value; }
    public float NowTime { get => _nowTime; set => _nowTime = value; }

    // Start is called before the first frame update
    void Start()
    {
        _nowTime = _limitTime;
        textTime.text = Mathf.Ceil(_nowTime).ToString();
        // textTime.text = _nowTime.ToString("F0");    // 現在のタイムを整数で表示
    }

    // Update is called once per frame
    void Update()
    {
        CountdownTimer();
    }

    // タイマーのカウントダウン
    private void CountdownTimer()
    {
        if (!_isStopTimer)
        {
            if (_nowTime > 0)
            {
                _nowTime -= Time.deltaTime;
                textTime.text = Mathf.Ceil(_nowTime).ToString();    // 現在のタイムを整数で表示
                // textTime.text = _nowTime.ToString("F0");    // 現在のタイムを整数で表示
            }
            else
            {
                Debug.Log("Time up!");
                _isStopTimer = true;
                // ゲームマネージャーにラウンド終了宣言が伝わる
            }
        }
    }

    // タイマーをリセット
    public void ResetTimer()
    {
        _nowTime = _limitTime;
    }
}
