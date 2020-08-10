using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowWaterRating : MonoBehaviour
{
    /// <summary>
    /// 貯水率更新
    /// </summary>
    public void UpdateGettingWaterRate(float getWater)
    {
        this.GetComponent<TextMesh>().text = getWater.ToString("F2") + "%";
    }
}
