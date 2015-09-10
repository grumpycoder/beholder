//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using NHibernate template.
// Code is generated on: 1/27/2013 7:27:37 PM
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------
using splc.infrastructure.Repository;

using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace splc.domain
{

    /// <summary>
    /// There are no comments for splc.domain.CommonMaritialStatus, splc.domain in the schema.
    /// </summary>
    public partial class CommonMaritialStatus : IKeyed<int>
    {

        private int _Id;

        private string _Name;

        private System.Nullable<int> _SortOrder;

        private System.DateTime _DateCreated;

        private System.Nullable<System.DateTime> _DateModified;

        private System.Nullable<System.DateTime> _DateDeleted;

        private Iesi.Collections.ISet _CommonPeople;

        private SecurityUser _SecurityUser_CreatedUserId;

        private SecurityUser _SecurityUser_DeletedUserId;

        private SecurityUser _SecurityUser_ModifiedUserId;
    
        #region Extensibility Method Definitions
        
        partial void OnCreated();
        
        #endregion

        public CommonMaritialStatus()
        {
            this._CommonPeople = new Iesi.Collections.HashedSet();
            OnCreated();
        }

    
        /// <summary>
        /// There are no comments for Id in the schema.
        /// </summary>
        public virtual int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                this._Id = value;
            }
        }

    
        /// <summary>
        /// There are no comments for Name in the schema.
        /// </summary>
        public virtual string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this._Name = value;
            }
        }

    
        /// <summary>
        /// There are no comments for SortOrder in the schema.
        /// </summary>
        public virtual System.Nullable<int> SortOrder
        {
            get
            {
                return this._SortOrder;
            }
            set
            {
                this._SortOrder = value;
            }
        }

    
        /// <summary>
        /// There are no comments for DateCreated in the schema.
        /// </summary>
        public virtual System.DateTime DateCreated
        {
            get
            {
                return this._DateCreated;
            }
            set
            {
                this._DateCreated = value;
            }
        }

    
        /// <summary>
        /// There are no comments for DateModified in the schema.
        /// </summary>
        public virtual System.Nullable<System.DateTime> DateModified
        {
            get
            {
                return this._DateModified;
            }
            set
            {
                this._DateModified = value;
            }
        }

    
        /// <summary>
        /// There are no comments for DateDeleted in the schema.
        /// </summary>
        public virtual System.Nullable<System.DateTime> DateDeleted
        {
            get
            {
                return this._DateDeleted;
            }
            set
            {
                this._DateDeleted = value;
            }
        }

    
        /// <summary>
        /// There are no comments for CommonPeople in the schema.
        /// </summary>
        public virtual Iesi.Collections.ISet CommonPeople
        {
            get
            {
                return this._CommonPeople;
            }
            set
            {
                this._CommonPeople = value;
            }
        }

    
        /// <summary>
        /// There are no comments for SecurityUser_CreatedUserId in the schema.
        /// </summary>
        public virtual SecurityUser SecurityUser_CreatedUserId
        {
            get
            {
                return this._SecurityUser_CreatedUserId;
            }
            set
            {
                this._SecurityUser_CreatedUserId = value;
            }
        }

    
        /// <summary>
        /// There are no comments for SecurityUser_DeletedUserId in the schema.
        /// </summary>
        public virtual SecurityUser SecurityUser_DeletedUserId
        {
            get
            {
                return this._SecurityUser_DeletedUserId;
            }
            set
            {
                this._SecurityUser_DeletedUserId = value;
            }
        }

    
        /// <summary>
        /// There are no comments for SecurityUser_ModifiedUserId in the schema.
        /// </summary>
        public virtual SecurityUser SecurityUser_ModifiedUserId
        {
            get
            {
                return this._SecurityUser_ModifiedUserId;
            }
            set
            {
                this._SecurityUser_ModifiedUserId = value;
            }
        }
    }

}
