using System;
using System.Collections;
using System.Collections.Generic;
using PX.Data;
using PX.Data.BQL.Fluent;

namespace PX.Objects.IB
{
	public class HMLKBOMMaint : PXGraph<HMLKBOMMaint, HMLKBOMInventory>
	{
		#region Views

		public SelectFrom<HMLKBOMInventory>.View Inventory;

		public SelectFrom<HMLKBOM>
			.Where<HMLKBOM.productNo
				.IsEqual<HMLKBOMInventory.partNo.FromCurrent>>.View InventoryBOM;

		#endregion

		#region Event Handlers

		protected void _(Events.RowSelected<HMLKBOMInventory> e)
		{
			Inventory.Cache.AllowDelete = false;
		}

		protected void _(Events.FieldVerifying<HMLKBOM, HMLKBOM.qty> e)
		{
			if ((int)e.NewValue < 0)
			{
				throw new PXSetPropertyException(Messages.QuantityCannotBeNegative);
			}
		}

		#endregion
	}
}
