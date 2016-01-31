using UnityEngine;
using DG.Tweening;
using System.Collections;

public enum EntityType
{
	Villain,
	Type0,
	Type1,
	Type2
}
public enum EntityState
{
	Idle,
	Walk,
	Fight,
	Dead
}

[RequireComponent(typeof(Collider), typeof(Rigidbody), typeof(Animator))]
public class Entity : MonoBehaviour
{
	#region Inspector Variables
	[SerializeField]
	EntityType _type;
	[SerializeField]
	EntityState _state;
	[SerializeField]
	bool _friendly;
	#endregion

	Animator _animator;

	public EntityType Type { get { return _type; } }
	public EntityState State { get { return _state; } }

	public void Kill()
	{
		_state = EntityState.Dead;

		_animator.SetTrigger("Die");

		if (!_friendly)
		{
			EnemyManager.AliveEnemies--;
		}

		GetComponents<BoxCollider>()[0].enabled = false;

		DOTween.Sequence()
			.AppendInterval(.5f)
			.Append(transform.DOLocalMoveY(-1f, 6f))
			.OnComplete(() => Destroy(this.gameObject));
	}
	public void Fight(Entity target)
	{
		_state = EntityState.Fight;

		_animator.SetTrigger("Fight");

		if (!_friendly)
		{
			EnemyManager.Fighting = true;
		}

		if (target.Type == Type && _friendly)
		{
			Kill();
		}
		if (Type == EntityType.Type0 && target.Type == EntityType.Type2)
		{
			Kill();
		}
		if (Type == EntityType.Type1 && target.Type == EntityType.Type0)
		{
			Kill();
		}
		if (Type == EntityType.Type2 && target.Type == EntityType.Type1)
		{
			Kill();
		}

		DOTween.Sequence().AppendInterval(1.5f).OnComplete(() => _state = EntityState.Walk);
	}

	void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	void OnTriggerEnter(Collider other)
	{
		var target = other.gameObject.GetComponent<Entity>();

		if (target == null || target._friendly == _friendly)
		{
			return;
		}

		Fight(target);
	}

	public void OnReachedLaneBegin()
	{
		_state = EntityState.Idle;
	}
	public void OnReachedLaneEnd()
	{
		_state = EntityState.Idle;

		GameManager.SwitchGameState(GameState.Lost);
	}
}
