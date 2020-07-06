using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosetBehavor : MonoBehaviour
{
    public Image scissors;  // はさみアビリティ
    public GameObject closetClosed; // クローゼット（未開放）
    public GameObject closetOpen;   // クローゼット（開放）
    public GameObject hasami;   // はさみ
    public PlayerReaction nekoReaction; // ねこリアクション

    private bool isOpenCloset = false;    // タンスが開いているか

    void OnTriggerStay2D(Collider2D col)
    {
        // はさみアビリティ取得してれば処理なし
        if (AbilityManager.scissorAbility)
        {
            return;
        }

        // ねこリアクション
        // タンスが閉まっていれば
        if (!isOpenCloset)
        {
            // ねこ疑問
            nekoReaction.Reaction(PlayerReaction.PLAYER_REANCTIONS.QUESTION);
        }
        // タンスが開いていれば
        else
        {
            // ねこびっくり
            nekoReaction.Reaction(PlayerReaction.PLAYER_REANCTIONS.EXCLAMATION);
        }

        // クリック時イベント
        if (Input.GetMouseButtonUp(0))
        {
            // レーザーを飛ばす
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    // マウスポインタからレーザ発射
            RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);  // レーザーに当たったオブジェクトを取得
            
            // レーザーがクローゼットに当たったら
            if (hit2d && hit2d.collider.gameObject == this.gameObject)  // レーザーに当たったのが自分
            {
                // タンスを開放する
                closetClosed.SetActive(false);  // 引き出しが閉まっている絵を消す
                closetOpen.SetActive(true); // 引き出しが開いている絵を表示する
                hasami.SetActive(true); // はさみ絵を表示する
                isOpenCloset = true;    // 開放判定をONにする
            }
        }
    }

    /// <summary>
    /// 接触状態から抜けたとき、リアクションをなしに
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerExit2D(Collider2D col)
    {
        nekoReaction.Reaction(PlayerReaction.PLAYER_REANCTIONS.NONE);
    }
}
