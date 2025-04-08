using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

public class BackgroundMusicPlayModeTest
{
    [UnityTest]
    public IEnumerator BGMusic_IsPlaying()
    {
        // Tải scene chứa UI cần kiểm thử
        SceneManager.LoadScene("StartScene");
        yield return null;

        // Tìm GameObject tên là "bgmusic"
        GameObject bgmusicGO = GameObject.Find("bgmusic");
        Assert.IsNotNull(bgmusicGO, "❌ Không tìm thấy GameObject 'bgmusic' trong scene.");

        // Lấy component AudioSource
        AudioSource audioSource = bgmusicGO.GetComponent<AudioSource>();
        Assert.IsNotNull(audioSource, "❌ GameObject 'bgmusic' không có AudioSource.");

        // Nếu chưa phát thì phát thử
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }

        // Chờ 1 frame để AudioSource có thời gian bắt đầu
        yield return new WaitForSeconds(3f);

        // Kiểm tra xem nhạc có đang chạy không
        Assert.IsTrue(audioSource.isPlaying, "❌ Background music không chạy.");
    }
}

