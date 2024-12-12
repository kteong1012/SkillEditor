using CarsonSkill.Runtime;
using UnityEngine;
using UnityEngine.UIElements;

namespace SkillEditor
{
    public class VE_TrackListCell : VisualElement
    {
        private Label _label;
        private TrackData _trackData;

        public VE_TrackListCell()
        {
            _label = new Label();
            // 文字左侧居中
            _label.style.unityTextAlign = TextAnchor.MiddleLeft;
            Add(_label);
        }

        public void BindItem(TrackData trackData)
        {
            _trackData = trackData;

            _label.text = _trackData.trackName;
        }
    }
}