using CarsonSkill.Runtime;
using UnityEngine;
using UnityEngine.UIElements;

namespace SkillEditor
{
    public class VE_TrackDetailListCell : VisualElement
    {
        private TrackData _trackData;

        public VE_TrackDetailListCell()
        {
            // 横向填充满整个单元格
            style.flexDirection = FlexDirection.Row;
            style.flexGrow = 1;
            
            // 所有颜色都是透明的
            style.backgroundColor = new StyleColor(Color.clear);
            style.borderTopColor = new StyleColor(Color.clear);
            style.borderRightColor = new StyleColor(Color.clear);
            style.borderBottomColor = new StyleColor(Color.clear);
            style.borderLeftColor = new StyleColor(Color.clear);
            
            // 高度为100
            style.height = 100;
            style.minHeight = 100;
            
            // 右键菜单，创建新的ClipItem
            var contextMenu = new ContextualMenuManipulator(evt =>
            {
                evt.menu.AppendAction("Create ClipItem", e =>
                {
                    var clipItem = new VE_ClipItem();
                    Add(clipItem);
                });
            });
            this.AddManipulator(contextMenu);
        }

        public void BindItem(TrackData trackData)
        {
            _trackData = trackData;
        }
    }
}