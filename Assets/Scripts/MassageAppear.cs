using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MassageAppear : MonoBehaviour
{
    public Text text;    //メッセージ
    public Image wtrGge;    //ウォーターゲージ   

    public bool onPuddle;  //水たまり上かどうか

    void Update()
    {
        //じょうろアビリティを持っている
        if (AbilityManager.waterAbility)
        {
            //水たまり上でスペースキー入力
            if (onPuddle && Input.GetKey(KeyCode.Space))
            {
                //給水
                wtrGge.fillAmount += 0.01f;
            }
        }
    }

    //主人公が接触したら
    private void OnTriggerEnter2D(Collider2D col)
    {
        //水たまり上をオンに
        onPuddle = true;

        //メッセージ出現
        Debug.Log("何かが当たりました！");
        Text txt = text.GetComponent<Text>();
        DOTween.ToAlpha
        (
            () => txt.color,
            color => txt.color = color,
            1.0f,   //目標値
            2.5f    //所要時間
        );
    }


    //主人公が離れたら
    private void OnTriggerExit2D(Collider2D col)
    {
        //水たまり上をオフに
        onPuddle = false;

        //メッセージ出現
        Debug.Log("何かが出ています！");
        Text txt = text.GetComponent<Text>();
        DOTween.ToAlpha
        (
            () => txt.color,
            color => txt.color = color,
            0.0f,   //目標値
            2.5f    //所要時間
        );
    }
}
