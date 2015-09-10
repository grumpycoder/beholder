using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using splc.domain.Models;

namespace splc.data.repository
{
    public class PersonRepository : IPersonRepository
    {
        readonly ACDBContext _ctx;

        public PersonRepository(ACDBContext ctx)
        {
            _ctx = ctx;
        }

        #region Main
 
        public IQueryable<BeholderPerson> Get(User user)
        {
            return _ctx.BeholderPersons.Where(b => b.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel).Where(x => x.DateDeleted == null && x.RemovalStatusId == null);
        }


        public IQueryable<CommonPerson> GetCommonPersons(Expression<Func<CommonPerson, bool>> filter)
        {
            //throw new NotImplementedException();
            if (filter != null)
            {
                return _ctx.CommonPersons.Where(filter).Where(x => x.DateDeleted == null);
            }
            return _ctx.CommonPersons.Where(x => x.DateDeleted == null);
 
        }

        public IQueryable<BeholderPerson> Get(User user, Expression<Func<BeholderPerson, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.BeholderPersons.Where(filter).Where(b => b.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel).Where(x => x.DateDeleted == null && x.RemovalStatusId == null);
            }
            return _ctx.BeholderPersons.Where(x => x.DateDeleted == null && x.RemovalStatusId == null);
        }

        public IQueryable<BeholderPerson> Get(Expression<Func<BeholderPerson, bool>> filter, params Expression<Func<BeholderPerson, object>>[] includeProperties)
        {
            IQueryable<BeholderPerson> query = _ctx.BeholderPersons.Where(x => x.DateDeleted == null && x.RemovalStatusId == null);
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public BeholderPerson Get(int id, User user)
        {
            var person = _ctx.BeholderPersons.Find(id);
            
            if (person != null && person.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel && person.DateDeleted == null && person.RemovalStatusId == null)
            {
                return person;
            }
            return null;
        }

        public IQueryable<BeholderPerson> GetPendingRemovals()
        {
            IQueryable<BeholderPerson> query = _ctx.BeholderPersons.Where(x => x.DateDeleted == null && x.RemovalReason != null);
            return query;
        }

        public void InsertOrUpdate(BeholderPerson beholderperson)
        {
            if (beholderperson.Id == default(int))
            {
                //New Person
                _ctx.CommonPersons.Add(beholderperson.CommonPerson);
                _ctx.BeholderPersons.Add(beholderperson);
            }
            else
            {
                _ctx.Entry(beholderperson).State = EntityState.Modified;
                if (beholderperson.CommonPerson != null)
                {
                    _ctx.Entry(beholderperson.CommonPerson).State = EntityState.Modified;
                }
            }
        }

        public void Delete(int id)
        {
            var person = _ctx.BeholderPersons.Find(id);
            _ctx.BeholderPersons.Remove(person);
        }

        public void Save()
        {
            _ctx.SaveChanges();
        }

        //TODO: Does this need to be in PersonRepo.  Yes, this gets primary image for this person.(slj)
        public PersonMediaImageRel GetPrimaryImage(int personId)
        {
            return _ctx.PersonMediaImageRels.Where(x => x.MediaImage.DateDeleted == null && x.MediaImage.RemovalStatusId == null).FirstOrDefault(x => x.BeholderPerson.Id.Equals(personId) && x.BeholderPerson.RemovalStatusId == null);
        }

        public PersonMediaImageRel GetImage(int id)
        {
            var ent = _ctx.PersonMediaImageRels.Find(id);
            if (ent.BeholderPerson.DateDeleted == null && ent.MediaImage.DateDeleted == null && ent.MediaImage.RemovalStatusId == null)
            {
                return ent;
            }
            return null;
        }

        #endregion

        #region Media Images

        public IQueryable<PersonMediaImageRel> GetImages(Expression<Func<PersonMediaImageRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.PersonMediaImageRels.Where(filter).Where(x => x.BeholderPerson.DateDeleted == null && x.BeholderPerson.RemovalStatusId == null).Where(x => x.MediaImage.DateDeleted == null && x.MediaImage.RemovalStatusId == null);
            }
            return _ctx.PersonMediaImageRels;
        }

