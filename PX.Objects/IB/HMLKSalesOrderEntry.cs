using System;
using System.Collections.Generic;
using System.Linq;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;

namespace PX.Objects.IB
{
	public class HMLKSalesOrderEntry : PXGraph<HMLKSalesOrderEntry, HMLKSalesOrder>
	{
		#region Views

		public SelectFrom<HMLKSalesOrder>.View SalesOrder;

		public SelectFrom<HMLKSalesOrderStockItem>
			 .InnerJoin<HMLKInventory>.On<HMLKInventory.partNo.IsEqual<HMLKSalesOrderStockItem.partNo>>
				.Where<HMLKSalesOrderStockItem.customerOrderNo.IsEqual<HMLKSalesOrder.customerOrderNo.FromCurrent>
				.And<HMLKInventory.inventoryType.IsEqual<stockInventoryType>>>.View SalesOrderPartItems;

		public SelectFrom<HMLKSalesOrderNonStockItem>
			.InnerJoin<HMLKInventory>.On<HMLKInventory.partNo.IsEqual<HMLKSalesOrderNonStockItem.partNo>>
				.Where<HMLKSalesOrderNonStockItem.customerOrderNo.IsEqual<HMLKSalesOrder.customerOrderNo.FromCurrent>
				.And<HMLKInventory.inventoryType.IsEqual<nonStockInventoryType>>>.View SalesOrderNoPartItems;

		//The view for the auto-numbering of records
		public PXSetup<HMLKSetup> AutoNumSetup;
		#endregion

		#region Graph constructor
		public HMLKSalesOrderEntry()
		{
			HMLKSetup setup = AutoNumSetup.Current;
		}
		#endregion

		#region Event Handlers

		protected void _(Events.FieldUpdated<HMLKSalesOrderStockItem, HMLKSalesOrderStockItem.partNo> e)
		{
			HMLKSalesOrderStockItem row = e.Row;

			if (row.PartNo == null) return;

			HMLKInventory inventory = HMLKInventory.PK.Find(this, row.PartNo);

			row.Description = inventory.Description;
			row.Price = inventory.Price;
		}

		protected void _(Events.FieldUpdated<HMLKSalesOrderNonStockItem, HMLKSalesOrderNonStockItem.partNo> e)
		{
			HMLKSalesOrderNonStockItem row = e.Row;

			if (row.PartNo == null) return;

			HMLKInventory inventory = HMLKInventory.PK.Find(this, row.PartNo);

			row.Description = inventory.Description;
			row.Price = inventory.Price;
		}

		protected void _(Events.FieldUpdated<HMLKSalesOrder, HMLKSalesOrder.customerID> e)
		{
			HMLKSalesOrder row = e.Row;

			if (row.CustomerOrderNo == null) return;

			Customer customer = Customer.PK.Find(this, row.CustomerID);
			CR.Address address = CR.Address.PK.Find(this, customer.DefAddressID);

			row.CustomerName = customer.AcctName;
			row.CustomerDeliveryAddress = address.AddressLine1;
		}

		protected void _(Events.FieldVerifying<HMLKSalesOrderStockItem, HMLKSalesOrderStockItem.qty> e)
		{
			if ((decimal)e.NewValue < 0)
			{
				throw new PXSetPropertyException(Messages.QuantityCannotBeNegative);
			}
		}

		protected void _(Events.FieldVerifying<HMLKSalesOrderNonStockItem, HMLKSalesOrderNonStockItem.qty> e)
		{
			if ((decimal)e.NewValue < 0)
			{
				throw new PXSetPropertyException(Messages.QuantityCannotBeNegative);
			}
		}

		protected void _(Events.RowSelected<HMLKSalesOrder> e)
		{
			HMLKSalesOrder row = e.Row;
			if (row == null) return;

			PXUIFieldAttribute.SetEnabled<HMLKSalesOrder.status>
				(SalesOrder.Cache, null, row.Status != SalesOrderStatusConstants.Cancelled &&
										 row.Status != SalesOrderStatusConstants.Closed);

			HMLKProductionOrder productionOrder = HMLKProductionOrder.PK.FindSalesOrder(this, SalesOrder.Current.CustomerOrderNo);

			if (productionOrder != null)
			{
				SalesOrder.Current.IsCreatePOOrder = true;
			}
		}

