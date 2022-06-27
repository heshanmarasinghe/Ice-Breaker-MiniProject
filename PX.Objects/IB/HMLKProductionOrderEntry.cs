using PX.Data;
using PX.Data.BQL.Fluent;
using System.Linq;
using System.Collections.Generic;
using PX.Web.UI;
using System.Collections;
using PX.Data.BQL;

namespace PX.Objects.IB
{
	public class HMLKProductionOrderEntry : PXGraph<HMLKProductionOrderEntry, HMLKProductionOrder>
	{
		#region Views

		public SelectFrom<HMLKProductionOrder>.View ProductionOrder;

		public SelectFrom<HMLKBOM>
				.Where<HMLKBOM.productNo
				.IsEqual<HMLKProductionOrder.partNo.FromCurrent>>.View BOM;

		//The view for the auto-numbering of records
		public PXSetup<HMLKSetup> AutoNumSetup;
		#endregion

		#region Graph constructor
		public HMLKProductionOrderEntry()
		{
			HMLKSetup setup = AutoNumSetup.Current;
		}
		#endregion

		#region Event Handlers

		protected void _(Events.RowSelected<HMLKProductionOrder> e)
		{
			HMLKProductionOrder row = e.Row;
			if (row == null) return;

			IssueMaterial.SetEnabled((row.Status == ProductionOrderStatusConstants.Released) &&
				ProductionOrder.Cache.GetStatus(row) != PXEntryStatus.Inserted);

			ReceiveShopOrder.SetEnabled((row.Status == ProductionOrderStatusConstants.Reserved) &&
				ProductionOrder.Cache.GetStatus(row) != PXEntryStatus.Inserted);

			CancelOrder.SetEnabled((row.Status == ProductionOrderStatusConstants.Released) &&
				ProductionOrder.Cache.GetStatus(row) != PXEntryStatus.Inserted);

			PXUIFieldAttribute.SetEnabled<HMLKProductionOrder.partNo>(ProductionOrder.Cache, null,
				row.Status == ProductionOrderStatusConstants.Released);

			PXUIFieldAttribute.SetEnabled<HMLKProductionOrder.qty>(ProductionOrder.Cache, null,
				row.Status == ProductionOrderStatusConstants.Released);

			BOM.AllowInsert = BOM.AllowDelete = BOM.AllowUpdate = false;
			ProductionOrder.Cache.AllowDelete = row.Status == ProductionOrderStatusConstants.Released;
		}

		protected void _(Events.RowSelecting<HMLKBOM> e)
		{
			if (e.Row == null) return;

			e.Row.TotalQty = e.Row.Qty * ProductionOrder.Current.Qty;
		}

		protected void _(Events.FieldUpdated<HMLKProductionOrder, HMLKProductionOrder.qty> e)
		{
			if (e.Row == null) return;

			if ((int)e.NewValue < 0)
			{
				throw new PXSetPropertyException(Messages.QuantityCannotBeNegative);
			}

			foreach (HMLKBOM item in BOM.Select())
			{
				item.TotalQty = item.Qty * ProductionOrder.Current.Qty;
			}
		}

		#endregion

		#region Actions

		public PXAction<HMLKProductionOrder> IssueMaterial;
		[PXButton]
		[PXUIField(DisplayName = "Issue Material", Enabled = true)]
		protected virtual IEnumerable issueMaterial(PXAdapter adapter)
		{
			var bomItems = BOM.Select();
			var stockItemsEntry = PXGraph.CreateInstance<HMLKStockAllocationEntry>();

			bool lowStockAvailable = CheckForLowStock(bomItems, stockItemsEntry);

			if (!lowStockAvailable)
			{
				//Status Change
				HMLKProductionOrder row = ProductionOrder.Current;
				row.Status = ProductionOrderStatusConstants.Reserved;
				ProductionOrder.Update(row);
				Save.Press();

				PXLongOperation.StartOperation(this, delegate ()
				{
					ReduceStock(bomItems, stockItemsEntry);
				});
			}

			return adapter.Get();
		}

