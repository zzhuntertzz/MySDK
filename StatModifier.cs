using System;
using Newtonsoft.Json;

public enum StatModType
{
	Flat = 100,
	PercentAdd = 200,
	PercentMult = 300,
}
[Serializable]
public class StatModifier
{
	[JsonProperty("v")]
	public float Value;
	[JsonProperty("t")]
	public StatModType Type;
	[JsonProperty("o")]
	public int Order;
	[JsonProperty("s")]
	public string Source;
	public StatModifier(float value, StatModType type, int order, string source = "")
	{
		Value = value;
		Type = type;
		Order = order;
		Source = source;
	}
	public StatModifier(float value, StatModType type) : this(value, type, (int)type) { }

	public StatModifier(float value, StatModType type, string source) : this(value, type, (int)type, source) { }
}