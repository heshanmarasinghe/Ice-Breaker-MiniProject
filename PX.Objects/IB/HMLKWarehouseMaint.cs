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
	}
}
