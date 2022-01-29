using Unity.FPS.AI;
using UnityEngine;

namespace Assets.FPS.Scripts.AI
{
	public class EnemySwitch : EnemyTurret
	{
		new AIState AiState;
		private void Update()
		{
			AiState = AIState.Idle;
		}
	}
}
