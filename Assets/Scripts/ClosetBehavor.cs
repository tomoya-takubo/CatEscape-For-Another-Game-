using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosetBehavor : ClickedObjectBase
{
    // 絵
    public GameObject openingCloset;        // クローゼット（開いている）

    // 効果音
    public AudioClip gachaGacha;            // ガチャガチャ音
    public AudioClip unLocking;             // 鍵が開く音

    // 判定
    public static bool unlock = false;      // アンロック判定

    // 他オブジェクト紐づけ
    public GameDirector gmDic;              // ゲームディレクター


    /// <summary>
    /// クリック処理
    /// </summary>
    public override void Clicked()
    {
        // ネコが近くにいる
        if(this.nearCat)
        {
            // ネコがカギを持っている
            if (PlayerController.GetHasKey())
            {
                // カギが開く音を再生
                this.GetComponent<AudioSource>().PlayOneShot(unLocking);

                // 開放判定をtrueにする
                unlock = true;

                // カギアイコンを非表示にする
                this.gmDic.SetKeyIcon(false);

                // 「開いているクローゼット」の絵を表示する
                this.GetComponent<SpriteRenderer>().enabled = false;    // 閉じた絵を非表示
                openingCloset.SetActive(true);                          // 開いた絵のSetActiveをtrueに

                // ネコのカギ所有判定をfalseにする
                PlayerController.SetHasKey(false);

                // 自分自身を消す
                Destroy(this.gameObject, 0.5f);

            }
            // ネコをカギを持っていない
            else
            {
                // ガチャガチャ音を鳴らす
                this.GetComponent<AudioSource>().PlayOneShot(this.gachaGacha, 0.5f);
            }
        }
    }

    /// <summary>
    /// ネコの感情を変える
    /// </summary>
    public override void ChangeNekoEmotion(PlayerController playerController)
    {
        // ネコはてな（デフォルト）
        base.ChangeNekoEmotion(playerController);
    }
}
