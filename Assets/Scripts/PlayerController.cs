using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public SpriteRenderer apple;            // りんご
    public SpriteRenderer appleBasket30;    // りんご30%
    public SpriteRenderer appleBasket50;    // りんご50%
    public float movePower = 3.0f;          // 左右の移動量

    public GameObject Wakaba;               // わかば

    public ShowWaterRating showWaterRating; // 貯水率表示

    public Image waterGauge;                // じょうろの水の量

    public SpriteRenderer wateringCan;      // じょうろ（ねこのハンド側）

    // ステージ範囲
    public Transform limitNekoPosXRight;    // 右限界
    public Transform limitNekoPosXLeft;     // 左限界

    // ネコの気持ち
    public EMOTION emotion;

    // ネコの気持ちの絵格納用
    public SpriteRenderer nekoEmotion;

    // カギ所有判定用
    public static bool hasKey = false;

    // りんご所有有無
    public static bool onApple = false;

    // カメラ
    public CameraMotion cameraMotion;

    // 判定
    bool leftChecker = true;   // 左移動限界点
    bool rightChecker = true;  // 右移動限界点

    // 感情
    public enum EMOTION
    {
        NOMAL,      // 通常
        QUESTION,   // はてな？
        SUPRISED    // びっくり！
    }

    void Update()
    {
        // 移動
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))  // 左
        {
            if(leftChecker) LButtonDown();
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))  // 右
        {
            if (rightChecker) RButtonDown();
        }
    }

    /// <summary>
    /// 左移動
    /// </summary>
    public void LButtonDown()
    {
        transform.Translate(-movePower, 0, 0);          //プレイヤー移動
        transform.localScale = new Vector3(-1, 1, 1);   //プレイヤー向き

        // （条件を満たせば）カメラ移動
        this.cameraMotion.MoveCameraPosLeftDirection(this.transform.position.x);

        //位置チェック
        LimitCheck();

        // ネコ感情絵オブジェクト取得
        Transform reactions = this.transform.Find("Reactions").transform;

        // 絵の向きをネコに合わせる
        reactions.localScale = new Vector2(-1, this.transform.localScale.y);

        // 回転をセット
        reactions.localRotation = Quaternion.Euler(0, 0, -15);

        if (!rightChecker) rightChecker = true;
    }

    /// <summary>
    /// 右移動
    /// </summary>
    public void RButtonDown()
    {
        transform.Translate(movePower, 0, 0);   //プレイヤー移動
        transform.localScale = new Vector3(1, 1, 1);    //プレイヤー向き

        // （条件を満たせば）カメラ移動
        this.cameraMotion.MoveCameraPosRightDirection(this.transform.position.x);

        //位置チェック
        LimitCheck();

        // ネコ感情絵オブジェクト取得
        Transform reactions = this.transform.Find("Reactions").transform;

        // 絵の向きをネコに合わせる
        reactions.localScale = new Vector2(1, this.transform.localScale.y);

        // 回転をセット
        reactions.localRotation = Quaternion.Euler(0, 0, 0);

        if (!leftChecker) leftChecker = true;
    }

    /// <summary>
    /// 左右移動ボタンのアクティブ/非アクティブ切り替え
    /// </summary>
    private void LimitCheck()
    {
        //右限界を超えていたら
        if(transform.position.x > limitNekoPosXRight.position.x)
        {
            rightChecker = false;
        }

        //左限界を超えていたら
        if (transform.position.x < limitNekoPosXLeft.position.x)
        {
            leftChecker = false;
        }
    }

    /// <summary>
    /// 接触処理
    /// </summary>
    /// <param name="col"></param>
    private void OnTriggerStay2D(Collider2D col)
    {
        // ClickedObjectBaseクラス取得
        if (col.gameObject.TryGetComponent(out ClickedObjectBase clickedObjectBase))
        {
            // ネコリアクション
            clickedObjectBase.ChangeNekoEmotion(this);
        }

        // りんごの木のそばにいるとき
        if(col.tag == "Tree")
        {
            // じょうろアビリティを持っていなければ
            if (!AbilityManager.waterAbility)
            {
                // 後続処理なし
                return;
            }

            // じょうろに水が残っていて、かつスペースキーが押されていたら
            if (waterGauge.fillAmount > 0 && Input.GetKey(KeyCode.Space))
            {
                // 水をあげる
                wateringCan.enabled = true;                                 // ねこにじょうろを持たせる
                WakabaBehavior wB = Wakaba.GetComponent<WakabaBehavior>();  // 木クラスを取得
                if (wB.getWater < 100.0f)
                {
                    wB.getWater += Time.deltaTime * 5f;                     // 木の貯水量更新
                }
                else
                {
                    wB.getWater = 100.0f;                                   // 100%限度
                }
                waterGauge.fillAmount -= Time.deltaTime / 10f;              // じょうろの水を減らす

                // 木を成長させる
                wB.Grow();

                // 貯水率表示更新
                showWaterRating.UpdateGettingWaterRate(wB.getWater);
            }

            // 水をあげ終えたら
            if(Input.GetKeyUp(KeyCode.Space))
            {
                // ねこに持たせていたじょうろを非表示にする
                wateringCan.enabled = false;
            }
        }

        // 水たまりを踏んでいるとき
        if (col.tag == "Puddle")
        {
            // じょうろアビリティを持っていなければ
            if(!AbilityManager.waterAbility)
            {
                // 後続処理なし
                return;
            }
        }
    }

    /// <summary>
    /// 離脱処理
    /// </summary>
    /// <param name="col"></param>
    private void OnTriggerExit2D(Collider2D col)
    {
        // 接触していたオブジェクトが離れたらネコの感情をNOMALに戻す
        this.emotion = EMOTION.NOMAL;

        // 絵を差し替える（無に）
        nekoEmotion.sprite = Resources.Load<Sprite>("EMOTIONS/EMOTION_" + ((int)emotion).ToString());
    }

    /// <summary>
    /// 感情絵の差し替え
    /// </summary>
    private void ChangeEmotionSprite(EMOTION emotion)
    {
        // 絵を差し替える  ↓（☆）重要（Projects中のResourcesフォルダ）
        this.nekoEmotion.sprite = Resources.Load<Sprite>("EMOTIONS/EMOTION_" + ((int)emotion).ToString());
    }

    /// <summary>
    /// カギの所有有無を返す
    /// </summary>
    /// <returns></returns>
    public static bool GetHasKey()
    {
        return hasKey;
    }

    /// <summary>
    /// カギを取得
    /// </summary>
    public static void SetHasKey(bool setKey)
    {
        hasKey = setKey;
    }
}
