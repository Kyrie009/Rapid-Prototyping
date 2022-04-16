using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging; //right click the component to find which script it belongs too or this would be hard to find

//Good for make players look at objects or npc's or any interactable
public class LookAtObjectAnimationRigging : MonoBehaviour
{
    private Rig rig;
    private float targetWeight;
    private void Awake()
    {
        rig = GetComponent<Rig>();
    }

    private void Update()
    {
        rig.weight = Mathf.Lerp(rig.weight, targetWeight, Time.deltaTime * 10f);

        if (Input.GetKeyDown(KeyCode.T))
        {
            targetWeight = 1f;
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            targetWeight = 0f;
        }
    }
}
