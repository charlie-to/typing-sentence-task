using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using TMPro;
public class InputText : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
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
        Debug.Log($"{(int)ch:x4}");
        // backspaceなら文字を削除
        if (ch == '\u007F')
        {
            TextData.DeleteChar();
            return;
        }
        // 改行ならLineをLinesに追加して初期化
        if (ch == '\u000d')
        {
            TextData.NextLine();
            return;
        }
        TextData.AddChar(ch);
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text = TextData.LinesToString();
    }
}