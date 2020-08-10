using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Abolition_PlayerReaction : MonoBehaviour
{
    public Transform nekoPos;    // ネコ

    public void ChangeNekoEmotionScale()
    {
        // ネコの向きに応じて絵を反転
        if (nekoPos.localScale.x == 1)
        {
            // 絵の向きをネコに合わせる
            this.transform.localScale = new Vector2(1, this.transform.localScale.y);

            // 回転をセット
            this.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (nekoPos.localScale.x == -1)
        {
            // 絵の向きをネコに合わせる
            this.transform.localScale = new Vector2(-1, this.transform.localScale.y);

            // 回転をセット
            this.transform.localRotation = Quaternion.Euler(0, 0, -15);
        }
    }
}
