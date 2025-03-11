using UnityEngine;
using TMPro;
using System.Collections;

public class TextGuide : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI uiText; // 対象のTextMeshProオブジェクト
    [SerializeField] private float duration = 2.0f; // 遷移にかかる時間（秒）
    [SerializeField] private Vector3 targetScale = new Vector3(1, 1, 1); // 目標のスケール
    [SerializeField] private Color targetColor = new Color(1, 1, 1, 0); // 目標の色（透明度を含む）

    private Vector3 initialScale;
    private Color initialColor;

    void Start()
    {
        initialScale = uiText.transform.localScale;
        initialColor = uiText.color;
        StartCoroutine(AppearTextPropertiesOverTime());
    }

    // テキストの出現
    // あとで引数で消失までの時間を決められるようにする
    IEnumerator AppearTextPropertiesOverTime()
    {
        float time = 0;

        while (time < duration)
        {
            // 経過時間に基づいて補間値を計算
            float t = time / duration;

            // スケールを補間
            uiText.transform.localScale = Vector3.Lerp(initialScale * 2, targetScale, t);

            // 色（透明度）を補間
            uiText.color = Color.Lerp(targetColor, initialColor, t);

            // 次のフレームまで待機
            time += Time.deltaTime;
            yield return null;
        }

        // 最終的なスケールと色を設定
        uiText.transform.localScale = targetScale;
        uiText.color = initialColor;

        yield return new WaitForSeconds(3f);
        StartCoroutine(DisappearTextPropertiesOverTime());
    }

    // テキストの消失
    // あとで小さくなって消えるかを引数で選択できるようにする
    IEnumerator DisappearTextPropertiesOverTime()
    {
        float time = 0;

        while (time < duration)
        {
            // 経過時間に基づいて補間値を計算
            float t = time / duration;

            // スケールを補間
            // uiText.transform.localScale = Vector3.Lerp(initialScale, new Vector3(0, 0, 0), t);

            // 色（透明度）を補間
            uiText.color = Color.Lerp(initialColor, targetColor, t);

            // 次のフレームまで待機
            time += Time.deltaTime;
            yield return null;
        }

        // 最終的なスケールと色を設定
        // uiText.transform.localScale = targetScale;
        uiText.color = targetColor;
    }

    
}
