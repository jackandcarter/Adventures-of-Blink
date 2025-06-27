using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using AdventuresOfBlink.Targeting;
using AdventuresOfBlink.UI;
using AdventuresOfBlink.Combat;

public class HealthAndTargetingTests
{
    [Test]
    public void CycleTarget_WorksWithoutHealthComponent()
    {
        var player = new GameObject("Player");
        var system = player.AddComponent<TargetingSystem>();

        var targetObj = new GameObject("Target");
        var target = targetObj.AddComponent<Targetable>();
        targetObj.transform.position = Vector3.one;

        system.CycleTarget();
        Assert.AreEqual(target, system.CurrentTarget);
    }

    [Test]
    public void TargetPanel_DisplaysHealthRatio()
    {
        var systemObj = new GameObject("System");
        var system = systemObj.AddComponent<TargetingSystem>();

        var targetObj = new GameObject("Enemy");
        targetObj.transform.position = Vector3.one;
        var target = targetObj.AddComponent<Targetable>();
        target.displayName = "Enemy";
        var health = targetObj.AddComponent<Health>();
        health.maxHealth = 100;
        health.currentHealth = 50;

        var panelObj = new GameObject("Panel");
        var text = panelObj.AddComponent<TextMeshProUGUI>();
        var slider = panelObj.AddComponent<Slider>();
        var panel = panelObj.AddComponent<TargetPanel>();
        panel.targetingSystem = system;
        panel.nameText = text;
        panel.healthSlider = slider;
        panel.OnEnable();

        system.CycleTarget();
        Assert.AreEqual("Enemy", text.text);
        Assert.AreEqual(0.5f, slider.value, 0.001f);
    }

    [Test]
    public void HealthBar_UpdatesWhenHealthChanges()
    {
        var obj = new GameObject("Target");
        var health = obj.AddComponent<Health>();
        health.maxHealth = 100;
        health.currentHealth = 100;

        var sliderObj = new GameObject("Slider");
        var slider = sliderObj.AddComponent<Slider>();
        var bar = sliderObj.AddComponent<HealthBar>();
        bar.slider = slider;
        bar.health = health;
        bar.alwaysVisible = true;

        typeof(HealthBar).GetMethod("Awake", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .Invoke(bar, null);

        Assert.AreEqual(1f, slider.value, 0.001f);

        health.ApplyDamage(30);
        bar.Update();
        Assert.AreEqual(0.7f, slider.value, 0.001f);
    }
}
