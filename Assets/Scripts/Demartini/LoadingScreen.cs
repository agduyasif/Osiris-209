using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadingScreen : MonoBehaviour
{
    [SerializeField] Image barra;
    public static string escenaDestino = "Nivel 1";
    void Start()
    {
        StartCoroutine(LoadScene(escenaDestino));
    }

    IEnumerator LoadScene(string sceneName)
    {
        var operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        while (!operation.isDone) 
        {
            float progress = operation.progress / 0.9f;
            barra.fillAmount = progress;

            if (progress >= 1f)
            {
                yield return new WaitForSeconds(0.5f);
                operation.allowSceneActivation = true;
            }
        }

        

    }

}
