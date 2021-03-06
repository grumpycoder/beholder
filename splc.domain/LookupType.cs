﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using NHibernate template.
// Code is generated on: 1/5/2013 5:40:36 PM
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using splc.infrastructure.Repository;

namespace splc.domain
{

    /// <summary>
    /// There are no comments for splc.domain.LookupType, splc.domain in the schema.
    /// </summary>
    public partial class LookupType : IKeyed<int>
    {
        [Key]
        public virtual int Id { get; set; }

        public virtual int LookupGroupId { get; set; }

        public virtual string Name { get; set; }

        public virtual System.Nullable<int> SortOrder { get; set; }

        public virtual System.DateTime DateCreated { get; set; }

        public virtual int CreatedUserId { get; set; }

        public virtual System.Nullable<System.DateTime> DateModified { get; set; }

        public virtual System.Nullable<int> ModifiedUserId { get; set; }

        public virtual System.Nullable<System.DateTime> DateDeleted { get; set; }

        public virtual System.Nullable<int> DeletedUserId { get; set; }

        public virtual Iesi.Collections.ISet Prefix { get; set; }

        public virtual Iesi.Collections.ISet Suffix { get; set; }

        public virtual Iesi.Collections.ISet LicenseType { get; set; }

        public virtual Iesi.Collections.ISet HairColor { get; set; }

        public virtual Iesi.Collections.ISet HairPattern { get; set; }

        public virtual Iesi.Collections.ISet EyeColor { get; set; }

        public virtual Iesi.Collections.ISet Race { get; set; }

        public virtual Iesi.Collections.ISet MartialStatus { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion

        public LookupType()
        {
            this.Prefix = new Iesi.Collections.HashedSet();
            this.Suffix = new Iesi.Collections.HashedSet();
            this.LicenseType = new Iesi.Collections.HashedSet();
            this.HairColor = new Iesi.Collections.HashedSet();
            this.HairPattern = new Iesi.Collections.HashedSet();
            this.EyeColor = new Iesi.Collections.HashedSet();
            this.Race = new Iesi.Collections.HashedSet();
            this.MartialStatus = new Iesi.Collections.HashedSet();
            OnCreated();
        }


    }

}
