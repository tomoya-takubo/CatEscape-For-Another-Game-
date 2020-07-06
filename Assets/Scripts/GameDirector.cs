using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   //UI部品を使うので忘れずに追加

public class GameDirector : MonoBehaviour
{
    //タイマー
    public float timeCounter = 60.0f;

    //時間表示
    public Text timeText;

    //プレイヤー
    public Animator player;

    //ハート管理フォルダ
    public GridLayoutGroup heartGroup;

    //GameObject hpGauge;
    GameObject waterGauge;

    //ハート個数管理
    int heartsNum;


    // Start is called before the first frame update
    void Start()
    {
        //this.hpGauge = GameObject.Find("hpGauge");
        this.waterGauge = GameObject.Find("WaterGauge");    //水ゲージ取得
        heartsNum = heartGroup.constraintCount; //ハート個数取得

        //各アビリティ状態デバッグログ
        Debug.Log("waterAbility: " + AbilityManager.waterAbility);
        Debug.Log("scissorAbility: " + AbilityManager.scissorAbility);

    }

    void Update()
    {
        //時間が0以下
        if(timeCounter <= 0)
        {
            //終わり
            timeCounter = 0.0f;
            Debug.Log("終了！");
        }
        //時間表示
        else
        {
            //時間更新
            timeCounter -= Time.deltaTime;
        }

        //表示
        Timer(timeCounter);
    }

    //ダメージ
    public void DecreaseHp()
    {
        if(heartsNum > 0)
        {
            //this.hpGauge.GetComponent<Image>().fillAmount -= 0.1f;

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

    //水取得
    public void IncreaseWater()
    {
        Image wtrGge = this.waterGauge.GetComponent<Image>();

        //水ゲージが満タンでなければ
        if (wtrGge.fillAmount <= 1.0f)
        {
            //水ゲージ回復
            wtrGge.fillAmount += 0.01f;
        }

        Debug.Log("水ゲージは" + wtrGge.fillAmount + "です");
    }

    //タイマー表示
    public void Timer(float time)
    {
        //時間表示
        timeText.text = timeCounter.ToString("F2");
    }
}
