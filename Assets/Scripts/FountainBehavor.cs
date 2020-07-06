using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FountainBehavor : MonoBehaviour
{
    public SpriteRenderer goddess;  //女神
    public GameObject WateringCan;  //じょうろ

    private bool isFoutain; //泉が出現しているか

    void Start()
    {
        isFoutain = false;  //スタート時は泉未出現
    }

    // Update is called once per frame
    void Update()
    {
        //泉未出現でマウスがクリックされたら
        if(Input.GetMouseButtonDown(0) && !isFoutain)
        {
            //スクリーンポイントからポイントからレーザー照射
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //レーザーに当たったものを取得
            RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

            //何かがレーザーに当たり、それが自分だったら
            if(hit2d && hit2d.transform.gameObject == this.gameObject)
            {
                //女神を表示させる
                goddess.enabled = true;

                //じょうろを出現させる
                WateringCan.SetActive(true);

                //泉出現フラグを立てる
                isFoutain = true;

            }
        }
    }
}
