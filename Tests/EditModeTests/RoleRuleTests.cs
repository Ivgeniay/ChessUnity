using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class RoleRuleTests
{
    // A Test behaves as an ordinary method
    [Test]
    [TestCase("A1", "A2")]
    public void RoleRuleTestsSimplePasses()
    {
        //RoleRules.canMove();
        
        
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator RoleRuleTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
