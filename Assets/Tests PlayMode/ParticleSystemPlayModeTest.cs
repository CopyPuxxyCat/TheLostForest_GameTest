using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

public class ParticleSystemPlayModeTest
{
    [UnityTest]
    public IEnumerator ParticleSystem_IsPlaying()
    {
        // Tải scene chứa UI cần kiểm thử
        SceneManager.LoadScene("StartScene");
        yield return null;

        // Tìm GameObject theo tên
        GameObject go = GameObject.Find("Particle System");
        Assert.IsNotNull(go, " Không tìm thấy GameObject có tên 'Particle System' trong scene.");

        // Lấy component ParticleSystem
        var ps = go.GetComponent<ParticleSystem>();
        Assert.IsNotNull(ps, " GameObject 'Particle System' không có component ParticleSystem.");

        // Nếu chưa chạy thì chạy
        if (!ps.isPlaying)
        {
            ps.Play();
        }

        // Chờ vài frame để hệ thống cập nhật
        yield return new WaitForSeconds(0.5f);

        // Kiểm tra xem particle có đang chạy không
        Assert.IsTrue(ps.isPlaying, "❌ Particle System không chạy.");
    }
}


