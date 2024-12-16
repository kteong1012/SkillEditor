using UnityEngine;

namespace SkillEditor
{
    public interface IResizableDrawer
    {
        bool IsResizing { get; set; }
        ScalableRect ItemRect { get; set; }
    }
}