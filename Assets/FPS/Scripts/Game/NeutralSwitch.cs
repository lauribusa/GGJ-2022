using System.Collections;
using UnityEngine;
using Unity.FPS.Game;
using System;

namespace Unity.FPS.Game
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
		private LevelManager levelManager;
		private Coroutine damageCoroutine;
		private int materialIndex;
		public SwitchState switchState { get; private set; }
		private void Awake()
		{
			levelManager = FindObjectOfType<LevelManager>();
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

		public void OnDamageTrigger()
		{
			if(damageCoroutine != null)
			{
				StopCoroutine(IsDamaged());
				damageCoroutine = null;
			}
			damageCoroutine = StartCoroutine(IsDamaged());

		}

		private void Update()
		{
			if(health.CurrentHealth < health.MaxHealth)
			{
				health.CurrentHealth = health.MaxHealth;
			}
		}
		public IEnumerator IsDamaged()
		{
			switchState = SwitchState.IsTriggered;
			ColorSwitchEvent evt = Events.ColorSwitchEvent;
			EventManager.Broadcast(evt);

			if(materialIndex == 0)
			{
				gameObject.GetComponent<MeshRenderer>().material = materials[1];
				materialIndex = 1;
			} else
			{
				gameObject.GetComponent<MeshRenderer>().material = materials[0];
				materialIndex = 0;
			}

			yield return new WaitForSeconds(1);
			switchState = SwitchState.Idle;
			yield return null;
		}

		
	}
}
