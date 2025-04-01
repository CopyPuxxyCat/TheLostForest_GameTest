using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TMPro;

public class ScorePlayModeTest
{
    private GameObject player;
    private Score scoreManager;
    private GameObject star;
    private TextMeshProUGUI scoreText;

    [UnitySetUp] // Chạy trước mỗi test
    public IEnumerator SetUp()
    {
        // Tạo Player
        player = new GameObject("Player");
        scoreManager = player.AddComponent<Score>();
        player.AddComponent<Rigidbody2D>();  // Để xử lý vật lý
        player.AddComponent<BoxCollider2D>(); // Collider để phát hiện va chạm

        // Tạo UI hiển thị điểm số
        GameObject scoreTextObj = new GameObject("ScoreText");
        scoreText = scoreTextObj.AddComponent<TextMeshProUGUI>();
        scoreManager._scoreText = scoreText;

        // Reset điểm số
        scoreManager.score = 0;
        scoreManager._scoreText.text = scoreManager.score.ToString();

        // Tạo Star
        star = new GameObject("Star");
        star.tag = "Star";
        star.AddComponent<BoxCollider2D>(); // Collider để va chạm
        star.transform.position = player.transform.position; // Đặt sao ngay vị trí nhân vật

        yield return null; // Chờ 1 frame để Unity cập nhật physics
    }

    [UnityTest]
    public IEnumerator Score_Increases_When_StarCollected()
    {
        // Kiểm tra điểm số ban đầu
        int initialScore = scoreManager.score;
        Debug.Log($"🔥 Score Start: {scoreManager.score}");

        // Giả lập va chạm với Star
        scoreManager.OnTriggerEnter2D(star.GetComponent<Collider2D>());

        // Chờ 1 frame để Unity xử lý va chạm
        yield return null;

        // Kiểm tra điểm số đã tăng chưa
        Assert.AreEqual(initialScore + 1, scoreManager.score, "❌ Score không tăng!");
        Debug.Log($"🔥 Score After: {scoreManager.score}");

        // Kiểm tra UI có cập nhật không
        Assert.AreEqual(scoreManager.score.ToString(), scoreManager._scoreText.text, "❌ UI không cập nhật!");

        // Kiểm tra Star đã bị hủy chưa
        yield return new WaitForSeconds(0.1f); // Chờ thêm 1 chút để kiểm tra Destroy
        Assert.IsTrue(star == null || !star.activeInHierarchy, "❌ Star chưa bị Destroy!");

        Debug.Log($"✅ Test Passed!:  { scoreManager.score}");
    }

    [UnityTearDown] // Dọn dẹp sau khi test
    public IEnumerator TearDown()
    {
        Object.Destroy(player);
        Object.Destroy(star);
        yield return null;
    }
}

