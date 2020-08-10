using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   //UI部品を使うので忘れずに追加
using DG.Tweening;

public class GameDirector : MonoBehaviour
{
    // 残り時間
    public float remainingTime = 60.0f;

    // 時間表示
    public Text timeText;

    // プレイヤー
    public Animator player;

    // ハート管理フォルダ
    public GridLayoutGroup heartGroup;

    // カギアイコン
    public Image keyIcon;

    // GameObject hpGauge;
    GameObject waterGauge;

    // ハート個数管理
    int heartsNum;

    // 説明文
    public CanvasGroup explainText;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        // 水ゲージ取得
        this.waterGauge = GameObject.Find("WaterGauge");

        // ハート個数取得
        heartsNum = heartGroup.constraintCount;

        yield return new WaitForSeconds(0.5f);  // （☆）遅延処理

        // 説明文表示
        explainText.DOFade(1.0f, 1.0f);

        yield return new WaitForSeconds(10.0f);

        // 説明文非表示
        explainText.DOFade(0.0f, 1.0f);
    }

    void Update()
    {
        //時間が0以下
        if(remainingTime <= 0)
        {
            //終わり
            remainingTime = 0.0f;
            // Debug.Log("終了！");
        }
        //時間表示
        else
        {
            //時間更新
            remainingTime -= Time.deltaTime;
        }

        // 残り時間表示
        DisplayTime(remainingTime);
    }

    //ダメージ
    public void Damaged()
    {
        // 体力がまだある
        if(heartsNum > 0)
        {
            //this.hpGauge.GetComponent<Image>().fillAmount -= 0.1f;

            // （デバッグ）体力表示
            Debug.Log(heartsNum);

            //攻撃を受けた音を再生する
            GetComponent<AudioSource>().Play();

            //プレイヤー泣く
            player.SetTrigger("isDamaged");

            //ハート取得
            GameObject heartNow = GameObject.Find("Heart_Full_" + heartsNum.ToString());

            //ハート半減
            heartNow.GetComponent<Image>().fillAmount -= 0.5f;

            //ハートなしでheartsNumを１つへらす
            if (heartNow.GetComponent<Image>().fillAmount == 0)
            {
                //heartsNum１つ減らす
                heartsNum--;
            }
        }
    }

    // 水取得
    public void StoreWater()
    {
        Image wtrGge = this.waterGauge.GetComponent<Image>();

        // 水ゲージが満タン
        if (wtrGge.fillAmount >= 1.0f)
        {
            // 後続処理実施しない
            return;
        }

        // （満タンでなければ）水ゲージ回復
        wtrGge.fillAmount += 0.01f;
    }

    // 残り時間表示
    public void DisplayTime(float remainingTime)
    {
        // 残り時間時間表示
        timeText.text = remainingTime.ToString("F2");
    }

    // キーアイコンを表示
    public void SetKeyIcon(bool showKeyIcon)
    {
        // 引数に応じてアイコンを表示 / 非表示
        this.keyIcon.enabled = showKeyIcon;
    }
}
