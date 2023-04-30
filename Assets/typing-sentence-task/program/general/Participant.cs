using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace typing_sentence_task.program.general
{
    

// 被験者の情報を格納するクラス
    public static class Participant
    {
        public static int participantId;

        // 
        public static string outputDir => "/Out/Logs/" + participantId.ToString() + "/";
    
        // 被験者番号を設定する
        public static void SetId(int id)
        {
            if (IsValidId(id))
            {
                participantId = id;
            }
            else
            {
                throw new System.Exception("Invalid ID");
            }
        }

        //　被験者番号が正しいかどうかをLuan formulaで判定する
        private static bool IsValidId(int id)
        {
            int sum = 0;
            int _id = (int)(id / 10);
            int[] digits = _id.ToString().ToCharArray().Select(x => (int)char.GetNumericValue(x)).ToArray();
            for (int i = 0; i < digits.Length; i++)
            {
                if (i % 2 == 0)
                {
                    sum += digits[i];
                }
                else
                {
                    if (digits[i] < 5)
                    {
                        sum += digits[i] * 2;
                    }
                    else
                    {
                        sum += (digits[i] * 2 - 9);
                    }
                }
            }
            // Debug.Log(sum);
            return sum % 10 == id % 10;
        }
    }

}