using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

[RequireComponent(typeof(AudioSource))]

public class ActionEvent : MonoBehaviour
{
    private Animator animator;
    public string ID;
    public bool isScheduled;
    private bool isDone;
    public string intro;
    public string feedback;
    public GameObject ActionWheel;
    private GameObject crosshair;
    private GameObject piedinoText;
    private GameObject hintText;
    private GameObject hintBTN;
    private GameObject screen;
    private GameObject piedinoBTN;
    private GameObject player;


	[SerializeField] public GameObject toAnimate;
    AudioSource clickSFX;
    MeshRenderer mr;
 
    // Start is called before the first frame update
    void Start()
    {
        clickSFX = GetComponent<AudioSource>();
        mr = GetComponent<MeshRenderer>();
        ActionWheel.SetActive(false);
        crosshair = GameObject.Find("Crosshair");
        player = GameObject.Find("Giocatore");
        piedinoText = GameObject.Find("PiedinoText");
        hintText = GameObject.Find("HintText");
        hintBTN = GameObject.Find("PlayerHints");  
        screen = GameObject.Find("SchermoPC");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isDone = false;

    }



    // Crossover hovering the object
    void OnMouseEnter() {
        Debug.Log("Entering");
        if((string.Equals(player.GetComponent<actionSchedule>().getID(),ID) || !isScheduled) && !isDone) {
            // Enabling glow
            // mr.material.EnableKeyword("_EMISSION");
        }

    }


    // Crossover left the object
    void OnMouseExit()
    {
        Debug.Log("Exiting");

        // Disabling glow
        mr.material.DisableKeyword("_EMISSION");   
    }


    // Left click on the object
    void OnMouseDown(){
        Debug.Log("Clicking On Object:" + gameObject.name);

        // PC - accendi
        if(string.Equals(player.GetComponent<actionSchedule>().getID(), "TRAVEL")) {
            Debug.Log("Schermo");
            screen.GetComponent<Transform> ().localScale = new Vector3((float) -0.008166407, (float) 41.64621, (float) 63.08562);
        }

        hintBTN.GetComponent<RectTransform> ().localScale = new Vector3 ((float) 0, (float) 0, 1);
        piedinoText.GetComponent<TextPiedino>().setFeedback(feedback);
        
        if(isScheduled) hintText.GetComponent<HintText>().setHint(intro);

        if((string.Equals(player.GetComponent<actionSchedule>().getID(),ID) || !isScheduled) && !isDone) {
            isDone = true;
            player.GetComponent<actionSchedule>().setCurrentScheduled(isScheduled);
            // Play chosen sound
            clickSFX.Play(0);
            // Enabling Actionwheel
            ActionWheel.SetActive(true);
            // Disabling Crosshair
            crosshair.GetComponent<RectTransform> ().localScale = new Vector3 (0, 0, 0);
            // Show cursor
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        

    }
	


    // Update is called once per frame
    void Update()
    {
        if((string.Equals(player.GetComponent<actionSchedule>().getID(),ID) || !isScheduled) && !isDone) {
            GetComponent<Outline>().enabled = true;
        }

        else {
            GetComponent<Outline>().enabled = false;
        }

    }
}
