using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenFun : JMC
{
    public GameObject player;
    public float moveDistance = 3;
    public float tweenTime = 0.5f;
    public Ease moveEase;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            player.transform.DOMoveZ(player.transform.position.z + moveDistance, tweenTime).SetEase(moveEase);
            DoEffects();

        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            player.transform.DOMoveZ(player.transform.position.z - moveDistance, tweenTime).SetEase(moveEase);
            DoEffects();
        }
    }
    void DoEffects()
    {
        ChangeColor();
        ShakeCamera();
        ChangeScale();

    }
    void ChangeScale()
    {
        player.transform.DOScale(Vector3.one * 2, tweenTime/2).SetLoops(2).OnComplete(() =>
        player.transform.DOScale(Vector3.one, tweenTime/2)).SetEase(moveEase);
    }

    void ChangeColor()
    {
        //player.GetComponent<Renderer>().material.DOColor(GetRandomColor(), tweenTime);
    }

    void ShakeCamera()
    {
        Camera.main.DOShakePosition(tweenTime * 2,0.1f);
    }
}