		protected void _(Events.RowPersisted<HMLKSalesOrder> e)
		{
			HMLKSalesOrder row = e.Row;
			if (row == null || e.TranStatus == PXTranStatus.Open) return;

			if (row.IsCreatePOOrder != null && (bool)row.IsCreatePOOrder)
			{
				var salesOrderItems = SalesOrderPartItems.Select();

				// Acuminator disable once PX1045 PXGraphCreateInstanceInEventHandlers [Justification]
				var productionOrderEntry = PXGraph.CreateInstance<HMLKProductionOrderEntry>();

				foreach (HMLKSalesOrderStockItem item in salesOrderItems)
				{
					HMLKProductionOrder productionItem = HMLKProductionOrder.UK.Find(this, SalesOrder.Current.CustomerOrderNo, item.PartNo);
					HMLKInventory inventory = HMLKInventory.PK.Find(this, item.PartNo);

					if (inventory.PartTypeNo == InventoryPartTypes.Manufactured)
					{
						if (productionItem == null)
						{
							HMLKProductionOrder hMLKProductionOrder = new HMLKProductionOrder
							{
								SalesOrderNo = SalesOrder.Current.CustomerOrderNo,
								PartNo = item.PartNo,
								Qty = (int)item.Qty,
								RequiredDate = DateTime.Now.AddMonths(1)
							};

							productionOrderEntry.ProductionOrder.Insert(hMLKProductionOrder);
							// Acuminator disable once PX1043 SavingChangesInEventHandlers [Justification]
							productionOrderEntry.ProductionOrder.Cache.Persist(PXDBOperation.Insert);
						}
					}
				}
			}
		}

		#endregion

		#region Actions

		public PXAction<HMLKSalesOrder> ReleaseOrder;
		[PXButton]
		[PXUIField(DisplayName = "Release", Enabled = true)]
		protected virtual void releaseOrder()
		{
			var salesOrder = SalesOrder.Current;

			if (SalesOrderPartItems.Select() == null) return;

			if (salesOrder.Status == SalesOrderStatusConstants.Cancelled || salesOrder.Status == SalesOrderStatusConstants.Closed)
			{
				string status = string.Empty;

				if (salesOrder.Status == SalesOrderStatusConstants.Cancelled) { status = Messages.Cancelled; }
				else if (salesOrder.Status == SalesOrderStatusConstants.Closed) { status = Messages.Closed; }

				throw new PXException(string.Format(Messages.SalesOrderCancelledOrClosed, status).ToString());
			}
			else
			{
				salesOrder.Status = SalesOrderStatusConstants.Released;

				SalesOrder.Update(salesOrder);
				Actions.PressSave();
			}
		}

		public PXAction<HMLKSalesOrder> CancelOrder;
		[PXButton]
		[PXUIField(DisplayName = "Cancel", Enabled = true)]
		protected virtual void cancelOrder()
		{
			var salesOrder = SalesOrder.Current;
			var salesOrderItems = SalesOrderPartItems.Select();

			if (salesOrderItems == null) return;

			bool anyDeliveredItems = salesOrderItems.ToQueryable<PXResult<HMLKSalesOrderStockItem>>()
											.Any(x => ((HMLKSalesOrderStockItem)x).Status == SalesOrderItemStatusConstants.Delivered);

			if (salesOrder.Status == SalesOrderStatusConstants.Planned)
			{
				salesOrder.Status = SalesOrderStatusConstants.Cancelled;

				SalesOrder.Update(salesOrder);
				Actions.PressSave();
			}
			else if (salesOrder.Status == SalesOrderStatusConstants.Released)
			{
				if (anyDeliveredItems)
				{
					throw new PXException(Messages.SalesOrderDeliveredItems);
				}

				salesOrder.Status = SalesOrderStatusConstants.Cancelled;

				SalesOrder.Update(salesOrder);
				Actions.PressSave();
			}
			else
			{
				throw new PXException(Messages.SalesOrderNotReleased);
			}

		}

		public PXAction<HMLKSalesOrder> CloseOrder;
		[PXButton]
		[PXUIField(DisplayName = "Close", Enabled = true)]
		protected virtual void closeOrder()
		{
			var salesOrder = SalesOrder.Current;
			var salesOrderItems = SalesOrderPartItems.Select();

			if (SalesOrderPartItems.Select() == null) return;

			if (salesOrder.Status != SalesOrderStatusConstants.Cancelled)
			{
				if (salesOrderItems.ToQueryable<PXResult<HMLKSalesOrderStockItem>>()
								.All(x => ((HMLKSalesOrderStockItem)x).Status == SalesOrderItemStatusConstants.Cancelled))
				{
					throw new PXException(Messages.SalesOrderCancelledItems);
				}
				else
				{
					salesOrder.Status = SalesOrderStatusConstants.Closed;

					SalesOrder.Update(salesOrder);
					Actions.PressSave();
				}
			}
			else
			{
				throw new PXException(string.Format(Messages.SalesOrderCancelledOrClosed, Messages.Cancelled).ToString());
			}
		}

