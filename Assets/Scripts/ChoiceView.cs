using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

[XmlType ("ChoiceView")]
public class ChoiceView : View {

   
    [XmlAttribute("ChoiceMessage")] //Message associated with the second choice.
    public string choice2;

    [XmlAttribute("ThirdChoiceMessage")] //Message associated with the third choice.
    public string choice3;

    [XmlAttribute("Scene1")] //The name of the scene that is loaded if the first choice is made.
    public string scene1;

    [XmlAttribute("Scene2")] //The name of the scene that is loaded if the second choice is made.
    public string scene2;

    [XmlAttribute("Scene3")] //The name of the scene that is loaded if the third choice is made.
    public string scene3;




}
