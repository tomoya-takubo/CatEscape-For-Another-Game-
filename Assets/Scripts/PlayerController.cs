using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public SpriteRenderer apple;    //りんご
    public SpriteRenderer appleBasket30;    //りんご30%
    public SpriteRenderer appleBasket50;    //りんご50%
    public float movePower = 3.0f; //左右の移動量
    public static bool isCatching = false; //りんごを持っているか

    public GameObject Wakaba;   //わかば

    public Text getWaterRatio;  //貯水率

    public Image waterGauge;    //じょうろの水の量

    public SpriteRenderer wateringCan;  //じょうろ（ねこのハンド側）

    //ボタン
    public Button buttonR;  //右
    public Button buttonL;  //左

    //ステージ範囲
    public float limitPosXRight; //右上限
    public float limitPosXLeft; //左上限


    private int appleNum = 7;   //木になっているりんごのナンバー管理
    private int n;  //カメラ移動のための変数

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        /*
        // スペースキーが押されていたら
        if (Input.GetKey(KeyCode.Space))
        {
            // 水ゲージを回復
            GameObject director = GameObject.Find("GameDirector");
            director.GetComponent<GameDirector>().IncreaseWater();
        }
        */

        // 水をあげる
        // スペースキー押下
        if (Input.GetKey(KeyCode.Space))
        {
            // 水をやる
            Water();

        }
        else
        {
            // ねこの手のじょうろを消す
            wateringCan.enabled = false;
        }
    }

    public void RButtonDown()
    {
        transform.Translate(movePower, 0, 0);   //プレイヤー移動
        transform.localScale = new Vector3(1, 1, 1);    //プレイヤー向き
        if(transform.position.x >= 10 + n * 18)
        {
            //カメラ移動
            Camera.main.transform.position += new Vector3(18, 0, 0);

            //n更新
            n++;

        }

        //位置チェック
        LimitCheck();

        Debug.Log("右のボタンが押されています！");
    }

    public void LButtonDown()
    {
        transform.Translate(-movePower, 0, 0);  //プレイヤー移動
        transform.localScale = new Vector3(-1, 1, 1);    //プレイヤー向き
        if (transform.position.x <= -10 + n * 18)
        {
            //カメラ移動
            Camera.main.transform.position -= new Vector3(18, 0, 0);

            //n更新
            n--;

        }

        //位置チェック
        LimitCheck();
    }

    /// <summary>
    /// 左右移動ボタンのアクティブ/非アクティブ切り替え
    /// </summary>
    private void LimitCheck()
    {
        //いったん左右ボタンをいずれもアクティブに
        buttonR.interactable = true;
        buttonL.interactable = true;

        //右限界を超えていたら
        if(transform.position.x > limitPosXRight)
        {
            //右移動ボタンを非アクティブ
            buttonR.interactable = false;
        }

        //左限界を超えていたら
        if (transform.position.x < limitPosXLeft)
        {
            //右移動ボタンを非アクティブ
            buttonL.interactable = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //木の場合
        if (col.tag == "tree")
        {
            //猫ちゃんがりんごを持っていなかったら
            if (!isCatching)
            {
                //猫ちゃんの頭にりんごをのせる
                apple.enabled = true;
                isCatching = true;

                //木になっているりんごを一つ減らす
                GameObject appleDisapp = GameObject.Find("fruit_ringo (" + appleNum.ToString() + ")");
                appleDisapp.GetComponent<SpriteRenderer>().enabled = false;
                appleNum--; //非表示対象のりんごの番号を１下げる

            }

        }
        //かごの場合
        else if (col.tag == "basket")
        {
            //猫ちゃんがりんごを持っていたら
            if (isCatching)
            {
                //猫ちゃんの頭のりんごをはずす
                apple.enabled = false;
                isCatching = false; //キャッチフラグをfalseに

                //りんご3分盛が非表示であれば
                if(appleBasket30.enabled == false)
                {
                    //かごを3分盛りにする
                    appleBasket30.enabled = true;
                }
                //りんご5分盛が非表示であれば
                else if (appleBasket50.enabled == false)
                {
                    //かごを5分盛する
                    appleBasket50.enabled = true;
                }
            }

        }

    }

    void Water()
    {
        //じょうろアビリティを持っていなければ
        if (!AbilityManager.waterAbility)
        {
            return;
        }

        //以下、じょうろアビリティがある前提

        WakabaBehavior wB = Wakaba.GetComponent<WakabaBehavior>();

        //わかばの付近かつじょうろに水が残っていたら
        if (wB.nearPlayer && waterGauge.fillAmount > 0)
        {
            //ねこのハンドにじょうろ
            wateringCan.enabled = true;

            //貯水（わかばサイド）
            wB.getWater += Time.deltaTime * 5f;

            //じょうろの水を減らす
            waterGauge.fillAmount -= Time.deltaTime / 10f;

            //貯水率更新
            getWaterRatio.text
                = (100f * wB.getWater / wB.waterMaxValue).ToString("F2") + "%";

            //木の絵を変える
            if (100f * wB.getWater / wB.waterMaxValue >= 30.0f
                    && 100f * wB.getWater / wB.waterMaxValue < 60.0f)    //木（裸）
            {
                //裸の木に変更
                //this.gameObject.SetActive(false); //←ミス
                GameObject.Find("wakaba").GetComponent<SpriteRenderer>().enabled = false;
                GameObject.Find("nakedTree").GetComponent<SpriteRenderer>().enabled = true;
            }
            else if(100f * wB.getWater / wB.waterMaxValue >= 60.0f
                    && 100f * wB.getWater / wB.waterMaxValue < 80.0f)   //木（葉っぱ）
            {
                //葉のついた木に変更
                //this.gameObject.SetActive(false); //←ミス
                GameObject.Find("nakedTree").GetComponent<SpriteRenderer>().enabled = false;
                GameObject.Find("tree").GetComponent<SpriteRenderer>().enabled = true;
            }
            else if(100f * wB.getWater / wB.waterMaxValue >= 80.0f
                    && 100f * wB.getWater / wB.waterMaxValue < 100.0f)  //木（実）
            {
                //実のなった木に変更
                //this.gameObject.SetActive(false); //←ミス
                GameObject.Find("tree").GetComponent<SpriteRenderer>().enabled = false;
                //GameObject.Find("tree(with apples)").GetComponent<SpriteRenderer>().enabled = true;
                wB.treeWithApples.SetActive(true);
            }
        }
    }
}
