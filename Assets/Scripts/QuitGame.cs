using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void ExitGame()
    {
        StartCoroutine(ExitDelay());
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode(); // Dừng chế độ Play trong Editor
#else
        Application.Quit();
#endif
    }

    IEnumerator ExitDelay()
    {
        yield return new WaitForSeconds(0.5f);
    }
}
