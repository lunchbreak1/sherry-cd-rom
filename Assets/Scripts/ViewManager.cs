using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("ViewManager")]
public class ViewManager
{
    [XmlArray("Views")]
    [XmlArrayItem("View", typeof(View)), XmlArrayItem("ChoiceView", typeof(ChoiceView))]
    public List<View> views = new List<View>();

     

    public static ViewManager Load(string path)
    {

        TextAsset xml = Resources.Load<TextAsset>(path);
        Debug.Log("XML file loaded.");

        XmlSerializer serializer = new XmlSerializer(typeof(ViewManager));
        Debug.Log("Serializer created.");

        StringReader reader = new StringReader(xml.text);
        Debug.Log("XML reader created.");

        ViewManager views = serializer.Deserialize(reader) as ViewManager;
        Debug.Log("XML file deserialized into list.");

        reader.Close();
        Debug.Log("Reader closed.");

        return views;
    }
	
}
