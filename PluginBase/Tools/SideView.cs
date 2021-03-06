﻿using OpenTK;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TokED;
using TokGL;

namespace PluginBase.Tools
{
    [Export("Side View", typeof(EditorTool)), PartCreationPolicy(CreationPolicy.Shared)]
    public class SideView : EditorTool
    {
        public SideView()
            : base(ToolEvent.CreateDown(Keys.NumPad3), false)
        {
        }

        public override void Activated()
        {
            var distance = (Editor.Camera.LookAt - Editor.Camera.Position).Length;
            Editor.Camera.Position = Editor.Camera.LookAt + Vector3.Multiply(new Vector3(1, 0, 0), distance);
            Editor.Camera.Up = new Vector3(0, 0, 1);
        }

        public override bool Availbable()
        {
            return Editor != null && Editor.Camera != null;
        }
    }
}
