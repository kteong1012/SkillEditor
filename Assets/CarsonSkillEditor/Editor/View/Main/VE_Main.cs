using System;
using System.Collections.Generic;
using CarsonSkill.Runtime;
using UnityEngine;
using UnityEngine.UIElements;

namespace SkillEditor
{
    public class VE_Main : VisualElement
    {
        private VE_MainLeft _mainLeft;
        private VE_MainRight _mainRight;
        private ListView _trackNameListView;
        private ListView _trackDetailListView;
        private readonly List<TrackData> _trackDataList = new();

        public VE_Main()
        {
            var leftRightSplitView = new VE_LeftRightSplitView();
            Add(leftRightSplitView);

            var leftView = CreateLeftView();
            leftRightSplitView.Add(leftView);
            // strech
            leftView.style.flexGrow = 1;
            

            var rightView = CreateRightView();
            leftRightSplitView.Add(rightView);
            // strech
            rightView.style.flexGrow = 1;
        }

        private VisualElement CreateLeftView()
        {
            if (_mainLeft != null)
            {
                return _mainLeft;
            }

            _mainLeft = new VE_MainLeft();

            // 右键菜单
            var contextMenuManipulator = new ContextualMenuManipulator(evt => { evt.menu.AppendAction("Create New Track", action => CreateNewTrack()); });
            _mainLeft.AddManipulator(contextMenuManipulator);

            return _mainLeft;
        }

        private VisualElement CreateRightView()
        {
            if (_mainRight != null)
            {
                return _mainRight;
            }

            _mainRight = new VE_MainRight();
            return _mainRight;
        }

        public void CreateNewTrack()
        {
            var newTrackData = new TrackData()
            {
                trackName = "New Track",
                trackValue = "0"
            };
            _trackDataList.Add(newTrackData);

            _mainLeft.UpdateTrackDataList(_trackDataList);
            _mainRight.UpdateTrackDataList(_trackDataList);
        }
    }
}