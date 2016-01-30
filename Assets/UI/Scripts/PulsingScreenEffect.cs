using UnityEngine;

public class PulsingScreenEffect : MonoBehaviour
{
	#region Inspector Variables
	[SerializeField]
	Color _color;
	[SerializeField]
	float _fadeTime = 1.0f;
	[SerializeField]
	Shader _shader;
	#endregion

	float _power = 0.0f;
	float _amount = 0.0f;
	Material _material;

	void Awake()
	{
		if (!SystemInfo.supportsImageEffects || !_shader.isSupported)
		{
			enabled = false;
		}
	}
	void OnEnable()
	{
		_amount = 0.0f;
		_material = new Material(_shader);
		_material.hideFlags = HideFlags.DontSave;
	}
	void OnDisable()
	{
		if (_material)
		{
			DestroyImmediate(_material);
		}
	}

	void Update()
	{
		if (_amount < 1.0f)
		{
			_amount += Time.deltaTime * _fadeTime;
		}

		_power = Mathf.PingPong(Time.time, 0.5f) + 0.5f;
	}

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		_material.SetColor("_Color", _color);
		_material.SetFloat("_Power", _power);
		_material.SetFloat("_Amount", _amount);

		Graphics.Blit(source, destination, _material, 0);
	}
}
