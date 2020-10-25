using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class NPCsearchScript : MonoBehaviour {
    public List<GameObject> Waypoints = new List<GameObject> ();
    Vector3 targetPos;
    int index = 0;
    bool waiting;
    public Coroutine waitRoutine;
    public Animator Anim;
    public float speed = 1;
    bool complete;
    bool init = false;
    public bool triggered;
    bool turning;

    public GameObject triggerObject;
    public bool loop;
    public bool reverse;
    Vector3 currentAngle;
    // Start is called before the first frame update
    void Start () {
        targetPos = Waypoints[0].transform.position;
        transform.LookAt (targetPos);
        Anim.SetBool ("isWalking", false);

    }

    // Update is called once per frame
    void Update () {

        if (triggered) {
            if (!init) {
                init = true;
                Anim.SetBool ("isWalking", true);
            }
            if (transform.position == targetPos & index < Waypoints.Count) {

                waiting = true;
                waitRoutine = StartCoroutine (WaitCoroutine ());

            }
            if (index >= Waypoints.Count & !complete) {
                if (loop) {
                    index = 0;
                } else if (reverse) {
                    index = 0;
                    Waypoints.Reverse ();

                } else {
                    waiting = true;
                    Anim.SetBool ("isWalking", false);
                    complete = true;
                }

            }
            if (!waiting & !complete) {

                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards (transform.position, targetPos, step);
                if (turning) {
                

                    Quaternion lookOnLook = Quaternion.LookRotation(targetPos - transform.position);
 
        transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, 1.0f * Time.deltaTime);

                }

            }

        }
    }
    void OnTriggerEnter (Collider other) {
        if (other.gameObject.tag == "Player") {
            waiting = true;
            Anim.SetBool ("isWalking", false);
        }
    }

    IEnumerator WaitCoroutine () {
        Anim.SetBool ("isWalking", false);

        index++;
        if (index < Waypoints.Count) {
            targetPos = Waypoints[index].transform.position;
        }

        yield return new WaitForSeconds (1);

        if (index < Waypoints.Count) {
            //Vector3 test = transform.LookAt (targetPos);
            turning = true;
            Anim.SetBool ("isWalking", true);

            waiting = false;
        }

    }

}