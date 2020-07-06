using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakabaBehavior : MonoBehaviour
{
    //貯水最大量
    public float waterMaxValue = 100.0f;

    //いくら水を受け取ったか
    public float getWater = 0.0f;

    //プレイヤーが接触しているかの判別
    public bool nearPlayer = false;

    //実のなった木
    public GameObject treeWithApples;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //プレイヤー接触ON
        nearPlayer = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        //プレイヤー接触OFF
        nearPlayer = false;
    }
}
