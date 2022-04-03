using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIManager : MonoBehaviour
{
    public GameObject prototypeSelect;
    void Start()
    {
        prototypeSelect = GameObject.Find("PrototypeSelect");
        OpenProtoSelect(false);

    }

    public void OpenProtoSelect(bool _toggle)
    {
        prototypeSelect.SetActive(_toggle);

    }
}
