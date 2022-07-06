using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;

namespace PX.Objects.IB
{
	public class HMLKProductionStockAllocationEntry : PXGraph<HMLKProductionStockAllocationEntry>
	{
		#region Views

		public SelectFrom<HMLKStockAllocation>.View StockItem;

		public SelectFrom<HMLKProductionOrder>.View ProductionOrder;

		#endregion

		#region Event Handlers

		protected void _(Events.RowSelected<HMLKStockAllocation> e)
		{
			HMLKStockAllocation row = e.Row;
			if (row.PartNo == null) return;

			PXUIFieldAttribute.SetEnabled<HMLKStockAllocation.partNo>(StockItem.Cache, null, false);
			PXUIFieldAttribute.SetEnabled<HMLKStockAllocation.qty>(StockItem.Cache, null, false);
		}

		protected void _(Events.FieldUpdated<HMLKStockAllocation, HMLKStockAllocation.warehouseNo> e)
		{
			HMLKStockAllocation row = e.Row;
			if (row.PartNo == null) return;

			StockItem.Cache.SetValueExt<HMLKStockAllocation.locationNo>(row, null);
		}

		#endregion

		#region Actions

		public PXSave<HMLKStockAllocation> Save;
		[PXUIField(DisplayName = "")]
		[PXSaveButton(ClosePopup = true)]
		protected virtual IEnumerable save(PXAdapter adapter)
		{
			HMLKProductionOrder productionOrder = ProductionOrder.Current;
			HMLKStockAllocation row = StockItem.Current;
			HMLKStockAllocation stockItem = HMLKStockAllocation.PK.Find(this, row.PartNo, row.LocationNo);

			this.Clear();

			if (stockItem != null)
			{
				stockItem.Qty += row.Qty;
				StockItem.Update(stockItem);
				StockItem.Cache.Persist(PXDBOperation.Update);
			}
			else
			{
				StockItem.Cache.Clear();
				StockItem.Insert(row);
				StockItem.Cache.Persist(PXDBOperation.Insert);
			}

			//Update SalesOrderItem Status
			if (productionOrder.SalesOrderNo != null)
			{
				var salesOrderEntry = CreateInstance<HMLKSalesOrderEntry>();
				HMLKSalesOrderStockItem salesOrderItem = HMLKSalesOrderStockItem.PK.Find(this, row.PartNo, productionOrder.SalesOrderNo);
				HMLKSalesOrder salesOrder = HMLKSalesOrder.PK.Find(this, productionOrder.SalesOrderNo);

				if (salesOrder?.Status == SalesOrderStatusConstants.Released)
				{
					salesOrderItem.Status = SalesOrderItemStatusConstants.Delivered;
					salesOrderEntry.SalesOrderPartItems.Update(salesOrderItem);
				}
				else
				{
					throw new PXException(Messages.SalesOrderNotReleasedForItem);
				}

				salesOrderEntry.SalesOrderPartItems.Cache.Persist(PXDBOperation.Update);

				var salesOrderPartItems = SelectFrom<HMLKSalesOrderStockItem>
												.Where<HMLKSalesOrderStockItem.customerOrderNo.IsEqual<@P.AsString>>
												.View.Select(this, productionOrder.SalesOrderNo);

				if (salesOrderPartItems.ToQueryable<PXResult<HMLKSalesOrderStockItem>>()
					.All(x => ((HMLKSalesOrderStockItem)x).Status == SalesOrderItemStatusConstants.Delivered))
				{
					salesOrder.Status = SalesOrderStatusConstants.Closed;
					salesOrderEntry.SalesOrder.Update(salesOrder);
					salesOrderEntry.SalesOrder.Cache.Persist(PXDBOperation.Update);
				}
			}

			//Update Production Order Status
			productionOrder.Status = ProductionOrderStatusConstants.Closed;
			ProductionOrder.Update(productionOrder);
			ProductionOrder.Cache.Persist(PXDBOperation.Update);

			return adapter.Get();
		}
		#endregion
	}
}
