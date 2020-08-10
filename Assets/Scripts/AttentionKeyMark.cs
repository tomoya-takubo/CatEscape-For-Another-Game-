using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttentionKeyMark : ClickedObjectBase
{
    public float timeCounterForFlashing;   // 時間管理
    public float timeSpanFlashing;      // 時間間隔

    void Update()
    {
        // 点滅処理
        Flashing();

        // クローゼットが開放されている
        if(ClosetBehavor.unlock)
        {
            // 自身を消す
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// 点滅処理
    /// </summary>
    private void Flashing()
    {
        // 時間更新
        timeCounterForFlashing += Time.deltaTime;

        // 累積時間が時間間隔を超えていなければ後続処理なし
        if(timeCounterForFlashing < timeSpanFlashing)
        {
            return;
        }

        // （累積時間が時間間隔を超えている場合）Sprite表示を反転させる
        this.GetComponent<SpriteRenderer>().enabled = !this.GetComponent<SpriteRenderer>().enabled;

        // timeCounter初期化
        timeCounterForFlashing = 0.0f;
    }

}
