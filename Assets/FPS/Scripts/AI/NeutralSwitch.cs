using System.Collections;
using UnityEngine;

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
			EventManager.Broadcast(Events.ColorSwitchEvent);

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
		}

		
	}
}