        //public IQueryable<PersonMediaImageRel> GetPersonMediaImages(int? id, int? mediaId)
        //{
        //    return _ctx.PersonMediaImageRels.Where(x => x.BeholderPerson.Id.Equals(id) || x.MediaImageId.Equals(mediaId)).Where(x => x.BeholderPerson.DateDeleted == null && x.BeholderPerson.RemovalStatusId == null).Where(x => x.MediaImage.DateDeleted == null);
        //}

        public void InsertOrUpdateImage(PersonMediaImageRel personMediaImage)
        {
            if (personMediaImage.Id == default(long))
            {
                // New entity
                _ctx.PersonMediaImageRels.Add(personMediaImage);
            }
            else
            {
                // Existing entity
                _ctx.Entry(personMediaImage).State = EntityState.Modified;
            }
        }

        public void DeleteImage(int id)
        {
            var personmediaimagerel = _ctx.PersonMediaImageRels.Find(id);
            _ctx.PersonMediaImageRels.Remove(personmediaimagerel);
        }

        #endregion

        #region Audio/Video

        public PersonMediaAudioVideoRel GetAudioVideo(int id)
        {
            var rel = _ctx.PersonMediaAudioVideoRels.Find(id);
            if (rel.BeholderPerson.DateDeleted == null && rel.MediaAudioVideo.DateDeleted == null && rel.MediaAudioVideo.RemovalStatusId == null)
            {
                return rel;
            }
            return null;

        }

