using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFade : JMC
{
    public CanvasGroup canvas;
    void Start()
    {
        canvas.alpha = 1;
        FadeOutCanvas(canvas);
    }

}
