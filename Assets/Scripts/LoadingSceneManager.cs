using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour

{
    private AsyncOperation op;
    [SerializeField] private Animation _animation;
    
    
    public void LoadSceneWithLoadingScene(int go)
    {
        StartCoroutine(LoadScene(go));
    }

    IEnumerator LoadScene(int go)
    {
        op = SceneManager.LoadSceneAsync(go);
        
        op.allowSceneActivation = false;
        _animation.Play("Loading_Close");
        yield return new WaitForSeconds(1f);
        while (!op.isDone)
        {
            yield return null;
            if (op.progress >= 0.9f)
            {
                _animation.Play("Loading_Open");
                op.allowSceneActivation = true;
            }
        }
        yield break;
    }

}