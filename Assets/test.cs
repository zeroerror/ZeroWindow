using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ZeroUIFrame;

public class test : UIBehavior
{

    UIButton button;

    // Start is called before the first frame update
    void OnEnable()
    {
        button = GetComponentInChildren<UIButton>();
        OnPointerDown("btn", click, 1, 2);
    }

    void click(params object[] args)
    {
        var selfArgs = args[0] as object[];
        var arg0 = selfArgs[0];
        var arg1 = selfArgs[1];

        var eventData = args[1] as PointerEventData;

        Debug.Log($"arg0 {arg0}  arg {arg1}  OnPointerDown {eventData.position}");
    }

    // Update is called once per frame
    void Update()
    {
        if (button.IsPressing)
        {
            Debug.Log("button.IsPressing");
        }
    }
}
