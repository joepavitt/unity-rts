using UnityEngine;
using System.Collections;
using System;

namespace UnityRTS
{
	public class RTS_Creatable : MonoBehaviour, IDamageable<float>, IKillable
	{
		public string name;
		public string family;

		public float health;

		// constructor 
		public RTS_Creatable ()
		{
			
		}

		#region IDamageable implementation
		public void Damage (float damage)
		{
			health -= damage;
			if (health < 0) {
				Kill ();
			}
		}
		#endregion

		#region IKillable implementation
		public void Kill ()
		{
			throw new NotImplementedException ();
		}
		#endregion
	}
}

