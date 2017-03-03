using splc.domain.Models;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace splc.data.repository
{

    public class LookupRepository : ILookupRepository
    {
        readonly ACDBContext _ctx;

        public LookupRepository(ACDBContext ctx)
        {
            _ctx = ctx;
        }

        public virtual List<EventType> GetEventTypes()
        {
            return _ctx.EventTypes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name).ToList();
        }

        public virtual List<AudioVideoType> GetAudioVideoTypes()
        {
            return _ctx.AudioVideoTypes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name).ToList();
        }

        public virtual List<Prefix> GetPrefixes()
        {
            return _ctx.Prefixes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name).ToList();
        }

        public virtual List<Suffix> GetSuffixes()
        {
            return _ctx.Suffixes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name).ToList();
        }

        public virtual List<Gender> GetGenders()
        {
            return _ctx.Genders.OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name).ToList();
        }

        public virtual List<Race> GetRaces()
        {
            return _ctx.Races.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name).ToList();
        }

        public virtual List<LicenseType> GetLicenseTypes()
        {
            return _ctx.LicenseTypes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name).ToList();
        }

        public virtual List<EyeColor> GetEyeColors()
        {
            return _ctx.EyeColors.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name).ToList();
        }

        public virtual List<HairColor> GetHairColors()
        {
            return _ctx.HairColors.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name).ToList();
        }

        public virtual List<HairPattern> GetHairPatterns()
        {
            return _ctx.HairPatterns.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name).ToList();
        }

        public virtual List<State> GetStates()
        {
            return _ctx.States.OrderBy(x => x.StateCode).ToList();
        }

        public virtual List<MaritialStatus> GetMaritialStatuses()
        {
            return _ctx.MaritialStatus.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name).ToList();
        }

        public virtual List<AddressType> GetAddressTypes()
        {
            return _ctx.AddressTypes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name).ToList();
        }

        public virtual List<ApprovalStatus> GetApprovalStatuses()
        {
            return _ctx.ApprovalStatus.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name).ToList();
        }

        public virtual List<ActiveStatus> GetActiveStatuses()
        {
            return _ctx.ActiveStatus.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name).ToList();
        }

        public virtual List<MovementClass> GetMovementClasses()
        {
            return _ctx.MovementClasses.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name).ToList();
        }

        public virtual List<ConfidentialityType> GetConfidentialityTypes(User user)
        {
            return _ctx.ConfidentialityTypes.Where(x => x.DateDeleted == null && x.SortOrder <= user.MaxConfidentialityLevel).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name).ToList();
        }

        public virtual List<ConfidentialityType> GetConfidentialityTypes()
        {
            return _ctx.ConfidentialityTypes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name).ToList();
        }

        public virtual List<RemovalStatus> GetRemovalStatus()
        {
            return _ctx.RemovalStatus.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name).ToList();
        }

        public virtual List<PrimaryStatus> GetPrimaryStatuses()
        {
            return _ctx.PrimaryStatus.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name).ToList();
        }

        public virtual List<RelationshipType> GetRelationshipTypes()
        {
            return _ctx.RelationshipTypes.Where(x => x.DateDeleted == null).OrderBy(x => x.ObjectFrom).ThenBy(x => x.ObjectTo).ThenBy(x => x.SortOrder ?? int.MaxValue).ToList();
        }

        public virtual List<OrganizationType> GetOrganizationTypes()
        {
            return _ctx.OrganizationTypes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name).ToList();
        }

        public virtual List<ChapterType> GetChapterTypes()
        {
            return _ctx.ChapterTypes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name).ToList();
        }

        public virtual List<ContactInfoType> GetContactInfoTypes()
        {
            return _ctx.ContactInfoTypes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name).ToList();
        }

        public virtual List<WebIncidentType> GetWebIncidentTypes()
        {
            return _ctx.WebIncidentTypes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name).ToList();
        }

        public virtual List<EventDocumentationType> GetEventDocumentationTypes()
        {
            return _ctx.EventDocumentationTypes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name).ToList(); ;
        }

        public virtual List<VehicleMake> GetVehicleMakes()
        {
            return _ctx.VehicleMakes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name).ToList(); ;
        }

        public virtual List<VehicleModel> GetVehicleModels()
        {
            return _ctx.VehicleModels.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name).ToList(); ;
        }

        public virtual List<VehicleType> GetVehicleTypes()
        {
            return _ctx.VehicleTypes.OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name).ToList(); ;
        }

        public virtual List<VehicleColor> GetVehicleColors()
        {
            return _ctx.VehicleColors.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name).ToList();
        }

        public virtual List<NewsSourceType> GetNewSourceType()
        {
            return _ctx.NewsSourceTypes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name).ToList();
        }

        public virtual List<MediaType> GetMediaTypes()
        {
            return _ctx.MediaTypes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name).ToList();
        }

        public virtual List<ImageType> GetImageTypes()
        {
            return _ctx.ImageTypes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name).ToList();
        }

        public virtual List<MimeType> GetMimeTypes()
        {
            return _ctx.MimeTypes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name).ToList();
        }

        public virtual List<RenewalPermmisionType> RenewalPermissionTypes()
        {
            return _ctx.RenewalPermmisionTypes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name).ToList();
        }

        public virtual List<NewsSource> GetNewsSources()
        {
            return _ctx.NewsSources.OrderBy(x => x.NewsSourceName).ToList();
        }

        public virtual List<CorrespondenceType> GetCorrespondenceTypes()
        {
            return _ctx.CorrespondenceTypes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name).ToList();
        }

        public virtual List<LibraryCategoryType> GetLibraryCategoryTypes()
        {
            return _ctx.LibraryCategoryTypes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name).ToList();
        }

        public virtual List<PublishedType> GetPublishedTypes()
        {
            return _ctx.PublishedTypes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name).ToList();
        }

        public virtual List<TagType> GetTagTypes()
        {
            return _ctx.TagTypes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name).ToList();
        }

        public virtual List<UserType> GetUserTypes()
        {
            return _ctx.UserTypes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name).ToList();
        }

        public virtual List<WebsiteGroupType> GetWebsiteGroupTypes()
        {
            return _ctx.WebsiteGroupTypes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name).ToList();
        }

        public virtual List<Religion> GetReligions()
        {
            return _ctx.Religions.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name).ToList();
        }

        public virtual List<ContactTopic> GetContactTopics()
        {
            return _ctx.ContactTopics.OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name).ToList();
        }

        public virtual List<ContactType> GetContactTypes()
        {
            return _ctx.ContactTypes.OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name).ToList();
        }

        public virtual int SaveEventType(EventType model)
        {
            _ctx.EventTypes.AddOrUpdate(model);
            return _ctx.SaveChanges();
        }

        public virtual int SaveAudioVideoTypes(AudioVideoType model)
        {
            _ctx.AudioVideoTypes.AddOrUpdate(model);
            return _ctx.SaveChanges();
        }

        public virtual int SavePrefixes(Prefix model)
        {
            _ctx.Prefixes.AddOrUpdate(model);
            return _ctx.SaveChanges();
        }

        public virtual int SaveSuffixes(Suffix model)
        {
            _ctx.Suffixes.AddOrUpdate(model);
            return _ctx.SaveChanges();
        }

        public virtual int SaveGenders(Gender model)
        {
            _ctx.Genders.AddOrUpdate(model);
            return _ctx.SaveChanges();
        }

        public virtual int SaveRaces(Race model)
        {
            _ctx.Races.AddOrUpdate(model);
            return _ctx.SaveChanges();
        }

        public virtual int SaveLicenseTypes(LicenseType model)
        {
            _ctx.LicenseTypes.AddOrUpdate(model);
            return _ctx.SaveChanges();
        }

        public virtual int SaveEyeColors(EyeColor model)
        {
            _ctx.EyeColors.AddOrUpdate(model);
            return _ctx.SaveChanges();
        }

        public virtual int SaveHairColors(HairColor model)
        {
            _ctx.HairColors.AddOrUpdate(model);
            return _ctx.SaveChanges();
        }

        public virtual int SaveHairPatterns(HairPattern model)
        {
            _ctx.HairPatterns.AddOrUpdate(model);
            return _ctx.SaveChanges();
        }

        public virtual int SaveStates(State model)
        {
            _ctx.States.AddOrUpdate(model);
            return _ctx.SaveChanges();
        }

        public virtual int SaveMaritialStatuses(MaritialStatus model)
        {
            _ctx.MaritialStatus.AddOrUpdate(model);
            return _ctx.SaveChanges();
        }

        public virtual int SaveAddressTypes(AddressType model)
        {
            _ctx.AddressTypes.AddOrUpdate(model);
            return _ctx.SaveChanges();
        }

        public virtual int SaveApprovalStatuses(ApprovalStatus model)
        {
            _ctx.ApprovalStatus.AddOrUpdate(model);
            return _ctx.SaveChanges();
        }

        public virtual int SaveActiveStatuses(ActiveStatus model)
        {
            _ctx.ActiveStatus.AddOrUpdate(model);
            return _ctx.SaveChanges();
        }

        public virtual int SaveMovementClasses(MovementClass model)
        {
            _ctx.MovementClasses.AddOrUpdate(model);
            return _ctx.SaveChanges();
        }

        public virtual int SaveConfidentialityTypes(ConfidentialityType model)
        {
            _ctx.ConfidentialityTypes.AddOrUpdate(model);
            return _ctx.SaveChanges();
        }

        public virtual int SaveRemovalStatus(RemovalStatus model)
        {
            _ctx.RemovalStatus.AddOrUpdate(model);
            return _ctx.SaveChanges();
        }

        public virtual int SavePrimaryStatuses(PrimaryStatus model)
        {
            _ctx.PrimaryStatus.AddOrUpdate(model);
            return _ctx.SaveChanges();
        }

        public virtual int SaveRelationshipTypes(RelationshipType model)
        {
            _ctx.RelationshipTypes.AddOrUpdate(model);
            return _ctx.SaveChanges();
        }

        public virtual int SaveOrganizationTypes(OrganizationType model)
        {
            _ctx.OrganizationTypes.AddOrUpdate(model);
            return _ctx.SaveChanges();
        }

        public virtual int SaveChapterTypes(ChapterType model)
        {
            _ctx.ChapterTypes.AddOrUpdate(model);
            return _ctx.SaveChanges();
        }

        public virtual int SaveContactInfoTypes(ContactInfoType model)
        {
            _ctx.ContactInfoTypes.AddOrUpdate(model);
            return _ctx.SaveChanges();
        }

        public virtual int SaveWebIncidentTypes(WebIncidentType model)
        {
            _ctx.WebIncidentTypes.AddOrUpdate(model);
            return _ctx.SaveChanges();
        }

        public virtual int SaveEventDocumentationTypes(EventDocumentationType model)
        {
            _ctx.EventDocumentationTypes.AddOrUpdate(model);
            return _ctx.SaveChanges();
        }

        public virtual int SaveVehicleMakes(VehicleMake model)
        {
            _ctx.VehicleMakes.AddOrUpdate(model);
            return _ctx.SaveChanges();
        }

        public virtual int SaveVehicleModels(VehicleModel model)
        {
            _ctx.VehicleModels.AddOrUpdate(model);
            return _ctx.SaveChanges();
        }

        public virtual int SaveVehicleTypes(VehicleType model)
        {
            _ctx.VehicleTypes.AddOrUpdate(model);
            return _ctx.SaveChanges();
        }

        public virtual int SaveVehicleColors(VehicleColor model)
        {
            _ctx.VehicleColors.AddOrUpdate(model);
            return _ctx.SaveChanges();
        }

        public virtual int SaveNewSourceType(NewsSourceType model)
        {
            _ctx.NewsSourceTypes.AddOrUpdate(model);
            return _ctx.SaveChanges();
        }

        public virtual int SaveMediaTypes(MediaType model)
        {
            _ctx.MediaTypes.AddOrUpdate(model);
            return _ctx.SaveChanges();
        }

        public virtual int SaveImageTypes(ImageType model)
        {
            _ctx.ImageTypes.AddOrUpdate(model);
            return _ctx.SaveChanges();
        }

        public virtual int SaveMimeTypes(MimeType model)
        {
            _ctx.MimeTypes.AddOrUpdate(model);
            return _ctx.SaveChanges();
        }

        public virtual int SaveRenewalPermissionTypes(RenewalPermmisionType model)
        {
            _ctx.RenewalPermmisionTypes.AddOrUpdate(model);
            return _ctx.SaveChanges();
        }

        public virtual int SaveNewsSources(NewsSource model)
        {
            _ctx.NewsSources.AddOrUpdate(model);
            return _ctx.SaveChanges();
        }

        public virtual int SaveContactTopics(ContactTopic model)
        {
            _ctx.ContactTopics.AddOrUpdate(model);
            return _ctx.SaveChanges();
        }

        public virtual int SaveContactTypes(ContactType model)
        {
            _ctx.ContactTypes.AddOrUpdate(model);
            return _ctx.SaveChanges();
        }

        public virtual int SaveCorrespondenceTypes(CorrespondenceType model)
        {
            _ctx.CorrespondenceTypes.AddOrUpdate(model);
            return _ctx.SaveChanges();
        }

        public virtual int SaveLibraryCategoryTypes(LibraryCategoryType model)
        {
            _ctx.LibraryCategoryTypes.AddOrUpdate(model);
            return _ctx.SaveChanges();
        }

        public virtual int SavePublishedTypes(PublishedType model)
        {
            _ctx.PublishedTypes.AddOrUpdate(model);
            return _ctx.SaveChanges();
        }

        public virtual int SaveTagTypes(TagType model)
        {
            _ctx.TagTypes.AddOrUpdate(model);
            return _ctx.SaveChanges();
        }

        public virtual int SaveUserTypes(UserType model)
        {
            _ctx.UserTypes.AddOrUpdate(model);
            return _ctx.SaveChanges();
        }

        public virtual int SaveWebsiteGroupTypes(WebsiteGroupType model)
        {
            _ctx.WebsiteGroupTypes.AddOrUpdate(model);
            return _ctx.SaveChanges();
        }

        public virtual int SaveReligions(Religion model)
        {
            _ctx.Religions.AddOrUpdate(model);
            return _ctx.SaveChanges();
        }


    }
}
