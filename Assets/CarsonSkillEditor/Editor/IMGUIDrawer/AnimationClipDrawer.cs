using UnityEditor;
using UnityEngine;

namespace SkillEditor
{
    public class AnimationClipDrawer : TimelineClipDrawer
    {
        public override void OnGUI(Rect rect)
        {
            base.OnGUI(rect);
            
            var itemRect = ItemRect;
            
            // 画一个矩形
            EditorGUI.DrawRect(itemRect, Color.red);
        }
    }
}