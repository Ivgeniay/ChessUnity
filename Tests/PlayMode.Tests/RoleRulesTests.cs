using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class RoleRulesTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void RoleRulesTestsSimplePasses()
    {
        Figure figure = new Figure();
        
        //figure.Appoint(new FigureConfig());
        //RoleRules.canMove(figure, )


        Assert.IsTrue(!figure.isMooved);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator RoleRulesTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
