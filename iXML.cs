using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
namespace npScripts
{
	public interface iXML
	{
		void ReadFromXML(XmlReader reader);
		void WriteToXML(XmlWriter writer);
		string GetElementName();
	}
}