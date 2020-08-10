using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassGenerator : MonoBehaviour
{
    [Header("Hierarchy")]
    public int generateGrassesMax;                  // インスタンス生成数
    public Transform LimitGrassGeneratePosXMIn;     // インスタンス生成範囲（左端）
    public Transform LimitGrassGeneratePosXMax;     // インスタンス生成範囲（右端）
    public GrassBehavor grassPrefab;                // 草むらのプレハブ
    public float grassAllowPosXDistance;            // 草むら同士の許容距離間

    public List<GrassBehavor> grassList = new List<GrassBehavor>();  // インスタンス格納用リスト

    void Start()
    {
        // 草むらを生成する
        GenerateGrass();
    }

    /// <summary>
    /// 草むらを生成するメソッド
    /// </summary>
    public void GenerateGrass()
    {
        // 指定した数だけ草むらをインスタンシエイトする
        for(int i = 0; i < generateGrassesMax; i++)
        {
            // インスタンスを生成し、管理用リストに格納
            grassList.Add(GenerateGrassInstance());
        }

        // カギを隠す草むらのリスト番号を決定する
        int index = UnityEngine.Random.Range(0, grassList.Count);

        // キー有無判定をtrueにする
        grassList[index].withKey = true;

    }

    /// <summary>
    /// 草むらのインスタンスを生成する
    /// </summary>
    public GrassBehavor GenerateGrassInstance()
    {
        // 乱数で決定したX軸位置格納
        float randomPosX;

        // インスタンス生成
        GrassBehavor g = Instantiate(grassPrefab);

        // ループ文試行数カウンター
        int loopCnt = 0;

        // 距離OKまで続行
        while (true)
        {
            // X軸方向のポジション決定
            randomPosX = UnityEngine.Random.Range(LimitGrassGeneratePosXMIn.position.x, LimitGrassGeneratePosXMax.position.x);

            // 既に生成されている草むらと距離感チェック
            if (CheckPosition(randomPosX))
            {
                // while文を抜ける
                break;
            }

            // カウントが50を越えたら抜ける
            if(loopCnt >= 50)
            {
                Debug.Log("規定回数を超えたため、while文を抜けます");

                g = null;

                break;
            }

            // breakされなければカウンターを１増やす
            loopCnt++;
        }

        // X軸方向のポジション設定
        g.transform.position = new Vector2(randomPosX, g.transform.position.y); // 決定したX軸方向の位置反映

        // インスタンスを返す
        return g;
    }

    /// <summary>
    /// 草むらが重ならないを確認
    /// </summary>
    private bool CheckPosition(float posX)
    {
        // 距離判定フラグ
        bool distanceCheck;

        // すべての草むらと距離差分チェック
        foreach (GrassBehavor g in grassList)
        {
            // 距離取得
            float distance = Mathf.Abs(g.transform.position.x - posX);

            // 判定
            distanceCheck = distance > grassAllowPosXDistance ? true : false;

            // 距離が許容範囲内であれば
            if(distanceCheck)
            {
                // 続行
                continue;
            }
            // 許容範囲外であれば
            else
            {
                // falseを返す
                return false;
            }
        }

        // すべて問題なければtrueを返す
        return true;
    }
}
