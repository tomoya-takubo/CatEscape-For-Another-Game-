using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBehavor : ClickedObjectBase
{
    private GameDirector gmDic;

    void Start()
    {
        // GameDirectorクラスを取得
        this.gmDic = GameObject.FindWithTag("GameDirector").GetComponent<GameDirector>();
    }

    /// <summary>
    /// クリック処理
    /// </summary>
    public override void Clicked()
    {
        // ネコが近くにいれば
        if (this.nearCat)
        {
            // カギ所有判定をtrueにする
            PlayerController.SetHasKey(true);

            // カギアイコンを表示する
            this.gmDic.SetKeyIcon(true);

            // 消滅する
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// ネコ感情を変える
    /// </summary>
    public override void ChangeNekoEmotion(PlayerController playerController)
    {
        // ネコはてな
        playerController.emotion = PlayerController.EMOTION.SUPRISED;   // （☆）右辺ポイント！

        // ネコ感情絵を更新
        playerController.nekoEmotion.sprite = Resources.Load<Sprite>("EMOTIONS/EMOTION_" + ((int)playerController.emotion).ToString());

    }
}
