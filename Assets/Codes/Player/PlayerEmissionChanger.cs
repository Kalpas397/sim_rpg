using System;   // Array.Resizeを使用
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの無敵時の明滅表現
/// </summary>
public class PlayerEmissionChanger : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Renderer[] objectRenderer; // 対象のオブジェクトのレンダラー
    [SerializeField] private Material[] material;
    [SerializeField] private float frequency = 1.0f; // sin波の周波数
    [SerializeField] private float amplitude = 1.0f; // sin波の振幅
    private float time;

    void Awake()
	{
		objectRenderer = GetComponentsInChildren<Renderer>(); 
        Array.Resize(ref material, objectRenderer.Length);
        // マテリアルを取得
        for (int i = 0; i < objectRenderer.Length; i++)
        {
            material[i] = objectRenderer[i].material;
        }
	}

    void Start()
    {
        
    }

    void Update()
    {
        if (playerController)
        {
            if (playerController.IsInvincible)
            {
                EmissionChange();
            }
            else
            {
                // エミッションカラーを設定
                for (int i = 0; i < objectRenderer.Length; i++)
                {
                    // マテリアルの状態を戻す。
                    Color emissionColor = new Color(0f, 0f, 0f);
                    material[i].SetColor("_EmissionColor", emissionColor);
                }
            }
        }
    }

    void EmissionChange()
    {
        // 時間経過に基づいてsin関数を計算
        time += Time.deltaTime;
        float sinValue = Mathf.Sin(time * frequency) * amplitude;

        // sin関数の値を0から1の範囲に変換し、0.8倍する
        float emissionValue =  ((sinValue + 1.0f) / 2.0f) * 0.5f;

        // Debug.Log("emissioncolor: " + emissionValue);

        // RGB値を設定
        Color emissionColor = new Color(emissionValue, emissionValue, emissionValue);

        // エミッションカラーを設定
        for (int i = 0; i < objectRenderer.Length; i++)
        {
            material[i].SetColor("_EmissionColor", emissionColor);
            // DynamicGI.SetEmissive(objectRenderer[i], emissionColor * 2.0f); // 強度を調整
        }
    }
}