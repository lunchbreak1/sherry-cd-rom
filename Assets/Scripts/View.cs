using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

[XmlType("View")]
public class View {

    [XmlAttribute("Actor1")] //The appearance of the first character in the scene.
    public string actor1;

    [XmlAttribute("Actor2")] //The apperance of the second character in the scene.
    public string actor2;

    [XmlAttribute("Actor3")] //The appearance of the third character in the scene.
    public string actor3;

    [XmlAttribute("Actor4")] //The apperance of the fourth character in the scene.
    public string actor4;

    [XmlAttribute("Background")] //The appearance of the image in the background.
    public string bg;

    [XmlAttribute("Text")] //What is displayed in the main textbox.
    public string txt = "";

    [XmlAttribute("ColorText")] //The message edited with special colors.
    public string color;

    [XmlAttribute("Font")] //What font is used for the textbox message.
    public string font;

    [XmlAttribute("Music")] //The music playing in the background.
    public string music;

    [XmlAttribute("Transition")]
    public string transition;

    public enum SpecialAnimation { //An action that a character can perform.
        [XmlEnum("Default")] //No animation.
        Default,

        [XmlEnum("Shaking")] //Shaking in place, due to nervousness.
        Shaking,

        [XmlEnum("Moving")] //Moving to a location.
        Moving
    };

    [XmlAttribute("Actor1Action")] //The action of the first character.
    public SpecialAnimation action1;

    [XmlAttribute("Actor2Action")] //The action of the second character.
    public SpecialAnimation action2;

    [XmlAttribute("Actor3Action")] //The action of the third character.
    public SpecialAnimation action3;

    [XmlAttribute("Actor4Action")] //The action of the fourth character.
    public SpecialAnimation action4;

    [XmlAttribute("Position1")] //The position of the first character.
    public string start1;

    [XmlAttribute("Position2")] //The position of the second character.
    public string start2;

    [XmlAttribute("Position3")] //The position of the third character.
    public string start3;

    [XmlAttribute("Position4")] //The position of the fourth character.
    public string start4;

    [XmlAttribute("Target1")] //The target of the first character.
    public string target1;

    [XmlAttribute("Target2")] //The target of the second character.
    public string target2;

    [XmlAttribute("Target3")] //The target of the third character.
    public string target3;

    [XmlAttribute("Target4")] //The target of the fourth character.
    public string target4;




}
