 using UnityEngine;
 using UnityEngine.UI;
 using System.Collections;
 using UnityEngine.EventSystems;
 
 public class ButtonDetector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler 
 {
     
     
    public bool buttonPressed;
     
     void Start ()
     {
        
     }
     
     void Update()
     {
         
     }
     
    public void OnPointerDown(PointerEventData eventData){
        
     buttonPressed = true;
}
 
public void OnPointerUp(PointerEventData eventData){
    buttonPressed = false;
}
     
 }