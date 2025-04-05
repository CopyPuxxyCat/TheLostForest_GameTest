

using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class CarrotPlayModeTest
{
    private GameObject player;
    private Score scoreScript;
    private Animator anim;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        // Load scene "1" nếu chưa load
        if (SceneManager.GetActiveScene().name != "1")
        {
            SceneManager.LoadScene("1");
            yield return null;
        }

        // Tìm player
        player = GameObject.FindWithTag("Player");
        Assert.NotNull(player, "Player không tồn tại trong scene.");

        scoreScript = player.GetComponent<Score>();
        Assert.NotNull(scoreScript, "Không tìm thấy script Score trên Player.");

        anim = player.GetComponent<Animator>();
        Assert.NotNull(anim, "Không tìm thấy Animator trên Player.");

        // Reset lại giá trị carrot
        Score.carrot = 0;

        yield return new WaitForSeconds(1f);
    }

    [UnityTest]
    public IEnumerator Player_PicksUp_Carrot_ByMovingRight()
    {
        // ✅ Load Prefab "Carrot" từ Resources/Prefabs
        GameObject carrotPrefab = Resources.Load<GameObject>("Prefabs/Carrot");
        Assert.IsNotNull(carrotPrefab, "Không tìm thấy Prefab 'Carrot' trong Resources/Prefabs.");

        // ✅ Instantiate carrot cách player 2f về bên phải
        Vector3 spawnPos = player.transform.position + new Vector3(3f, 0, 0);
        GameObject carrot = Object.Instantiate(carrotPrefab, spawnPos, Quaternion.identity);
        carrot.name = "Carrot"; // để tiện tìm sau

        // ✅ Di chuyển player dần về phía carrot (giả lập nhấn D)
        float speed = 2f;
        float timePassed = 0f;
        float duration = 2f;

        while (timePassed < duration)
        {
            player.transform.position += Vector3.right * speed * Time.deltaTime;
            anim.SetFloat("speed", speed);
            timePassed += Time.deltaTime;
            yield return null; // chờ 1 frame
        }

        // Đợi xử lý va chạm
        yield return new WaitForSeconds(3f);

        // ✅ Kiểm tra carrot đã bị hủy chưa
        Assert.IsNull(GameObject.Find("Carrot"), "Carrot không bị hủy sau va chạm.");

        // ✅ Kiểm tra số lượng carrot đã tăng
        Assert.AreEqual(1, Score.carrot, "Carrot count không tăng sau khi nhặt.");

        Debug.Log("Test Passed! Player nhặt carrot thành công.");
    }
}


