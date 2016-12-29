﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WydatkiDomowe
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="HouseholdExpenses")]
	public partial class BillsBaseDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertAccount(Account instance);
    partial void UpdateAccount(Account instance);
    partial void DeleteAccount(Account instance);
    partial void InsertBillName(BillName instance);
    partial void UpdateBillName(BillName instance);
    partial void DeleteBillName(BillName instance);
    partial void InsertBill(Bill instance);
    partial void UpdateBill(Bill instance);
    partial void DeleteBill(Bill instance);
    partial void InsertCity(City instance);
    partial void UpdateCity(City instance);
    partial void DeleteCity(City instance);
    partial void InsertPostCode(PostCode instance);
    partial void UpdatePostCode(PostCode instance);
    partial void DeletePostCode(PostCode instance);
    partial void InsertRecipient(Recipient instance);
    partial void UpdateRecipient(Recipient instance);
    partial void DeleteRecipient(Recipient instance);
    partial void InsertStreet(Street instance);
    partial void UpdateStreet(Street instance);
    partial void DeleteStreet(Street instance);
    #endregion
		
		public BillsBaseDataContext() : 
				base(global::WydatkiDomowe.Properties.Settings.Default.HouseholdExpensesConnectionString2, mappingSource)
		{
			OnCreated();
		}
		
		public BillsBaseDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public BillsBaseDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public BillsBaseDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public BillsBaseDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Account> Accounts
		{
			get
			{
				return this.GetTable<Account>();
			}
		}
		
		public System.Data.Linq.Table<BillName> BillNames
		{
			get
			{
				return this.GetTable<BillName>();
			}
		}
		
		public System.Data.Linq.Table<Bill> Bills
		{
			get
			{
				return this.GetTable<Bill>();
			}
		}
		
		public System.Data.Linq.Table<City> Cities
		{
			get
			{
				return this.GetTable<City>();
			}
		}
		
		public System.Data.Linq.Table<PostCode> PostCodes
		{
			get
			{
				return this.GetTable<PostCode>();
			}
		}
		
		public System.Data.Linq.Table<Recipient> Recipients
		{
			get
			{
				return this.GetTable<Recipient>();
			}
		}
		
		public System.Data.Linq.Table<Street> Streets
		{
			get
			{
				return this.GetTable<Street>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="Expenses.Account")]
	public partial class Account : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _AccountID;
		
		private string _Name;
		
		private EntitySet<Recipient> _Recipients;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnAccountIDChanging(int value);
    partial void OnAccountIDChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    #endregion
		
		public Account()
		{
			this._Recipients = new EntitySet<Recipient>(new Action<Recipient>(this.attach_Recipients), new Action<Recipient>(this.detach_Recipients));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AccountID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int AccountID
		{
			get
			{
				return this._AccountID;
			}
			set
			{
				if ((this._AccountID != value))
				{
					this.OnAccountIDChanging(value);
					this.SendPropertyChanging();
					this._AccountID = value;
					this.SendPropertyChanged("AccountID");
					this.OnAccountIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="VarChar(26) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Account_Recipient", Storage="_Recipients", ThisKey="AccountID", OtherKey="AccountID")]
		public EntitySet<Recipient> Recipients
		{
			get
			{
				return this._Recipients;
			}
			set
			{
				this._Recipients.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Recipients(Recipient entity)
		{
			this.SendPropertyChanging();
			entity.Account = this;
		}
		
		private void detach_Recipients(Recipient entity)
		{
			this.SendPropertyChanging();
			entity.Account = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="Expenses.BillName")]
	public partial class BillName : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _BillNameID;
		
		private string _Name;
		
		private System.DateTime _RequiredDate;
		
		private EntitySet<Bill> _Bills;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnBillNameIDChanging(int value);
    partial void OnBillNameIDChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnRequiredDateChanging(System.DateTime value);
    partial void OnRequiredDateChanged();
    #endregion
		
		public BillName()
		{
			this._Bills = new EntitySet<Bill>(new Action<Bill>(this.attach_Bills), new Action<Bill>(this.detach_Bills));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BillNameID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int BillNameID
		{
			get
			{
				return this._BillNameID;
			}
			set
			{
				if ((this._BillNameID != value))
				{
					this.OnBillNameIDChanging(value);
					this.SendPropertyChanging();
					this._BillNameID = value;
					this.SendPropertyChanged("BillNameID");
					this.OnBillNameIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="VarChar(15) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RequiredDate", DbType="Date NOT NULL")]
		public System.DateTime RequiredDate
		{
			get
			{
				return this._RequiredDate;
			}
			set
			{
				if ((this._RequiredDate != value))
				{
					this.OnRequiredDateChanging(value);
					this.SendPropertyChanging();
					this._RequiredDate = value;
					this.SendPropertyChanged("RequiredDate");
					this.OnRequiredDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="BillName_Bill", Storage="_Bills", ThisKey="BillNameID", OtherKey="BillNameID")]
		public EntitySet<Bill> Bills
		{
			get
			{
				return this._Bills;
			}
			set
			{
				this._Bills.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Bills(Bill entity)
		{
			this.SendPropertyChanging();
			entity.BillName = this;
		}
		
		private void detach_Bills(Bill entity)
		{
			this.SendPropertyChanging();
			entity.BillName = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="Expenses.Bills")]
	public partial class Bill : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _BillsID;
		
		private int _BillNameID;
		
		private decimal _Amount;
		
		private System.DateTime _PaymentDate;
		
		private int _RecipientID;
		
		private EntityRef<BillName> _BillName;
		
		private EntityRef<Recipient> _Recipient;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnBillsIDChanging(int value);
    partial void OnBillsIDChanged();
    partial void OnBillNameIDChanging(int value);
    partial void OnBillNameIDChanged();
    partial void OnAmountChanging(decimal value);
    partial void OnAmountChanged();
    partial void OnPaymentDateChanging(System.DateTime value);
    partial void OnPaymentDateChanged();
    partial void OnRecipientIDChanging(int value);
    partial void OnRecipientIDChanged();
    #endregion
		
		public Bill()
		{
			this._BillName = default(EntityRef<BillName>);
			this._Recipient = default(EntityRef<Recipient>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BillsID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int BillsID
		{
			get
			{
				return this._BillsID;
			}
			set
			{
				if ((this._BillsID != value))
				{
					this.OnBillsIDChanging(value);
					this.SendPropertyChanging();
					this._BillsID = value;
					this.SendPropertyChanged("BillsID");
					this.OnBillsIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BillNameID", DbType="Int NOT NULL")]
		public int BillNameID
		{
			get
			{
				return this._BillNameID;
			}
			set
			{
				if ((this._BillNameID != value))
				{
					if (this._BillName.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnBillNameIDChanging(value);
					this.SendPropertyChanging();
					this._BillNameID = value;
					this.SendPropertyChanged("BillNameID");
					this.OnBillNameIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Amount", DbType="Money NOT NULL")]
		public decimal Amount
		{
			get
			{
				return this._Amount;
			}
			set
			{
				if ((this._Amount != value))
				{
					this.OnAmountChanging(value);
					this.SendPropertyChanging();
					this._Amount = value;
					this.SendPropertyChanged("Amount");
					this.OnAmountChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PaymentDate", DbType="Date NOT NULL")]
		public System.DateTime PaymentDate
		{
			get
			{
				return this._PaymentDate;
			}
			set
			{
				if ((this._PaymentDate != value))
				{
					this.OnPaymentDateChanging(value);
					this.SendPropertyChanging();
					this._PaymentDate = value;
					this.SendPropertyChanged("PaymentDate");
					this.OnPaymentDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RecipientID", DbType="Int NOT NULL")]
		public int RecipientID
		{
			get
			{
				return this._RecipientID;
			}
			set
			{
				if ((this._RecipientID != value))
				{
					if (this._Recipient.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnRecipientIDChanging(value);
					this.SendPropertyChanging();
					this._RecipientID = value;
					this.SendPropertyChanged("RecipientID");
					this.OnRecipientIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="BillName_Bill", Storage="_BillName", ThisKey="BillNameID", OtherKey="BillNameID", IsForeignKey=true)]
		public BillName BillName
		{
			get
			{
				return this._BillName.Entity;
			}
			set
			{
				BillName previousValue = this._BillName.Entity;
				if (((previousValue != value) 
							|| (this._BillName.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._BillName.Entity = null;
						previousValue.Bills.Remove(this);
					}
					this._BillName.Entity = value;
					if ((value != null))
					{
						value.Bills.Add(this);
						this._BillNameID = value.BillNameID;
					}
					else
					{
						this._BillNameID = default(int);
					}
					this.SendPropertyChanged("BillName");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Recipient_Bill", Storage="_Recipient", ThisKey="RecipientID", OtherKey="RecipientID", IsForeignKey=true)]
		public Recipient Recipient
		{
			get
			{
				return this._Recipient.Entity;
			}
			set
			{
				Recipient previousValue = this._Recipient.Entity;
				if (((previousValue != value) 
							|| (this._Recipient.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Recipient.Entity = null;
						previousValue.Bills.Remove(this);
					}
					this._Recipient.Entity = value;
					if ((value != null))
					{
						value.Bills.Add(this);
						this._RecipientID = value.RecipientID;
					}
					else
					{
						this._RecipientID = default(int);
					}
					this.SendPropertyChanged("Recipient");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="Expenses.City")]
	public partial class City : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _CityID;
		
		private string _Name;
		
		private EntitySet<Recipient> _Recipients;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnCityIDChanging(int value);
    partial void OnCityIDChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    #endregion
		
		public City()
		{
			this._Recipients = new EntitySet<Recipient>(new Action<Recipient>(this.attach_Recipients), new Action<Recipient>(this.detach_Recipients));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CityID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int CityID
		{
			get
			{
				return this._CityID;
			}
			set
			{
				if ((this._CityID != value))
				{
					this.OnCityIDChanging(value);
					this.SendPropertyChanging();
					this._CityID = value;
					this.SendPropertyChanged("CityID");
					this.OnCityIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="VarChar(15) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="City_Recipient", Storage="_Recipients", ThisKey="CityID", OtherKey="CityID")]
		public EntitySet<Recipient> Recipients
		{
			get
			{
				return this._Recipients;
			}
			set
			{
				this._Recipients.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Recipients(Recipient entity)
		{
			this.SendPropertyChanging();
			entity.City = this;
		}
		
		private void detach_Recipients(Recipient entity)
		{
			this.SendPropertyChanging();
			entity.City = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="Expenses.PostCode")]
	public partial class PostCode : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _PostCodeID;
		
		private string _Name;
		
		private EntitySet<Recipient> _Recipients;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnPostCodeIDChanging(int value);
    partial void OnPostCodeIDChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    #endregion
		
		public PostCode()
		{
			this._Recipients = new EntitySet<Recipient>(new Action<Recipient>(this.attach_Recipients), new Action<Recipient>(this.detach_Recipients));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PostCodeID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int PostCodeID
		{
			get
			{
				return this._PostCodeID;
			}
			set
			{
				if ((this._PostCodeID != value))
				{
					this.OnPostCodeIDChanging(value);
					this.SendPropertyChanging();
					this._PostCodeID = value;
					this.SendPropertyChanged("PostCodeID");
					this.OnPostCodeIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="VarChar(6) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="PostCode_Recipient", Storage="_Recipients", ThisKey="PostCodeID", OtherKey="PostCodeID")]
		public EntitySet<Recipient> Recipients
		{
			get
			{
				return this._Recipients;
			}
			set
			{
				this._Recipients.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Recipients(Recipient entity)
		{
			this.SendPropertyChanging();
			entity.PostCode = this;
		}
		
		private void detach_Recipients(Recipient entity)
		{
			this.SendPropertyChanging();
			entity.PostCode = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="Expenses.Recipient")]
	public partial class Recipient : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _RecipientID;
		
		private string _Name;
		
		private int _AccountID;
		
		private int _PostCodeID;
		
		private int _CityID;
		
		private int _StreetID;
		
		private string _BuildingNR;
		
		private EntitySet<Bill> _Bills;
		
		private EntityRef<Account> _Account;
		
		private EntityRef<City> _City;
		
		private EntityRef<PostCode> _PostCode;
		
		private EntityRef<Street> _Street;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnRecipientIDChanging(int value);
    partial void OnRecipientIDChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnAccountIDChanging(int value);
    partial void OnAccountIDChanged();
    partial void OnPostCodeIDChanging(int value);
    partial void OnPostCodeIDChanged();
    partial void OnCityIDChanging(int value);
    partial void OnCityIDChanged();
    partial void OnStreetIDChanging(int value);
    partial void OnStreetIDChanged();
    partial void OnBuildingNRChanging(string value);
    partial void OnBuildingNRChanged();
    #endregion
		
		public Recipient()
		{
			this._Bills = new EntitySet<Bill>(new Action<Bill>(this.attach_Bills), new Action<Bill>(this.detach_Bills));
			this._Account = default(EntityRef<Account>);
			this._City = default(EntityRef<City>);
			this._PostCode = default(EntityRef<PostCode>);
			this._Street = default(EntityRef<Street>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RecipientID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int RecipientID
		{
			get
			{
				return this._RecipientID;
			}
			set
			{
				if ((this._RecipientID != value))
				{
					this.OnRecipientIDChanging(value);
					this.SendPropertyChanging();
					this._RecipientID = value;
					this.SendPropertyChanged("RecipientID");
					this.OnRecipientIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="VarChar(15) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AccountID", DbType="Int NOT NULL")]
		public int AccountID
		{
			get
			{
				return this._AccountID;
			}
			set
			{
				if ((this._AccountID != value))
				{
					if (this._Account.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnAccountIDChanging(value);
					this.SendPropertyChanging();
					this._AccountID = value;
					this.SendPropertyChanged("AccountID");
					this.OnAccountIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PostCodeID", DbType="Int NOT NULL")]
		public int PostCodeID
		{
			get
			{
				return this._PostCodeID;
			}
			set
			{
				if ((this._PostCodeID != value))
				{
					if (this._PostCode.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnPostCodeIDChanging(value);
					this.SendPropertyChanging();
					this._PostCodeID = value;
					this.SendPropertyChanged("PostCodeID");
					this.OnPostCodeIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CityID", DbType="Int NOT NULL")]
		public int CityID
		{
			get
			{
				return this._CityID;
			}
			set
			{
				if ((this._CityID != value))
				{
					if (this._City.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnCityIDChanging(value);
					this.SendPropertyChanging();
					this._CityID = value;
					this.SendPropertyChanged("CityID");
					this.OnCityIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_StreetID", DbType="Int NOT NULL")]
		public int StreetID
		{
			get
			{
				return this._StreetID;
			}
			set
			{
				if ((this._StreetID != value))
				{
					if (this._Street.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnStreetIDChanging(value);
					this.SendPropertyChanging();
					this._StreetID = value;
					this.SendPropertyChanged("StreetID");
					this.OnStreetIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BuildingNR", DbType="VarChar(5) NOT NULL", CanBeNull=false)]
		public string BuildingNR
		{
			get
			{
				return this._BuildingNR;
			}
			set
			{
				if ((this._BuildingNR != value))
				{
					this.OnBuildingNRChanging(value);
					this.SendPropertyChanging();
					this._BuildingNR = value;
					this.SendPropertyChanged("BuildingNR");
					this.OnBuildingNRChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Recipient_Bill", Storage="_Bills", ThisKey="RecipientID", OtherKey="RecipientID")]
		public EntitySet<Bill> Bills
		{
			get
			{
				return this._Bills;
			}
			set
			{
				this._Bills.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Account_Recipient", Storage="_Account", ThisKey="AccountID", OtherKey="AccountID", IsForeignKey=true)]
		public Account Account
		{
			get
			{
				return this._Account.Entity;
			}
			set
			{
				Account previousValue = this._Account.Entity;
				if (((previousValue != value) 
							|| (this._Account.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Account.Entity = null;
						previousValue.Recipients.Remove(this);
					}
					this._Account.Entity = value;
					if ((value != null))
					{
						value.Recipients.Add(this);
						this._AccountID = value.AccountID;
					}
					else
					{
						this._AccountID = default(int);
					}
					this.SendPropertyChanged("Account");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="City_Recipient", Storage="_City", ThisKey="CityID", OtherKey="CityID", IsForeignKey=true)]
		public City City
		{
			get
			{
				return this._City.Entity;
			}
			set
			{
				City previousValue = this._City.Entity;
				if (((previousValue != value) 
							|| (this._City.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._City.Entity = null;
						previousValue.Recipients.Remove(this);
					}
					this._City.Entity = value;
					if ((value != null))
					{
						value.Recipients.Add(this);
						this._CityID = value.CityID;
					}
					else
					{
						this._CityID = default(int);
					}
					this.SendPropertyChanged("City");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="PostCode_Recipient", Storage="_PostCode", ThisKey="PostCodeID", OtherKey="PostCodeID", IsForeignKey=true)]
		public PostCode PostCode
		{
			get
			{
				return this._PostCode.Entity;
			}
			set
			{
				PostCode previousValue = this._PostCode.Entity;
				if (((previousValue != value) 
							|| (this._PostCode.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._PostCode.Entity = null;
						previousValue.Recipients.Remove(this);
					}
					this._PostCode.Entity = value;
					if ((value != null))
					{
						value.Recipients.Add(this);
						this._PostCodeID = value.PostCodeID;
					}
					else
					{
						this._PostCodeID = default(int);
					}
					this.SendPropertyChanged("PostCode");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Street_Recipient", Storage="_Street", ThisKey="StreetID", OtherKey="StreetID", IsForeignKey=true)]
		public Street Street
		{
			get
			{
				return this._Street.Entity;
			}
			set
			{
				Street previousValue = this._Street.Entity;
				if (((previousValue != value) 
							|| (this._Street.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Street.Entity = null;
						previousValue.Recipients.Remove(this);
					}
					this._Street.Entity = value;
					if ((value != null))
					{
						value.Recipients.Add(this);
						this._StreetID = value.StreetID;
					}
					else
					{
						this._StreetID = default(int);
					}
					this.SendPropertyChanged("Street");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Bills(Bill entity)
		{
			this.SendPropertyChanging();
			entity.Recipient = this;
		}
		
		private void detach_Bills(Bill entity)
		{
			this.SendPropertyChanging();
			entity.Recipient = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="Expenses.Street")]
	public partial class Street : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _StreetID;
		
		private string _Name;
		
		private EntitySet<Recipient> _Recipients;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnStreetIDChanging(int value);
    partial void OnStreetIDChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    #endregion
		
		public Street()
		{
			this._Recipients = new EntitySet<Recipient>(new Action<Recipient>(this.attach_Recipients), new Action<Recipient>(this.detach_Recipients));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_StreetID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int StreetID
		{
			get
			{
				return this._StreetID;
			}
			set
			{
				if ((this._StreetID != value))
				{
					this.OnStreetIDChanging(value);
					this.SendPropertyChanging();
					this._StreetID = value;
					this.SendPropertyChanged("StreetID");
					this.OnStreetIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="VarChar(15) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Street_Recipient", Storage="_Recipients", ThisKey="StreetID", OtherKey="StreetID")]
		public EntitySet<Recipient> Recipients
		{
			get
			{
				return this._Recipients;
			}
			set
			{
				this._Recipients.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Recipients(Recipient entity)
		{
			this.SendPropertyChanging();
			entity.Street = this;
		}
		
		private void detach_Recipients(Recipient entity)
		{
			this.SendPropertyChanging();
			entity.Street = null;
		}
	}
}
#pragma warning restore 1591
