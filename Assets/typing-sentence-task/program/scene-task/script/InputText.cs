using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class InputText : MonoBehaviour
{
    //入力したテキスト
    public TextData TextData { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        TextData = new TextData();
    }

    public void OnEnable()
    {
        // イベントの登録
        Keyboard.current.onTextInput += OnTextInput;
    }
    public void OnDisable()
    {
        // イベントの登録解除
        Keyboard.current.onTextInput -= OnTextInput;
    }
    public void OnTextInput(char ch)
    {
        // 入力した文字を表示
        Debug.Log(ch);
        //TextData.AddText(ch);
    }

    // Update is called once per frame
    void Update()
    {
    }
}