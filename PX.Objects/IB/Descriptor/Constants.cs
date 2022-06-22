using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Objects.IB
{
	public static class InventoryTypes
	{
		public const string Stock = "S";
		public const string NonStock = "NS";
	}

	public static class InventoryPartTypes
	{
		public const int Manufactured = 1;
		public const int Purchased = 2;
	}

	public static class ProductionOrderStatusConstants
	{
		public const string Released = "RLD";
		public const string Reserved = "RSD";
		public const string Closed = "CLD";
		public const string Cancelled = "CND";
	}

	public static class SalesOrderStatusConstants
	{
		public const string Planned = "PND";
		public const string Released = "RLD";
		public const string Closed = "CLD";
		public const string Cancelled = "CND";
	}

	public static class SalesOrderItemStatusConstants
	{
		public const string Required = "RQD";
		public const string Delivered = "DLD";
		public const string Cancelled = "CND";
	}

	public class manufacturedInventoryPartType : PX.Data.BQL.BqlInt.Constant<manufacturedInventoryPartType>
	{
		public manufacturedInventoryPartType() : base(InventoryPartTypes.Manufactured)
		{
		}
	}

	public class purchasedInventoryPartType : PX.Data.BQL.BqlInt.Constant<purchasedInventoryPartType>
	{
		public purchasedInventoryPartType() : base(InventoryPartTypes.Purchased)
		{
		}
	}

	public class stockInventoryType : PX.Data.BQL.BqlString.Constant<stockInventoryType>
	{
		public stockInventoryType() : base(InventoryTypes.Stock)
		{
		}
	}

	public class nonStockInventoryType : PX.Data.BQL.BqlString.Constant<nonStockInventoryType>
	{
		public nonStockInventoryType() : base(InventoryTypes.NonStock)
		{
		}
	}
}
