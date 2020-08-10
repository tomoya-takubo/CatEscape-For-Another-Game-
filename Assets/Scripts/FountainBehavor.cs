using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FountainBehavor : ClickedObjectBase
{
    public GameObject goddess;  //女神
    public GameObject WateringCan;  //じょうろ

    /// <summary>
    /// クリックされたときの処理
    /// </summary>
    public override void Clicked()
    {
        // ネコが近くにいれば
        if (this.nearCat)
        {
            // 女神を表示
            goddess.SetActive(true);

            // じょうろを表示
            WateringCan.SetActive(true);

            // コライダーを消す
            this.GetComponent<CapsuleCollider2D>().enabled = false;
        }
    }

    /// <summary>
    /// ネコの感情を変える
    /// </summary>
    /// <param name="playerController"></param>
    public override void ChangeNekoEmotion(PlayerController playerController)
    {
        base.ChangeNekoEmotion(playerController);
    }
}
