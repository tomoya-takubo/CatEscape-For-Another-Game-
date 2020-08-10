using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    // レイヤー
    public LayerMask clickedObjectLayer;

    // Update is called once per frame
    void Update()
    {
        // 左クリックされたとき
        if(Input.GetMouseButtonUp(0))
        {
            // オブジェクト毎に記述した通りにふるまう
            DoClicked();
        }
    }

    /// <summary>
    /// クリック時に設定どおりふるまう
    /// </summary>
    public void DoClicked()
    {
        // クリックされたGameObjectを取得
        GameObject clickedGameObject = GetGameObjectClicked();

        // nullチェック
        if (!clickedGameObject)
        {
            return;
        }

        // クリックされたGameObjectがClickedObjectBaseを持っていた場合
        // ↑   （☆）多態性（ポリモーフィズム）が高い書き方
        if (clickedGameObject.TryGetComponent(out ClickedObjectBase clickedObjectBase))
        {
            // クリック処理
            clickedObjectBase.Clicked();
        }

        /*  ↓   （☆）多態性（ポリモーフィズム）が低い書き方
        // tag情報を取得
        string tag = clickedGameObject.tag;

        // 処理分岐
        switch(tag)
        {
            // 草むら
            case "Grass":
                // 草むらがクリックされたときの処理
                clickedGameObject.GetComponent<GrassBehavor>().Clicked();
                break;

            // クローゼット
            case "Closet":
                // クローゼットがクリックされたときの処理
                clickedGameObject.GetComponent<ClosetBehavor>().Clicked();
                break;
        }
        */
    }

    /// <summary>
    /// クリックしたオブジェクトを取得する
    /// </summary>
    /// <returns></returns>
    private GameObject GetGameObjectClicked()
    {
        // タンスがタッチされたら
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    // マウスポインタからレーザ発射
        RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, Mathf.Infinity, clickedObjectLayer);  // レーザーに当たったオブジェクトを取得

        // ↑　（☆）RayCastメソッドはオーバーロードメソッドであり、
        //          今回は上記の引数のメソッドを使い、レイヤーを第4引数で指定することで
        //          クリックしたときに処理させたいオブジェクトをそのレイヤーに乗せることで
        //          確実にクリックできるようにしている

        // 何もクリックされていなければ処理中断
        if (!hit2d)
        {
            return null;
        }

        // クリックされたGameObjectを取得
        GameObject g = hit2d.collider.gameObject;

        // 引数として返す
        return g;
    }
}
