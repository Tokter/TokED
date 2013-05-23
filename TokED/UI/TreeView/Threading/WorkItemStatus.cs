using System;
using System.Collections.Generic;
using System.Text;

namespace TokED.UI.Threading
{
	public enum WorkItemStatus 
	{ 
		Completed, 
		Queued, 
		Executing, 
		Aborted 
	}
}
