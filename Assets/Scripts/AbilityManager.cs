using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager
{
    public static bool waterAbility;   // じょうろアビリティ持ってるかどうか
    public static bool scissorAbility; // はさみアビリティ持ってるかどうか

    //初期値
    public AbilityManager() // コンストラクタ（別名：イニシャライザー。インスタンシエイトしたときに必ず初期化する）
    {
        waterAbility = false;   // じょうろアビリティ初期化
        scissorAbility = false; // はさみアビリティ初期化

        // デバッグログ出力
        Debug.Log("waterAbility: " + waterAbility);
        Debug.Log("scissorAbility: " + scissorAbility);
    }
}
