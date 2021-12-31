using TMPro;
using UnityEngine;

[DisallowMultipleComponent]
public class HUDController : MonoBehaviour
{

	[SerializeField] private Noise3DTerrainGenerator _generator;
	[SerializeField] private TMP_InputField _seedInputField;
	[SerializeField] private TextMeshProUGUI _offsetLabel;

	public void Quit()
	{
		Application.Quit();
	}

	private void OnEnable()
	{
		_seedInputField.text = _generator.Seed.ToString();
		_seedInputField.onEndEdit.AddListener(SetSeedFromInput);
	}

	private void OnDisable()
	{
		_seedInputField.onEndEdit.RemoveListener(SetSeedFromInput);
	}

	private void Update()
	{
		_offsetLabel.text = $"Offset:\n{_generator.Offset}";
	}

	public void SetSeedFromInput(string seed)
	{
		_generator.Seed = seed;
	}

	public void ResetOffset()
	{
		_generator.ResetOffset();
	}
}