		public PXAction<HMLKSalesOrder> DeliverOrder;
		[PXButton(DisplayOnMainToolbar = false)]
		[PXUIField(DisplayName = "Deliver", Enabled = true)]
		protected virtual void deliverOrder()
		{
			var salesOrderItems = SalesOrderPartItems.Select();
			var salesOrder = SalesOrder.Current;

			if (salesOrderItems == null) return;

			if (salesOrder.Status == SalesOrderStatusConstants.Released)
			{
				var salesOrderSelectedItems = salesOrderItems.ToQueryable<PXResult<HMLKSalesOrderStockItem>>()
												.Where(x => ((HMLKSalesOrderStockItem)x).Selected == true &&
												((HMLKSalesOrderStockItem)x).Status != SalesOrderItemStatusConstants.Delivered);

				try
				{
					foreach (HMLKSalesOrderStockItem item in salesOrderSelectedItems)
					{
						if (CheckForQuantity(item))
						{
							item.Status = SalesOrderItemStatusConstants.Delivered;
							SalesOrderPartItems.Update(item);
						}
						else
						{
							HMLKInventory inventory = HMLKInventory.PK.Find(this, item.PartNo);

							SalesOrderPartItems.Cache.RaiseExceptionHandling<HMLKSalesOrderStockItem.qty>(item, item.Qty,
								new PXException(string.Format(Messages.LessQuantityForSalesComponent, inventory.PartCD).ToString()));
						}
					}

					if (SalesOrderPartItems.Select().ToQueryable<PXResult<HMLKSalesOrderStockItem>>()
								.All(x => ((HMLKSalesOrderStockItem)x).Status == SalesOrderItemStatusConstants.Delivered))
					{
						salesOrder.Status = SalesOrderStatusConstants.Closed;
					}

					Actions.PressSave();
				}
				catch (Exception e)
				{
					throw e;
				}
			}
			else
			{
				throw new PXException(Messages.SalesOrderNotReleased);
			}
		}

		public PXAction<HMLKSalesOrder> CancelOrderItem;
		[PXButton(DisplayOnMainToolbar = false)]
		[PXUIField(DisplayName = "Cancel", Enabled = true)]
		protected virtual void cancelOrderItem()
		{
			var salesOrderItems = SalesOrderPartItems.Select();
			var salesOrder = SalesOrder.Current;

			if (salesOrderItems == null) return;

			var salesOrderSelectedItems = salesOrderItems.ToQueryable<PXResult<HMLKSalesOrderStockItem>>()
								.Where(x => ((HMLKSalesOrderStockItem)x).Selected == true);

			foreach (HMLKSalesOrderStockItem item in salesOrderSelectedItems)
			{
				if (item.Status != SalesOrderItemStatusConstants.Delivered)
				{
					item.Status = SalesOrderItemStatusConstants.Cancelled;
					SalesOrderPartItems.Update(item);
				}
				else
				{
					throw new PXException(Messages.SalesOrderItemDelivered);
				}
			}

			Actions.PressSave();
		}

		#endregion

		#region Methods

		public static bool CheckForQuantity(HMLKSalesOrderStockItem salesOrderStockItem)
		{
			var stockItemsEntry = PXGraph.CreateInstance<HMLKStockAllocationEntry>();

			var stockItems = stockItemsEntry.StockItem.Select().ToQueryable<PXResult<HMLKStockAllocation>>()
									.Where(x => ((HMLKStockAllocation)x).PartNo == salesOrderStockItem.PartNo);

			var totalStockQty = stockItems.Where(x => ((HMLKStockAllocation)x).PartNo == salesOrderStockItem.PartNo)
									.Select(x => ((HMLKStockAllocation)x).Qty).Sum();

			if (salesOrderStockItem.Qty <= totalStockQty)
			{
				int? itemQty = (int)salesOrderStockItem.Qty;

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

				return true;
			}

			return false;
		}

		#endregion
	}
}
