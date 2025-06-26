using NUnit.Framework;
using UnityEngine;
using AdventuresOfBlink.Targeting;

public class TargetingSystemTests
{
    [Test]
    public void CycleTarget_SelectsTargetsFromNearestToFarthest()
    {
        var player = new GameObject("Player");
        var system = player.AddComponent<TargetingSystem>();

        var near = new GameObject("Near");
        var nearTarget = near.AddComponent<Targetable>();
        near.transform.position = new Vector3(1f, 0f, 0f);

        var middle = new GameObject("Middle");
        var middleTarget = middle.AddComponent<Targetable>();
        middle.transform.position = new Vector3(3f, 0f, 0f);

        var far = new GameObject("Far");
        var farTarget = far.AddComponent<Targetable>();
        far.transform.position = new Vector3(5f, 0f, 0f);

        system.CycleTarget();
        Assert.AreEqual(nearTarget, system.CurrentTarget);

        system.CycleTarget();
        Assert.AreEqual(middleTarget, system.CurrentTarget);

        system.CycleTarget();
        Assert.AreEqual(farTarget, system.CurrentTarget);

        system.CycleTarget();
        Assert.AreEqual(nearTarget, system.CurrentTarget);
    }
}
