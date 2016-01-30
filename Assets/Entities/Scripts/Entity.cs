using UnityEngine;
using System.Collections;

public enum EntityType
{
	Type0,
	Type1,
	Type2
}

public enum EntityState
{
	walk,
	fight,
	dead
}

public class Entity
{
	public EntityType _entityType;
	public EntityState _entityState;
}
