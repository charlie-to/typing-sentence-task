using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class SubmitButton : MonoBehaviour
{
    // 被験者番号の入力欄
    [SerializeField] TMP_InputField inputField;
    // エラーメッセージ
    [SerializeField] TMP_Text errorMessage;
    // 被験者の情報を格納するクラス
    readonly Participant participant = new Participant();
    // ボタン押下時の処理
    public void CheckAndGoNextScene()
    {
        // エラーメッセージを非表示にする
        errorMessage.text = "";
        // 被験者番号の入力値を取得
        int id = int.Parse(inputField.text);
        // 被験者番号が正しいかどうかを判定
        if (participant.IsValidId(id))
        {
            // 被験者番号を保存
            // 次のシーンに遷移
            SceneManager.LoadScene("scene-WaitMri");
        }
        else
        {
            // 被験者番号が正しくない場合はエラーメッセージを表示
            errorMessage.text = "Please type a valid ID.";
        }
    }
}
