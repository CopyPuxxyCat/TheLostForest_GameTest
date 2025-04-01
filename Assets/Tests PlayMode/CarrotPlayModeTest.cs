using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class CarrotPlayModeTest
{
    private GameObject player;
    private Score scoreScript;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        // Load scene nếu cần thiết
        if (SceneManager.GetActiveScene().name != "1")
        {
            SceneManager.LoadScene("1");
            yield return null;
        }

        // Tạo player hoặc lấy player từ scene
        player = GameObject.FindWithTag("Player");
        Assert.NotNull(player, "Player không tồn tại trong scene.");

        scoreScript = player.GetComponent<Score>();
        Assert.NotNull(scoreScript, "Không tìm thấy script Score trên Player.");

        // Đặt lại giá trị carrot
        Score.carrot = 0;
        yield return null;
    }

    [UnityTest]
    public IEnumerator Player_PicksUp_Carrot()
    {
        // Tạo một Carrot trong scene
        GameObject carrot = new GameObject("Carrot");
        carrot.tag = "Carrot";
        var collider = carrot.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        carrot.transform.position = player.transform.position;

        // Đợi 1 frame để xử lý va chạm
        yield return null;

        // Kiểm tra xem carrot đã bị hủy chưa
        Assert.IsNull(GameObject.Find("Carrot"), "Carrot không bị hủy sau khi va chạm.");

        // Kiểm tra số lượng carrot nhặt được
        Assert.AreEqual(1, Score.carrot, "Carrot count không tăng lên sau khi nhặt.");
        Debug.Log($"✅ Test Passed!:  {Score.carrot}");
    }
}

