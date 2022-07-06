using System;
using PX.Data;
using PX.Data.BQL.Fluent;

namespace PX.Objects.IB
{
	public class HMLKInventoryLocationMaint : PXGraph<HMLKInventoryLocationMaint, HMLKInventoryLocation>
	{
		#region Views

		public SelectFrom<HMLKInventoryLocation>.View Locations;

		public SelectFrom<HMLKStockAllocation>
			.InnerJoin<HMLKInventory>.On<HMLKInventory.partNo
			  .IsEqual<HMLKStockAllocation.partNo>>
			.Where<HMLKStockAllocation.locationNo
			  .IsEqual<HMLKInventoryLocation.locationNo.FromCurrent>
			.And<HMLKStockAllocation.warehouseNo
				.IsEqual<HMLKInventoryLocation.warehouseNo.FromCurrent>>>.View PartStockItems;

		#endregion

		#region Event Handlers

		protected void _(Events.RowSelected<HMLKInventoryLocation> e)
		{
			HMLKInventoryLocation row = e.Row;
			if (row == null) return;

			Locations.Cache.AllowInsert = Locations.Cache.AllowDelete = Locations.AllowUpdate = false;
		}

		#endregion

		#region Actions

		public PXAction<HMLKInventoryLocation> AddQty;
		[PXButton(OnClosingPopup = PXSpecialButtonType.Refresh)]
		[PXUIField(DisplayName = "Add Quantity", Enabled = true)]
		protected virtual void addQty()
		{
			var graph = CreateInstance<HMLKStockAllocationEntry>();
			graph.StockItem.Cache.Clear();
			graph.StockItem.Current = (HMLKStockAllocation)graph.StockItem.Cache.CreateInstance();
			graph.StockItem.Current.WarehouseNo = Locations.Current.WarehouseNo;
			graph.StockItem.Current.LocationNo = Locations.Current.LocationNo;
			graph.StockItem.UpdateCurrent();

			throw new PXPopupRedirectException(graph, Messages.DirectInventoryReceipt);
		}

		#endregion
	}
}
