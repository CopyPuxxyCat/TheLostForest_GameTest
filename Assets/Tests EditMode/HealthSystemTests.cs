using NUnit.Framework;
using UnityEngine;
using System;

public class HealthSystemTests
{
    [Test]
    public void HealthChanges_ShouldAffectUI()
    {
        GameObject obj = new GameObject();
        HealthSystem healthSystem = obj.AddComponent<HealthSystem>();

        healthSystem.GetType().GetField("live3", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)?.SetValue(healthSystem, new GameObject());
        healthSystem.GetType().GetField("live2", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)?.SetValue(healthSystem, new GameObject());
        healthSystem.GetType().GetField("live1", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)?.SetValue(healthSystem, new GameObject());
        healthSystem.GetType().GetField("live0", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)?.SetValue(healthSystem, new GameObject());

        HealthSystem.health = 2;
        healthSystem.Update();

        Assert.IsFalse(healthSystem.live3.activeSelf);
        Assert.IsTrue(healthSystem.live2.activeSelf);
        Debug.Log(GetObjectState(healthSystem.live3));
        Debug.Log(GetObjectState(healthSystem.live2));
        Debug.Log(GetObjectState(healthSystem.live1));
        Debug.Log(GetObjectState(healthSystem.live0));
        //Assert.IsTrue(healthSystem.live3.activeSelf);
        //Assert.IsTrue(healthSystem.live2.activeSelf);

        // kiem tra neu live2 = true thi dung
        /*if(healthSystem.live2.activeSelf == true)
        {
            Assert.IsTrue(healthSystem.live2.activeSelf == true);
        }
        Assert.IsFalse(healthSystem.live2.activeSelf);*/
    }
    string GetObjectState(GameObject obj)
    {
        if (!obj)
            return "GameObject is null";

        return $"activeSelf: {obj.activeSelf}, activeInHierarchy: {obj.activeInHierarchy}";
    }
}

