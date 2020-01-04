using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MultiplayerMainMenuController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text t;
    public void OnPointerEnter(PointerEventData eventData)
    {
        t.color = Color.red; //Or however you do your color
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        t.color = Color.white; //Or however you do your color
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
