using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassBehavor : MonoBehaviour
{
    public PlayerReaction nekoReaction; // ねこリアクション

    private bool isGrass = true; // 草があるか

    /*
    /// <summary>
    /// 草を取る
    /// </summary>
    public void TakeGrass()
    {
        // 自分自身のSetActiveをOFFにする
        this.gameObject.SetActive(false);
    }
    */

    void OnTriggerStay2D(Collider2D col)
    {
        // ねこリアクション
        // くさがいれば
        if (isGrass)
        {
            // ねこ疑問
            nekoReaction.Reaction(PlayerReaction.PLAYER_REANCTIONS.QUESTION);

            // Debug.Log("そばに草があります！");
        }
        /*
        // タンスが開いていれば
        else
        {
            // ねこびっくり
            nekoReaction.Reaction(PlayerReaction.PLAYER_REANCTIONS.EXCLAMATION);
        }
        */

        // クリック時イベント
        if (Input.GetMouseButtonUp(0))
        {
            // タンスがタッチされたら
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    // マウスポインタからレーザ発射
            RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);  // レーザーに当たったオブジェクトを取得
            if (hit2d && hit2d.collider.gameObject == this.gameObject)  // レーザーに当たったのが自分
            {
                // Debug.Log("クリックして草を検知しました！");

                // SetActiveをOFFに
                this.gameObject.SetActive(false);
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
