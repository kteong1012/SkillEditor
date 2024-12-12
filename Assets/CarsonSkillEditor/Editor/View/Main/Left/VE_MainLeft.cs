using System.Collections.Generic;
using CarsonSkill.Runtime;
using UnityEngine;
using UnityEngine.UIElements;

namespace SkillEditor
{
    public class VE_MainLeft : VisualElement
    {
        private ListView _trackNameListView;
        private List<TrackData> _trackDataList = new();
        private readonly Label _axisScaleLabel;

        public VE_MainLeft()
        {
            // 添加一个高度为50的空区域
            var emptyArea = new VisualElement();
            emptyArea.style.height = 50;
            _axisScaleLabel = new Label("Axis Scale");
            emptyArea.Add(_axisScaleLabel);
            TimelineAxisManager.OnScaleChanged -= OnScaleChanged;
            TimelineAxisManager.OnScaleChanged += OnScaleChanged;
            Add(emptyArea);
            
            _trackNameListView = new ListView(_trackDataList, 100, MakeItem, BindItem);
            _trackNameListView.style.flexGrow = 1;
            Add(_trackNameListView);
        }
        
        private void OnScaleChanged(float scale)
        {
            _axisScaleLabel.text = $"Axis Scale: {scale:F2}";
        }

        public void UpdateTrackDataList(List<TrackData> trackDataList)
        {
            _trackDataList = trackDataList;
            _trackNameListView.itemsSource = trackDataList;
            _trackNameListView.Rebuild();
        }

        private VisualElement MakeItem()
        {
            return new VE_TrackListCell();
        }

        private void BindItem(VisualElement element, int index)
        {
            var cell = element as VE_TrackListCell;
            cell.BindItem(_trackDataList[index]);
        }
    }
}