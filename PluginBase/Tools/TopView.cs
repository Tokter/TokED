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
    [Export("Top View", typeof(EditorTool)), PartCreationPolicy(CreationPolicy.Shared)]
    public class TopView : EditorTool
    {
        public TopView()
            : base(ToolEvent.CreateDown(Keys.NumPad7), false)
        {
        }

        public override void Activated()
        {
            var distance = (Editor.Camera.LookAt - Editor.Camera.Position).Length;
            Editor.Camera.Position = Editor.Camera.LookAt + Vector3.Multiply(new Vector3(0, 0, 1), distance);
            Editor.Camera.Up = new Vector3(0, 1, 0);
        }

        public override bool Availbable()
        {
            return Editor != null && Editor.Camera != null;
        }
    }
}
