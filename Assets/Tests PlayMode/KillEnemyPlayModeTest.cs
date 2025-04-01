using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TMPro;

public class KillEnemyPlayModeTest
{
    private GameObject player;
    private GameObject enemy;
    private Enemy enemyScript;
    private Animator animator;
    private AudioSource audioSource;
    private AudioClip audioClipX;
    private Score scoreManager;

    [SetUp]
    public void SetUp()
    {
        // Tạo Player
        player = new GameObject("Player");
        player.tag = "Player";
        player.AddComponent<Rigidbody2D>();
        player.AddComponent<BoxCollider2D>();

        // Tạo Enemy
        enemy = new GameObject("Enemy");
        enemy.tag = "Enemy";
        enemy.AddComponent<BoxCollider2D>();
        enemy.AddComponent<Rigidbody2D>().isKinematic = true;
        enemyScript = enemy.AddComponent<Enemy>();

        // ✅ Thêm Animator
        animator = enemy.AddComponent<Animator>();
        RuntimeAnimatorController beeController = Resources.Load<RuntimeAnimatorController>("bee-1");
        if (beeController != null)
        {
            animator.runtimeAnimatorController = beeController;
        }
        enemyScript.GetType().GetField("anim", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            ?.SetValue(enemyScript, animator);

        // ✅ Thêm AudioSource vào enemy
        audioSource = enemy.AddComponent<AudioSource>();
        

        // Tải AudioClip từ thư mục Resources
        AudioClip kickClip = Resources.Load<AudioClip>("kick");



        // Kiểm tra nếu AudioClip tồn tại, nếu không thì log lỗi
        if (kickClip == null)
        {
            Debug.LogError("Không tìm thấy AudioClip 'kick' trong thư mục Resources.");
        }
        else
        {
            // Gán AudioClip vào AudioSource
            audioSource.clip = kickClip;

            // Đảm bảo AudioSource được thêm vào đúng
            enemyScript.GetType().GetField("audioDeadEnemy", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(enemyScript, kickClip);
        }

        // ✅ Thêm Score Manager giả lập
        GameObject scoreManagerObj = new GameObject("ScoreManager");
        scoreManager = scoreManagerObj.AddComponent<Score>();
        enemyScript.GetType().GetField("scoreM", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            ?.SetValue(enemyScript, scoreManager);

        // ✅ Thêm UI Score Text
        GameObject scoreTextObj = new GameObject("ScoreText");
        TextMeshProUGUI scoreText = scoreTextObj.AddComponent<TextMeshProUGUI>();
        enemyScript.GetType().GetField("_scoreText", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            ?.SetValue(enemyScript, scoreText);
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(player);
        Object.Destroy(enemy);
    }

    [UnityTest]
    public IEnumerator Enemy_OnTriggerEnter_Player_ShouldDie()
    {
        // Kiểm tra điểm số ban đầu
        Assert.AreEqual(0, scoreManager.score, "⚠️ Điểm số ban đầu phải là 0!");
        Assert.AreEqual(0, Enemy.killcounter, "⚠️ Biến killcounter ban đầu phải là 0!");
        Assert.IsFalse(Enemy.isEnemyDeath, "⚠️ Enemy không được chết trước va chạm!");

        // ✅ Giả lập Player chạm vào Enemy
        enemyScript.OnTriggerEnter2D(player.GetComponent<BoxCollider2D>());
        yield return new WaitForSeconds(0.1f); // Chờ 1 frame để xử lý

        // ✅ Kiểm tra điểm số tăng lên 5
        Assert.AreEqual(5, scoreManager.score, "❌ Điểm số không tăng đúng!");

        // ✅ Kiểm tra UI Score Text cập nhật đúng
        Assert.AreEqual("5", enemyScript.GetType().GetField("_scoreText", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            ?.GetValue(enemyScript).ToString(), "❌ UI Score Text không cập nhật đúng!");

        // ✅ Kiểm tra âm thanh phát khi Enemy chết
        Assert.IsTrue(audioSource.isPlaying, "❌ Âm thanh chết của Enemy không được phát!");

        // ✅ Kiểm tra Animation "enemyDeath" được kích hoạt
        Assert.IsTrue(animator.GetCurrentAnimatorStateInfo(0).IsName("enemyDeath"), "❌ Animation không được kích hoạt!");

        // ✅ Kiểm tra biến `isEnemyDeath` đổi thành `true`
        Assert.IsTrue(Enemy.isEnemyDeath, "❌ Enemy không được đánh dấu là đã chết!");

        // ✅ Kiểm tra `killcounter` tăng lên 1
        Assert.AreEqual(1, Enemy.killcounter, "❌ Biến killcounter không tăng đúng!");
    }
}



