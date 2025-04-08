using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;
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
        // Tìm nút
        var button = GameObject.Find("ResumeButton").GetComponent<UnityEngine.UI.Button>();

        // Mô phỏng click nút
        button.onClick.Invoke();
        yield return new WaitForSeconds(5f);

        // Kiểm tra scene đã chuyển chưa
        Assert.AreEqual("1", SceneManager.GetActiveScene().name);

        // Kiểm tra game có đang chạy không
        Assert.AreEqual(1f, Time.timeScale, "TimeScale không bằng 1 như mong đợi khi game đang chạy.");
    }

    [UnityTest]
    public IEnumerator RestartButton_PauseMenu()
    {
        // Tìm nút
        var button = GameObject.Find("RestartButton").GetComponent<UnityEngine.UI.Button>();

        // Mô phỏng click nút
        button.onClick.Invoke();
        yield return new WaitForSeconds(5f);

        // Kiểm tra scene vẫn còn là Scene 1
        Assert.AreEqual("1", SceneManager.GetActiveScene().name);

        // Kiểm tra game có đang chạy không
        Assert.AreEqual(1f, Time.timeScale, "TimeScale không bằng 1 như mong đợi khi game đang chạy.");

        // Kiểm tra xem máu player được đưa về 3 không
        var health3 = GameObject.Find("Live3");
        Assert.NotNull(health3, "Health3 không được tìm thấy");
        Assert.IsTrue(health3.activeSelf, "Health3 không được active");

        // Kiểm tra xem điểm số có được reset không
        var scoreObj = GameObject.Find("Points/Score");
        Assert.IsNotNull(scoreObj, "Không tìm thấy canvas tên Score");
        TextMeshProUGUI scoreText = scoreObj.GetComponentInChildren<TextMeshProUGUI>(true);
        Assert.IsNotNull(scoreText, "Không tìm thấy TMP_InputField trong canvas Score");
        string userInput = scoreText.text;
        Assert.AreEqual("0", userInput, "Điểm số chưa được reset về 0");
        // Kiểm tra xem số lượng Carrot có được reset về 0 0
        /*var carrot0 = GameObject.Find("Carrots/Carrot0");
        Assert.IsNotNull(carrot0, "Không tìm thấy canvas tên Carrot0");
        var carrot1 = GameObject.Find("Carrots/Carrot1");
        Assert.IsNotNull(carrot1, "Không tìm thấy canvas tên Carrot1");
        var carrot2 = GameObject.Find("Carrots/Carrot2");
        Assert.IsNotNull(carrot2, "Không tìm thấy canvas tên Carrot2");
        // Lấy màu và kiểm tra alpha
        //float alpha = .color.a;
        Assert.IsFalse(carrot0.activeSelf, "Carrot không được reset về 0");
        Assert.IsFalse(carrot1.activeSelf, "Carrot không được reset về 0");
        Assert.IsFalse(carrot2.activeSelf, "Carrot không được reset về 0");*/
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
