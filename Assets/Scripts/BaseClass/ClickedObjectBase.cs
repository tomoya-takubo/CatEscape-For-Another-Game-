using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class ClickedObjectBase : MonoBehaviour
{
    // 判定
    public bool nearCat;      // ネコが近くにいる

    /// <summary>
    /// 接触判定（進行形）
    /// </summary>
    /// <param name="col"></param>
    protected virtual void OnTriggerStay2D(Collider2D col)
    {
        // 近くにネコがいれば
        if (col.tag == "Player")
        {
            // onCatフラグをtrueに
            this.nearCat = true;
        }
    }

    /// <summary>
    /// 離脱判定
    /// </summary>
    /// <param name="col"></param>
    protected virtual void OnTriggerExit2D(Collider2D col)
    {
        // ネコが離れれば
        if (col.tag == "Player")
        {
            // onCatフラグをfalseに
            this.nearCat = false;
        }
    }

    /// <summary>
    /// クリックされたときの処理
    /// </summary>
    public virtual void Clicked()
    {

    }

    /// <summary>
    /// ネコの感情を変化させる
    /// </summary>
    public virtual void ChangeNekoEmotion(PlayerController playerController)
    {
        // ネコはてな
        playerController.emotion = PlayerController.EMOTION.QUESTION;   // （☆）右辺ポイント！

        // ネコ感情絵を更新
        playerController.nekoEmotion.sprite = Resources.Load<Sprite>("EMOTIONS/EMOTION_" + ((int)playerController.emotion).ToString());
    }
}
