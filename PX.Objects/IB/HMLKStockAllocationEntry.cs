using System;
using System.Collections;
using System.Collections.Generic;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Web.UI;

namespace PX.Objects.IB
{
	public class HMLKStockAllocationEntry : PXGraph<HMLKStockAllocationEntry>
	{
		#region Views

		public SelectFrom<HMLKStockAllocation>.View StockItem;

		#endregion

		#region Event Handlers

		protected void _(Events.FieldVerifying<HMLKStockAllocation, HMLKStockAllocation.qty> e)
		{
			if ((int)e.NewValue < 0)
			{
				throw new PXSetPropertyException(Messages.QuantityCannotBeNegative);
			}
		}

		#endregion

		#region Actions
		public PXSave<HMLKStockAllocation> Save;
		[PXUIField(DisplayName = "")]
		[PXSaveButton(ClosePopup = true)]
		protected virtual IEnumerable save(PXAdapter adapter)
		{
			HMLKStockAllocation row = StockItem.Current;
			HMLKStockAllocation stockItem = HMLKStockAllocation.PK.Find(this, row.PartNo, row.LocationNo);

			if (stockItem != null)
			{
				StockItem.Cache.Clear();
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

			return adapter.Get();
		}
		#endregion
	}
}
