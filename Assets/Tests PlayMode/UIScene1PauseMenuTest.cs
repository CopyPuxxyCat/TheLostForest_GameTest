using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UIElements;

public class UIScene1PauseMenuTest
{
    private GameObject pauseMenu;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        // Load scene "1" nếu chưa load
        if (SceneManager.GetActiveScene().name != "1")
        {
            SceneManager.LoadScene("1");
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        pauseMenu = FindInactiveObject("PauseScene");
        Assert.NotNull(pauseMenu, "PauseScene không tồn tại dù đang bị tắt.");

        yield return null;
    }

    [UnityTest]
    public IEnumerator CanActive_PauseMenu()
    {
        pauseMenu.SetActive(true);
        Assert.IsTrue(pauseMenu.activeSelf, "Pause Menu đã được bật trong scene 1.");
        yield return new WaitForSeconds(1f);

    }

    [UnityTest]
    public IEnumerator ResumeButton_PauseMenu()
    {

        yield return new WaitForSeconds(1f);

    }

    [UnityTest]
    public IEnumerator RestartButton_PauseMenu()
    {

        yield return new WaitForSeconds(1f);

    }

    [UnityTest]
    public IEnumerator QuitButton_PauseMenu()
    {

        yield return new WaitForSeconds(1f);

    }

    private GameObject FindInactiveObject(string name)
    {
        GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject obj in rootObjects)
        {
            if (obj.name == name)
                return obj;

            Transform found = FindInChildren(obj.transform, name);
            if (found != null)
                return found.gameObject;
        }
        return null;
    }

    private Transform FindInChildren(Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
                return child;

            var result = FindInChildren(child, name);
            if (result != null)
                return result;
        }
        return null;
    }
}
