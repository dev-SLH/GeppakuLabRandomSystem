using System;
using UnityEngine;

public class GetResolutionRatio : MonoBehaviour
{
    public TMPro.TMP_Text text;

    // Update is called once per frame
    void Update()
    {
        text.text = Screen.width + "x" + Screen.height + " / " + string.Format("{0:F2}",
            Convert.ToSingle(Screen.width) / Convert.ToSingle(Screen.height));
    }
}
