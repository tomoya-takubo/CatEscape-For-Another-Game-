using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleBehavor : ClickedObjectBase
{
    /// <summary>
    /// クリック処理
    /// </summary>
    public override void Clicked()
    {
        // ネコにりんごを持たせる
        SpriteRenderer appleOnHead = GameObject.Find("AppleOnHead").GetComponent<SpriteRenderer>();
        appleOnHead.enabled = true;

        // クリックされたりんごを消す
        Destroy(this.gameObject);
    }
}
