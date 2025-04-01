using NUnit.Framework;
using UnityEngine;
using TMPro;

[TestFixture]
public class ScoreTests
{
    private GameObject scoreGameObject;
    private Score scoreComponent;

    [SetUp]
    public void SetUp()
    {
        // Tạo một GameObject giả lập có component Score
        scoreGameObject = new GameObject();
        scoreComponent = scoreGameObject.AddComponent<Score>();

        // Thêm TextMeshPro để test score
        GameObject textObject = new GameObject();
        scoreComponent._scoreText = textObject.AddComponent<TextMeshProUGUI>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(scoreGameObject);
    }

    [Test]
    public void TestScoreIncrease()
    {
        // Arrange
        scoreComponent.score = 5;

        // Act
        scoreComponent.AddScoreToText();

        // Assert
        Assert.AreEqual("5", scoreComponent._scoreText.text, "Score text should be updated correctly.");
        Debug.Log("Score = : " + scoreComponent.score);
    }
}

