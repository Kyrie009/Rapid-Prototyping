using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger : GameBehaviour
{    
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player4>() != null)
        {
            _UI4.charUI.InteractionText_Enable("Press E to Escape?");
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Player4>() != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _SL.LoadScene(SceneManager.GetActiveScene().name);
            }
        }      
    }

    private void OnTriggerExit(Collider other)
    {
        _UI4.charUI.InteractionText_Disable();
    }
}
