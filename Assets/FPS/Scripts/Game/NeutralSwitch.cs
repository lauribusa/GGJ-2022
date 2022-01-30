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
		private int materialIndex;
		public SwitchState switchState { get; private set; }
		private void Awake()
		{
			levelManager = FindObjectOfType<LevelManager>();
			damageable = GetComponent<Damageable>();
			health = GetComponent<Health>();
			EventManager.AddListener<ColorSwitchEvent>(OnColorSwitchEvent);
		}

		public void OnDamageTrigger()
		{
			levelManager.TriggerColorSwitchEvent();
		}

		private void Update()
		{
			if(health.CurrentHealth < health.MaxHealth)
			{
				health.CurrentHealth = health.MaxHealth;
			}
		}

		public void OnColorSwitchEvent(ColorSwitchEvent evt)
		{
			switch (evt.newActiveColor)
			{
				case ActiveColor.RED:
					gameObject.GetComponent<MeshRenderer>().material = materials[0];
					materialIndex = 0;
					break;
				case ActiveColor.BLUE:
					gameObject.GetComponent<MeshRenderer>().material = materials[1];
					materialIndex = 1;
					break;
				default:
					throw new Exception("Out of range");
			}
		}
		private void OnDestroy()
		{
			EventManager.RemoveListener<ColorSwitchEvent>(OnColorSwitchEvent);
		}
	}
}
