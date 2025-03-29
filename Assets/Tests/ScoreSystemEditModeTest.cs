/*using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class ScoreSystemEditModeTest
{
    private GameObject player;
    private GameObject star;
    public Score scoreManager;

    [SetUp]
    public void Setup()
    {
        // Tạo Player và gán Script quản lý điểm
        player = new GameObject("Player");
        scoreManager = player.AddComponent<Score>();

        // Tạo Star (ngôi sao)
        star = new GameObject("Star");
        star.tag = "Star"; // Gán tag để kiểm tra điều kiện
        star.AddComponent<BoxCollider2D>(); // Cần Collider2D để kích hoạt OnTriggerEnter2D
    }

    [Test]
    public void ScoreIncreases_When_PlayerTouchesStar()
    {
        int initialScore = scoreManager.score;

        // Giả lập sự kiện chạm vào Star
        scoreManager.OnTriggerEnter2D(star.GetComponent<Collider2D>());

        // Kiểm tra xem điểm có tăng lên 1 không
        Assert.AreEqual(initialScore + 1, scoreManager.score, "Score should increase by 1 when player touches a star.");
    }

    [TearDown]
    public void Teardown()
    {
        // Xóa đối tượng sau khi test để tránh lỗi bộ nhớ
        Object.DestroyImmediate(player);
        Object.DestroyImmediate(star);
    }
}
*/