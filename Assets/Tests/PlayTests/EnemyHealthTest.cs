using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;

public class EnemyHealthTest
{
    [Test]
    public void TestTakeDamage()
    {
        GameObject enemy = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Enemies/EnemyBomber.prefab"), 
                                     new Vector3(0, 0, 0),
                                     Quaternion.identity);

        enemy.GetComponent<EnemyHealth>().TakeDamage(1);
        Assert.AreEqual(enemy.GetComponent<EnemyHealth>().CurrentHealth, 5); // bombers have 6 health
        enemy.GetComponent<EnemyHealth>().TakeDamage(8);
        Assert.AreEqual(enemy.GetComponent<EnemyHealth>().CurrentHealth, 0); // cant dip below 0 on health
        Assert.AreEqual(enemy.activeInHierarchy, false); // enemy should destroy itself
    }
}
