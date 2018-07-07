using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSettings : MonoBehaviour {
    public GameObject[] roomButtons;
    public GameObject[] viewButtons;
    public bool cardboardMode;
    public bool livingRoom;

    private AsyncOperation async;

    [SerializeField]
    private FadeSprite _blackScreenCover;

    public static MenuSettings Instance {
        get;
        set;
    }

    void Awake()
    {
        Instance = this;
    }

    void Start() {
        foreach (GameObject button in viewButtons)
        {
            button.SetActive(false);
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex > 1)   //0 Start, 1 Menu, > 1 le altre
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("Menu");
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }

    public void RoomSelection(bool livingRoomFlag) {
        livingRoom = livingRoomFlag;
        ShowViewMenu();
    }

    public void ViewSelection(bool cardboardFlag)
    {
        cardboardMode = cardboardFlag;
        string nextScene = "LivingGiuseppe";
        //SceneManager.LoadScene(nextScene);
        //StartCoroutine(LoadSceneAsync(nextScene)); //buono
        
        StartCoroutine(LoadSceneCoroutine(nextScene));
    }

    private void ShowViewMenu()
    {
        foreach (GameObject button in roomButtons) {
            button.SetActive(false);
        }

        foreach (GameObject button in viewButtons)
        {
            button.SetActive(true);
        }
    }

    public void Exit() {
        Application.Quit();
    }

    /*
     * fade in e fade out della schermata di caricamento mentre si passa dal menu alla scena da navigare
     */

    //public IEnumerator LoadSceneAsync(string sceneName)
    public IEnumerator LoadSceneCoroutine(string nextScene)
    {
        
        yield return StartCoroutine(_blackScreenCover.FadeIn());
        
        yield return SceneManager.LoadSceneAsync("LoadingScreen");

        //assegno il text prima del fade out
        Text loadingText = GameObject.Find("TextLoading").GetComponent<Text>();
        if (cardboardMode) {
            loadingText.text = "Tocca per continuare";
        } else {
            loadingText.text = "Tocca per iniziare";
        }

        yield return StartCoroutine(_blackScreenCover.FadeOut());


        if (cardboardMode)
        {
            //tocca per continuare
            while (!Input.GetMouseButtonDown(0))
            {
                yield return null;
            }

            yield return SceneManager.LoadSceneAsync("CardboardInstructions");

            //cambio scena quindi devo aggiornare il riferimento di loadingText
            loadingText = GameObject.Find("TextLoading").GetComponent<Text>();
            loadingText.text = "Tocca per iniziare";

        }

        //inizio a caricare la scena in modo asincrono
        async = SceneManager.LoadSceneAsync(nextScene);
        async.allowSceneActivation = false;
        
        //while (!async.isDone)
        //{
        //    float loadProgress = async.progress;

        //    if (loadProgress >= 0.9f)
        //    {
        //        // Almost done.
        //        break;
        //    }

        //    yield return null;
        //}


        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }


        loadingText.text = "Caricamento in corso";


        async.allowSceneActivation = true;
        yield return async;
    }
}
