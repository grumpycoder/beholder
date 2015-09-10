using splc.domain.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace splc.data.repository
{
    public class ChapterRepository : IChapterRepository
    {
        private readonly ACDBContext _ctx;

        public ChapterRepository(ACDBContext ctx)
        {
            _ctx = ctx;
        }

        #region Main

        public ChapterMediaImageRel GetPrimaryImage(int chapterId)
        {
            return _ctx.ChapterMediaImageRels.Where(x => x.MediaImage.DateDeleted == null && x.MediaImage.RemovalStatusId == null).FirstOrDefault(x => x.Chapter.Id.Equals(chapterId));
        }

        public IQueryable<DropdownChapter> GetDropdown(Expression<Func<DropdownChapter, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.DropdownChapters.Where(filter);
            }
            return _ctx.DropdownChapters;
        }

        public Chapter GetChapter(int id)
        {
            var ent = _ctx.Chapters.Find(id);
            if (ent.DateDeleted == null)
            {
                return ent;
            }
            return null;
        }

        public IQueryable<ChapterReport> GetChaptersReport()
        {
            return _ctx.ChapterReports;
        }

        public IQueryable<Chapter> GetChapters()
        {
            return _ctx.Chapters.Where(x => x.DateDeleted == null && x.RemovalStatusId == null);
        }

        public IQueryable<Chapter> GetChapters(Expression<Func<Chapter, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.Chapters.Where(filter).Where(x => x.DateDeleted == null && x.RemovalStatusId == null);
            }
            return _ctx.Chapters.Where(x => x.DateDeleted == null && x.RemovalStatusId == null);
        }

        public Chapter GetChapter(User user, int id)
        {
            //var chapter = _ctx.Chapters.
            //           Where(p => p.DateDeleted == null && p.Id == id && p.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel).
            //           Select(p => new
            //                     {
            //                         ParentItem = p,
            //                         ChildItems = p.ChapterContactInfoRels.Where(c=>c.DateDeleted == null)
            //                     }).FirstOrDefault<Chapter>();

            //var chapter = _ctx.Chapters.Select(.Where(x => x.Id == id && x.DateDeleted == null).Where(x=>x.ChapterContactInfoRels.Where(b=>b.DateDeleted == null));
            //if (chapter.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel)
            //{
            //    return chapter;
            //}
            var chapter = _ctx.Chapters.Find(id);
            if (chapter.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel && chapter.DateDeleted == null && chapter.RemovalStatusId == null)
            {
                return chapter;
            }
            return null;
        }

        public IQueryable<Chapter> GetChapters(User user)
        {
            return _ctx.Chapters.Where(b => b.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel && b.DateDeleted == null && b.RemovalStatusId == null);
        }

        public IQueryable<Chapter> GetChapters(User user, Expression<Func<Chapter, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.Chapters.Where(filter).Where(b => b.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel && b.DateDeleted == null && b.RemovalStatusId == null);
            }
            return _ctx.Chapters.Where(b => b.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel && b.DateDeleted == null && b.RemovalStatusId == null);
        }

        public void InsertOrUpdate(Chapter chapter)
        {
            if (chapter.Id == default(int))
            {
                // New entity
                _ctx.Chapters.Add(chapter);
            }
            else
            {
                // Existing entity
                _ctx.Entry(chapter).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var chapter = _ctx.Chapters.Find(id);
            _ctx.Chapters.Remove(chapter);
        }

        public void Save()
        {

            _ctx.SaveChanges();
        }

        #endregion


        #region Person

        public ChapterPersonRel GetChapterPerson(int id)
        {
            var ent = _ctx.ChapterPersonRels.Find(id);
            if (ent.Chapter.DateDeleted == null && ent.BeholderPerson.DateDeleted == null && ent.Chapter.RemovalStatusId == null && ent.BeholderPerson.RemovalStatusId == null)
            {
                return ent;
            }
            return null;
        }

        public IQueryable<ChapterPersonRel> GetChapterPersons(Expression<Func<ChapterPersonRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.ChapterPersonRels.Where(filter).Where(x => x.Chapter.DateDeleted == null && x.BeholderPerson.DateDeleted == null && x.Chapter.RemovalStatusId == null && x.BeholderPerson.RemovalStatusId == null);
            }
            return _ctx.ChapterPersonRels.Where(x => x.Chapter.DateDeleted == null && x.BeholderPerson.DateDeleted == null && x.Chapter.RemovalStatusId == null && x.BeholderPerson.RemovalStatusId == null);
        }

        public void InsertOrUpdateChapterPerson(ChapterPersonRel chapterpersonrel)
        {
            if (chapterpersonrel.Id == default(long))
            {
                // New entity
                _ctx.ChapterPersonRels.Add(chapterpersonrel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(chapterpersonrel).State = EntityState.Modified;
            }
        }

        public void DeleteChapterPerson(int id)
        {
            var chapterPerson = _ctx.ChapterPersonRels.Find(id);
            _ctx.ChapterPersonRels.Remove(chapterPerson);
        }

        #endregion


        #region Contact

        public ChapterContactRel GetChapterContact(int id)
        {
            var ent = _ctx.ChapterContactRels.Find(id);
            if (ent.Chapter.DateDeleted == null && ent.Contact.DateDeleted == null && ent.Chapter.RemovalStatusId == null && ent.Contact.RemovalStatusId == null)
            {
                return ent;
            }
            return null;
        }

        public IQueryable<ChapterContactRel> GetChapterContacts(Expression<Func<ChapterContactRel, bool>> filter)
        {
            if (filter != null)
            {
                var list = _ctx.ChapterContactRels.Where(filter).Where(x => x.Chapter.DateDeleted == null && x.Chapter.RemovalStatusId == null);
                return list;
                //return _ctx.ChapterContactRels.Where(filter).Where(x => x.Chapter.DateDeleted == null && x.Contact.DateDeleted == null);
            }
            //return _ctx.ChapterContactRels.Where(x => x.Chapter.DateDeleted == null && x.Contact.DateDeleted == null);
            var list2 = _ctx.ChapterContactRels.Where(x => x.Chapter.DateDeleted == null && x.Chapter.RemovalStatusId == null).Where(x => x.Contact.DateDeleted == null && x.Contact.RemovalStatusId == null);
            return list2;
        }

        public void InsertOrUpdateChapterContact(ChapterContactRel chapterContactRel)
        {
            if (chapterContactRel.Id == default(int))
            {
                // New entity
                _ctx.ChapterContactRels.Add(chapterContactRel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(chapterContactRel).State = EntityState.Modified;
            }
        }

        public void DeleteChapterContact(int id)
        {
            var chapterContact = _ctx.ChapterContactRels.Find(id);
            _ctx.ChapterContactRels.Remove(chapterContact);
        }


        #endregion


        #region Organization

        public ChapterOrganizationRel GetChaperOrganization(int id)
        {
            var ent = _ctx.ChapterOrganizationRels.Find(id);
            if (ent.Chapter.DateDeleted == null && ent.Organization.DateDeleted == null && ent.Chapter.RemovalStatusId == null && ent.Organization.RemovalStatusId == null)
            {
                return ent;
            }
            return null;
        }

        public IQueryable<ChapterOrganizationRel> GetChapterOrganizations(Expression<Func<ChapterOrganizationRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.ChapterOrganizationRels.Where(filter).Where(x => x.Chapter.DateDeleted == null && x.Organization.DateDeleted == null && x.Chapter.RemovalStatusId == null && x.Organization.RemovalStatusId == null);
            }
            return _ctx.ChapterOrganizationRels.Where(x => x.Chapter.DateDeleted == null && x.Organization.DateDeleted == null && x.Chapter.RemovalStatusId == null && x.Organization.RemovalStatusId == null);
        }

        public void InsertOrUpdateChapterOrganization(ChapterOrganizationRel chapterorganizationrel)
        {
            if (chapterorganizationrel.Id == default(int))
            {
                // New entity
                _ctx.ChapterOrganizationRels.Add(chapterorganizationrel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(chapterorganizationrel).State = EntityState.Modified;
            }
        }

        public void DeleteChapterOrganization(int id)
        {
            var chapterOrganization = _ctx.ChapterOrganizationRels.Find(id);
            _ctx.ChapterOrganizationRels.Remove(chapterOrganization);
        }

        #endregion


        #region Event

        public ChapterEventRel GetChapterEvent(int id)
        {
            var ent = _ctx.ChapterEventRels.Find(id);
            if (ent.Chapter.DateDeleted == null && ent.Event.DateDeleted == null && ent.Chapter.RemovalStatusId == null && ent.Event.RemovalStatusId == null)
            {
                return ent;
            }
            return null;
        }

        public IQueryable<ChapterEventRel> GetChapterEvents(Expression<Func<ChapterEventRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.ChapterEventRels.Where(filter).Where(x => x.Chapter.DateDeleted == null && x.Event.DateDeleted == null && x.Chapter.RemovalStatusId == null && x.Event.RemovalStatusId == null);
            }
            return _ctx.ChapterEventRels.Where(x => x.Chapter.DateDeleted == null && x.Event.DateDeleted == null && x.Chapter.RemovalStatusId == null && x.Event.RemovalStatusId == null);
        }

        public void InsertOrUpdateChapterEvent(ChapterEventRel chapterEventRel)
        {
            if (chapterEventRel.Id == default(int))
            {
                // New entity
                _ctx.ChapterEventRels.Add(chapterEventRel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(chapterEventRel).State = EntityState.Modified;
            }
        }

        public void DeleteChapterEvent(int id)
        {
            var chapterEvent = _ctx.ChapterEventRels.Find(id);
            _ctx.ChapterEventRels.Remove(chapterEvent);
        }


        #endregion


        #region MediaImage

        public ChapterMediaImageRel GetMediaImage(int id)
        {
            var ent = _ctx.ChapterMediaImageRels.Find(id);
            if (ent.Chapter.DateDeleted == null && ent.MediaImage.DateDeleted == null && ent.Chapter.RemovalStatusId == null && ent.MediaImage.RemovalStatusId == null)
            {
                return ent;
            }
            return null;
        }

        public ChapterMediaImageRel GetChapterMediaImage(int id)
        {
            return _ctx.ChapterMediaImageRels.Find(id);
        }

        public IQueryable<ChapterMediaImageRel> GetMediaImages(Expression<Func<ChapterMediaImageRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.ChapterMediaImageRels.Where(filter).Where(x => x.Chapter.DateDeleted == null && x.MediaImage.DateDeleted == null && x.Chapter.RemovalStatusId == null && x.MediaImage.RemovalStatusId == null);
            }
            return _ctx.ChapterMediaImageRels.Where(x => x.Chapter.DateDeleted == null && x.MediaImage.DateDeleted == null && x.Chapter.RemovalStatusId == null && x.MediaImage.RemovalStatusId == null);
        }

        public void InsertOrUpdateMediaImage(ChapterMediaImageRel chapterMediaImageRel)
        {
            if (chapterMediaImageRel.Id == default(int))
            {
                // New entity
                //if (organizationmediaimagerel.Organization.Id == default(long))
                //{
                //    context.Organizations.Add(organizationmediaimagerel.Organization);
                //}
                //if (chaptermediaimagerel.MediaImage.Id == default(int))
                //{
                //    context.MediaImages.Add(chaptermediaimagerel.MediaImage);
                //}
                _ctx.ChapterMediaImageRels.Add(chapterMediaImageRel);
            }
            else
            {
                // Existing entity
                //context.Entry(chaptermediaimagerel.MediaImage).State = EntityState.Modified;
                //context.Entry(chaptermediaimagerel.Chapter).State = EntityState.Modified;
                _ctx.Entry(chapterMediaImageRel).State = EntityState.Modified;
            }
        }

        public void DeleteChapterMedia(int id)
        {
            var chapterMedia = _ctx.ChapterMediaImageRels.Find(id);
            _ctx.ChapterMediaImageRels.Remove(chapterMedia);
        }

        #endregion


        #region Media Audio/Video

        public ChapterMediaAudioVideoRel GetChapterMediaAudioVideo(int id)
        {
            var ent = _ctx.ChapterMediaAudioVideoRels.Find(id);
            if (ent.Chapter.DateDeleted == null && ent.MediaAudioVideo.DateDeleted == null && ent.Chapter.RemovalStatusId == null && ent.MediaAudioVideo.RemovalStatusId == null)
            {
                return ent;
            }
            return null;
        }

        public IQueryable<ChapterMediaAudioVideoRel> GetChapterMediaAudioVideos(Expression<Func<ChapterMediaAudioVideoRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.ChapterMediaAudioVideoRels.Where(filter).Where(x => x.Chapter.DateDeleted == null && x.MediaAudioVideo.DateDeleted == null && x.Chapter.RemovalStatusId == null && x.MediaAudioVideo.RemovalStatusId == null);
            }
            return _ctx.ChapterMediaAudioVideoRels.Where(x => x.Chapter.DateDeleted == null && x.MediaAudioVideo.DateDeleted == null && x.Chapter.RemovalStatusId == null && x.MediaAudioVideo.RemovalStatusId == null);
        }

        public void InsertOrUpdateChapterMediaAudioVideo(ChapterMediaAudioVideoRel chapterMediaAudioVideoRel)
        {
            if (chapterMediaAudioVideoRel.Id == default(int))
            {
                // New entity
                //if (organizationmediaimagerel.Organization.Id == default(long))
                //{
                //    context.Organizations.Add(organizationmediaimagerel.Organization);
                //}
                //if (chaptermediaimagerel.MediaImage.Id == default(int))
                //{
                //    context.MediaImages.Add(chaptermediaimagerel.MediaImage);
                //}
                _ctx.ChapterMediaAudioVideoRels.Add(chapterMediaAudioVideoRel);
            }
            else
            {
                // Existing entity
                //context.Entry(chaptermediaimagerel.MediaImage).State = EntityState.Modified;
                //context.Entry(chaptermediaimagerel.Chapter).State = EntityState.Modified;
                _ctx.Entry(chapterMediaAudioVideoRel).State = EntityState.Modified;
            }
        }

        public void DeleteChapterMediaAudioVideo(int id)
        {
            var chapterMediaAudioVideo = _ctx.ChapterMediaAudioVideoRels.Find(id);
            _ctx.ChapterMediaAudioVideoRels.Remove(chapterMediaAudioVideo);
        }

        #endregion


        #region Chapter

        public ChapterChapterRel GetChapterChapter(int chapterId)
        {
            var ent = _ctx.ChapterChapterRels.Find(chapterId);
            if (ent.Chapter.DateDeleted == null && ent.Chapter2.DateDeleted == null && ent.Chapter.RemovalStatusId == null && ent.Chapter2.RemovalStatusId == null)
            {
                return ent;
            }
            return null;
        }

        public IQueryable<ChapterChapterRel> GetChapterChapters(Expression<Func<ChapterChapterRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.ChapterChapterRels.Where(filter).Where(x => x.Chapter.DateDeleted == null && x.Chapter2.DateDeleted == null && x.Chapter.RemovalStatusId == null && x.Chapter2.RemovalStatusId == null);
            }
            return _ctx.ChapterChapterRels.Where(x => x.Chapter.DateDeleted == null && x.Chapter2.DateDeleted == null && x.Chapter.RemovalStatusId == null && x.Chapter2.RemovalStatusId == null);
        }

        public void InsertOrUpdateChapterChapter(ChapterChapterRel chapterChapterRel)
        {
            if (chapterChapterRel.Id == default(int))
            {
                // New entity
                _ctx.ChapterChapterRels.Add(chapterChapterRel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(chapterChapterRel).State = EntityState.Modified;
            }
        }

        public void DeleteChapterChapter(int id)
        {
            var chapterChapter = _ctx.ChapterChapterRels.Find(id);
            _ctx.ChapterChapterRels.Remove(chapterChapter);
        }

        #endregion


        #region Vehicle

        public ChapterVehicleRel GetChapterVehicle(int id)
        {
            var ent = _ctx.ChapterVehicleRels.Find(id);
            if (ent.Chapter.DateDeleted == null && ent.Vehicle.DateDeleted == null && ent.Chapter.RemovalStatusId == null && ent.Vehicle.RemovalStatusId == null)
            {
                return ent;
            }
            return null;
        }

        public IQueryable<ChapterVehicleRel> GetChapterVehicles(Expression<Func<ChapterVehicleRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.ChapterVehicleRels.Where(filter).Where(x => x.Chapter.DateDeleted == null && x.Vehicle.DateDeleted == null && x.Chapter.RemovalStatusId == null && x.Vehicle.RemovalStatusId == null);
            }
            return _ctx.ChapterVehicleRels.Where(x => x.Chapter.DateDeleted == null && x.Vehicle.DateDeleted == null && x.Chapter.RemovalStatusId == null && x.Vehicle.RemovalStatusId == null);
        }

        public void InsertOrUpdateChapterVehicle(ChapterVehicleRel chapterVehicle)
        {
            if (chapterVehicle.Id == default(int))
            {
                // New entity
                _ctx.ChapterVehicleRels.Add(chapterVehicle);
            }
            else
            {
                // Existing entity
                _ctx.Entry(chapterVehicle).State = EntityState.Modified;
            }
        }

        public void DeleteChapterVehicle(int id)
        {
            var chapterVehicle = _ctx.ChapterVehicleRels.Find(id);
            _ctx.ChapterVehicleRels.Remove(chapterVehicle);
        }

        #endregion


        #region Contact Info

        public ChapterContactInfoRel GetContactInfo(int id)
        {
            var ent = _ctx.ChapterContactInfoRels.Find(id);
            if (ent.Chapter.DateDeleted == null && ent.ContactInfo.DateDeleted == null && ent.Chapter.RemovalStatusId == null)
            {
                return ent;
            }
            return null;
        }

        public IQueryable<ChapterContactInfoRel> GetContactInfo(Expression<Func<ChapterContactInfoRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.ChapterContactInfoRels.Where(filter).Where(b => b.DateDeleted == null);
            }
            return _ctx.ChapterContactInfoRels.Where(b => b.DateDeleted == null);
        }

        public void InsertOrUpdateContactInfo(ChapterContactInfoRel contactInfo)
        {
            if (contactInfo.Id == default(int))
            {
                // New entity
                _ctx.ContactInfo.Add(contactInfo.ContactInfo);
                _ctx.ChapterContactInfoRels.Add(contactInfo);
            }
            else
            {
                // Existing entity
                _ctx.Entry(contactInfo).State = EntityState.Modified;
                _ctx.Entry(contactInfo.ContactInfo).State = EntityState.Modified;
            }
        }

        public void DeleteContactInfo(int id)
        {
            var contactInfo = _ctx.ChapterContactInfoRels.Find(id);
            _ctx.ChapterContactInfoRels.Remove(contactInfo);
        }

        #endregion


        #region MediaCorrespondence

        public ChapterMediaCorrespondenceRel GetChapterMediaCorrespondence(int id)
        {
            var ent = _ctx.ChapterMediaCorrespondenceRels.Find(id);
            if (ent.Chapter.DateDeleted == null && ent.MediaCorrespondence.DateDeleted == null && ent.Chapter.RemovalStatusId == null && ent.MediaCorrespondence.RemovalStatusId == null)
            {
                return ent;
            }
            return null;
        }

        public IQueryable<ChapterMediaCorrespondenceRel> GetChapterMediaCorrespondences(Expression<Func<ChapterMediaCorrespondenceRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.ChapterMediaCorrespondenceRels.Where(filter).Where(x => x.Chapter.DateDeleted == null && x.MediaCorrespondence.DateDeleted == null && x.Chapter.RemovalStatusId == null && x.MediaCorrespondence.RemovalStatusId == null);
            }
            return _ctx.ChapterMediaCorrespondenceRels.Where(x => x.Chapter.DateDeleted == null && x.MediaCorrespondence.DateDeleted == null && x.Chapter.RemovalStatusId == null && x.MediaCorrespondence.RemovalStatusId == null);
        }

        public void InsertOrUpdateChapterMediaCorrespondence(ChapterMediaCorrespondenceRel chaptermediacorrespondencerel)
        {
            if (chaptermediacorrespondencerel.Id == default(int))
            {
                // New entity
                _ctx.ChapterMediaCorrespondenceRels.Add(chaptermediacorrespondencerel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(chaptermediacorrespondencerel).State = EntityState.Modified;
            }
        }

        public void DeleteChapterMediaCorrespondence(int id)
        {
            var rel = _ctx.ChapterMediaCorrespondenceRels.Find(id);
            _ctx.ChapterMediaCorrespondenceRels.Remove(rel);
        }

        #endregion


        #region MediaPublished

        public ChapterMediaPublishedRel GetChapterMediaPublished(int id)
        {
            var ent = _ctx.ChapterMediaPublishedRels.Find(id);
            if (ent.Chapter.DateDeleted == null && ent.MediaPublished.DateDeleted == null && ent.Chapter.RemovalStatusId == null && ent.MediaPublished.RemovalStatusId == null)
            {
                return ent;
            }
            return null;
        }

        public IQueryable<ChapterMediaPublishedRel> GetChapterMediaPublisheds(Expression<Func<ChapterMediaPublishedRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.ChapterMediaPublishedRels.Where(filter).Where(x => x.Chapter.DateDeleted == null && x.MediaPublished.DateDeleted == null && x.Chapter.RemovalStatusId == null && x.MediaPublished.RemovalStatusId == null);
            }
            return _ctx.ChapterMediaPublishedRels.Where(x => x.Chapter.DateDeleted == null && x.MediaPublished.DateDeleted == null && x.Chapter.RemovalStatusId == null && x.MediaPublished.RemovalStatusId == null);
        }

        public void InsertOrUpdateChapterMediaPublished(ChapterMediaPublishedRel chaptermediapublishedrel)
        {
            if (chaptermediapublishedrel.Id == default(int))
            {
                // New entity
                _ctx.ChapterMediaPublishedRels.Add(chaptermediapublishedrel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(chaptermediapublishedrel).State = EntityState.Modified;
            }
        }

        public void DeleteChapterMediaPublished(int id)
        {
            var rel = _ctx.ChapterMediaPublishedRels.Find(id);
            _ctx.ChapterMediaPublishedRels.Remove(rel);
        }


        #endregion


        #region Subscriptions

        public ChapterSubscriptionRel GetChapterSubscription(int id)
        {
            var ent = _ctx.ChapterSubscriptionRels.Find(id);
            if (ent.Chapter.DateDeleted == null && ent.Subscription.DateDeleted == null && ent.Chapter.RemovalStatusId == null && ent.Subscription.RemovalStatusId == null)
            {
                return ent;
            }
            return null;
        }

        public IQueryable<ChapterSubscriptionRel> GetChapterSubscriptions(Expression<Func<ChapterSubscriptionRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.ChapterSubscriptionRels.Where(filter).Where(x => x.Chapter.DateDeleted == null && x.Subscription.DateDeleted == null && x.Chapter.RemovalStatusId == null && x.Subscription.RemovalStatusId == null);
            }
            return _ctx.ChapterSubscriptionRels.Where(x => x.Chapter.DateDeleted == null && x.Subscription.DateDeleted == null && x.Chapter.RemovalStatusId == null && x.Subscription.RemovalStatusId == null);
        }

        public void InsertOrUpdateChapterSubscription(ChapterSubscriptionRel chaptersubscriptionrel)
        {
            if (chaptersubscriptionrel.Id == default(int))
            {
                // New entity
                _ctx.ChapterSubscriptionRels.Add(chaptersubscriptionrel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(chaptersubscriptionrel).State = EntityState.Modified;
            }
        }

        public void DeleteChapterSubscription(int id)
        {
            var rel = _ctx.ChapterSubscriptionRels.Find(id);
            _ctx.ChapterSubscriptionRels.Remove(rel);
        }


        #endregion


        #region MediaWebsiteEGroup

        public ChapterMediaWebsiteEGroupRel GetChapterMediaWebsiteEGroup(int id)
        {
            var ent = _ctx.ChapterMediaWebsiteEGroupRels.Find(id);
            if (ent.Chapter.DateDeleted == null && ent.MediaWebsiteEGroup.DateDeleted == null && ent.Chapter.RemovalStatusId == null && ent.MediaWebsiteEGroup.RemovalStatusId == null)
            {
                return ent;
            }
            return null;
        }

        public IQueryable<ChapterMediaWebsiteEGroupRel> GetChapterMediaWebsiteEGroups(Expression<Func<ChapterMediaWebsiteEGroupRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.ChapterMediaWebsiteEGroupRels.Where(filter).Where(x => x.Chapter.DateDeleted == null && x.MediaWebsiteEGroup.DateDeleted == null && x.Chapter.RemovalStatusId == null && x.MediaWebsiteEGroup.RemovalStatusId == null);
            }
            return _ctx.ChapterMediaWebsiteEGroupRels.Where(x => x.Chapter.DateDeleted == null && x.MediaWebsiteEGroup.DateDeleted == null && x.Chapter.RemovalStatusId == null && x.MediaWebsiteEGroup.RemovalStatusId == null);
        }

        public void InsertOrUpdateChapterMediaWebsiteEGroup(ChapterMediaWebsiteEGroupRel chaptermediapublishedrel)
        {
            if (chaptermediapublishedrel.Id == default(int))
            {
                // New entity
                _ctx.ChapterMediaWebsiteEGroupRels.Add(chaptermediapublishedrel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(chaptermediapublishedrel).State = EntityState.Modified;
            }
        }

        public void DeleteChapterMediaWebsiteEGroup(int id)
        {
            var rel = _ctx.ChapterMediaWebsiteEGroupRels.Find(id);
            _ctx.ChapterMediaWebsiteEGroupRels.Remove(rel);
        }


        #endregion


        #region StatusHistory

        public IQueryable<ChapterStatusHistory> GetChapterStatusHistories(Expression<Func<ChapterStatusHistory, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.ChapterStatusHistories.Where(filter).Where(x => x.Chapter.DateDeleted == null && x.DateDeleted == null && x.Chapter.RemovalStatusId == null);
            }
            return _ctx.ChapterStatusHistories.Where(x => x.Chapter.DateDeleted == null && x.DateDeleted == null && x.Chapter.RemovalStatusId == null);
        }

        public ChapterStatusHistory GetChapterStatusHistory(int id)
        {
            return _ctx.ChapterStatusHistories.Find(id);
        }

        #endregion


    }

    public interface IChapterRepository
    {
        IQueryable<ChapterReport> GetChaptersReport();

        IQueryable<DropdownChapter> GetDropdown(Expression<Func<DropdownChapter, bool>> filter);
        Chapter GetChapter(int id);
        IQueryable<Chapter> GetChapters();
        IQueryable<Chapter> GetChapters(Expression<Func<Chapter, bool>> filter);

        Chapter GetChapter(User user, int id);
        IQueryable<Chapter> GetChapters(User user);
        IQueryable<Chapter> GetChapters(User user, Expression<Func<Chapter, bool>> filter);
        void InsertOrUpdate(Chapter chapter);
        void Delete(int id);
        void Save();
        ChapterMediaImageRel GetPrimaryImage(int chapterId);

        ChapterPersonRel GetChapterPerson(int id);
        IQueryable<ChapterPersonRel> GetChapterPersons(Expression<Func<ChapterPersonRel, bool>> filter);
        void InsertOrUpdateChapterPerson(ChapterPersonRel chapterpersonrel);
        void DeleteChapterPerson(int id);

        ChapterOrganizationRel GetChaperOrganization(int id);
        IQueryable<ChapterOrganizationRel> GetChapterOrganizations(Expression<Func<ChapterOrganizationRel, bool>> filter);
        void InsertOrUpdateChapterOrganization(ChapterOrganizationRel chapterorganizationrel);
        void DeleteChapterOrganization(int id);

        ChapterEventRel GetChapterEvent(int id);
        IQueryable<ChapterEventRel> GetChapterEvents(Expression<Func<ChapterEventRel, bool>> filter);
        void InsertOrUpdateChapterEvent(ChapterEventRel chapterEventRel);
        void DeleteChapterEvent(int id);

        ChapterContactRel GetChapterContact(int id);
        IQueryable<ChapterContactRel> GetChapterContacts(Expression<Func<ChapterContactRel, bool>> filter);
        void InsertOrUpdateChapterContact(ChapterContactRel chapterContactRel);
        void DeleteChapterContact(int id);

        ChapterMediaImageRel GetChapterMediaImage(int id);
        ChapterMediaImageRel GetMediaImage(int id);
        IQueryable<ChapterMediaImageRel> GetMediaImages(Expression<Func<ChapterMediaImageRel, bool>> filter);
        void InsertOrUpdateMediaImage(ChapterMediaImageRel chapterMediaImageRel);
        void DeleteChapterMedia(int id);

        ChapterMediaAudioVideoRel GetChapterMediaAudioVideo(int id);
        IQueryable<ChapterMediaAudioVideoRel> GetChapterMediaAudioVideos(Expression<Func<ChapterMediaAudioVideoRel, bool>> filter);
        void InsertOrUpdateChapterMediaAudioVideo(ChapterMediaAudioVideoRel chapterMediaAudioVideoRel);
        void DeleteChapterMediaAudioVideo(int id);


        ChapterChapterRel GetChapterChapter(int chapterId);
        IQueryable<ChapterChapterRel> GetChapterChapters(Expression<Func<ChapterChapterRel, bool>> filter);
        void InsertOrUpdateChapterChapter(ChapterChapterRel chapterChapterRel);
        void DeleteChapterChapter(int id);


        ChapterVehicleRel GetChapterVehicle(int id);
        IQueryable<ChapterVehicleRel> GetChapterVehicles(Expression<Func<ChapterVehicleRel, bool>> filter);
        void InsertOrUpdateChapterVehicle(ChapterVehicleRel chapterVehicle);
        void DeleteChapterVehicle(int id);


        ChapterMediaCorrespondenceRel GetChapterMediaCorrespondence(int id);
        IQueryable<ChapterMediaCorrespondenceRel> GetChapterMediaCorrespondences(Expression<Func<ChapterMediaCorrespondenceRel, bool>> filter);
        void InsertOrUpdateChapterMediaCorrespondence(ChapterMediaCorrespondenceRel chaptermediacorrespondence);
        void DeleteChapterMediaCorrespondence(int id);

        ChapterMediaPublishedRel GetChapterMediaPublished(int id);
        IQueryable<ChapterMediaPublishedRel> GetChapterMediaPublisheds(Expression<Func<ChapterMediaPublishedRel, bool>> filter);
        void InsertOrUpdateChapterMediaPublished(ChapterMediaPublishedRel chaptermediapublished);
        void DeleteChapterMediaPublished(int id);

        ChapterSubscriptionRel GetChapterSubscription(int id);
        IQueryable<ChapterSubscriptionRel> GetChapterSubscriptions(Expression<Func<ChapterSubscriptionRel, bool>> filter);
        void InsertOrUpdateChapterSubscription(ChapterSubscriptionRel chaptersubscription);
        void DeleteChapterSubscription(int id);


        ChapterContactInfoRel GetContactInfo(int id);
        IQueryable<ChapterContactInfoRel> GetContactInfo(Expression<Func<ChapterContactInfoRel, bool>> filter);
        void InsertOrUpdateContactInfo(ChapterContactInfoRel contactInfo);
        void DeleteContactInfo(int id);

        ChapterMediaWebsiteEGroupRel GetChapterMediaWebsiteEGroup(int id);
        IQueryable<ChapterMediaWebsiteEGroupRel> GetChapterMediaWebsiteEGroups(Expression<Func<ChapterMediaWebsiteEGroupRel, bool>> filter);
        void InsertOrUpdateChapterMediaWebsiteEGroup(ChapterMediaWebsiteEGroupRel chaptermediapublished);
        void DeleteChapterMediaWebsiteEGroup(int id);

        ChapterStatusHistory GetChapterStatusHistory(int id);
        IQueryable<ChapterStatusHistory> GetChapterStatusHistories(Expression<Func<ChapterStatusHistory, bool>> filter);

    }
}


