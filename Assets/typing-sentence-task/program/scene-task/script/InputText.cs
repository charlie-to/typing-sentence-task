using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using TMPro;
using Assets.InputLogs.program;
using System.Text.RegularExpressions;
public class InputText : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    //入力したテキスト
    public TextData TextData { get; set; }

    // インプットロガーのインスタンス
    public InputsStorage inputsStorage;
    // タスクのセーバー
    public SaveTaskInputData saveTaskInputData;


    // Start is called before the first frame update
    void Start()
    {
        TextData = new TextData();
        // セーブするためのインスタンスを作成
        inputsStorage = new InputsStorage("InputText", "test");
        saveTaskInputData = new SaveTaskInputData("/Out/Logs/");
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
        // // backspaceなら文字を削除
        // if (ch == '\u007F')
        // {
        //     TextData.DeleteChar();
        //     // InputDatumを追加
        //     inputsStorage.AddInputDatum("Backspace");
        //     return;
        // }
        // // 改行ならLineをLinesに追加して初期化
        // if (ch == '\u000d')
        // {
        //     TextData.NextLine();
        //     // InputDatumを追加
        //     inputsStorage.AddInputDatum("Enter");
        //     return;
        // }
        // 基本的なラテン文字なら文字を追加
        if (!(Regex.IsMatch(ch.ToString(), @"\p{P}\d")) && ch != '\u000d' && ch != '\u007F' && ch != '\u001B' && ch != '\u0020')
        {
            TextData.AddChar(ch);
            // InputDatumを追加
            inputsStorage.AddInputDatum(ch.ToString());
            return;
        }
        // // escapeならタスクを終了
        // if (ch == '\u001B')
        // {
        //     TaskManager.Instance.EndTask();
        //     // InputDatumを追加
        //     // inputsStorage.AddInputDatum("Escape");
        //     return;
        // }
        // それ以外の文字は無視
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text = TextData.LinesToString();
        // enterキーで改行
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            TextData.NextLine();
            // InputDatumを追加
            inputsStorage.AddInputDatum("Enter");
        }
        // backspaceキーで文字を削除
        if (Keyboard.current.backspaceKey.wasPressedThisFrame)
        {
            TextData.DeleteChar();
            // InputDatumを追加
            inputsStorage.AddInputDatum("Backspace");
        }
        // spaceキーでスペースを追加
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            TextData.AddSpace();
            // InputDatumを追加
            inputsStorage.AddInputDatum("Space");
        }
        // escapeキーでタスクを終了
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            TaskManager.Instance.EndTask();
        }
    }
}