using System;
using PX.Data;

namespace PX.Objects.IB
{
	[Serializable]
	[PXCacheName("Warehouse")]
	public class HMLKWarehouse : IBqlTable
	{
		#region WarehouseNo
		[PXDBIdentity]
		public virtual int? WarehouseNo { get; set; }
		public abstract class warehouseNo : PX.Data.BQL.BqlInt.Field<warehouseNo> { }
		#endregion

		#region WarehouseCD
		[PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">aaaaaaaaaaaaaaa")]
		[PXDefault]
		[PXUIField(DisplayName = "Warehouse ID")]
		[PXSelector(typeof(Search<warehouseCD>),
		typeof(warehouseCD),
		typeof(description),
		typeof(address))]
		public virtual string WarehouseCD { get; set; }
		public abstract class warehouseCD : PX.Data.BQL.BqlString.Field<warehouseCD> { }
		#endregion

		#region Description
		[PXDBString(256, IsUnicode = true, InputMask = "")]
		[PXUIField(DisplayName = "Description")]
		public virtual string Description { get; set; }
		public abstract class description : PX.Data.BQL.BqlString.Field<description> { }
		#endregion

		#region Address
		[PXDBString(256, IsUnicode = true, InputMask = "")]
		[PXUIField(DisplayName = "Address")]
		public virtual string Address { get; set; }
		public abstract class address : PX.Data.BQL.BqlString.Field<address> { }
		#endregion

		#region CreatedByID
		[PXDBCreatedByID()]
		public virtual Guid? CreatedByID { get; set; }
		public abstract class createdByID : PX.Data.BQL.BqlGuid.Field<createdByID> { }
		#endregion

		#region CreatedByScreenID
		[PXDBCreatedByScreenID()]
		public virtual string CreatedByScreenID { get; set; }
		public abstract class createdByScreenID : PX.Data.BQL.BqlString.Field<createdByScreenID> { }
		#endregion

		#region CreatedDateTime
		[PXDBCreatedDateTime()]
		public virtual DateTime? CreatedDateTime { get; set; }
		public abstract class createdDateTime : PX.Data.BQL.BqlDateTime.Field<createdDateTime> { }
		#endregion

		#region LastModifiedByID
		[PXDBLastModifiedByID()]
		public virtual Guid? LastModifiedByID { get; set; }
		public abstract class lastModifiedByID : PX.Data.BQL.BqlGuid.Field<lastModifiedByID> { }
		#endregion

		#region LastModifiedByScreenID
		[PXDBLastModifiedByScreenID()]
		public virtual string LastModifiedByScreenID { get; set; }
		public abstract class lastModifiedByScreenID : PX.Data.BQL.BqlString.Field<lastModifiedByScreenID> { }
		#endregion

		#region LastModifiedDateTime
		[PXDBLastModifiedDateTime()]
		public virtual DateTime? LastModifiedDateTime { get; set; }
		public abstract class lastModifiedDateTime : PX.Data.BQL.BqlDateTime.Field<lastModifiedDateTime> { }
		#endregion

		#region Tstamp
		[PXDBTimestamp()]
		[PXUIField(DisplayName = "Tstamp")]
		public virtual byte[] Tstamp { get; set; }
		public abstract class tstamp : PX.Data.BQL.BqlByteArray.Field<tstamp> { }
		#endregion

		#region Noteid
		[PXNote()]
		public virtual Guid? Noteid { get; set; }
		public abstract class noteid : PX.Data.BQL.BqlGuid.Field<noteid> { }
		#endregion
	}
}
