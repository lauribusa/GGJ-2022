using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.FPS.Game;
using UnityEngine;

namespace Assets.FPS.Scripts.AI
{
	public class NeutralSwitch : MonoBehaviour
	{
		public enum SwitchState
		{
			Idle,
			IsTriggered,
		}

		[Tooltip("The random hit damage effects")]
		public ParticleSystem[] RandomHitSparks;
		public AudioClip OnColorToggleSfx;
		public Material[] materials;
		public Damageable damageable;
		public Health health;
		private int materialIndex;
		public SwitchState switchState { get; private set; }
		private void Awake()
		{
			damageable = GetComponent<Damageable>();
			health = GetComponent<Health>();
			if(materials[0] == gameObject.GetComponent<MeshRenderer>().material)
			{
				materialIndex = 0;
				gameObject.GetComponent<MeshRenderer>().material = materials[0];
			} else
			{
				materialIndex = 1;
				gameObject.GetComponent<MeshRenderer>().material = materials[1];
			}
		}
		public IEnumerator IsDamaged()
		{
			switchState = SwitchState.IsTriggered;
			EventManager.Broadcast(Events.ColorSwitchEvent);
			gameObject.GetComponent<MeshRenderer>().material = materialIndex == 0 ? materials[1] : materials[0];
			yield return new WaitForSeconds(1);
			switchState = SwitchState.Idle;
		}

		
	}
}
