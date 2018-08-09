﻿using UnityEngine;

namespace ShootAR.Enemies
{
	/// <summary>
	/// Parent class of all types of enemies.
	/// </summary>
	public abstract class Enemy : MonoBehaviour, ISpawnable, IOrbiter
	{
		[SerializeField] private EnemyBase @base;

		public EnemyBase Base { get { return @base; } }

		/// <summary>
		/// Count of currently active enemies.
		/// </summary>
		public static int ActiveCount { get; set; }

		[SerializeField] protected AudioClip attackSfx;
		[SerializeField] protected GameObject explosion;
		protected AudioSource sfx;
		protected static GameManager gameManager;

		public static Enemy Create(float speed, int damage, int pointsValue,
			float x = 0f, float y = 0f, float z = 0f)
		{
			var o = new GameObject("Enemy").AddComponent<Enemy>();
			o.@base = new EnemyBase(speed, damage, pointsValue);
			o.transform.position = new Vector3(x, y, z);
			return o;
		}

		protected void Awake()
		{
			ActiveCount++;
		}

		protected virtual void Start()
		{
			//Create an audio source to play the audio clips
			sfx = gameObject.AddComponent<AudioSource>();
			sfx.clip = attackSfx;
			sfx.volume = 0.3f;
			sfx.playOnAwake = false;
			sfx.maxDistance = 10f;

			Base.Orbiter = this;

			if (gameManager != null) gameManager = FindObjectOfType<GameManager>();
		}

		protected virtual void OnDestroy()
		{
			if (!gameManager.gameOver)
			{
				gameManager.AddScore(Base.PointsValue);
				Instantiate(explosion, transform.position, transform.rotation);
			}
			ActiveCount--;
		}

		/// <summary>
		/// Enemy moves towards a point using the physics engine.
		/// </summary>
		public void MoveTo(Vector3 point)
		{
			transform.LookAt(point);
			transform.forward = -transform.position;
			GetComponent<Rigidbody>().velocity = transform.forward * Base.Speed;
		}

		public void MoveTo(float x, float y, float z)
		{
			Vector3 point = new Vector3(x, y, z);
			MoveTo(point);
		}

		/// <summary>
		/// Object orbits around a defined point by an angle based on its speed.
		/// </summary>
		/// <param name="orbit">The orbit to move in</param>
		public void OrbitAround(Orbit orbit)
		{
			transform.LookAt(orbit.direction, orbit.perpendicularAxis);
			transform.RotateAround(orbit.direction, orbit.perpendicularAxis, Base.Speed * Time.deltaTime);
		}
	}
}