		public PXAction<HMLKProductionOrder> ReceiveShopOrder;
		[PXButton(OnClosingPopup = PXSpecialButtonType.Cancel)]
		[PXUIField(DisplayName = "Receive Shop Order", Enabled = true)]
		protected virtual IEnumerable receiveShopOrder(PXAdapter adapter)
		{
			try
			{
				if (ProductionOrder.Current != null)
				{
					var stockItemsEntry = CreateInstance<HMLKProductionStockAllocationEntry>();
					stockItemsEntry.StockItem.Current = SelectFrom<HMLKStockAllocation>.View.Select(stockItemsEntry);
					stockItemsEntry.StockItem.Current.PartNo = ProductionOrder.Current.PartNo;
					stockItemsEntry.StockItem.Current.Qty = ProductionOrder.Current.Qty;

					stockItemsEntry.StockItem.UpdateCurrent();

					throw new PXPopupRedirectException(stockItemsEntry, Messages.ReceiveShopOrder);
				}
				return adapter.Get();
			}
			finally
			{
				HMLKProductionOrder row = ProductionOrder.Current;

				//Update SalesOrderItem Status
				if (row.SalesOrderNo != null)
				{
					var salesOrderEntry = CreateInstance<HMLKSalesOrderEntry>();
					HMLKSalesOrderStockItem salesOrderItem = HMLKSalesOrderStockItem.PK.Find(this, row.PartNo, row.SalesOrderNo);
					HMLKSalesOrder salesOrder = HMLKSalesOrder.PK.Find(this, row.SalesOrderNo);

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
													.View.Select(this, row.SalesOrderNo);

					if (salesOrderPartItems.ToQueryable<PXResult<HMLKSalesOrderStockItem>>()
						.All(x => ((HMLKSalesOrderStockItem)x).Status == SalesOrderItemStatusConstants.Delivered))
					{
						salesOrder.Status = SalesOrderStatusConstants.Closed;
						salesOrderEntry.SalesOrder.Update(salesOrder);
						salesOrderEntry.SalesOrder.Cache.Persist(PXDBOperation.Update);
					}
				}

				//Update Production Order Status
				row.Status = ProductionOrderStatusConstants.Closed;
				ProductionOrder.Update(row);
				Actions.PressSave();
			}
		}

		public PXAction<HMLKProductionOrder> CancelOrder;
		[PXButton]
		[PXUIField(DisplayName = "Cancel Order", Enabled = true)]
		protected virtual void cancelOrder()
		{
			var productionOrder = ProductionOrder.Current;

			if (productionOrder == null) return;

			productionOrder.Status = ProductionOrderStatusConstants.Cancelled;

			ProductionOrder.Update(productionOrder);
			Actions.PressSave();

			//Update SalesOrderItem Status
			HMLKProductionOrder row = ProductionOrder.Current;
			var salesOrderEntry = CreateInstance<HMLKSalesOrderEntry>();
			HMLKSalesOrderStockItem salesOrderItem = HMLKSalesOrderStockItem.PK.Find(this, row.PartNo, row.SalesOrderNo);

			if (salesOrderItem.Status != SalesOrderItemStatusConstants.Delivered)
			{
				salesOrderItem.Status = SalesOrderItemStatusConstants.Cancelled;

				salesOrderEntry.SalesOrderPartItems.Update(salesOrderItem);
				salesOrderEntry.SalesOrderPartItems.Cache.Persist(PXDBOperation.Update);
			}
		}

		#endregion

		#region Methods

		public bool CheckForLowStock(PXResultset<HMLKBOM> bomItems, HMLKStockAllocationEntry stockItemsEntry)
		{
			List<bool> lowStockItems = new List<bool>();

			foreach (HMLKBOM item in bomItems)
			{
				var stockItems = stockItemsEntry.StockItem.Select().ToQueryable<PXResult<HMLKStockAllocation>>()
											.Where(x => ((HMLKStockAllocation)x).PartNo == item.PartNo);

				var totalStockQty = stockItems.Where(x => ((HMLKStockAllocation)x).PartNo == item.PartNo).Select(x => ((HMLKStockAllocation)x).Qty).Sum();

				if (item.TotalQty > totalStockQty || totalStockQty == null)
				{
					BOM.Cache.RaiseExceptionHandling<HMLKBOM.totalQty>(item, item.TotalQty,
						new PXException(Messages.LessQuantity));
				}

				lowStockItems.Add(item.TotalQty > totalStockQty || totalStockQty == null ? false : true);
			}

			return lowStockItems.Any(x => x.Equals(false));
		}

		public static void ReduceStock(PXResultset<HMLKBOM> bomItems, HMLKStockAllocationEntry stockItemsEntry)
		{
			foreach (HMLKBOM item in bomItems)
			{
				var stockItems = stockItemsEntry.StockItem.Select().ToQueryable<PXResult<HMLKStockAllocation>>()
											.Where(x => ((HMLKStockAllocation)x).PartNo == item.PartNo);

				var totalStockQty = stockItems.Where(x => ((HMLKStockAllocation)x).PartNo == item.PartNo).Select(x => ((HMLKStockAllocation)x).Qty).Sum();

				int? itemQty = item.TotalQty;

				foreach (HMLKStockAllocation stockItem in stockItems.OrderByDescending(x => ((HMLKStockAllocation)x).Qty))
				{
					if (itemQty < 1) break;

					if (stockItem.Qty >= itemQty)
					{
						stockItem.Qty -= itemQty;
						itemQty = 0;
					}
					else
					{
						itemQty -= stockItem.Qty;
						stockItem.Qty = 0;
					}

					stockItemsEntry.StockItem.Update(stockItem);
					stockItemsEntry.StockItem.Cache.Persist(PXDBOperation.Update);
				}
			}
		}

		#endregion
	}
}
