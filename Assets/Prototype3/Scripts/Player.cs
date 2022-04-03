using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : GameBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= -50)
        {
            _UI3.GameOver();
        }
    }
}
