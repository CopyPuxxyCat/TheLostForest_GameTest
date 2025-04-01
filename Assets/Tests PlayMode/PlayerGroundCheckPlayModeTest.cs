using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PlayerGroundCheckPlayModeTest
{
    private GameObject player;
    private Rigidbody2D playerRb;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        // Load Scene nếu cần thiết
        if (SceneManager.GetActiveScene().name != "1")
        {
            SceneManager.LoadScene("1");
            yield return null; // Chờ Scene load
        }

        // Lấy Player từ Scene
        player = GameObject.FindWithTag("Player");
        Assert.NotNull(player, "Player không tồn tại trong Scene.");

        // Kiểm tra nếu Player có Rigidbody2D
        playerRb = player.GetComponent<Rigidbody2D>();
        Assert.NotNull(playerRb, "Player không có Rigidbody2D.");

        yield return null;
    }

    [UnityTest]
    public IEnumerator Player_TouchesGround_ShouldBeGrounded()
    {
        // Tạo một đối tượng Ground trong Scene
        GameObject ground = new GameObject("Ground");
        ground.tag = "Ground";
        var groundCollider = ground.AddComponent<BoxCollider2D>();
        ground.transform.position = new Vector2(0, -1);

        // Đảm bảo Player có Collider để va chạm hoạt động
        var playerCollider = player.GetComponent<BoxCollider2D>();
        if (playerCollider == null)
        {
            playerCollider = player.AddComponent<BoxCollider2D>();
        }

        // Đặt Player trên không trung
        player.transform.position = new Vector2(0, 2);

        // Bật trọng lực để Player rơi xuống
        playerRb.gravityScale = 1;
        yield return new WaitForSeconds(0.5f); // Đợi Player rơi xuống

        // Kiểm tra xem Player đã chạm đất chưa
        bool isGrounded = player.GetComponent<CharacterControl>()?.grounded ?? false;
        Assert.IsTrue(isGrounded, "Player không nhận diện được chạm đất.");
    }
}



