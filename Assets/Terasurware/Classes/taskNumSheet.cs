using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class taskNumSheet : ScriptableObject
{	
	public List<Sheet> sheets = new List<Sheet> ();

	[System.SerializableAttribute]
	public class Sheet
	{
		public string name = string.Empty;
		public List<Param> list = new List<Param>();
	}

	[System.SerializableAttribute]
	public class Param
	{
		
		public int col1;
		public int col2;
		public int col3;
		public int col4;
	}
}

