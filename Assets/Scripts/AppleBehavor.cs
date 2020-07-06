using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleBehavor : MonoBehaviour
{
    //猫の頭のりんご
    public SpriteRenderer onCatHeadApple;
    
    // Update is called once per frame
    void Update()
    {
        //マウスがクリックされたら
        if (Input.GetMouseButtonDown(0))
        {
            //クリックしたオブジェクトを取得する
            GameObject clickedGameObject = null;
            Ray ray
                = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2d
                = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

            //Debug.Log("hit2d：" + hit2d.collider.gameObject);

            //自分がクリックされていたら
            if (hit2d && hit2d.collider.gameObject == this.gameObject)
            {
                //猫がりんごをもっていなければ
                if (!onCatHeadApple.enabled)
                {
                    //ねこにりんごを持たせる
                    onCatHeadApple.enabled = true;
                    PlayerController.isCatching = true;

                    //自分自身を消す
                    Destroy(this.gameObject);
                }
            }

        }
    }
}