        public IQueryable<PersonMediaAudioVideoRel> GetAudioVideos(Expression<Func<PersonMediaAudioVideoRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.PersonMediaAudioVideoRels.Where(filter).Where(x => x.BeholderPerson.DateDeleted == null && x.BeholderPerson.RemovalStatusId == null).Where(x => x.MediaAudioVideo.DateDeleted == null && x.MediaAudioVideo.RemovalStatusId == null);
            }
            return _ctx.PersonMediaAudioVideoRels.Where(x => x.BeholderPerson.DateDeleted == null && x.BeholderPerson.RemovalStatusId == null).Where(x => x.MediaAudioVideo.DateDeleted == null && x.MediaAudioVideo.RemovalStatusId == null);
        }

        public IQueryable<PersonMediaAudioVideoRel> GetAudioVideos(int? id, int? mediaId)
        {
            return _ctx.PersonMediaAudioVideoRels.Where(x => x.BeholderPerson.Id.Equals(id) || x.MediaAudioVideoId.Equals(mediaId)).Where(x => x.BeholderPerson.DateDeleted == null && x.BeholderPerson.RemovalStatusId == null).Where(x => x.MediaAudioVideo.DateDeleted == null && x.MediaAudioVideo.RemovalStatusId == null);
        }

        public void InsertOrUpdateAudioVideo(PersonMediaAudioVideoRel personMediaAudioVideo)
        {
            if (personMediaAudioVideo.Id == default(long))
            {
                // New entity
                _ctx.PersonMediaAudioVideoRels.Add(personMediaAudioVideo);
            }
            else
            {
                // Existing entity
                _ctx.Entry(personMediaAudioVideo).State = EntityState.Modified;
            }
        }

        public void DeleteAudioVideo(int id)
        {
            var personmediaAudioVideorel = _ctx.PersonMediaAudioVideoRels.Find(id);
            _ctx.PersonMediaAudioVideoRels.Remove(personmediaAudioVideorel);
        }

        #endregion

        #region Events

        public IQueryable<PersonEventRel> GetEvents(int? personId, int? eventId)
        {
            return _ctx.PersonEventRels.Where(x => x.PersonId.Equals(personId) || x.EventId.Equals(eventId)).Where(x => x.BeholderPerson.DateDeleted == null && x.BeholderPerson.RemovalStatusId == null).Where(x => x.Event.DateDeleted == null && x.Event.RemovalStatusId == null);
        }

        public IQueryable<PersonEventRel> GetEvents(Expression<Func<PersonEventRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.PersonEventRels.Where(filter).Where(x => x.BeholderPerson.DateDeleted == null && x.BeholderPerson.RemovalStatusId == null).Where(x => x.Event.DateDeleted == null && x.Event.RemovalStatusId == null);
            }
            return _ctx.PersonEventRels.Where(x => x.BeholderPerson.DateDeleted == null && x.BeholderPerson.RemovalStatusId == null).Where(x => x.Event.DateDeleted == null && x.Event.RemovalStatusId == null);
        }

        public PersonEventRel GetPersonEvent(int id)
        {
            var rel = _ctx.PersonEventRels.Find(id);
            if (rel.BeholderPerson.DateDeleted == null && rel.Event.DateDeleted == null && rel.Event.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public void InsertOrUpdateEvent(PersonEventRel personeventrel)
        {
            if (personeventrel.Id == default(int))
            {
                // New entity
                _ctx.PersonEventRels.Add(personeventrel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(personeventrel).State = EntityState.Modified;
            }
        }

        public void DeleteEvent(int id)
        {
            var personeventrel = _ctx.PersonEventRels.Find(id);
            _ctx.PersonEventRels.Remove(personeventrel);
        }

        #endregion

        #region Vehicle

        public PersonVehicleRel GetVehicle(int id)
        {
            var rel = _ctx.PersonVehicleRels.Find(id);
            if (rel.BeholderPerson.DateDeleted == null && rel.Vehicle.DateDeleted == null && rel.Vehicle.RemovalStatusId == null && rel.BeholderPerson.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<PersonVehicleRel> GetVehicles(Expression<Func<PersonVehicleRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.PersonVehicleRels.Where(filter).Where(x => x.BeholderPerson.DateDeleted == null && x.BeholderPerson.RemovalStatusId == null).Where(x => x.Vehicle.DateDeleted == null && x.Vehicle.RemovalStatusId == null);
            }
            return _ctx.PersonVehicleRels.Where(x => x.BeholderPerson.DateDeleted == null && x.BeholderPerson.RemovalStatusId == null).Where(x => x.Vehicle.DateDeleted == null && x.Vehicle.RemovalStatusId == null);
        }

        public IQueryable<PersonVehicleRel> GetVehicles(int? personId, int? vehicleId)
        {
            return _ctx.PersonVehicleRels.Where(x => x.PersonId.Equals(personId) || x.VehicleId.Equals(vehicleId)).Where(x => x.BeholderPerson.DateDeleted == null && x.BeholderPerson.RemovalStatusId == null).Where(x => x.Vehicle.DateDeleted == null && x.Vehicle.RemovalStatusId == null);
        }

        public void InsertOrUpdateVehicle(PersonVehicleRel personvehiclerel)
        {
            if (personvehiclerel.Id == default(int))
            {
                // New entity
                _ctx.PersonVehicleRels.Add(personvehiclerel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(personvehiclerel).State = EntityState.Modified;
            }
        }

        public void DeleteVehicle(int id)
        {
            var personVehicle = _ctx.PersonVehicleRels.Find(id);
            _ctx.PersonVehicleRels.Remove(personVehicle);
        }

        #endregion

        #region Person

        public PersonPersonRel GetPersonPerson(int id)
        {
            var rel = _ctx.PersonPersonRels.Find(id);
            if (rel.BeholderPerson.DateDeleted == null && rel.BeholderPerson2.DateDeleted == null &&  rel.BeholderPerson.RemovalStatusId == null && rel.BeholderPerson2.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<PersonPersonRel> GetPersonPersons(Expression<Func<PersonPersonRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.PersonPersonRels.Where(filter).Where(x => x.BeholderPerson.DateDeleted == null && x.BeholderPerson.RemovalStatusId == null).Where(x => x.BeholderPerson2.DateDeleted == null && x.BeholderPerson2.RemovalStatusId == null);
            }
            return _ctx.PersonPersonRels.Where(x => x.BeholderPerson.DateDeleted == null && x.BeholderPerson.RemovalStatusId == null).Where(x => x.BeholderPerson2.DateDeleted == null && x.BeholderPerson2.RemovalStatusId == null);
        }

        public IQueryable<PersonPersonRel> GetPersonPersons(int personId)
        {
            return _ctx.PersonPersonRels.Where(x => x.PersonId.Equals(personId) || x.Person2Id.Equals(personId)).Where(x => x.BeholderPerson.DateDeleted == null && x.BeholderPerson.RemovalStatusId == null).Where(x => x.BeholderPerson2.DateDeleted == null && x.BeholderPerson2.RemovalStatusId == null);
        }

        public void InsertOrUpdatePersonPerson(PersonPersonRel personpersonrel)
        {
            if (personpersonrel.Id == default(int))
            {
                // New entity
                _ctx.PersonPersonRels.Add(personpersonrel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(personpersonrel).State = EntityState.Modified;
            }
        }

        public void DeletePersonPerson(int id)
        {
            var personPerson = _ctx.PersonPersonRels.Find(id);
            _ctx.PersonPersonRels.Remove(personPerson);
        }

        #endregion

        #region Screen Name

        public PersonScreenName GetScreenName(int id)
        {
            var rel = _ctx.PersonScreenNames.Find(id);
            if (rel.BeholderPerson.DateDeleted == null && rel.DateDeleted == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<PersonScreenName> GetScreenNames(Expression<Func<PersonScreenName, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.PersonScreenNames.Where(filter).Where(x => x.BeholderPerson.DateDeleted == null && x.BeholderPerson.RemovalStatusId == null).Where(x => x.DateDeleted == null);
            }
            return _ctx.PersonScreenNames.Where(x => x.BeholderPerson.DateDeleted == null && x.BeholderPerson.RemovalStatusId == null).Where(x => x.DateDeleted == null);
        }

        public void InsertOrUpdateScreenName(PersonScreenName personscreenname)
        {
            if (personscreenname.Id == default(int))
            {
                // New entity
                _ctx.PersonScreenNames.Add(personscreenname);
            }
            else
            {
                // Existing entity
                _ctx.Entry(personscreenname).State = EntityState.Modified;
            }
        }

        public void DeleteScreenName(int id)
        {
            var personscreenname = _ctx.PersonScreenNames.Find(id);
            _ctx.PersonScreenNames.Remove(personscreenname);
        }
    
        #endregion

        #region Contacts

        public IQueryable<PersonContactRel> GetContacts(int? personId, int? contactId)
        {
            return _ctx.PersonContactRels.Where(x => x.PersonId.Equals(personId) || x.ContactId.Equals(contactId)).Where(x => x.BeholderPerson.DateDeleted == null && x.BeholderPerson.RemovalStatusId == null).Where(x => x.Contact.DateDeleted == null && x.Contact.RemovalStatusId == null);
        }

        public IQueryable<PersonContactRel> GetContacts(Expression<Func<PersonContactRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.PersonContactRels.Where(filter).Where(x => x.BeholderPerson.DateDeleted == null && x.BeholderPerson.RemovalStatusId == null).Where(x => x.Contact.DateDeleted == null && x.Contact.RemovalStatusId == null);
            }
            return _ctx.PersonContactRels.Where(x => x.BeholderPerson.DateDeleted == null && x.BeholderPerson.RemovalStatusId == null).Where(x => x.Contact.DateDeleted == null && x.Contact.RemovalStatusId == null);
        }

        public PersonContactRel GetPersonContact(int id)
        {
            var rel = _ctx.PersonContactRels.Find(id);
            if (rel.BeholderPerson.DateDeleted == null && rel.BeholderPerson.RemovalStatusId == null && rel.Contact.DateDeleted == null && rel.Contact.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public void InsertOrUpdateContact(PersonContactRel personcontactrel)
        {
            if (personcontactrel.Id == default(int))
            {
                // New entity
                _ctx.PersonContactRels.Add(personcontactrel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(personcontactrel).State = EntityState.Modified;
            }
        }

        public void DeleteContact(int id)
        {
            var personcontactrel = _ctx.PersonContactRels.Find(id);
            _ctx.PersonContactRels.Remove(personcontactrel);
        }

        #endregion

        #region MediaCorrespondences

        public PersonMediaCorrespondenceRel GetCorrespondence(int id)
        {
            var rel = _ctx.PersonMediaCorrespondenceRels.Find(id);
            if (rel.BeholderPerson.DateDeleted == null && rel.MediaCorrespondence.DateDeleted == null && rel.MediaCorrespondence.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<PersonMediaCorrespondenceRel> GetCorrespondences(Expression<Func<PersonMediaCorrespondenceRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.PersonMediaCorrespondenceRels.Where(filter).Where(x => x.BeholderPerson.DateDeleted == null && x.BeholderPerson.RemovalStatusId == null).Where(x => x.MediaCorrespondence.DateDeleted == null && x.MediaCorrespondence.RemovalStatusId == null);
            }
            return _ctx.PersonMediaCorrespondenceRels.Where(x => x.BeholderPerson.DateDeleted == null && x.BeholderPerson.RemovalStatusId == null).Where(x => x.MediaCorrespondence.DateDeleted == null && x.MediaCorrespondence.RemovalStatusId == null);
        }

        public void InsertOrUpdateCorrespondence(PersonMediaCorrespondenceRel personmediacorrespondencerel)
        {
            if (personmediacorrespondencerel.Id == default(int))
            {
                // New entity
                _ctx.PersonMediaCorrespondenceRels.Add(personmediacorrespondencerel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(personmediacorrespondencerel).State = EntityState.Modified;
            }
        }

        public void DeleteCorrespondence(int id)
        {
            var rel = _ctx.PersonMediaCorrespondenceRels.Find(id);
            _ctx.PersonMediaCorrespondenceRels.Remove(rel);
        }

        #endregion

        #region MediaPublisheds

        public PersonMediaPublishedRel GetPublished(int id)
        {
            var rel = _ctx.PersonMediaPublishedRels.Find(id);
            if (rel.BeholderPerson.DateDeleted == null && rel.MediaPublished.DateDeleted == null && rel.MediaPublished.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<PersonMediaPublishedRel> GetPublisheds(Expression<Func<PersonMediaPublishedRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.PersonMediaPublishedRels.Where(filter).Where(x => x.BeholderPerson.DateDeleted == null && x.BeholderPerson.RemovalStatusId == null).Where(x => x.MediaPublished.DateDeleted == null && x.MediaPublished.RemovalStatusId == null);
            }
            return _ctx.PersonMediaPublishedRels.Where(x => x.BeholderPerson.DateDeleted == null && x.BeholderPerson.RemovalStatusId == null).Where(x => x.MediaPublished.DateDeleted == null && x.MediaPublished.RemovalStatusId == null);
        }

        public void InsertOrUpdatePublished(PersonMediaPublishedRel personmediapublishedrel)
        {
            if (personmediapublishedrel.Id == default(int))
            {
                // New entity
                _ctx.PersonMediaPublishedRels.Add(personmediapublishedrel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(personmediapublishedrel).State = EntityState.Modified;
            }
        }

        public void DeletePublished(int id)
        {
            var rel = _ctx.PersonMediaPublishedRels.Find(id);
            _ctx.PersonMediaPublishedRels.Remove(rel);
        }

        #endregion

        #region MediaWebsiteEGroups

        public PersonMediaWebsiteEGroupRel GetWebsiteEGroup(int id)
        {
            var rel = _ctx.PersonMediaWebsiteEGroupRels.Find(id);
            if (rel.BeholderPerson.DateDeleted == null && rel.MediaWebsiteEGroup.DateDeleted == null && rel.MediaWebsiteEGroup.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<PersonMediaWebsiteEGroupRel> GetWebsiteEGroups(Expression<Func<PersonMediaWebsiteEGroupRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.PersonMediaWebsiteEGroupRels.Where(filter).Where(x => x.BeholderPerson.DateDeleted == null && x.BeholderPerson.RemovalStatusId == null).Where(x => x.MediaWebsiteEGroup.DateDeleted == null && x.MediaWebsiteEGroup.RemovalStatusId == null);
            }
            return _ctx.PersonMediaWebsiteEGroupRels.Where(x => x.BeholderPerson.DateDeleted == null && x.BeholderPerson.RemovalStatusId == null).Where(x => x.MediaWebsiteEGroup.DateDeleted == null && x.MediaWebsiteEGroup.RemovalStatusId == null);
        }

        public void InsertOrUpdateWebsiteEGroup(PersonMediaWebsiteEGroupRel personmediawebsiteegrouprel)
        {
            if (personmediawebsiteegrouprel.Id == default(int))
            {
                // New entity
                _ctx.PersonMediaWebsiteEGroupRels.Add(personmediawebsiteegrouprel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(personmediawebsiteegrouprel).State = EntityState.Modified;
            }
        }

        public void DeleteWebsiteEGroup(int id)
        {
            var rel = _ctx.PersonMediaWebsiteEGroupRels.Find(id);
            _ctx.PersonMediaWebsiteEGroupRels.Remove(rel);
        }

        #endregion
        
    }

    public interface IPersonRepository
    {
        IQueryable<BeholderPerson> Get(User user);
        IQueryable<BeholderPerson> Get(User user, Expression<Func<BeholderPerson, bool>> filter);
        IQueryable<BeholderPerson> Get(Expression<Func<BeholderPerson, bool>> filter, params Expression<Func<BeholderPerson, object>>[] includeProperties);
        BeholderPerson Get(int id, User user);
        IQueryable<BeholderPerson> GetPendingRemovals();
        void InsertOrUpdate(BeholderPerson beholderperson);
        void Delete(int id);
        void Save();

        PersonMediaImageRel GetPrimaryImage(int id);
        PersonMediaImageRel GetImage(int id);
        IQueryable<PersonMediaImageRel> GetImages(Expression<Func<PersonMediaImageRel, bool>> filter);
        void InsertOrUpdateImage(PersonMediaImageRel personMediaImage);
        void DeleteImage(int id);

        PersonMediaAudioVideoRel GetAudioVideo(int id);
        IQueryable<PersonMediaAudioVideoRel> GetAudioVideos(Expression<Func<PersonMediaAudioVideoRel, bool>> filter);
        void InsertOrUpdateAudioVideo(PersonMediaAudioVideoRel personMediaAudioVideo);
        void DeleteAudioVideo(int id);

        IQueryable<PersonEventRel> GetEvents(int? personId, int? eventId);
        IQueryable<PersonEventRel> GetEvents(Expression<Func<PersonEventRel, bool>> filter);
        PersonEventRel GetPersonEvent(int id);
        void InsertOrUpdateEvent(PersonEventRel personeventrel);
        void DeleteEvent(int id);

        PersonVehicleRel GetVehicle(int id);
        IQueryable<PersonVehicleRel> GetVehicles(int? personId, int? vehicleId);
        IQueryable<PersonVehicleRel> GetVehicles(Expression<Func<PersonVehicleRel, bool>> filter);
        void InsertOrUpdateVehicle(PersonVehicleRel personvehiclerel);
        void DeleteVehicle(int id);

        PersonPersonRel GetPersonPerson(int id);
        IQueryable<PersonPersonRel> GetPersonPersons(int personId);
        IQueryable<PersonPersonRel> GetPersonPersons(Expression<Func<PersonPersonRel, bool>> filter);
        void InsertOrUpdatePersonPerson(PersonPersonRel personpersonrel);
        void DeletePersonPerson(int id);

        PersonScreenName GetScreenName(int id);
        IQueryable<PersonScreenName> GetScreenNames(Expression<Func<PersonScreenName, bool>> filter);
        void InsertOrUpdateScreenName(PersonScreenName personscreenname);
        void DeleteScreenName(int id);

        PersonContactRel GetPersonContact(int id);
        IQueryable<PersonContactRel> GetContacts(int? personId, int? contactId);
        IQueryable<PersonContactRel> GetContacts(Expression<Func<PersonContactRel, bool>> filter);
        void InsertOrUpdateContact(PersonContactRel personcontactrel);
        void DeleteContact(int id);

        PersonMediaCorrespondenceRel GetCorrespondence(int id);
        IQueryable<PersonMediaCorrespondenceRel> GetCorrespondences(Expression<Func<PersonMediaCorrespondenceRel, bool>> filter);
        void InsertOrUpdateCorrespondence(PersonMediaCorrespondenceRel personmediacorrespondence);
        void DeleteCorrespondence(int id);

        PersonMediaPublishedRel GetPublished(int id);
        IQueryable<PersonMediaPublishedRel> GetPublisheds(Expression<Func<PersonMediaPublishedRel, bool>> filter);
        void InsertOrUpdatePublished(PersonMediaPublishedRel personmediapublished);
        void DeletePublished(int id);

        PersonMediaWebsiteEGroupRel GetWebsiteEGroup(int id);
        IQueryable<PersonMediaWebsiteEGroupRel> GetWebsiteEGroups(Expression<Func<PersonMediaWebsiteEGroupRel, bool>> filter);
        void InsertOrUpdateWebsiteEGroup(PersonMediaWebsiteEGroupRel personmediawebsiteegroup);
        void DeleteWebsiteEGroup(int id);
        
        IQueryable<CommonPerson> GetCommonPersons(Expression<Func<CommonPerson, bool>> filter);

    }
}
