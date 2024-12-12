using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace SkillEditor
{
    public class SkillEditorWindow : EditorWindow
    {
        [MenuItem("编辑器/Skill Editor")]
        public static void ShowWindow()
        {
            SkillEditorWindow wnd = GetWindow<SkillEditorWindow>();
            wnd.titleContent = new GUIContent("Skill Editor");
        }

        public void OnEnable()
        {
            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;
            
            var main = new VE_Main();
            
            root.Add(main);
            
            // stretch to the window size
            main.StretchToParentSize();
        }
    }
}