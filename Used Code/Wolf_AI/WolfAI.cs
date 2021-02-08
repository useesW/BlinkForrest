using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAI : MonoBehaviour {

    Transform playerTransform;
    Animator myAnim;
    Vector3 lastPosition;
    bool agroState = false;
    bool playerisdead = false; //------------------------------
    
    float roamStartTime;
    [SerializeField] float roamTime;
    [SerializeField] float spotDistance;
    
    [Header("Prototyping")]
    [SerializeField] bool prototyping;
    [SerializeField] SpriteRenderer childRenderer;
    [SerializeField] Material roamingMat;
    [SerializeField] Material agroMat;

    private void OnDrawGizmos() {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, spotDistance);
    }

    private void Start() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); // Make Sure that the player has the player tag
        myAnim = GetComponent<Animator>();
        lastPosition = transform.position;
        roamStartTime = Time.time;
        GetComponent<WolfAudio>().PlaySound(0);//-----------------------
    }

    private void Update() {
        if(playerisdead){return;}
        if(Vector3.Distance(transform.position,playerTransform.position) < 0.5f){
            transform.position = playerTransform.position;
            myAnim.SetTrigger("Player Kill");
            playerTransform.GetComponent<PlayerAnimatorController>().OnDeath();
            GetComponent<WolfAudio>().PlaySound(0); //------------------------------------------
            playerisdead = true;
        }
        else if(BodySpot(transform,playerTransform,spotDistance)){ChangeState(1);}
        else{ChangeState(0);}

        Movement();
    }

    void Movement(){
        //Wolf will chanse the player
        if(agroState){
            // set destination to player
        }

        //Wolf will roam around randomly
        else {
            if (Time.time - roamStartTime > roamTime){
                roamStartTime = Time.time;

                // set destination to random spot on map
                float mapXBounds = 100.0f;
                float mapYBounds = 100.0f;
                Vector3 destinationRandomOnMap = new Vector3(Random.Range(-mapXBounds,mapXBounds),Random.Range(-mapYBounds,mapYBounds),0);

                // or set destination to random location around body
                float destinationRadius = 50.0f;
                Vector3 destinationRandomAroundBody = transform.position + new Vector3(Random.Range(-destinationRadius,destinationRadius),Random.Range(-destinationRadius,destinationRadius),0);
            }
        }

        #region Animation
        Vector3 temp = transform.position - lastPosition;
        GetComponent<WolfAudio>().Walk((temp.magnitude > 0.01f)? true:false);//----------------------------------------
        if(temp.magnitude > 0.05f){ // set "0.05f" to something else that makes it look better (The distance that needs to be reached for the run animation to play)
            myAnim.SetBool("Moving",true);
            myAnim.SetFloat("XInput",temp.x);
            myAnim.SetFloat("YInput",temp.y);
        }
        else{myAnim.SetBool("Moving",false);}
        lastPosition = transform.position;
        #endregion
    }

    #region AI
    //change to raycast spot player so the wolf cant see the player through walls ---> We'll do this after the levels have been created b/c I don't know if we're using colliders or not
    bool BodySpot(Transform Abody,Transform Bbody, float distanceCheck){
        if(Vector3.Distance(Abody.position,Bbody.position) < distanceCheck){return true;}
        return false;
    }

    void ChangeState(int stateIndex){
        switch(stateIndex){
            case 0:
                if(!agroState){break;}
                agroState = false;
                if(prototyping){childRenderer.material = roamingMat;}
                GetComponent<WolfAudio>().PlaySound(0);//--------------------------------------------
                break;

            case 1:
                if(agroState){break;}
                agroState = true;
                Howl();
                if(prototyping){childRenderer.material = agroMat;}
                GetComponent<WolfAudio>().PlaySound(1);//--------------------------------------------
                break;
        }
    }

    void Howl(){
        //GetComponent<WolfAudio>().PlaySound(2);//----------------------------------
        GameObject[] activeWolves = GameObject.FindGameObjectsWithTag("Wolf");
        foreach(GameObject wolf in activeWolves){
            if(this.gameObject!= wolf && wolf.GetComponent<WolfAI>().GetAgroState() == false && BodySpot(transform,wolf.transform,spotDistance)){wolf.GetComponent<WolfAI>().ChangeState(1);}
        }
    }
    #endregion

    public bool GetAgroState(){return agroState;}
}
