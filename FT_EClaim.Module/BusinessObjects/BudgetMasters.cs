﻿using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace FT_EClaim.Module.BusinessObjects
{
    //[DefaultClassOptions]
    //[ImageName("BO_Contact")]
    [NavigationItem("Setup")]
    [DefaultProperty("BoFullName")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    [RuleCriteria("BudgetMastersDeleteRule", DefaultContexts.Delete, "1=0", "Cannot Delete.")]
    [RuleCriteria("BudgetMastersSaveRule", DefaultContexts.Save, "IsOptionValid", "Please select at least 1 option with [?] behind.")]
    [RuleCriteria("BudgetMastersSaveRule2", DefaultContexts.Save, "IsDocumentValid", "Budget Amount is required when budget type = Document.")]
    [RuleCriteria("BudgetMastersSaveRule3", DefaultContexts.Save, "IsPeriodValid", "Budget Amount is not required when budget type = Period.")]

    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class BudgetMasters : XPObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public BudgetMasters(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
            IsActive = true;
            BudgetType = BudgetTypes.Document;
            BoCode = "";
            BoName = "";
        }
        //private string _PersistentProperty;
        //[XafDisplayName("My display name"), ToolTip("My hint message")]
        //[ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)]
        //[Persistent("DatabaseColumnName"), RuleRequiredField(DefaultContexts.Save)]
        //public string PersistentProperty {
        //    get { return _PersistentProperty; }
        //    set { SetPropertyValue("PersistentProperty", ref _PersistentProperty, value); }
        //}

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.PersistentProperty = "Paid";
        //}

        private string _BoCode;
        [XafDisplayName("Code"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [RuleUniqueValue]
        [Appearance("BoCode", Enabled = false, Criteria = "not IsNew")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Index(0)]
        public string BoCode
        {
            get { return _BoCode; }
            set
            {                
                SetPropertyValue("BoCode", ref _BoCode, value);
            }
        }

        private string _BoName;
        [XafDisplayName("Name"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        [Index(1)]
        public string BoName
        {
            get { return _BoName; }
            set
            {
                SetPropertyValue("BoName", ref _BoName, value);
            }
        }

        [PersistentAlias("concat(BoCode, '::', BoName)")]
        [Index(2), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string BoFullName
        {
            get { return EvaluateAlias("BoFullName").ToString(); }
        }

        private bool _IsActive;
        [XafDisplayName("Active")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [Index(10)]
        public bool IsActive
        {
            get { return _IsActive; }
            set
            {
                SetPropertyValue("IsActive", ref _IsActive, value);
            }
        }

        private Accounts _Account;
        [XafDisplayName("Account ?")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [Index(20)]
        public Accounts Account
        {
            get { return _Account; }
            set
            {
                SetPropertyValue("Account", ref _Account, value);
            }
        }

        private Departments _Department;
        [XafDisplayName("Department ?")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [Index(21)]
        public Departments Department
        {
            get { return _Department; }
            set
            {
                SetPropertyValue("Department", ref _Department, value);
            }
        }
        private Divisions _Division;
        [XafDisplayName("Division ?")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [Index(22)]
        public Divisions Division
        {
            get { return _Division; }
            set
            {
                SetPropertyValue("Division", ref _Division, value);
            }
        }
        private Projects _Project;
        [XafDisplayName("Project ?")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [Index(23)]
        public Projects Project
        {
            get { return _Project; }
            set
            {
                SetPropertyValue("Project", ref _Project, value);
            }
        }

        private Positions _Position;
        [XafDisplayName("Postition ?")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [Index(24)]
        public Positions Position
        {
            get { return _Position; }
            set
            {
                SetPropertyValue("Position", ref _Position, value);
            }
        }

        private Employees _Employee;
        [XafDisplayName("Employee ?")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [Index(25)]
        public Employees Employee
        {
            get { return _Employee; }
            set
            {
                SetPropertyValue("Employee", ref _Employee, value);
            }
        }
        private BudgetTypes _BudgetType;
        [ImmediatePostData]
        [XafDisplayName("Budget Type")]
        [Appearance("BudgetType", Enabled = false, Criteria = "not IsNew")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Index(50)]
        public BudgetTypes BudgetType
        {
            get { return _BudgetType; }
            set
            {
                SetPropertyValue("BudgetType", ref _BudgetType, value);
            }
        }

        private decimal _Amount;
        [XafDisplayName("Budget Amount")]
        [Appearance("Amount", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "IsShowBudget")]
        //[RuleRequiredField(DefaultContexts.Save)]
        [Index(60)]
        public decimal Amount
        {
            get { return _Amount; }
            set
            {
                SetPropertyValue("Amount", ref _Amount, value);
            }
        }

        [Browsable(false)]
        public bool IsNew
        {
            get
            { return Session.IsNewObject(this); }
        }
        [Browsable(false)]
        public bool IsOptionValid
        {
            get
            {
                int temp = 0;

                if (Account != null)
                    temp++;
                if (Department != null)
                    temp++;
                if (Project != null)
                    temp++;
                if (Position != null)
                    temp++;
                if (Employee != null)
                    temp++;
                return temp > 0 ? true : false;
            }
        }
        [Browsable(false)]
        public bool IsDocumentValid
        {
            get
            {
                if (BudgetType == BudgetTypes.Document)
                    if (Amount <= 0) return false;
                return true;
            }
        }
        [Browsable(false)]
        public bool IsPeriodValid
        {
            get
            {
                return true;
            }
        }
        [Browsable(false)]
        public bool IsShowBudget
        {
            get
            {
                return BudgetType == BudgetTypes.Period? true : false;
            }
        }
        [Association("BudgetMasters-Budgets")]
        [Appearance("Budget", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "not IsShowBudget")]
        public XPCollection<Budgets> Budget
        {
            get { return GetCollection<Budgets>("Budget"); }
        }

        [Association("ApprovalBudgetMasters")]
        [XafDisplayName("Approval")]
        [Appearance("Approval", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        public XPCollection<Approvals> Approval
        {
            get { return GetCollection<Approvals>("Approval"); }
        }
    }
}