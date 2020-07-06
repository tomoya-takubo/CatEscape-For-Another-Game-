using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WateringCanMoving : MonoBehaviour
{
    public SpriteRenderer goddess;   //女神
    public Image WaterAblty;   //じょうろアビリティ

    private float startPosY;    //じょうろ初期位置（Y座標）格納用
    private float timeStock = 0.0f; //じょうろ出現後累積経過時間格納用（sin関数の引数用）

    void Start()
    {
        startPosY = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        //ゆらゆら待機
        timeStock += Time.deltaTime;
        this.transform.position = new Vector3(this.transform.position.x
                                            , startPosY + 0.50f * Mathf.Sin(2 * timeStock)
                                            , this.transform.position.z);

        //クリックされたら
        if (Input.GetMouseButtonDown(0))
        {
            //レーザー照射
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //レーザーで当たったオブジェクトを取得
            RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

            //何かしら取得でき、それが自分自身だったら
            if(hit2d && hit2d.transform.gameObject == this.gameObject)
            {
                //自身の存在を消す
                this.gameObject.SetActive(false);

                //女神表示消す
                this.goddess.gameObject.SetActive(false);

                //じょうろアビリティ付与
                WaterAblty.enabled = true;  //UI表示
                AbilityManager.waterAbility = true; //判定
            }
        }
    }
}
