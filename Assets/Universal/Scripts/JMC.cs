using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JMC : MonoBehaviour
{
    /// <summary>
    /// Shuffles list based on unity's random
    /// </summary>
    /// <typeparam name="T">the data type</typeparam>
    /// <param name="_list"> the list to shuffle</param>
    /// <returns></returns>
    public static List<T> ShuffleList<T>(List<T> _list)
    {
        for(int i = 0; i < _list.Count; i++)
        {
            T temp = _list[i];
            int randomIndex = UnityEngine.Random.Range(i, _list.Count);
            _list[i] = _list[randomIndex];
            _list[randomIndex] = temp;
        }
        return _list;
    }

    /// <summary>
    /// Gets a random color
    /// </summary>
    /// <returns>a random color</returns>
    public Color GetRandomColor()
    {
        return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    /// <summary>
    /// Fades in a canvas group
    /// </summary>
    /// <param name="_cvg">fade to canvas group</param>
    public void FadeInCanvas(CanvasGroup _cvg)
    {
        _cvg.DOFade(1, 0.5f);
    }
    /// <summary>
    /// Fades out a canvas group
    /// </summary>
    /// <param name="_cvg">the canvas group to fade</param>
    public void FadeOutCanvas(CanvasGroup _cvg)
    {
        _cvg.DOFade(0,0.5f);
    }

}
