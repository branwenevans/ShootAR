﻿/* The reason why this class exists is because Spawner.objectToSpawn can not
 * be assigned an interface through Unity's Inspector. Best solution was to
 * use a class that is parent of every object that can be spawned from a
 * spawner. */

using UnityEngine;

namespace ShootAR
{
	public abstract class Spawnable : MonoBehaviour
	{
		public const int GLOBAL_SPAWN_LIMIT = 50;

		[SerializeField] private float speed;
		/// <summary>
		/// The speed at which this object is moving.
		/// </summary>
		public float Speed {
			get { return speed; }
			protected set { speed = value; }
		}

		/// <summary>
		/// Reference to the object holding the game state.
		/// </summary>
		protected GameState gameState;
		/// <summary>
		/// Setter for <see cref="Spawner"/>s to set a spawned object's game state.
		/// </summary>
		public GameState GameState {
			set { gameState = value; }
		}
	}
}
