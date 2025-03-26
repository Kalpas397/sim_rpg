using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TMPtest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var obj = GameObject.Find("Text (TMP)");

        var tmpro = obj.GetComponent<TextMeshProUGUI>();
        tmpro.text = "好きな文字を書いてみてください";
        tmpro.text += "\n追記もできます";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnValueChanged()
    {
        var obj1 = GameObject.Find("Text (TMP)");
        var tmpro = obj1.GetComponent<TextMeshProUGUI>();
        var obj2 = GameObject.Find("InputField (TMP)");
        var inputField = obj2.GetComponent<TMP_InputField>();
        tmpro.text = inputField.text;
    }
}
