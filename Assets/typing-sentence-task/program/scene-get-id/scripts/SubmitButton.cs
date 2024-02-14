
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using typing_sentence_task.program.general;


public class SubmitButton : MonoBehaviour
{
    // 被験者番号の入力欄
    [SerializeField] TMP_InputField inputField;
    // エラーメッセージ
    [SerializeField] TMP_Text errorMessage;

    private void Start()
    {
        inputField = GameObject.Find("InputField-ID").GetComponent<TMP_InputField>();
        errorMessage = GameObject.Find("Text-Warning").GetComponent<TMP_Text>();
        // 被験者番号の入力欄にフォーカスする
        inputField.ActivateInputField();
    }


    // ボタン押下時の処理
    public void CheckAndGoNextScene()
    {
        // エラーメッセージを非表示にする
        errorMessage.text = "";
        // 被験者番号の入力値を取得
        int id = int.Parse(inputField.text);
        try
        {
            Participant.SetId(id);
            
        }catch
        {
            errorMessage.text = "Please type a valid ID.";
            return;
        }
        
        // 被験者番号が正しい場合は次のシーンに遷移する
        SceneManager.LoadScene("scene-task-choice");
    }
}
