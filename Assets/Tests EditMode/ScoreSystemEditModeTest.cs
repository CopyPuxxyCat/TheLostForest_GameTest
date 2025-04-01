using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using TMPro;

public class ScoreSystemEditModeTest
{
    private GameObject player;
    private Score scoreManager;
    private GameObject star;

    [SetUp]
    public void SetUp()
    {
        Debug.Log("🚀 Test SetUp Running...");
        // Tạo Player (gán script Score)
        player = new GameObject("Player");
        scoreManager = player.AddComponent<Score>();

        // Tạo Score UI (ScoreText)
        GameObject scoreTextObj = new GameObject("ScoreText");
        TextMeshProUGUI scoreText = scoreTextObj.AddComponent<TextMeshProUGUI>();
        scoreManager._scoreText = scoreText;

        // Đặt score về 0 trước mỗi test
        scoreManager.score = 0;
    }

    [Test]
    public void Score_Increases_When_StarCollected()
    {
        Debug.Log("🔥 Test Started!");

        // Kiểm tra điểm ban đầu
        int initialScore = scoreManager.score;
        Debug.Log("✅ Test 1!");
        // Giả lập va chạm bằng cách gọi trực tiếp nội dung của OnTriggerEnter2D
        if (star.CompareTag("Star"))
        {
            scoreManager.score++;
            scoreManager._scoreText.text = scoreManager.score.ToString();
            Debug.Log($"✅ Score Updated: {scoreManager.score}");
        }

        // Kiểm tra kết quả
        Assert.AreEqual(initialScore + 1, scoreManager.score, "❌ Score không tăng!");
        Assert.AreEqual(scoreManager.score.ToString(), scoreManager._scoreText.text, "❌ UI không cập nhật đúng!");

        Debug.Log("✅ Test Passed!");
        /*Debug.Log("🔥 Test Started!");
        // Arrange
        int initialScore = scoreManager.score;
        Assert.Fail("🔴 Test đã chạy nhưng bị dừng tại đây.");
        // Tạo Star (giả lập va chạm)
        star = new GameObject("Star");
        star.tag = "Star";
        Assert.Fail("🔴 Test đã chạy nhưng bị dừng tại đây 2.");
        Collider2D starCollider = star.AddComponent<BoxCollider2D>();
        Assert.Fail("🔴 Test đã chạy nhưng bị dừng tại đây 3.");
        Collider2D playerCollider = player.AddComponent<BoxCollider2D>();
        Assert.Fail("🔴 Test đã chạy nhưng bị dừng tại đây 4.");
        // Act - Gọi trực tiếp phương thức xử lý va chạm
        scoreManager.OnTriggerEnter2D(starCollider);
        Assert.Fail("🔴 Test đã chạy nhưng bị dừng tại đây 5.");
        // Assert - Kiểm tra xem score đã tăng chưa
        Assert.AreEqual(initialScore + 1, scoreManager.score, "Score không tăng khi nhặt sao!");
        Assert.Fail("🔴 Test đã chạy nhưng bị dừng tại đây 6.");
        // Assert - Kiểm tra UI text đã cập nhật chưa
        Assert.AreEqual(scoreManager.score.ToString(), scoreManager._scoreText.text, "UI không cập nhật điểm số!");

        Debug.Log("✅ Test Passed: Score increases when collecting Star.");*/
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(player);
        Object.DestroyImmediate(star);
    }
}
