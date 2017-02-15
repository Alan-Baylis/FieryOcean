using UnityEngine;
using System.Collections;


public class InspectorNameAttribute : PropertyAttribute
{
	public string name;

	public InspectorNameAttribute(string name)
	{
		this.name = name;
	}
}