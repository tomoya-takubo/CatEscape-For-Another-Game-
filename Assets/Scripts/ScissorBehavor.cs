using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScissorBehavor : ClickedObjectBase
{
    public Abolition_PlayerReaction nekoReaction;     // ねこリアクション
    public Image Scissors;                  // はさみアビリティ

    public override void ChangeNekoEmotion(PlayerController playerController)
    {
        // ネコはてな
        playerController.emotion = PlayerController.EMOTION.SUPRISED;   // （☆）右辺ポイント！

        // ネコ感情絵を更新
        playerController.nekoEmotion.sprite = Resources.Load<Sprite>("EMOTIONS/EMOTION_" + ((int)playerController.emotion).ToString());
    }

    /// <summary>
    /// クリック処理
    /// </summary>
    public override void Clicked()
    {
        if (this.nearCat)
        {
            // キャンバスにはさみ出現
            this.Scissors.enabled = true;

            // 自身を消す
            Destroy(this.gameObject);
        }
    }
}
