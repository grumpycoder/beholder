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
    /// There are no comments for splc.domain.BeholderContactChapterRel, splc.domain in the schema.
    /// </summary>
    public partial class BeholderContactChapterRel : IKeyed<int>
    {

        private int _Id;

        private System.Nullable<System.DateTime> _FirstKnownUseDate;

        private System.Nullable<System.DateTime> _LastKnownUseDate;

        private string _Comments;

        private string _Priority;

        private System.DateTime _DateCreated;

        private System.Nullable<System.DateTime> _DateModified;

        private System.Nullable<System.DateTime> _DateDeleted;

        private BeholderChapter _BeholderChapter;

        private CommonContact _CommonContact;

        private CommonContactType _CommonContactType;

        private SecurityUser _SecurityUser_CreatedUserId;

        private SecurityUser _SecurityUser_DeletedUserId;

        private SecurityUser _SecurityUser_ModifiedUserId;

        private CommonPrimaryStatus _CommonPrimaryStatus;
    
        #region Extensibility Method Definitions
        
        partial void OnCreated();
        
        #endregion

        public BeholderContactChapterRel()
        {
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
        /// There are no comments for FirstKnownUseDate in the schema.
        /// </summary>
        public virtual System.Nullable<System.DateTime> FirstKnownUseDate
        {
            get
            {
                return this._FirstKnownUseDate;
            }
            set
            {
                this._FirstKnownUseDate = value;
            }
        }

    
        /// <summary>
        /// There are no comments for LastKnownUseDate in the schema.
        /// </summary>
        public virtual System.Nullable<System.DateTime> LastKnownUseDate
        {
            get
            {
                return this._LastKnownUseDate;
            }
            set
            {
                this._LastKnownUseDate = value;
            }
        }

    
        /// <summary>
        /// There are no comments for Comments in the schema.
        /// </summary>
        public virtual string Comments
        {
            get
            {
                return this._Comments;
            }
            set
            {
                this._Comments = value;
            }
        }

    
        /// <summary>
        /// There are no comments for Priority in the schema.
        /// </summary>
        public virtual string Priority
        {
            get
            {
                return this._Priority;
            }
            set
            {
                this._Priority = value;
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
        /// There are no comments for BeholderChapter in the schema.
        /// </summary>
        public virtual BeholderChapter BeholderChapter
        {
            get
            {
                return this._BeholderChapter;
            }
            set
            {
                this._BeholderChapter = value;
            }
        }

    
        /// <summary>
        /// There are no comments for CommonContact in the schema.
        /// </summary>
        public virtual CommonContact CommonContact
        {
            get
            {
                return this._CommonContact;
            }
            set
            {
                this._CommonContact = value;
            }
        }

    
        /// <summary>
        /// There are no comments for CommonContactType in the schema.
        /// </summary>
        public virtual CommonContactType CommonContactType
        {
            get
            {
                return this._CommonContactType;
            }
            set
            {
                this._CommonContactType = value;
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

    
        /// <summary>
        /// There are no comments for CommonPrimaryStatus in the schema.
        /// </summary>
        public virtual CommonPrimaryStatus CommonPrimaryStatus
        {
            get
            {
                return this._CommonPrimaryStatus;
            }
            set
            {
                this._CommonPrimaryStatus = value;
            }
        }
    }

}
