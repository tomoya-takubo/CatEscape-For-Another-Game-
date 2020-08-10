using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakabaBehavior : ClickedObjectBase
{
    // 貯水量（受け取った水の量）管理
    public float getWater = 0.0f;

    // 木の差し替え絵格納用
    public Sprite[] tree;

    // SpriteRendererコンポ格納用
    private SpriteRenderer sprRen;

    // りんごプレハブ
    public AppleBehavor applePrefab;

    // りんごの実管理用のリスト
    private List<AppleBehavor> apples;

    // りんごの実の位置リスト
    public List<Vector2> applesPos;

    void Start()
    {
        // SpriteRendererコンポ取得
        this.sprRen = this.GetComponent<SpriteRenderer>();

        // 初期の木の絵（Tree_0、わかば）を挿入
        this.sprRen.sprite = tree[0];
    }

    /// <summary>
    /// 木を成長させる
    /// </summary>
    public void Grow()
    {
        // 貯水量100%
        if(this.getWater == 100.0f)
        {
            // りんごの実を成らせる
            SetApples();
        }
        // 貯水量70%超
        else if(this.getWater >= 70.0f)
        {
            // 木のスプライトに差し替える
            this.sprRen.sprite = tree[2];
        }
        // 貯水量30%越
        else if (this.getWater >= 30.0f)
        {
            // 木の幹のスプライトに差し替える
            this.sprRen.sprite = tree[1];
        }
    }

    /// <summary>
    /// りんごの実を成らせる
    /// </summary>
    private void SetApples()
    {
        // りんごリスト初期化
        apples = new List<AppleBehavor>();

        for(int i = 0; i < applesPos.Count; i++)
        {
            // インスタンス生成
            AppleBehavor apple = Instantiate(applePrefab);

            // 位置設定
            apple.GetComponent<Transform>().position = applesPos[i];

            // リストに追加
            apples.Add(apple);
        }
    }

    /// <summary>
    /// ねこの感情を変える処理
    /// </summary>
    /// <param name="playerController"></param>
    public override void ChangeNekoEmotion(PlayerController playerController)
    {
        // デフォルト（ネコはてな）
        base.ChangeNekoEmotion(playerController);
    }
}
