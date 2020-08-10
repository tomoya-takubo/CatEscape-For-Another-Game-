using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WateringCanMoving : ClickedObjectBase
{
    public SpriteRenderer goddess;   //女神
    public Image WaterAblty;   //じょうろアビリティ

    private float startPosY;    //じょうろ初期位置（Y座標）格納用
    private float timeStock = 0.0f; //じょうろ出現後累積経過時間格納用（sin関数の引数用）

    void Start()
    {
        // 初期位置取得（Y座標）
        startPosY = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // ゆらゆら上下に動く
        Waving();
    }

    /// <summary>
    /// ゆらゆら上下に動く
    /// </summary>
    public void Waving()
    {
        // 累積時間更新
        timeStock += Time.deltaTime;

        // Y軸方向の位置を更新
        this.transform.position = new Vector3(this.transform.position.x
                                            , startPosY + 0.50f * Mathf.Sin(2 * timeStock)
                                            , this.transform.position.z);
    }

    /// <summary>
    /// クリック時の処理
    /// </summary>
    public override void Clicked()
    {
        // じょうろアビリティ付与
        WaterAblty.enabled = true;          // UI表示
        AbilityManager.waterAbility = true; // 判定

        //女神表示消す
        this.goddess.gameObject.SetActive(false);

        //自身の存在を消す
        this.gameObject.SetActive(false);
    }


    public override void ChangeNekoEmotion(PlayerController playerController)
    {
        // ネコはてな
        playerController.emotion = PlayerController.EMOTION.SUPRISED;   // （☆）右辺ポイント！

        // ネコ感情絵を更新
        playerController.nekoEmotion.sprite = Resources.Load<Sprite>("EMOTIONS/EMOTION_" + ((int)playerController.emotion).ToString());

    }
}
