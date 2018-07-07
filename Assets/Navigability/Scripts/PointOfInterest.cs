using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider))]
public class PointOfInterest : MonoBehaviour, IGvrGazeResponder, TimedInputHandler
{
    public POVManager pinManager = null;
    private bool shrinking = false;
    private bool expanding = false;
    private float originalSize;
    private float targetSize;
    private float resizeSpeed;
    private bool cardboardFound = false;


    private GameObject text = null;
    private Renderer text_rend = null;

    public PointOfView targetPOV = null;
    private Animator anim;


    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        text = GameObject.Find("Testo_Icona");
        text_rend = text.GetComponent<Renderer>();
        Debug.Log("Start", gameObject);
        originalSize = transform.localScale.x;
        targetSize = originalSize * 1.2f;
        resizeSpeed = originalSize * 2f;
        /*
        if (GameObject.Find("CardboardMain"))
        {
            cardboardFound = true;
            Cardboard.SDK.OnTilt += () => { SceneManager.LoadScene("Menu"); };
        }*/
    }

    

    void Shrink()
    {
        expanding = false;
        shrinking = true;
    }

    void Expand()
    {
        shrinking = false;
        expanding = true;
    }

    void Update()
    {
        if (shrinking)
        {
            transform.localScale -= Vector3.one * Time.deltaTime * resizeSpeed;
            if (transform.localScale.x < originalSize)
                shrinking = false;
        }
        else if (expanding)
        {
            transform.localScale += Vector3.one * Time.deltaTime * resizeSpeed;
            if (transform.localScale.x > targetSize)
                expanding = false;
        }
    }



    void LateUpdate()
    {
        if (cardboardFound)
        {
            GvrViewer.Instance.UpdateState();
            if (GvrViewer.Instance.BackButtonPressed)
            {
                //Application.Quit();
                SceneManager.LoadScene("Menu");
            }
        }
    }


    #region ICardboardGazeResponder implementation

    /// Called when the user is looking on a GameObject with this script,
    /// as long as it is set to an appropriate layer (see CardboardGaze).
    public void OnGazeEnter()
    {
        Debug.Log("Entrato", gameObject);
        Expand();
        anim.SetBool("FocusOn", true);

        if (text.GetComponent<VirginText>().virgin)
        {
            text.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.2f, this.transform.position.z);
            text.transform.LookAt(Camera.main.transform);
            Vector3 rotationV3 = text.transform.eulerAngles;
            rotationV3.y = rotationV3.y - 180;
            text.transform.rotation = Quaternion.Euler(rotationV3);

            text_rend.enabled = true;
        }
    }

    /// Called when the user stops looking on the GameObject, after OnGazeEnter
    /// was already called.
    public void OnGazeExit()
    {
        Debug.Log("Uscito", gameObject);
        Shrink();
        anim.SetBool("FocusOn", false);

        if (text_rend != null && text_rend.enabled)
            text_rend.enabled = false;
    }

    // Called when the Cardboard trigger is used, between OnGazeEnter
    /// and OnGazeExit.
    public void OnGazeTrigger()
    {
        Debug.Log("Cliccato", gameObject);
        pinManager.updatePoV(targetPOV);

        text.GetComponent<VirginText>().virgin = false;

        if (text_rend != null && text_rend.enabled)
            text_rend.enabled = false;
    }

    public void HandleTimedInput() {
        Debug.Log("Cliccato", gameObject);
        pinManager.updatePoV(targetPOV);

        text.GetComponent<VirginText>().virgin = false;

        if (text_rend != null && text_rend.enabled)
            text_rend.enabled = false;
    }

    #endregion
}
