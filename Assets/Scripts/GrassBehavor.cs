using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassBehavor : ClickedObjectBase
{
    public Sprite[] grasses;                    // [Sprite: 配列] 草むらの絵
    public SpriteRenderer childGrassSprite;     // [SpriteRenderer] Grass(NoKey)
    public CapsuleCollider2D grassCol;          // [CapsuleCollider2D] Grass
    public KeyBehavor keyPrefab;                // [GameObject] KeyPrefab
    public SpriteRenderer childKeySprite;       // [SpriteRenderer] Key
    public CircleCollider2D keyCol;             // [CircleCollider2D] Key
    public bool withKey = false;                // キーが隠れた草むらか

    private void Start()
    {
        // 草むらの絵をランダムに選ぶ
        // childGrassSprite.sprite = grasses[Random.Range(0, grasses.Length)]; // （★）覚えとく（Random.Rangeの最大値に配列.Lengthを
                                                                               // 　　　設定することで変更に柔軟に対応できる
        childGrassSprite.sprite = grasses[UnityEngine.Random.Range(0, grasses.Length)];
    }

    /// <summary>
    /// クリック処理
    /// </summary>
    public override void Clicked()
    {
        // ネコが近くにいれば
        if(this.nearCat)
        {
            // キーを持っているか
            if (withKey)
            {
                // キーのインスタンス生成
                KeyBehavor key = Instantiate(keyPrefab);    // （★）this.transformで（位置・回転）両方付与できる

                // 草むらの位置情報取得
                Vector2 pos = this.transform.position;

                // キー位置情報付与
                key.transform.position = pos;

                // 他の草むらを消す
                foreach(GameObject gO in GameObject.FindGameObjectsWithTag("Grass"))
                {
                    // 自身なら飛ばす
                    if(gO == this.gameObject)
                    {
                        continue;
                    }

                    // 削除
                    Destroy(gO);
                }
            }

            // 消滅
            Destroy(this.gameObject);
        }
    }

    public override void ChangeNekoEmotion(PlayerController playerController)
    {
        // デフォルト（ネコはてな）
        base.ChangeNekoEmotion(playerController);
    }
}
