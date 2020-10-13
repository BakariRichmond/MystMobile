//this script loads a new scene with a transition effect
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoad : MonoBehaviour

{
    private bool loadScene = false;
    public Text loadingText;
    public Image LoadingBG;
    // Start is called before the first frame update
    void Start()
    {
        LoadingBG.enabled = true;
        StartCoroutine(FadeRoutine());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     IEnumerator FadeRoutine() {
         //fades screen
		 float fadeAmount;
		 float fadeSpeed = 2;
		  Color tempColor = LoadingBG.color;

		 while (LoadingBG.color.a > 0){
			 fadeAmount = tempColor.a - (fadeSpeed * Time.deltaTime);
			 tempColor = new Color(tempColor.r,tempColor.g,tempColor.b,fadeAmount);
		 
          
          LoadingBG.color = tempColor;
		  yield return null;
		  }
          LoadingBG.enabled = false;
	 }
  
  
}
