using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal.Input;
using UnityEditor;
using UnityEditor.Build.Content;



public class InputManager : MonoBehaviour
{
    public string[] inputNumber;
    public Object[] trys;

    public static int controllerOne;
    public static int controllerTwo;

    [Header("Caratéristique")]
    public float sensitivity;
    public float gravity;
    public float dead;

        
    public void Awake()
    {
        this.inputNumber = Input.GetJoystickNames();
        for (int i = 0; i < inputNumber.Length; i++)
        {
            if (inputNumber[i] != "")
            {
                controllerOne = i + 1;
                break;
            }
        }
        for (int i = 0; i < inputNumber.Length; i++)
        {
            if (inputNumber[i] != "" && i > controllerOne - 1)
            {
                controllerTwo = i + 1;
                break;
            }
        }

        //SetupInputManager(controllerOne, controllerTwo);

        



    }

}

//    private static SerializedProperty GetChildProperty(SerializedProperty parent, string name)
//    {
//        SerializedProperty child = parent.Copy();
//        child.Next(true);

//        do
//        {
//            if (child.name == name) return child;
//        }
//        while (child.Next(false));
//        return null;
//    }

//    private static bool AxisDefined(string axisaName)
//    {
//        SerializedObject serializedObject = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset"));
//        SerializedProperty axesProperty = serializedObject.FindProperty("m_Axes");
//        axesProperty.Next(true);
//        axesProperty.Next(true);
//        while (axesProperty.Next(false))
//        {
//            SerializedProperty axis = axesProperty.Copy();
//            axis.Next(true);
//            if (axis.stringValue == axisaName) return true;
//        }
//        return false;
//    }

//    public enum AxisType { KeyOrMouseButton = 0, MouseMouvement = 1, JoystickAxis = 2 };

//    public class InputAxis
//    {
//        public string name;
//        public string descriptiveName;
//        public string descriptiveNegativeName;
//        public string negativeButton;
//        public string positiveButton;
//        public string altNegativeButton;
//        public string altPositiveButton;

//        public float gravity;
//        public float dead;
//        public float sensitivity;

//        public bool snap = false;
//        public bool invert = false;

//        public AxisType type;

//        public int axis;
//        public int joyNum;
//    }
//    private static void AddAxis(InputAxis axis)
//    {
//        if (AxisDefined(axis.name)) return;

//        SerializedObject serializedObject = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0]);
//        SerializedProperty axesProperty = serializedObject.FindProperty("m_Axes");

//        axesProperty.arraySize++;
//        serializedObject.ApplyModifiedProperties();

//        SerializedProperty axisProperty = axesProperty.GetArrayElementAtIndex(axesProperty.arraySize - 1);

//        GetChildProperty(axisProperty, "m_Name").stringValue = axis.name;
//        GetChildProperty(axisProperty, "descriptiveName").stringValue = axis.descriptiveName;
//        GetChildProperty(axisProperty, "descriptiveNegativeName").stringValue = axis.descriptiveNegativeName;
//        GetChildProperty(axisProperty, "negativeButton").stringValue = axis.negativeButton;
//        GetChildProperty(axisProperty, "positiveButton").stringValue = axis.positiveButton;
//        GetChildProperty(axisProperty, "altNegativeButton").stringValue = axis.altNegativeButton;
//        GetChildProperty(axisProperty, "altPositiveButton").stringValue = axis.altPositiveButton;
//        GetChildProperty(axisProperty, "gravity").floatValue = axis.gravity;
//        GetChildProperty(axisProperty, "dead").floatValue = axis.dead;
//        GetChildProperty(axisProperty, "sensitivity").floatValue = axis.sensitivity;
//        GetChildProperty(axisProperty, "snap").boolValue = axis.snap;
//        GetChildProperty(axisProperty, "invert").boolValue = axis.invert;
//        GetChildProperty(axisProperty, "type").intValue = (int)axis.type;
//        GetChildProperty(axisProperty, "axis").intValue = axis.axis - 1;
//        GetChildProperty(axisProperty, "joyNum").intValue = axis.joyNum;

//        serializedObject.ApplyModifiedProperties();
//    }

//    public  void SetupInputManager(int i, int j)
//    {
//        for (int h = 1; h < 3; h++)
//        {
//            if (h < 2)
//            {
//                AddAxis(new InputAxis()
//                {
//                    name = "Horizontal" + 1.ToString(),
//                    gravity = gravity ,
//                    dead = dead,
//                    sensitivity = sensitivity,
//                    snap = true,
//                    invert = false,
//                    type = AxisType.JoystickAxis,
//                    axis = 1,
//                    joyNum = i,


//                });
//                AddAxis(new InputAxis()
//                {
//                    name = "Vertical" + 1.ToString(),
//                    gravity = 1f,
//                    dead = 0.2f,
//                    sensitivity = 1,
//                    snap = true,
//                    invert = true,
//                    type = AxisType.JoystickAxis,
//                    axis = 2,
//                    joyNum = i,


//                });
//            }
//            else
//            {
//                AddAxis(new InputAxis()
//                {
//                    name = "Horizontal" + 2.ToString(),
//                    gravity = 1f,
//                    dead = 0.2f,
//                    sensitivity = 1,
//                    snap = true,
//                    invert = false,
//                    type = AxisType.JoystickAxis,
//                    axis = 1,
//                    joyNum = j,


//                });
//                AddAxis(new InputAxis()
//                {
//                    name = "Vertical" + 2.ToString(),
//                    gravity = 1f,
//                    dead = 0.2f,
//                    sensitivity = 1,
//                    snap = true,
//                    invert = true,
//                    type = AxisType.JoystickAxis,
//                    axis = 2,
//                    joyNum = j,


//                });
//            }
//        }
//    }
//}
