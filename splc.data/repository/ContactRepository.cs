using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using splc.domain.Models;

namespace splc.data.repository
{
    public class ContactRepository : IContactRepository
    {

        private readonly ACDBContext context;

        public ContactRepository(ACDBContext Context)
        {
            context = Context;
        }

        #region Main

        public IQueryable<Contact> All
        {
            get { return context.Contacts.Where(x => x.DateDeleted == null && x.RemovalStatusId == null); }
        }

        public IQueryable<Contact> GetContactsIncluding(params Expression<Func<Contact, object>>[] includeProperties)
        {
            IQueryable<Contact> query = context.Contacts.Where(x => x.DateDeleted == null && x.RemovalStatusId == null);
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public IQueryable<Contact> Get(Expression<Func<Contact, bool>> filter)
        {
            if (filter != null)
            {
                return context.Contacts.Where(filter).Where(x => x.DateDeleted == null && x.RemovalStatusId == null);
            }
            return context.Contacts.Where(x => x.DateDeleted == null && x.RemovalStatusId == null);
        }

        public IQueryable<Contact> GetContacts()
        {
            return context.Contacts.Where(x => x.DateDeleted == null && x.RemovalStatusId == null);
        }

        public IQueryable<Contact> GetContacts(Expression<Func<Contact, bool>> filter)
        {
            if (filter != null)
            {
                return context.Contacts.Where(filter).Where(x => x.DateDeleted == null && x.RemovalStatusId == null);
            }
            return context.Contacts.Where(x => x.DateDeleted == null && x.RemovalStatusId == null);
        }

        public IQueryable<Contact> Get(User user, Expression<Func<Contact, bool>> filter)
        {
            if (filter != null)
            {
                return context.Contacts.Where(filter).Where(b => b.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel && b.DateDeleted == null && b.RemovalStatusId == null);
            }
            return context.Contacts.Where(b => b.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel && b.DateDeleted == null && b.RemovalStatusId == null);
        }

        public IQueryable<Contact> GetContacts(User user)
        {
            return context.Contacts.Where(b => b.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel && b.DateDeleted == null && b.RemovalStatusId == null);
        }

        public IQueryable<Contact> GetContacts(User user, Expression<Func<Contact, bool>> filter)
        {
            if (filter != null)
            {
                return context.Contacts.Where(filter).Where(b => b.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel && b.DateDeleted == null && b.RemovalStatusId == null);
            }
            return context.Contacts.Where(b => b.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel && b.DateDeleted == null && b.RemovalStatusId == null);
        }

        public Contact Find(int id)
        {
            var ent = context.Contacts.Find(id);
            if (ent.DateDeleted == null)
            {
                return ent;
            }
            return null;
        }

        public Contact Find(User user, int id)
        {
            var ent = context.Contacts.Find(id);

            if (ent.DateDeleted == null && ent.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel && ent.RemovalStatusId == null)
            {
                return ent;
            }
            return null;
        }

        public void InsertOrUpdate(Contact entity)
        {
            if (entity.Id == default(int))
            {
                // New entity
                context.CommonPersons.Add(entity.CommonPerson);
                context.Contacts.Add(entity);
            }
            else
            {
                // Existing entity
                context.Entry(entity).State = EntityState.Modified;
                context.Entry(entity.CommonPerson).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var entity = context.Contacts.Find(id);
            context.Contacts.Remove(entity);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        #endregion


        #region Media Image

        public ContactMediaImageRel GetPrimaryImage(int contactId)
        {
            return context.ContactMediaImageRels.FirstOrDefault(x => x.Contact.Id.Equals(contactId) && x.MediaImage.DateDeleted == null && x.MediaImage.RemovalStatusId == null);
        }

        public ContactMediaImageRel GetImage(int id)
        {
            var ent = context.ContactMediaImageRels.Find(id);
            if (ent.Contact.DateDeleted == null && ent.MediaImage.DateDeleted == null && ent.Contact.RemovalStatusId == null && ent.MediaImage.RemovalStatusId == null)
            {
                return ent;
            }
            return null;
        }

        public IQueryable<ContactMediaImageRel> GetContactMediaImages(Expression<Func<ContactMediaImageRel, bool>> filter)
        {
            if (filter != null)
            {
                return context.ContactMediaImageRels.Where(filter).Where(x => x.Contact.DateDeleted == null && x.MediaImage.DateDeleted == null && x.Contact.RemovalStatusId == null && x.MediaImage.RemovalStatusId == null);
            }
            return context.ContactMediaImageRels.Where(x => x.Contact.DateDeleted == null && x.MediaImage.DateDeleted == null && x.Contact.RemovalStatusId == null && x.MediaImage.RemovalStatusId == null);
        }

        public IQueryable<ContactMediaImageRel> GetContactMediaImages(int? id, int? mediaId)
        {
            return context.ContactMediaImageRels.Where(x => (x.Contact.Id.Equals(id) || x.MediaImageId.Equals(mediaId)) && x.Contact.DateDeleted == null && x.MediaImage.DateDeleted == null && x.Contact.RemovalStatusId == null && x.MediaImage.RemovalStatusId == null);
        }

        public void InsertOrUpdateContactImage(ContactMediaImageRel contactMediaImage)
        {
            if (contactMediaImage.Id == default(long))
            {
                // New entity
                context.ContactMediaImageRels.Add(contactMediaImage);
            }
            else
            {
                // Existing entity
                context.Entry(contactMediaImage).State = EntityState.Modified;
            }
        }

        public void DeleteContactImage(int id)
        {
            var contactmediaimagerel = context.ContactMediaImageRels.Find(id);
            context.ContactMediaImageRels.Remove(contactmediaimagerel);
        }

        #endregion


        #region Contact Relationship

        public ContactContactRel GetContactContact(int id)
        {
            var ent = context.ContactContactRels.Find(id);
            if (ent.Contact.DateDeleted == null && ent.Contact2.DateDeleted == null && ent.Contact.RemovalStatusId == null && ent.Contact2.RemovalStatusId == null)
            {
                return ent;
            }
            return null;
        }

        public IQueryable<ContactContactRel> GetContactContacts(Expression<Func<ContactContactRel, bool>> filter)
        {
            if (filter != null)
            {
                return context.ContactContactRels.Where(filter).Where(x => x.Contact.DateDeleted == null && x.Contact2.DateDeleted == null && x.Contact.RemovalStatusId == null && x.Contact2.RemovalStatusId == null);
            }
            return context.ContactContactRels.Where(x => x.Contact.DateDeleted == null && x.Contact2.DateDeleted == null && x.Contact.RemovalStatusId == null && x.Contact2.RemovalStatusId == null);
        }

        public IQueryable<ContactContactRel> GetContactContacts(int contactId)
        {
            return context.ContactContactRels.Where(x => (x.Contact.Id.Equals(contactId) || x.Contact2Id.Equals(contactId)) && x.Contact.DateDeleted == null && x.Contact2.DateDeleted == null && x.Contact.RemovalStatusId == null && x.Contact2.RemovalStatusId == null);
        }

        public void InsertOrUpdateContactContact(ContactContactRel contactcontactrel)
        {
            if (contactcontactrel.Id == default(int))
            {
                // New entity
                context.ContactContactRels.Add(contactcontactrel);
            }
            else
            {
                // Existing entity
                context.Entry(contactcontactrel).State = EntityState.Modified;
            }
        }

        public void DeleteContactContact(int id)
        {
            var entity = context.ContactContactRels.Find(id);
            context.ContactContactRels.Remove(entity);
        }

        #endregion


        #region MediaCorrespondences

        public ContactMediaCorrespondenceRel GetContactMediaCorrespondence(int id)
        {
            var ent = context.ContactMediaCorrespondenceRels.Find(id);
            if (ent.Contact.DateDeleted == null && ent.MediaCorrespondence.DateDeleted == null && ent.Contact.RemovalStatusId == null && ent.MediaCorrespondence.RemovalStatusId == null)
            {
                return ent;
            }
            return null;
        }

        public IQueryable<ContactMediaCorrespondenceRel> GetContactMediaCorrespondences(Expression<Func<ContactMediaCorrespondenceRel, bool>> filter)
        {
            if (filter != null)
            {
                return context.ContactMediaCorrespondenceRels.Where(filter).Where(x => x.Contact.DateDeleted == null && x.MediaCorrespondence.DateDeleted == null && x.Contact.RemovalStatusId == null && x.MediaCorrespondence.RemovalStatusId == null);
            }
            return context.ContactMediaCorrespondenceRels.Where(x => x.Contact.DateDeleted == null && x.MediaCorrespondence.DateDeleted == null && x.Contact.RemovalStatusId == null && x.MediaCorrespondence.RemovalStatusId == null);
        }

        public void InsertOrUpdateContactMediaCorrespondence(ContactMediaCorrespondenceRel contactmediacorrespondencerel)
        {
            if (contactmediacorrespondencerel.Id == default(int))
            {
                // New entity
                context.ContactMediaCorrespondenceRels.Add(contactmediacorrespondencerel);
            }
            else
            {
                // Existing entity
                context.Entry(contactmediacorrespondencerel).State = EntityState.Modified;
            }
        }

        public void DeleteContactMediaCorrespondence(int id)
        {
            var rel = context.ContactMediaCorrespondenceRels.Find(id);
            context.ContactMediaCorrespondenceRels.Remove(rel);
        }


        #endregion


        #region Subscriptions

        public ContactSubscriptionRel GetContactSubscription(int id)
        {
            var ent = context.ContactSubscriptionRels.Find(id);
            if (ent.Contact.DateDeleted == null && ent.Subscription.DateDeleted == null && ent.Contact.RemovalStatusId == null && ent.Subscription.RemovalStatusId == null)
            {
                return ent;
            }
            return null;
        }

        public IQueryable<ContactSubscriptionRel> GetContactSubscriptions(Expression<Func<ContactSubscriptionRel, bool>> filter)
        {
            if (filter != null)
            {
                return context.ContactSubscriptionRels.Where(filter).Where(x => x.Contact.DateDeleted == null && x.Subscription.DateDeleted == null && x.Contact.RemovalStatusId == null && x.Subscription.RemovalStatusId == null);
            }
            return context.ContactSubscriptionRels.Where(x => x.Contact.DateDeleted == null && x.Subscription.DateDeleted == null && x.Contact.RemovalStatusId == null && x.Subscription.RemovalStatusId == null);
        }

        public void InsertOrUpdateContactSubscription(ContactSubscriptionRel contactsubscriptionrel)
        {
            if (contactsubscriptionrel.Id == default(int))
            {
                // New entity
                context.ContactSubscriptionRels.Add(contactsubscriptionrel);
            }
            else
            {
                // Existing entity
                context.Entry(contactsubscriptionrel).State = EntityState.Modified;
            }
        }

        public void DeleteContactSubscription(int id)
        {
            var rel = context.ContactSubscriptionRels.Find(id);
            context.ContactSubscriptionRels.Remove(rel);
        }


        #endregion


        //#region MediaPublisheds

        //public ContactMediaPublishedRel GetContactMediaPublished(int id)
        //{
        //    return context.ContactMediaPublishedRels.Find(id);
        //}

        //public IQueryable<ContactMediaPublishedRel> GetContactMediaPublisheds(Expression<Func<ContactMediaPublishedRel, bool>> filter)
        //{
        //    if (filter != null)
        //    {
        //        return context.ContactMediaPublishedRels.Where(filter);
        //    }
        //    return context.ContactMediaPublishedRels;
        //}

        //public void InsertOrUpdateContactMediaPublished(ContactMediaPublishedRel contactmediapublishedrel)
        //{
        //    if (contactmediapublishedrel.Id == default(int))
        //    {
        //        // New entity
        //        context.ContactMediaPublishedRels.Add(contactmediapublishedrel);
        //    }
        //    else
        //    {
        //        // Existing entity
        //        context.Entry(contactmediapublishedrel).State = EntityState.Modified;
        //    }
        //}

        //public void DeleteContactMediaPublished(int id)
        //{
        //    var rel = context.ContactMediaPublishedRels.Find(id);
        //    context.ContactMediaPublishedRels.Remove(rel);
        //}


        //#endregion


        //#region MediaWebsiteEGroups

        //public ContactMediaWebsiteEGroupRel GetContactMediaWebsiteEGroup(int id)
        //{
        //    return context.ContactMediaWebsiteEGroupRels.Find(id);
        //}

        //public IQueryable<ContactMediaWebsiteEGroupRel> GetContactMediaWebsiteEGroups(Expression<Func<ContactMediaWebsiteEGroupRel, bool>> filter)
        //{
        //    if (filter != null)
        //    {
        //        return context.ContactMediaWebsiteEGroupRels.Where(filter);
        //    }
        //    return context.ContactMediaWebsiteEGroupRels;
        //}

        //public void InsertOrUpdateContactMediaWebsiteEGroup(ContactMediaWebsiteEGroupRel contactmediawebsiteegrouprel)
        //{
        //    if (contactmediawebsiteegrouprel.Id == default(int))
        //    {
        //        // New entity
        //        context.ContactMediaWebsiteEGroupRels.Add(contactmediawebsiteegrouprel);
        //    }
        //    else
        //    {
        //        // Existing entity
        //        context.Entry(contactmediawebsiteegrouprel).State = EntityState.Modified;
        //    }
        //}

        //public void DeleteContactMediaWebsiteEGroup(int id)
        //{
        //    var rel = context.ContactMediaWebsiteEGroupRels.Find(id);
        //    context.ContactMediaWebsiteEGroupRels.Remove(rel);
        //}


        //#endregion


    }

    public interface IContactRepository : IDisposable
    {
        IQueryable<Contact> All { get; }
        IQueryable<Contact> GetContactsIncluding(params Expression<Func<Contact, object>>[] includeProperties);
        Contact Find(int id);
        Contact Find(User user, int id);
        IQueryable<Contact> GetContacts();
        IQueryable<Contact> Get(Expression<Func<Contact, bool>> filter);
        IQueryable<Contact> GetContacts(Expression<Func<Contact, bool>> filter);

        IQueryable<Contact> GetContacts(User user);
        IQueryable<Contact> Get(User user, Expression<Func<Contact, bool>> filter);
        IQueryable<Contact> GetContacts(User user, Expression<Func<Contact, bool>> filter);

        void InsertOrUpdate(Contact entity);
        void Delete(int id);
        void Save();

        ContactMediaImageRel GetPrimaryImage(int id);
        ContactMediaImageRel GetImage(int id);
        IQueryable<ContactMediaImageRel> GetContactMediaImages(Expression<Func<ContactMediaImageRel, bool>> filter);
        IQueryable<ContactMediaImageRel> GetContactMediaImages(int? contactId, int? mediaId);
        void InsertOrUpdateContactImage(ContactMediaImageRel contactMediaImage);
        void DeleteContactImage(int id);

        ContactContactRel GetContactContact(int id);
        IQueryable<ContactContactRel> GetContactContacts(int contactId);
        IQueryable<ContactContactRel> GetContactContacts(Expression<Func<ContactContactRel, bool>> filter);
        void InsertOrUpdateContactContact(ContactContactRel contactcontactrel);
        void DeleteContactContact(int id);

        ContactMediaCorrespondenceRel GetContactMediaCorrespondence(int id);
        IQueryable<ContactMediaCorrespondenceRel> GetContactMediaCorrespondences(Expression<Func<ContactMediaCorrespondenceRel, bool>> filter);
        void InsertOrUpdateContactMediaCorrespondence(ContactMediaCorrespondenceRel contactmediacorrespondencerel);
        void DeleteContactMediaCorrespondence(int id);

        //ContactMediaPublishedRel GetContactMediaPublished(int id);
        //IQueryable<ContactMediaPublishedRel> GetContactMediaPublisheds(Expression<Func<ContactMediaPublishedRel, bool>> filter);
        //void InsertOrUpdateContactMediaPublished(ContactMediaPublishedRel contactmediapublishedrel);
        //void DeleteContactMediaPublished(int id);

        //ContactMediaWebsiteEGroupRel GetContactMediaWebsiteEGroup(int id);
        //IQueryable<ContactMediaWebsiteEGroupRel> GetContactMediaWebsiteEGroups(Expression<Func<ContactMediaWebsiteEGroupRel, bool>> filter);
        //void InsertOrUpdateContactMediaWebsiteEGroup(ContactMediaWebsiteEGroupRel contactmediawebsiteegrouprel);
        //void DeleteContactMediaWebsiteEGroup(int id);

        ContactSubscriptionRel GetContactSubscription(int id);
        IQueryable<ContactSubscriptionRel> GetContactSubscriptions(Expression<Func<ContactSubscriptionRel, bool>> filter);
        void InsertOrUpdateContactSubscription(ContactSubscriptionRel contactsubscriptionrel);
        void DeleteContactSubscription(int id);


    }
}