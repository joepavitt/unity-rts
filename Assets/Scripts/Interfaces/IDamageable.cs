using UnityEngine;
using System.Collections;
using System;

namespace UnityRTS
{
	public interface IDamageable<T>
	{
		void Damage(T damage);
	}
}