//   public class ChapterRepository : IChapterRepository
//    {
//        readonly ACDBContext _context = new ACDBContext();

//        public IQueryable<Chapter> All
//        {
//            get { return _context.Chapters; }
//        }

//        public IQueryable<Chapter> AllIncluding(params Expression<Func<Chapter, object>>[] includeProperties)
//        {
//            IQueryable<Chapter> query = _context.Chapters;
//            foreach (var includeProperty in includeProperties) {
//                query = query.Include(includeProperty);
//            }
//            return query;
//        }

//        public IQueryable<DropdownChapter> GetDropdown(Expression<Func<DropdownChapter, bool>> filter)
//        {
//            if (filter != null)
//            {
//                return _context.DropdownChapters.Where(filter);
//            }
//            return _context.DropdownChapters;
//        }

//        public IQueryable<Chapter> Get(Expression<Func<Chapter, bool>> filter)
//        {
//            if (filter != null)
//            {
//                return _context.Chapters.Where(filter);
//            }
//            return _context.Chapters;
//        }

//        public Chapter Find(int id)
//        {
//            return _context.Chapters.Find(id);
//        }

//        public void InsertOrUpdate(Chapter chapter)
//        {
//            if (chapter.Id == default(int)) {
//                // New entity
//                _context.Chapters.Add(chapter);
//            } else {
//                // Existing entity
//                _context.Entry(chapter).State = EntityState.Modified;
//            }
//        }

//        public void Delete(int id)
//        {
//            var chapter = _context.Chapters.Find(id);
//            _context.Chapters.Remove(chapter);
//        }

//        public void Save()
//        {
//            _context.SaveChanges();
//        }

//        public void Dispose() 
//        {
//            _context.Dispose();
//        }
//    }

//    public interface IChapterRepository : IDisposable
//    {
//        IQueryable<Chapter> All { get; }
//        IQueryable<Chapter> AllIncluding(params Expression<Func<Chapter, object>>[] includeProperties);
//        IQueryable<Chapter> Get(Expression<Func<Chapter, bool>> filter);
//        Chapter Find(int id);
//        IQueryable<DropdownChapter> GetDropdown(Expression<Func<DropdownChapter, bool>> filter);
//        void InsertOrUpdate(Chapter chapter);
//        void Delete(int id);
//        void Save();
//    }
//}