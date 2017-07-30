using UnityEngine;
using System.Collections;
using System;

namespace UnityRTS 
{
	public class RTS_Building : RTS_Selectable
	{
		// constructor
		public RTS_Building () 
		{

		}

		protected override void Awake () {
			base.Awake (); // call -parent awake function
		}
	}
}

