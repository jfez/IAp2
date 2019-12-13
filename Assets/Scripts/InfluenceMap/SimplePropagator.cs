using UnityEngine;
using System.Collections;

public interface IPropagator
{
	Vector2I GridPosition { get; }
	float Value { get; }
}

public class SimplePropagator : MonoBehaviour, IPropagator
{
	[SerializeField]
	float _value;
	public float Value { get{ return _value; } }

	[SerializeField]
	InfluenceMapControl _map;

	//CharacterController _move;

	Vector3 _bottomLeft;
	Vector3 _topRight;

	Vector3 _destination;

	Vector3 _velocity;

	[SerializeField]
	float _speed;

	

	public Vector2I GridPosition
	{
		get
		{
			return _map.GetGridPosition(transform.position);
		}
	}

	// Use this for initialization
	void Start()
	{
		//_move = GetComponent<CharacterController>();
		//print(GameObject.FindGameObjectWithTag("InfluenceMapControl"));
		_map = GameObject.FindGameObjectWithTag("InfluenceMapControl").GetComponent<InfluenceMapControl>();
		_map.RegisterPropagator(this);
		_map.GetMovementLimits(out _bottomLeft, out _topRight);

		//InvokeRepeating("ChooseNewDestination", 0.001f, 3.0f);
	}

	// Update is called once per frame
	void Update()
	{
		/*_velocity = _destination - transform.position;
		_velocity.Normalize();
		_velocity *= _speed;

		_move.SimpleMove(_velocity);*/
	}

	public void removePropagator(){
		_map.DeletePropagator(this);
	}

	/*void ChooseNewDestination()
	{
		_destination = PickDestination();
	}*/

	/*Vector3 PickDestination()
	{
		return new Vector3(
			Random.Range(_bottomLeft.x, _topRight.x),
			Random.Range(_bottomLeft.y, _topRight.y),
			Random.Range(_bottomLeft.z, _topRight.z)
		);
	}*/
}
