using splc.domain.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace splc.data.repository
{

    public class LookupRepository : ILookupRepository
    {
        readonly ACDBContext _ctx;

        public LookupRepository(ACDBContext ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<EventType> GetEventTypes()
        {
            return _ctx.EventTypes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name);
        }

        public IQueryable<AudioVideoType> GetAudioVideoTypes()
        {
            return _ctx.AudioVideoTypes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name);
        }

        public IQueryable<Prefix> GetPrefixes()
        {
            return _ctx.Prefixes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name);
        }

        public IQueryable<Suffix> GetSuffixes()
        {
            return _ctx.Suffixes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name);
        }

        public IQueryable<Gender> GetGenders()
        {
            return _ctx.Genders.OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name);
        }

        public IQueryable<Race> GetRaces()
        {
            return _ctx.Races.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name);
        }

        public IQueryable<LicenseType> GetLicenseTypes()
        {
            return _ctx.LicenseTypes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name);
        }

        public IQueryable<EyeColor> GetEyeColors()
        {
            return _ctx.EyeColors.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name);
        }

        public IQueryable<HairColor> GetHairColors()
        {
            return _ctx.HairColors.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name);
        }

        public IQueryable<HairPattern> GetHairPatterns()
        {
            return _ctx.HairPatterns.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name);
        }

        public virtual List<State> GetStates()
        {
            return _ctx.States.OrderBy(x => x.StateCode).ToList();
        }

        public IQueryable<MaritialStatus> GetMaritialStatuses()
        {
            return _ctx.MaritialStatus.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name);
        }

        public IQueryable<AddressType> GetAddressTypes()
        {
            return _ctx.AddressTypes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name);
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

        public IQueryable<ConfidentialityType> GetConfidentialityTypes(User user)
        {
            return _ctx.ConfidentialityTypes.Where(x => x.DateDeleted == null && x.SortOrder <= user.MaxConfidentialityLevel).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name);
        }

        public IQueryable<ConfidentialityType> GetConfidentialityTypes()
        {
            return _ctx.ConfidentialityTypes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name);
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

        public IQueryable<OrganizationType> GetOrganizationTypes()
        {
            return _ctx.OrganizationTypes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name);
        }

        public virtual List<ChapterType> GetChapterTypes()
        {
            return _ctx.ChapterTypes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name).ToList();
        }

        public virtual List<ContactInfoType> GetContactInfoTypes()
        {
            return _ctx.ContactInfoTypes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name).ToList();
        }

        public IQueryable<WebIncidentType> GetWebIncidentTypes()
        {
            return _ctx.WebIncidentTypes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name);
        }

        public IQueryable<EventDocumentationType> GetEventDocumentationTypes()
        {
            return _ctx.EventDocumentationTypes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name); ;
        }

        public IQueryable<VehicleMake> GetVehicleMakes()
        {
            return _ctx.VehicleMakes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name); ;
        }

        public IQueryable<VehicleModel> GetVehicleModels()
        {
            return _ctx.VehicleModels.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name); ;
        }

        public IQueryable<VehicleType> GetVehicleTypes()
        {
            return _ctx.VehicleTypes.OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name); ;
        }

        public IQueryable<VehicleColor> GetVehicleColors()
        {
            return _ctx.VehicleColors.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name);
        }

        public IQueryable<NewsSourceType> GetNewSourceType()
        {
            return _ctx.NewsSourceTypes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name);
        }

        public IQueryable<MediaType> GetMediaTypes()
        {
            return _ctx.MediaTypes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name);
        }

        public IQueryable<ImageType> GetImageTypes()
        {
            return _ctx.ImageTypes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name);
        }

        public IQueryable<MimeType> GetMimeTypes()
        {
            return _ctx.MimeTypes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name);
        }

        public IQueryable<RenewalPermmisionType> RenewalPermissionTypes()
        {
            return _ctx.RenewalPermmisionTypes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name);
        }

        public IQueryable<NewsSource> GetNewsSources()
        {
            return _ctx.NewsSources.OrderBy(x => x.NewsSourceName);
        }


        public IQueryable<CorrespondenceType> GetCorrespondenceTypes()
        {
            return _ctx.CorrespondenceTypes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name);
        }


        public IQueryable<LibraryCategoryType> GetLibraryCategoryTypes()
        {
            return _ctx.LibraryCategoryTypes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name);
        }


        public IQueryable<PublishedType> GetPublishedTypes()
        {
            return _ctx.PublishedTypes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name);
        }

        public IQueryable<TagType> GetTagTypes()
        {
            return _ctx.TagTypes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name);
        }


        public IQueryable<UserType> GetUserTypes()
        {
            return _ctx.UserTypes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name);
        }

        public IQueryable<WebsiteGroupType> GetWebsiteGroupTypes()
        {
            return _ctx.WebsiteGroupTypes.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name);
        }

        public IQueryable<Religion> GetReligions()
        {
            return _ctx.Religions.Where(x => x.DateDeleted == null).OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name);
        }

        public IQueryable<ContactTopic> GetContactTopics()
        {
            return _ctx.ContactTopics.OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name);
        }

        public IQueryable<ContactType> GetContactTypes()
        {
            return _ctx.ContactTypes.OrderBy(x => x.SortOrder ?? int.MaxValue).ThenBy(x => x.Name);
        }
    }

    public class CachedLookupRepository : LookupRepository
    {
        private static readonly object CacheLockObject = new object();

        public CachedLookupRepository(ACDBContext ctx) : base(ctx)
        {
        }

        public override List<State> GetStates()
        {
            Debug.Print("CachedLookupRepository:GetStates");
            var cacheKey = "States";
            var result = HttpRuntime.Cache[cacheKey] as List<State>;
            if (result != null) return result;
            lock (CacheLockObject)
            {
                result = HttpRuntime.Cache[cacheKey] as List<State>;
                if (result != null) return result;
                result = base.GetStates();
                HttpRuntime.Cache.Insert(cacheKey, result, null,
                    DateTime.Now.AddSeconds(1440), TimeSpan.Zero);
            }
            return result;
        }

        public override List<ChapterType> GetChapterTypes()
        {
            Debug.Print("CachedLookupRepository:GetChapterTypes");
            var cacheKey = "ChapterTypes";
            var result = HttpRuntime.Cache[cacheKey] as List<ChapterType>;
            if (result != null) return result;
            lock (CacheLockObject)
            {
                result = HttpRuntime.Cache[cacheKey] as List<ChapterType>;
                if (result != null) return result;
                result = base.GetChapterTypes();
                HttpRuntime.Cache.Insert(cacheKey, result, null,
                    DateTime.Now.AddSeconds(1440), TimeSpan.Zero);
            }
            return result;
        }

        public override List<ApprovalStatus> GetApprovalStatuses()
        {
            Debug.Print("CachedLookupRepository:GetApprovalStatuses");
            var cacheKey = "ApprovalStatus";
            var result = HttpRuntime.Cache[cacheKey] as List<ApprovalStatus>;
            if (result != null) return result;
            lock (CacheLockObject)
            {
                result = HttpRuntime.Cache[cacheKey] as List<ApprovalStatus>;
                if (result != null) return result;
                result = base.GetApprovalStatuses();
                HttpRuntime.Cache.Insert(cacheKey, result, null,
                    DateTime.Now.AddSeconds(1440), TimeSpan.Zero);
            }
            return result;
        }

        public override List<ActiveStatus> GetActiveStatuses()
        {
            Debug.Print("CachedLookupRepository:GetActiveStatuses");
            var cacheKey = "ActiveStatus";
            var result = HttpRuntime.Cache[cacheKey] as List<ActiveStatus>;
            if (result != null) return result;
            lock (CacheLockObject)
            {
                result = HttpRuntime.Cache[cacheKey] as List<ActiveStatus>;
                if (result != null) return result;
                result = base.GetActiveStatuses();
                HttpRuntime.Cache.Insert(cacheKey, result, null,
                    DateTime.Now.AddSeconds(1440), TimeSpan.Zero);
            }
            return result;
        }

        public override List<MovementClass> GetMovementClasses()
        {
            Debug.Print("CachedLookupRepository:GetMovementClasses");
            var cacheKey = "MovementClass";
            var result = HttpRuntime.Cache[cacheKey] as List<MovementClass>;
            if (result != null) return result;
            lock (CacheLockObject)
            {
                result = HttpRuntime.Cache[cacheKey] as List<MovementClass>;
                if (result != null) return result;
                result = base.GetMovementClasses();
                HttpRuntime.Cache.Insert(cacheKey, result, null,
                    DateTime.Now.AddSeconds(1440), TimeSpan.Zero);
            }
            return result;
        }

        public override List<RemovalStatus> GetRemovalStatus()
        {
            Debug.Print("CachedLookupRepository:GetRemovalStatus");
            var cacheKey = "RemovalStatus";
            var result = HttpRuntime.Cache[cacheKey] as List<RemovalStatus>;
            if (result != null) return result;
            lock (CacheLockObject)
            {
                result = HttpRuntime.Cache[cacheKey] as List<RemovalStatus>;
                if (result != null) return result;
                result = base.GetRemovalStatus();
                HttpRuntime.Cache.Insert(cacheKey, result, null,
                    DateTime.Now.AddSeconds(1440), TimeSpan.Zero);
            }
            return result;
        }

        public override List<RelationshipType> GetRelationshipTypes()
        {
            Debug.Print("CachedLookupRepository:GetRelationshipTypes");
            var cacheKey = "RelationshipType";
            var result = HttpRuntime.Cache[cacheKey] as List<RelationshipType>;
            if (result != null) return result;
            lock (CacheLockObject)
            {
                result = HttpRuntime.Cache[cacheKey] as List<RelationshipType>;
                if (result != null) return result;
                result = base.GetRelationshipTypes();
                HttpRuntime.Cache.Insert(cacheKey, result, null,
                    DateTime.Now.AddSeconds(1440), TimeSpan.Zero);
            }
            return result;
        }

        public override List<ContactInfoType> GetContactInfoTypes()
        {
            Debug.Print("CachedLookupRepository:GetContactInfoTypes");
            var cacheKey = "ContactInfoType";
            var result = HttpRuntime.Cache[cacheKey] as List<ContactInfoType>;
            if (result != null) return result;
            lock (CacheLockObject)
            {
                result = HttpRuntime.Cache[cacheKey] as List<ContactInfoType>;
                if (result != null) return result;
                result = base.GetContactInfoTypes();
                HttpRuntime.Cache.Insert(cacheKey, result, null,
                    DateTime.Now.AddSeconds(1440), TimeSpan.Zero);
            }
            return result;
        }

        public override List<PrimaryStatus> GetPrimaryStatuses()
        {
            Debug.Print("CachedLookupRepository:GetPrimaryStatuses");
            var cacheKey = "PrimaryStatus";
            var result = HttpRuntime.Cache[cacheKey] as List<PrimaryStatus>;
            if (result != null) return result;
            lock (CacheLockObject)
            {
                result = HttpRuntime.Cache[cacheKey] as List<PrimaryStatus>;
                if (result != null) return result;
                result = base.GetPrimaryStatuses();
                HttpRuntime.Cache.Insert(cacheKey, result, null,
                    DateTime.Now.AddSeconds(1440), TimeSpan.Zero);
            }
            return result;
        }

    }

    public interface ILookupRepository
    {
        IQueryable<EventType> GetEventTypes();
        IQueryable<AudioVideoType> GetAudioVideoTypes();
        IQueryable<Prefix> GetPrefixes();
        IQueryable<Suffix> GetSuffixes();
        IQueryable<Gender> GetGenders();
        IQueryable<Race> GetRaces();
        IQueryable<LicenseType> GetLicenseTypes();
        IQueryable<EyeColor> GetEyeColors();
        IQueryable<HairColor> GetHairColors();
        IQueryable<HairPattern> GetHairPatterns();
        List<State> GetStates();
        IQueryable<MaritialStatus> GetMaritialStatuses();
        IQueryable<AddressType> GetAddressTypes();
        List<ApprovalStatus> GetApprovalStatuses();
        List<ActiveStatus> GetActiveStatuses();
        List<MovementClass> GetMovementClasses();
        IQueryable<ConfidentialityType> GetConfidentialityTypes(User user);
        IQueryable<ConfidentialityType> GetConfidentialityTypes();
        List<RemovalStatus> GetRemovalStatus();
        List<PrimaryStatus> GetPrimaryStatuses();
        List<RelationshipType> GetRelationshipTypes();
        IQueryable<OrganizationType> GetOrganizationTypes();
        List<ChapterType> GetChapterTypes();
        List<ContactInfoType> GetContactInfoTypes();
        IQueryable<WebIncidentType> GetWebIncidentTypes();
        IQueryable<EventDocumentationType> GetEventDocumentationTypes();
        IQueryable<VehicleMake> GetVehicleMakes();
        IQueryable<VehicleModel> GetVehicleModels();
        IQueryable<VehicleType> GetVehicleTypes();
        IQueryable<VehicleColor> GetVehicleColors();
        IQueryable<NewsSourceType> GetNewSourceType();
        IQueryable<MediaType> GetMediaTypes();
        IQueryable<ImageType> GetImageTypes();
        IQueryable<MimeType> GetMimeTypes();
        IQueryable<RenewalPermmisionType> RenewalPermissionTypes();
        IQueryable<NewsSource> GetNewsSources();
        IQueryable<ContactTopic> GetContactTopics();
        IQueryable<ContactType> GetContactTypes();
        IQueryable<CorrespondenceType> GetCorrespondenceTypes();
        IQueryable<LibraryCategoryType> GetLibraryCategoryTypes();
        IQueryable<PublishedType> GetPublishedTypes();
        IQueryable<TagType> GetTagTypes();
        IQueryable<UserType> GetUserTypes();
        IQueryable<WebsiteGroupType> GetWebsiteGroupTypes();
        IQueryable<Religion> GetReligions();

    }
}
