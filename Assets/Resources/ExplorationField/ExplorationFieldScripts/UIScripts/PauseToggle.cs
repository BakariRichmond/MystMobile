//this script toggles  the games pause
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityStandardAssets.CrossPlatformInput {

    public class PauseToggle : MonoBehaviour {
        public GameObject MiniMap;
        public GameObject menu;
        public GameObject blur;
        public bool open = false;
        public AudioSource OpenAudio;
        public AudioSource CloseAudio;
        public string Name;
        void Start () {
            MiniMap = GameObject.Find("MiniMapGroup");

        }

        void OnEnable () {

        }

        public void SetDownState () {
            CrossPlatformInputManager.SetButtonDown (Name);
        }

        public void SetUpState () {
            CrossPlatformInputManager.SetButtonUp (Name);
        }

        public void SetAxisPositiveState () {
            CrossPlatformInputManager.SetAxisPositive (Name);
        }

        public void SetAxisNeutralState () {
            CrossPlatformInputManager.SetAxisZero (Name);
        }

        public void SetAxisNegativeState () {
            CrossPlatformInputManager.SetAxisNegative (Name);
        }

        public void Update () {
            if (CrossPlatformInputManager.GetButtonDown (gameObject.name) & open == false) {
                //if open is false and buton clicked, open pause menu and set open to true
                MiniMap.transform.localScale = new Vector3 (0, 0, 0);
                menu.transform.localScale = new Vector3 (1f, 1f, 1);
                blur.transform.localScale = new Vector3 (1f, 1f, 1);
                OpenAudio.Play ();
                open = true;

            } else if (CrossPlatformInputManager.GetButtonDown (gameObject.name) & open == true) {
                //if open is true and button pressed, close menu and set open to false
                MiniMap.transform.localScale = new Vector3 (1.719539f, 1.719539f, 1.719539f);
                menu.transform.localScale = new Vector3 (0f, 0f, 0);
                blur.transform.localScale = new Vector3 (0f, 0f, 0);
                CloseAudio.Play ();
                open = false;

            }
        }
    }
}