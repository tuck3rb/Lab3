using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    
    public static GameManager Instance {get; private set;}

    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;

    public GameObject bigScreenBox;
    public TextMeshProUGUI bigScreenText;

    public GameObject curtain;
    private bool raiseLower = false;


    public GameObject canvas;
    public GameObject eventSystem;

    public GameObject mainScreen;

    private bool poem = false;

    public void DialogShow(string text) {
        dialogBox.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(TypeText(text));
    }

    public void TypeBigShow(string text) {
        bigScreenBox.SetActive(true);
        StartCoroutine(TypeBig(text));
    }

    public void DialogHide() {
        dialogBox.SetActive(false);
    }

    IEnumerator TypeText(string text) {
        dialogText.text = "";
        foreach (char c in text.ToCharArray()) {
            dialogText.text += c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator TypeBig(string text) {
        print("big");
        bigScreenText.text = "";
        foreach (char c in text.ToCharArray()) {
            bigScreenText.text += c;
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(5.00f);
        if (poem){
            bigScreenBox.SetActive(false);
            ChangeScene("ZooCityScene");
        } else {
            bigScreenBox.SetActive(false);
            ChangeScene("Start");
            mainScreen.SetActive(true);
        }
    }

    public void printCheck(string texting){
        print(texting);
    }

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

    }


    IEnumerator ColorLerpFunction(bool fadeout, float duration)
    {
        float time = 0;
        raiseLower = true;
        Image curtainImg = curtain.GetComponent<Image>();
        Color startValue;
        Color endValue;
        if (fadeout) {
            startValue = new Color(0, 0, 0, 0);
            endValue = new Color(0, 0, 0, 1);
        } else {
            startValue = new Color(0, 0, 0, 1);
            endValue = new Color(0, 0, 0, 0);
        }

        while (time < duration)
        {
            curtainImg.color = Color.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        curtainImg.color = endValue;
        raiseLower = false;
    }


     IEnumerator LoadYourAsyncScene(string scene)
     {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        StartCoroutine(ColorLerpFunction(true, 1));

        while (raiseLower)
        {
            yield return null;
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        StartCoroutine(ColorLerpFunction(false, 1));
    }


    public void ChangeScene(string scene) {
        print("check");
        StartCoroutine(LoadYourAsyncScene(scene));
        mainScreen.SetActive(false);
        if (scene == "Start"){
            mainScreen.SetActive(true);
        }
    }

    public void StartGame() {
        StartCoroutine(LoadYourAsyncScene("Poem"));
        mainScreen.SetActive(false);
        poem = true;
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
