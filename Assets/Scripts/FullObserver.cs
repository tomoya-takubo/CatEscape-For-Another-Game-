using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullObserver : MonoBehaviour
{
    //給水量
    public Image waterGauge;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //給水MAXの場合
        if(waterGauge.fillAmount == 1)
        {
            //Full出現
            GetComponent<Text>().enabled = true;
        }
    }
}
