using System;
using PX.Data;
using PX.Data.BQL.Fluent;

namespace PX.Objects.IB
{
	public class HMLKInventoryPartTypeMaint : PXGraph<HMLKInventoryPartTypeMaint>
	{
		#region Views

		public PXSave<HMLKInventoryPartType> Save;
		public PXCancel<HMLKInventoryPartType> Cancel;

		public SelectFrom<HMLKInventoryPartType>.View PartTypes;

		#endregion
	}
}
