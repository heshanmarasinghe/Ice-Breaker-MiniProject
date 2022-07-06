using System;
using PX.Data;
using PX.Data.BQL.Fluent;

namespace PX.Objects.IB
{
	public class HMLKWarehouseMaint : PXGraph<HMLKWarehouseMaint, HMLKWarehouse>
	{
		#region Views

		public SelectFrom<HMLKWarehouse>.View Warehouses;

		public SelectFrom<HMLKInventoryWarehouseLocation>
			  .Where<HMLKInventoryLocation.warehouseNo
			  .IsEqual<HMLKWarehouse.warehouseNo.FromCurrent>>.View WarehouseLocations;

		#endregion

		#region Event Handler

		protected virtual void _(Events.RowInserting<HMLKInventoryWarehouseLocation> e)
		{
			HMLKInventoryWarehouseLocation row = e.Row;
			if (row == null) return;

			var location = HMLKInventoryWarehouseLocation.PK.Find(this, row.LocationCD);

			if (location != null)
			{
				throw new PXException(Messages.DuplicateLocation);
			}
		}

		#endregion
	}
}
