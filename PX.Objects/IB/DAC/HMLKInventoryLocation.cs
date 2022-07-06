using System;
using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;

namespace PX.Objects.IB
{
	[Serializable]
	[PXCacheName("Location")]
	public class HMLKInventoryLocation : IBqlTable
	{
		#region LocationNo
		[PXDBIdentity]
		public virtual int? LocationNo { get; set; }
		public abstract class locationNo : PX.Data.BQL.BqlInt.Field<locationNo> { }
		#endregion

		#region WarehouseNo
		[PXDBInt(IsKey = true)]
		[PXDBDefault(typeof(HMLKWarehouse.warehouseNo))]
		[PXUIField(DisplayName = "Warehouse ID")]
		[PXParent(typeof(SelectFrom<HMLKWarehouse>.
						 Where<HMLKWarehouse.warehouseNo.
						 IsEqual<HMLKInventoryLocation.warehouseNo.FromCurrent>>))]
		[PXSelector(typeof(Search<HMLKWarehouse.warehouseNo>),
			typeof(HMLKWarehouse.warehouseCD),
			typeof(HMLKWarehouse.description),
			SubstituteKey = typeof(HMLKWarehouse.warehouseCD),
			DescriptionField = typeof(HMLKWarehouse.description))]
		public virtual int? WarehouseNo { get; set; }
		public abstract class warehouseNo : PX.Data.BQL.BqlInt.Field<warehouseNo> { }
		#endregion

		#region LocationCD
		[PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">aaaaaaaaaaaaaaa")]
		[PXDefault]
		[PXUIField(DisplayName = "Location ID")]
		[PXSelector(typeof(Search<HMLKInventoryLocation.locationCD, Where<HMLKInventoryLocation.warehouseNo, Equal<Current<warehouseNo>>>>),
			typeof(HMLKInventoryLocation.locationCD),
			typeof(HMLKInventoryLocation.description),
			typeof(HMLKInventoryLocation.address))]
		public virtual string LocationCD { get; set; }
		public abstract class locationCD : PX.Data.BQL.BqlString.Field<locationCD> { }
		#endregion

		#region Address
		[PXDBString(256, IsUnicode = true, InputMask = "")]
		[PXUIField(DisplayName = "Address")]
		public virtual string Address { get; set; }
		public abstract class address : PX.Data.BQL.BqlString.Field<address> { }
		#endregion

		#region Description
		[PXDBString(50, IsUnicode = true, InputMask = "")]
		[PXUIField(DisplayName = "Description")]
		public virtual string Description { get; set; }
		public abstract class description : PX.Data.BQL.BqlString.Field<description> { }
		#endregion

		#region CreatedDateTime
		[PXDBCreatedDateTime()]
		public virtual DateTime? CreatedDateTime { get; set; }
		public abstract class createdDateTime : PX.Data.BQL.BqlDateTime.Field<createdDateTime> { }
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

		#region LastModifiedDateTime
		[PXDBLastModifiedDateTime()]
		public virtual DateTime? LastModifiedDateTime { get; set; }
		public abstract class lastModifiedDateTime : PX.Data.BQL.BqlDateTime.Field<lastModifiedDateTime> { }
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

	[PXCacheName("Warehouse Locations")]
	public class HMLKInventoryWarehouseLocation : HMLKInventoryLocation
	{
		#region Keys
		public class PK : PrimaryKeyOf<HMLKInventoryWarehouseLocation>.By<locationCD>
		{
			public static HMLKInventoryWarehouseLocation Find(PXGraph graph, string locationCD) => FindBy(graph, locationCD);
		}
		#endregion

		#region LocationCD
		[PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">aaaaaaaaaaaaaaa")]
		[PXDefault]
		[PXUIField(DisplayName = "Location ID")]
		public new virtual string LocationCD { get; set; }
		public new abstract class locationCD : PX.Data.BQL.BqlString.Field<locationCD> { }
		#endregion
	}
}
