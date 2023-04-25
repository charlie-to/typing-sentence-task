using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// 被験者の情報を格納するクラス
public class Participant
{
    readonly int id; // 被験者のID

    //　被験者番号が正しいかどうかをLuhn formulaで判定する
    public bool IsValidId(int id)
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
        Debug.Log(sum);
        return sum % 10 == id % 10;
    }
}
