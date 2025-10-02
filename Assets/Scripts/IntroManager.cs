using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class IntroManager : MonoBehaviour
{
    [Header("Story Settings")]
    public Image[] storyImages;       // Ảnh minh họa cốt truyện
    public string[] storyTexts;       // Nội dung từng đoạn
    public TextMeshProUGUI storyText;
    public float typeSpeed = 0.05f;   // Tốc độ chữ hiện

    [Header("Scene Settings")]
    public string nextScene = "MainGame";

    [Header("Logo cuối")]
    public Image gameLogo;            // Logo game cuối cùng
    public float logoDisplayTime = 2f;

    private int currentIndex = 0;

    private void Start()
    {
        // Ẩn tất cả ảnh và logo lúc đầu
        foreach (var img in storyImages)
            img.gameObject.SetActive(false);

        if (gameLogo != null)
            gameLogo.gameObject.SetActive(false);

        StartCoroutine(PlayStory());
    }

    private IEnumerator PlayStory()
    {
        for (currentIndex = 0; currentIndex < storyImages.Length; currentIndex++)
        {
            // Hiển thị hình ảnh
            storyImages[currentIndex].gameObject.SetActive(true);

            // Hiển thị chữ kiểu đánh máy
            yield return StartCoroutine(TypeText(storyTexts[currentIndex]));

            // Chờ người chơi bấm phím để chuyển đoạn hoặc tự động sau 1-2 giây
            float waitTime = 1.5f;
            float t = 0f;
            bool skipped = false;
            while (t < waitTime)
            {
                if (Input.anyKeyDown)
                {
                    skipped = true;
                    break;
                }
                t += Time.deltaTime;
                yield return null;
            }

            // Ẩn hình ảnh trước khi chuyển đoạn
            storyImages[currentIndex].gameObject.SetActive(false);
        }

        // Hiển thị logo cuối cùng nếu có
        if (gameLogo != null)
        {
            gameLogo.gameObject.SetActive(true);
            yield return new WaitForSeconds(logoDisplayTime);
        }

        // Chuyển scene chính
        SceneManager.LoadScene(nextScene);
    }

    private IEnumerator TypeText(string text)
    {
        storyText.text = "";
        foreach (char c in text)
        {
            storyText.text += c;
            yield return new WaitForSeconds(typeSpeed);

            // Skip typewriter nếu người chơi bấm phím
            if (Input.anyKeyDown)
            {
                storyText.text = text;
                yield break;
            }
        }
    }
}
