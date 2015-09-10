using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using splc.domain.Models;

namespace splc.data.repository
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly ACDBContext _ctx;

        public OrganizationRepository(ACDBContext ctx)
        {
            _ctx = ctx;
        }

        #region Main

        public OrganizationMediaImageRel GetPrimaryImage(int organizationId)
        {
            return _ctx.OrganizationMediaImageRels.Where(x => x.MediaImage.DateDeleted == null && x.MediaImage.RemovalStatusId == null).FirstOrDefault(x => x.Organization.Id.Equals(organizationId));
        }

        public IQueryable<DropdownOrganization> GetDropdown(Expression<Func<DropdownOrganization, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.DropdownOrganizations.Where(filter);
            }
            return _ctx.DropdownOrganizations;
        }

        public Organization GetOrganization(int Id)
        {
            var rel = _ctx.Organizations.Find(Id);
            if (rel.DateDeleted == null && rel.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public Organization GetOrganization(User user, int Id)
        {
           
            var org = _ctx.Organizations.Find(Id);
            if (org.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel && org.DateDeleted == null && org.RemovalStatusId == null)
            {
                return org; 
            }
            return null; 
        }

        public IQueryable<Organization> GetOrganizations(Expression<Func<Organization, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.Organizations.Where(filter).Where(x => x.DateDeleted == null && x.RemovalStatusId == null);
            }
            return _ctx.Organizations.Where(x => x.DateDeleted == null && x.RemovalStatusId == null);
        }

        public IQueryable<Organization> GetOrganizations()
        {
            return _ctx.Organizations.Where(x => x.DateDeleted == null && x.RemovalStatusId == null);
        }

        public IQueryable<Organization> GetOrganizations(User user, Expression<Func<Organization, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.Organizations.Where(filter).Where(b => b.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel).Where(x => x.DateDeleted == null && x.RemovalStatusId == null);
            }
            return _ctx.Organizations.Where(x => x.DateDeleted == null && x.RemovalStatusId == null);
        }

        public IQueryable<Organization> GetOrganizations(User user)
        {
            return _ctx.Organizations.Where(o => o.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel).Where(x => x.DateDeleted == null && x.RemovalStatusId == null);
        }

        public void InsertOrUpdate(Organization organization)
        {
            if (organization.Id == default(int))
            {
                // New entity
                _ctx.Organizations.Add(organization);
            }
            else
            {
                // Existing entity
                _ctx.Entry(organization).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var organization = _ctx.Organizations.Find(id);
            _ctx.Organizations.Remove(organization);
        }

        public void Save()
        {
            _ctx.SaveChanges();
        }

        #endregion


        #region Person

        public IQueryable<OrganizationPersonRel> GetOrganizationPersons(Expression<Func<OrganizationPersonRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.OrganizationPersonRels.Where(filter).Where(x => x.Organization.DateDeleted == null && x.Organization.RemovalStatusId == null).Where(x => x.BeholderPerson.DateDeleted == null && x.BeholderPerson.RemovalStatusId == null);
            }
            return _ctx.OrganizationPersonRels.Where(x => x.Organization.DateDeleted == null && x.Organization.RemovalStatusId == null).Where(x => x.BeholderPerson.DateDeleted == null && x.BeholderPerson.RemovalStatusId == null);
        }

        public OrganizationPersonRel GetOrganziationPerson(int id)
        {
            var rel = _ctx.OrganizationPersonRels.Find(id);
            if (rel.Organization.DateDeleted == null && rel.BeholderPerson.DateDeleted == null && rel.Organization.RemovalStatusId == null && rel.BeholderPerson.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public void InsertOrUpdateOrganizationPerson(OrganizationPersonRel organizationpersonrel)
        {
            if (organizationpersonrel.Id == default(int))
            {
                // New entity
                _ctx.OrganizationPersonRels.Add(organizationpersonrel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(organizationpersonrel).State = EntityState.Modified;
            }
        }

        public void DeleteOrganizationPerson(int id)
        {
            var organizationpersonrel = _ctx.OrganizationPersonRels.Find(id);
            _ctx.OrganizationPersonRels.Remove(organizationpersonrel);
        }


        #endregion


        #region Event

        public OrganizationEventRel GetOrganizationEvent(int id)
        {
            var rel = _ctx.OrganizationEventRels.Find(id);
            if (rel.Organization.DateDeleted == null && rel.Event.DateDeleted == null && rel.Organization.RemovalStatusId == null && rel.Event.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<OrganizationEventRel> GetOrganizationEvents(Expression<Func<OrganizationEventRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.OrganizationEventRels.Where(filter).Where(x => x.Organization.DateDeleted == null && x.Organization.RemovalStatusId == null).Where(x => x.Event.DateDeleted == null && x.Event.RemovalStatusId == null);
            }
            return _ctx.OrganizationEventRels.Where(x => x.Organization.DateDeleted == null && x.Organization.RemovalStatusId == null).Where(x => x.Event.DateDeleted == null && x.Event.RemovalStatusId == null);
        }

        public void InsertOrUpdateOrganizationEvent(OrganizationEventRel organizationeventrel)
        {
            if (organizationeventrel.Id == default(int))
            {
                // New entity
                _ctx.OrganizationEventRels.Add(organizationeventrel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(organizationeventrel).State = EntityState.Modified;
            }
        }

        public void DeleteOrganizationEvent(int id)
        {
            var organizationEvent = _ctx.OrganizationEventRels.Find(id);
            _ctx.OrganizationEventRels.Remove(organizationEvent);
        }


        #endregion


        #region Vehicle

        public OrganizationVehicleRel GetOrganizationVehicle(int id)
        {
            var rel = _ctx.OrganizationVehicleRels.Find(id);
            if (rel.Organization.DateDeleted == null && rel.Vehicle.DateDeleted == null && rel.Organization.RemovalStatusId == null && rel.Vehicle.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<OrganizationVehicleRel> GetOrganizationVehicles(Expression<Func<OrganizationVehicleRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.OrganizationVehicleRels.Where(filter).Where(x => x.Organization.DateDeleted == null && x.Organization.RemovalStatusId == null).Where(x => x.Vehicle.DateDeleted == null && x.Vehicle.RemovalStatusId == null);
            }
            return _ctx.OrganizationVehicleRels.Where(x => x.Organization.DateDeleted == null && x.Organization.RemovalStatusId == null).Where(x => x.Vehicle.DateDeleted == null && x.Vehicle.RemovalStatusId == null);
        }

        public void InsertOrUpdateOrganizationVehicle(OrganizationVehicleRel organizationvehiclerel)
        {
            if (organizationvehiclerel.Id == default(int))
            {
                // New entity
                _ctx.OrganizationVehicleRels.Add(organizationvehiclerel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(organizationvehiclerel).State = EntityState.Modified;
            }
        }

        public void DeleteOrganizationVehicle(int id)
        {
            var organizationVehicle = _ctx.OrganizationVehicleRels.Find(id);
            _ctx.OrganizationVehicleRels.Remove(organizationVehicle);
        }

        #endregion


        #region MediaImage

        public OrganizationMediaImageRel GetOrganizationMediaImage(int id)
        {
            var rel = _ctx.OrganizationMediaImageRels.Find(id);
            if (rel.Organization.DateDeleted == null && rel.MediaImage.DateDeleted == null && rel.Organization.RemovalStatusId == null && rel.MediaImage.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<OrganizationMediaImageRel> GetOrganizationMediaImages(Expression<Func<OrganizationMediaImageRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.OrganizationMediaImageRels.Where(filter).Where(x => x.Organization.DateDeleted == null && x.Organization.RemovalStatusId == null).Where(x => x.MediaImage.DateDeleted == null && x.MediaImage.RemovalStatusId == null);
            }
            return _ctx.OrganizationMediaImageRels.Where(x => x.Organization.DateDeleted == null && x.Organization.RemovalStatusId == null).Where(x => x.MediaImage.DateDeleted == null && x.MediaImage.RemovalStatusId == null);
        }

        public void InsertOrUpdateOrganizationMediaImage(OrganizationMediaImageRel organizationMediaImageRel)
        {
            if (organizationMediaImageRel.Id == default(long))
            {
                // New entity
                //if (organizationmediaimagerel.Organization.Id == default(long))
                //{
                //    context.Organizations.Add(organizationmediaimagerel.Organization);
                //}
                //if (organizationmediaimagerel.MediaImage.Id == default(int))
                //{
                //    context.MediaImages.Add(organizationmediaimagerel.MediaImage);
                //}
                _ctx.OrganizationMediaImageRels.Add(organizationMediaImageRel);
            }
            else
            {
                // Existing entity
                //context.Entry(organizationmediaimagerel.MediaImage).State = EntityState.Modified;
                //context.Entry(organizationmediaimagerel.Organization).State = EntityState.Modified;
                _ctx.Entry(organizationMediaImageRel).State = EntityState.Modified;
            }
        }

        public void DeleteOrganizationMediaImage(int id)
        {
            var organizationMediaImage = _ctx.OrganizationMediaImageRels.Find(id);
            _ctx.OrganizationMediaImageRels.Remove(organizationMediaImage);
        }

        #endregion


        #region Organization

        public OrganizationOrganizationRel GetOrganizationOrganization(int id)
        {
            var rel = _ctx.OrganizationOrganizationRels.Find(id);
            if (rel.Organization.DateDeleted == null && rel.Organization2.DateDeleted == null && rel.Organization.RemovalStatusId == null && rel.Organization2.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public void InsertOrUpdateOrganizationOrganization(OrganizationOrganizationRel organizationOrganizationRel)
        {
            if (organizationOrganizationRel.Id == default(int))
            {
                // New entity
                _ctx.OrganizationOrganizationRels.Add(organizationOrganizationRel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(organizationOrganizationRel).State = EntityState.Modified;
            }
        }

        public void DeleteOrganizationOrganization(int id)
        {
            var organizationOrganization = _ctx.OrganizationOrganizationRels.Find(id);
            _ctx.OrganizationOrganizationRels.Remove(organizationOrganization);
        }

        #endregion


        #region Audio/Video

        public OrganizationMediaAudioVideoRel GetOrganizationMediaAudioVideo(int id)
        {
            var rel = _ctx.OrganizationMediaAudioVideoRels.Find(id);
            if (rel.Organization.DateDeleted == null && rel.MediaAudioVideo.DateDeleted == null && rel.Organization.RemovalStatusId == null && rel.MediaAudioVideo.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<OrganizationMediaAudioVideoRel> GetOrganizationMediaAudioVideos(Expression<Func<OrganizationMediaAudioVideoRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.OrganizationMediaAudioVideoRels.Where(filter).Where(x => x.Organization.DateDeleted == null && x.Organization.RemovalStatusId == null).Where(x => x.MediaAudioVideo.DateDeleted == null && x.MediaAudioVideo.RemovalStatusId == null);
            }
            return _ctx.OrganizationMediaAudioVideoRels;
        }

        public void InsertOrUpdateOrganizationMediaAudioVideo(OrganizationMediaAudioVideoRel organizationMediaAudioVideoRel)
        {
            if (organizationMediaAudioVideoRel.Id == default(int))
            {
                _ctx.OrganizationMediaAudioVideoRels.Add(organizationMediaAudioVideoRel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(organizationMediaAudioVideoRel).State = EntityState.Modified;
            }
        }

        public void DeleteOrganizationMediaAudioVideo(int id)
        {
            var organizationMediaAudioVideo = _ctx.OrganizationMediaAudioVideoRels.Find(id);
            _ctx.OrganizationMediaAudioVideoRels.Remove(organizationMediaAudioVideo);
        }

        #endregion


        #region MediaCorrespondences

        public OrganizationMediaCorrespondenceRel GetOrganizationMediaCorrespondence(int id)
        {
            var rel = _ctx.OrganizationMediaCorrespondenceRels.Find(id);
            if (rel.Organization.DateDeleted == null && rel.MediaCorrespondence.DateDeleted == null && rel.Organization.RemovalStatusId == null && rel.MediaCorrespondence.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<OrganizationMediaCorrespondenceRel> GetOrganizationMediaCorrespondences(Expression<Func<OrganizationMediaCorrespondenceRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.OrganizationMediaCorrespondenceRels.Where(filter).Where(x => x.Organization.DateDeleted == null && x.Organization.RemovalStatusId == null).Where(x => x.MediaCorrespondence.DateDeleted == null && x.MediaCorrespondence.RemovalStatusId == null);
            }
            return _ctx.OrganizationMediaCorrespondenceRels.Where(x => x.Organization.DateDeleted == null && x.Organization.RemovalStatusId == null).Where(x => x.MediaCorrespondence.DateDeleted == null && x.MediaCorrespondence.RemovalStatusId == null);
        }

        public void InsertOrUpdateOrganizationMediaCorrespondence(OrganizationMediaCorrespondenceRel organizationmediacorrespondencerel)
        {
            if (organizationmediacorrespondencerel.Id == default(int))
            {
                // New entity
                _ctx.OrganizationMediaCorrespondenceRels.Add(organizationmediacorrespondencerel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(organizationmediacorrespondencerel).State = EntityState.Modified;
            }
        }

        public void DeleteOrganizationMediaCorrespondence(int id)
        {
            var rel = _ctx.OrganizationMediaCorrespondenceRels.Find(id);
            _ctx.OrganizationMediaCorrespondenceRels.Remove(rel);
        }


        #endregion


        #region MediaPublisheds

        public OrganizationMediaPublishedRel GetOrganizationMediaPublished(int id)
        {
            var rel = _ctx.OrganizationMediaPublishedRels.Find(id);
            if (rel.Organization.DateDeleted == null && rel.MediaPublished.DateDeleted == null && rel.MediaPublished.RemovalStatusId == null && rel.Organization.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<OrganizationMediaPublishedRel> GetOrganizationMediaPublisheds(Expression<Func<OrganizationMediaPublishedRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.OrganizationMediaPublishedRels.Where(filter).Where(x => x.Organization.DateDeleted == null && x.Organization.RemovalStatusId == null).Where(x => x.MediaPublished.DateDeleted == null && x.MediaPublished.RemovalStatusId == null);
            }
            return _ctx.OrganizationMediaPublishedRels.Where(x => x.Organization.DateDeleted == null).Where(x => x.MediaPublished.DateDeleted == null);
        }

        public void InsertOrUpdateOrganizationMediaPublished(OrganizationMediaPublishedRel organizationmediapublishedrel)
        {
            if (organizationmediapublishedrel.Id == default(int))
            {
                // New entity
                _ctx.OrganizationMediaPublishedRels.Add(organizationmediapublishedrel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(organizationmediapublishedrel).State = EntityState.Modified;
            }
        }

        public void DeleteOrganizationMediaPublished(int id)
        {
            var rel = _ctx.OrganizationMediaPublishedRels.Find(id);
            _ctx.OrganizationMediaPublishedRels.Remove(rel);
        }


        #endregion


        #region MediaWebsiteEGroups

        public OrganizationMediaWebsiteEGroupRel GetOrganizationMediaWebsiteEGroup(int id)
        {
            var rel = _ctx.OrganizationMediaWebsiteEGroupRels.Find(id);
            if (rel.Organization.DateDeleted == null && rel.MediaWebsiteEGroup.DateDeleted == null && rel.Organization.RemovalStatusId == null && rel.MediaWebsiteEGroup.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<OrganizationMediaWebsiteEGroupRel> GetOrganizationMediaWebsiteEGroups(Expression<Func<OrganizationMediaWebsiteEGroupRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.OrganizationMediaWebsiteEGroupRels.Where(filter).Where(x => x.Organization.DateDeleted == null && x.Organization.RemovalStatusId == null).Where(x => x.MediaWebsiteEGroup.DateDeleted == null && x.MediaWebsiteEGroup.RemovalStatusId == null);
            }
            return _ctx.OrganizationMediaWebsiteEGroupRels.Where(x => x.Organization.DateDeleted == null && x.Organization.RemovalStatusId == null).Where(x => x.MediaWebsiteEGroup.DateDeleted == null && x.MediaWebsiteEGroup.RemovalStatusId == null);
        }

        public void InsertOrUpdateOrganizationMediaWebsiteEGroup(OrganizationMediaWebsiteEGroupRel organizationmediawebsiteegrouprel)
        {
            if (organizationmediawebsiteegrouprel.Id == default(int))
            {
                // New entity
                _ctx.OrganizationMediaWebsiteEGroupRels.Add(organizationmediawebsiteegrouprel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(organizationmediawebsiteegrouprel).State = EntityState.Modified;
            }
        }

        public void DeleteOrganizationMediaWebsiteEGroup(int id)
        {
            var rel = _ctx.OrganizationMediaWebsiteEGroupRels.Find(id);
            _ctx.OrganizationMediaWebsiteEGroupRels.Remove(rel);
        }


        #endregion


        #region Subscriptions

        public OrganizationSubscriptionRel GetOrganizationSubscription(int id)
        {
            var rel = _ctx.OrganizationSubscriptionRels.Find(id);
            if (rel.Organization.DateDeleted == null && rel.Subscription.DateDeleted == null && rel.Organization.RemovalStatusId == null && rel.Subscription.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<OrganizationSubscriptionRel> GetOrganizationSubscriptions(Expression<Func<OrganizationSubscriptionRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.OrganizationSubscriptionRels.Where(filter).Where(x => x.Organization.DateDeleted == null && x.Organization.RemovalStatusId == null).Where(x => x.Subscription.DateDeleted == null && x.Subscription.RemovalStatusId == null);
            }
            return _ctx.OrganizationSubscriptionRels.Where(x => x.Organization.DateDeleted == null && x.Organization.RemovalStatusId == null).Where(x => x.Subscription.DateDeleted == null && x.Subscription.RemovalStatusId == null);
        }

        public void InsertOrUpdateOrganizationSubscription(OrganizationSubscriptionRel organizationsubscriptionrel)
        {
            if (organizationsubscriptionrel.Id == default(int))
            {
                // New entity
                _ctx.OrganizationSubscriptionRels.Add(organizationsubscriptionrel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(organizationsubscriptionrel).State = EntityState.Modified;
            }
        }

        public void DeleteOrganizationSubscription(int id)
        {
            var rel = _ctx.OrganizationSubscriptionRels.Find(id);
            _ctx.OrganizationSubscriptionRels.Remove(rel);
        }


        #endregion


        #region StatusHistory

        public IQueryable<OrganizationStatusHistory> GetOrganizationStatusHistories(Expression<Func<OrganizationStatusHistory, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.OrganizationStatusHistories.Where(filter).Where(x => x.Organization.DateDeleted == null && x.Organization.RemovalStatusId == null).Where(x => x.DateDeleted == null);
            }
            return _ctx.OrganizationStatusHistories.Where(x => x.Organization.DateDeleted == null && x.Organization.RemovalStatusId == null).Where(x => x.DateDeleted == null);
        }

        public OrganizationStatusHistory GetOrganizationStatusHistory(int id)
        {
            var rel = _ctx.OrganizationStatusHistories.Find(id);
            if (rel.Organization.DateDeleted == null && rel.DateDeleted == null)
            {
                return rel;
            }
            return null;
        }

        #endregion


    }

    public interface IOrganizationRepository
    {
        //TODO: Needs refactoring to not need special class for dropdown
        IQueryable<DropdownOrganization> GetDropdown(Expression<Func<DropdownOrganization, bool>> filter);

        Organization GetOrganization(int Id);
        IQueryable<Organization> GetOrganizations();
        Organization GetOrganization(User user, int Id);
        IQueryable<Organization> GetOrganizations(User user);
        IQueryable<Organization> GetOrganizations(Expression<Func<Organization, bool>> filter);
        IQueryable<Organization> GetOrganizations(User user, Expression<Func<Organization, bool>> filter);
        void InsertOrUpdate(Organization organization);
        void Delete(int id);
        void Save();
        OrganizationMediaImageRel GetPrimaryImage(int organizationId);

        IQueryable<OrganizationPersonRel> GetOrganizationPersons(Expression<Func<OrganizationPersonRel, bool>> filter);
        
        OrganizationPersonRel GetOrganziationPerson(int id);
        void InsertOrUpdateOrganizationPerson(OrganizationPersonRel organizationpersonrel);
        void DeleteOrganizationPerson(int id);

        OrganizationEventRel GetOrganizationEvent(int id);
        IQueryable<OrganizationEventRel> GetOrganizationEvents(Expression<Func<OrganizationEventRel, bool>> filter);
        void InsertOrUpdateOrganizationEvent(OrganizationEventRel organizationeventrel);
        void DeleteOrganizationEvent(int id);

        OrganizationVehicleRel GetOrganizationVehicle(int id);
        IQueryable<OrganizationVehicleRel> GetOrganizationVehicles(Expression<Func<OrganizationVehicleRel, bool>> filter);
        void InsertOrUpdateOrganizationVehicle(OrganizationVehicleRel organizationvehiclerel);
        void DeleteOrganizationVehicle(int id);

        IQueryable<OrganizationMediaImageRel> GetOrganizationMediaImages(Expression<Func<OrganizationMediaImageRel, bool>> filter);
        OrganizationMediaImageRel GetOrganizationMediaImage(int id);
        void InsertOrUpdateOrganizationMediaImage(OrganizationMediaImageRel organizationMediaImageRel);
        void DeleteOrganizationMediaImage(int id);

        OrganizationOrganizationRel GetOrganizationOrganization(int id);
        void InsertOrUpdateOrganizationOrganization(OrganizationOrganizationRel organizationOrganizationRel);
        void DeleteOrganizationOrganization(int id);

        OrganizationMediaAudioVideoRel GetOrganizationMediaAudioVideo(int id);
        IQueryable<OrganizationMediaAudioVideoRel> GetOrganizationMediaAudioVideos(Expression<Func<OrganizationMediaAudioVideoRel, bool>> filter);
        void InsertOrUpdateOrganizationMediaAudioVideo(OrganizationMediaAudioVideoRel organizationMediaAudioVideoRel);
        void DeleteOrganizationMediaAudioVideo(int id);

        OrganizationMediaCorrespondenceRel GetOrganizationMediaCorrespondence(int id);
        IQueryable<OrganizationMediaCorrespondenceRel> GetOrganizationMediaCorrespondences(Expression<Func<OrganizationMediaCorrespondenceRel, bool>> filter);
        void InsertOrUpdateOrganizationMediaCorrespondence(OrganizationMediaCorrespondenceRel organizationmediacorrespondencerel);
        void DeleteOrganizationMediaCorrespondence(int id);

        OrganizationMediaPublishedRel GetOrganizationMediaPublished(int id);
        IQueryable<OrganizationMediaPublishedRel> GetOrganizationMediaPublisheds(Expression<Func<OrganizationMediaPublishedRel, bool>> filter);
        void InsertOrUpdateOrganizationMediaPublished(OrganizationMediaPublishedRel organizationmediapublishedrel);
        void DeleteOrganizationMediaPublished(int id);

        OrganizationMediaWebsiteEGroupRel GetOrganizationMediaWebsiteEGroup(int id);
        IQueryable<OrganizationMediaWebsiteEGroupRel> GetOrganizationMediaWebsiteEGroups(Expression<Func<OrganizationMediaWebsiteEGroupRel, bool>> filter);
        void InsertOrUpdateOrganizationMediaWebsiteEGroup(OrganizationMediaWebsiteEGroupRel organizationmediawebsiteegrouprel);
        void DeleteOrganizationMediaWebsiteEGroup(int id);

        OrganizationSubscriptionRel GetOrganizationSubscription(int id);
        IQueryable<OrganizationSubscriptionRel> GetOrganizationSubscriptions(Expression<Func<OrganizationSubscriptionRel, bool>> filter);
        void InsertOrUpdateOrganizationSubscription(OrganizationSubscriptionRel organizationsubscriptionrel);
        void DeleteOrganizationSubscription(int id);

        OrganizationStatusHistory GetOrganizationStatusHistory(int id);
        IQueryable<OrganizationStatusHistory> GetOrganizationStatusHistories(Expression<Func<OrganizationStatusHistory, bool>> filter);



    }

}
