using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Objects.IB
{
	[PXLocalizable()]
	public class Messages
	{
		//Inventory Types
		public const string Stock = "Stock";
		public const string NonStock = "Non Stock";

		//Order Statuses
		public const string Planned = "Planned";
		public const string Released = "Released";
		public const string Reserved = "Reserved";
		public const string Closed = "Closed";
		public const string Cancelled = "Cancelled";

		//Order Item Statuses
		public const string Required = "Required";
		public const string Delivered = "Delivered";

		//Messages
		public const string LessQuantity = "The Quantity is Less to Manufacture the Product!!";
		public const string ProductionOrderCancelled = "The Production Order cannot be Cancelled!!";
		public const string LessQuantityForSalesComponent = "The Quantity for the {0} is less to proceed with the Sales Order!!";
		public const string SalesOrderNotReleased = "The Sales Order is not in Released Status!!";
		public const string SalesOrderNotReleasedForItem = "The Corresponding Sales Order is not in Released Status to Deliver the Product!!";
		public const string SalesOrderStatusUpdate = "Cannot update the Status for the Sales Order!!";
		public const string SalesOrderDeliveredItems = "The Sales Order cannot be Cancelled because there are Delivered Items!!";
		public const string SalesOrderCancelledItems = "The Sales Order cannot be Closed because all the items are Cancelled!!";
		public const string SalesOrderItemDelivered = "The Sales Order Item is Delivered!!";
		public const string SalesOrderCancelled = "The Sales Order is Cancelled!!";
		public const string QuantityCannotBeNegative = "The value in the Quantity column cannot be negative.";

		//Graph Names
		public const string ReceiveShopOrder = "Receive Shop Order";
		public const string DirectInventoryReceipt = "Direct Inventory Receipts";
	}
}
