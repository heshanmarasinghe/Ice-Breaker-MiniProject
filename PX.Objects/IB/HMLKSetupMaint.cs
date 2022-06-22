using System;
using System.Collections;
using System.Collections.Generic;
using PX.Data;
using PX.Data.BQL.Fluent;

namespace PX.Objects.IB
{
	public class HMLKSetupMaint : PXGraph<HMLKSetupMaint>
	{
		#region Views

		public PXSave<HMLKSetup> Save;
		public PXCancel<HMLKSetup> Cancel;

		public SelectFrom<HMLKSetup>.View Setup;

		#endregion
	}
}
