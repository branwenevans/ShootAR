﻿using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using ShootAR;

public class SpawnerTests {

	[UnityTest]
	public IEnumerator SpawnerStopsWhenLimitReached() {

		Spawner<OrbitTester> spawner = Spawner<OrbitTester>.Create(
			objectToSpawn: new GameObject("OrbitTester").AddComponent<OrbitTester>(),
			spawnLimit: 5,
			spawnRate: 1,
			maxDistanceToSpawn: 10f,
			minDistanceToSpawn: 3f
		);

		spawner.StartSpawning();
		yield return new WaitUntil(() => spawner.SpawnCount == spawner.SpawnLimit);

		Assert.That(spawner.IsSpawning == false, "Spawner stops when the spawn" +
			" limit is reached.");
	}

	[UnityTest]
	public IEnumerator SpawnerSpawnsTheCorrectNumberOfObjects()
	{
		Spawner<OrbitTester> spawner = Spawner<OrbitTester>.Create(
			objectToSpawn: new GameObject("SpawnableObject").AddComponent<OrbitTester>(),
			spawnLimit: 5,
			spawnRate: 1,
			maxDistanceToSpawn: 10f,
			minDistanceToSpawn: 3f
		);

		spawner.StartSpawning();
		yield return new WaitWhile(() => spawner.IsSpawning);

		int numberOfSpawned = Object.FindObjectsOfType<OrbitTester>().Length - 1;
		Assert.AreEqual(5, numberOfSpawned, "Spawn 5 objects.");
	}

	[UnityTest]
	public IEnumerator SpawnerCanStopSpawning()
	{
		Spawner<OrbitTester> spawner = Spawner<OrbitTester>.Create(
			new GameObject("Spawnable").AddComponent<OrbitTester>(), 5, 1, 10f, 3f);

		spawner.StartSpawning();
		yield return new WaitWhile(() => spawner.SpawnCount < 3);
		spawner.StopSpawning();
		

		int numberOfSpawned = Object.FindObjectsOfType<OrbitTester>().Length - 1;
		Assert.AreEqual(3, numberOfSpawned, "Interrupt spawning after spawning 5 objects.");
	}

	[UnityTest]
	public IEnumerator SpawnerStopsSpawningAtGameOver()
	{
		yield return null;
	}

	[UnityTest]
	public IEnumerator SpawnerStopsSpawningAtRoundEnd()
	{
		yield return null;
	}

	[UnityTest]
	public IEnumerator ObjectsAreSpawnedInsideTheDesiredArea()
	{
		yield return null;
	}

	[TearDown]
	public void ClearTestEnvironment()
	{
		OrbitTester[] objects = Object.FindObjectsOfType<OrbitTester>();
		foreach (var o in objects)
		{
			Object.Destroy(o.gameObject);
		}

		Spawner<OrbitTester>[] spawners = Object.FindObjectsOfType<Spawner<OrbitTester>>();
		foreach (var s in spawners)
		{
			Object.Destroy(s.gameObject);
		}
	}
}