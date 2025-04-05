using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class UIMenuSceneTests
{
    private NextLevel nx;
    [UnityTest]
    public IEnumerator ButtonClickChangesText()
    {
        // Tải scene chứa UI cần kiểm thử
        SceneManager.LoadScene("StartScene");
        yield return null;

        // Tìm nút và văn bản
        var button = GameObject.Find("StartButton").GetComponent<UnityEngine.UI.Button>();

        // Mô phỏng click nút
        button.onClick.Invoke();
        yield return new WaitForSeconds(5f);

        // Kiểm tra scene đã chuyển chưa
        Assert.AreEqual("1", SceneManager.GetActiveScene().name);
    }
}
