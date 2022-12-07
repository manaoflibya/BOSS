using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class LoadingCanvas_Controller : MonoBehaviour
{
    static string nextScene;
    public static void LoadSene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }
    GameObject LoadingCircle;
    public float CircleRotSpeed = 0f;
    Slider LoadingBar;
    public TextMeshProUGUI LoadingValue_Text;

    private void Awake()
    {
        LoadingCircle = GameObject.Find("Loading_Rotate");
        LoadingBar = GameObject.Find("Slider_Loading_Bar").GetComponent<Slider>();
        LoadingValue_Text.text = "Loading... "+ 0+ "%";
        LoadingBar.value = 0f;
        StartCoroutine(LoadSeneProcess());

    }


    void Update()
    {
        LoadingCircle.transform.Rotate(Vector3.back * Time.deltaTime * CircleRotSpeed);
    }

    IEnumerator LoadSeneProcess()
    {
        yield return new WaitForSeconds(3f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);
        operation.allowSceneActivation = false;

        float timer = 0f;
        while(!operation.isDone)
        {

            yield return null;

            if (operation.progress < 0.9f)
            {
                LoadingBar.value = operation.progress;
                int change_F = Mathf.RoundToInt(LoadingBar.value * 100);
                LoadingValue_Text.text = "Loading... " + change_F + "%";
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                LoadingBar.value = Mathf.Lerp(0.9f, 1f, timer);
               //Debug.Log(1);
                int change_F = Mathf.RoundToInt(LoadingBar.value * 100);
                LoadingValue_Text.text = "Loading... " + change_F + "%";

                if (LoadingBar.value >= 1f)
                {

                    LoadingValue_Text.text = "Loading... " + 100 + "%";

                    operation.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
