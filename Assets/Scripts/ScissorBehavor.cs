using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScissorBehavor : MonoBehaviour
{
    public PlayerReaction nekoReaction; //ねこリアクション
    public Image Scissors;    //はさみアビリティ

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //タッチされたら
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    //カメラからビーム飛ばす
            RaycastHit2D hit2d
                = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);   //ビームに当たったオブジェクトを取得
            if(hit2d && hit2d.collider.gameObject == this.gameObject)
            {
                Debug.Log("はさみがクリックされました！");

                //ねこにはさみアビリティを付与する
                this.gameObject.SetActive(false);    //はさみを消す
                Scissors.enabled = true;    //キャンバスにはさみ出現
                AbilityManager.scissorAbility = true;   //はさみアビリティON

                //ねこのリアクションを平常に戻す
                nekoReaction.Reaction(PlayerReaction.PLAYER_REANCTIONS.NONE);
            }

        }

        //はさみアビリティを取得する
    }
}
