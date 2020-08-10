using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotion : MonoBehaviour
{
    // カメラ移動用変数
    private int cameraPosNum = 0;

    /// <summary>
    /// カメラ移動（右方向）
    /// </summary>
    /// <param name="posX"></param>
    public void MoveCameraPosRightDirection(float posX)
    {
        // 条件を満たせば
        if (posX >= 10 + cameraPosNum * 18)
        {
            //カメラ移動
            this.transform.position += new Vector3(18, 0, 0);

            //n更新
            this.cameraPosNum++;

        }
    }

    /// <summary>
    /// カメラ移動（左方向）
    /// </summary>
    /// <param name="posX"></param>
    public void MoveCameraPosLeftDirection(float posX)
    {
        // 条件を満たせば
        if (posX <= -10 + cameraPosNum * 18)
        {
            //カメラ移動
            this.transform.position -= new Vector3(18, 0, 0);

            //n更新
            this.cameraPosNum--;

        }
    }
}
