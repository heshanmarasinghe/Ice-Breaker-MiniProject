using System;
using PX.Data;
using PX.Data.BQL.Fluent;

namespace PX.Objects.IB
{
	public class HMLKInventoryMaint : PXGraph<HMLKInventoryMaint>
	{
		#region Views

		public PXSave<HMLKInventory> Save;
		public PXCancel<HMLKInventory> Cancel;

		public SelectFrom<HMLKInventory>.View Inventory;

		#endregion
	}
}
