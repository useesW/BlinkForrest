using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    Animator myAnim;
    Lantern myLantern;
    Vector3 lastPosition;
    Vector3 currentOffset;
    bool hiding = false;
    bool lanternState;
    [SerializeField] Color hiddenColor;

    private void Start() {
        myAnim = GetComponent<Animator>();
        myLantern = GetComponentInChildren<Lantern>();
        lastPosition = transform.position;
        lanternState = true;
    }
    //change!!!!!
    private void Update() {
        currentOffset = transform.position - lastPosition;
        GetComponent<PlayerAudio>().Walk((currentOffset.magnitude > 0.01f)? true:false);
    
        if(currentOffset.magnitude > 0.05f){ // set "0.05f" to something else that makes it look better (The distance that needs to be reached for the run animation to play)
            myAnim.SetBool("Moving",true);
            myAnim.SetFloat("XInput",currentOffset.x);
            myAnim.SetFloat("YInput",currentOffset.y);
        }
        else{myAnim.SetBool("Moving",false);}
        lastPosition = transform.position;
        if(Input.GetKeyDown(KeyCode.E) && !hiding){lanternState = !lanternState; myLantern.SetLantern((lanternState)?1:0);}
    }

    public void OnDeath(){
        myAnim.SetTrigger("PlayerDeath");
    }

    private void EnterExitBush(int state){
        switch(state){
            case 0:
            hiding = true;
            GetComponent<SpriteRenderer>().color = hiddenColor;
            myLantern.SetLantern(0);
            myLantern.transform.GetComponent<SpriteRenderer>().color = hiddenColor;
                break;

            case 1:
            hiding = false;
            GetComponent<SpriteRenderer>().color = Color.white;
            myLantern.transform.GetComponent<SpriteRenderer>().color = hiddenColor;
            Debug.Break();
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Bush"){EnterExitBush(0);}
        if(other.gameObject.tag == "Respawn"){
            //end game
            Debug.Log("aaaaaaaaaaaaa");
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Bush"){EnterExitBush(1);}
    }

    public bool IsHidden(){return hiding;}
    public Vector3 GetCurrentOffset(){return currentOffset;}
    
}
