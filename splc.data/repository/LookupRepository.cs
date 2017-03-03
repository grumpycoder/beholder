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

        public override List<EventType> GetEventTypes()
        {
            Debug.Print("CachedLookupRepository:GetEventTypes");
            var cacheKey = "EventType";
            var result = HttpRuntime.Cache[cacheKey] as List<EventType>;
            if (result != null) return result;
            lock (CacheLockObject)
            {
                result = HttpRuntime.Cache[cacheKey] as List<EventType>;
                if (result != null) return result;
                result = base.GetEventTypes();
                HttpRuntime.Cache.Insert(cacheKey, result, null,
                    DateTime.Now.AddSeconds(1440), TimeSpan.Zero);
            }
            return result;
        }

        public override List<AudioVideoType> GetAudioVideoTypes()
        {
            Debug.Print("CachedLookupRepository:GetAudioVideoTypes");
            var cacheKey = "AudioVideoType";
            var result = HttpRuntime.Cache[cacheKey] as List<AudioVideoType>;
            if (result != null) return result;
            lock (CacheLockObject)
            {
                result = HttpRuntime.Cache[cacheKey] as List<AudioVideoType>;
                if (result != null) return result;
                result = base.GetAudioVideoTypes();
                HttpRuntime.Cache.Insert(cacheKey, result, null,
                    DateTime.Now.AddSeconds(1440), TimeSpan.Zero);
            }
            return result;
        }

        public override List<Prefix> GetPrefixes()
        {
            Debug.Print("CachedLookupRepository:GetPrefixes");
            var cacheKey = "Prefix";
            var result = HttpRuntime.Cache[cacheKey] as List<Prefix>;
            if (result != null) return result;
            lock (CacheLockObject)
            {
                result = HttpRuntime.Cache[cacheKey] as List<Prefix>;
                if (result != null) return result;
                result = base.GetPrefixes();
                HttpRuntime.Cache.Insert(cacheKey, result, null,
                    DateTime.Now.AddSeconds(1440), TimeSpan.Zero);
            }
            return result;
        }

        public override List<Suffix> GetSuffixes()
        {
            Debug.Print("CachedLookupRepository:GetSuffixes");
            var cacheKey = "Suffix";
            var result = HttpRuntime.Cache[cacheKey] as List<Suffix>;
            if (result != null) return result;
            lock (CacheLockObject)
            {
                result = HttpRuntime.Cache[cacheKey] as List<Suffix>;
                if (result != null) return result;
                result = base.GetSuffixes();
                HttpRuntime.Cache.Insert(cacheKey, result, null,
                    DateTime.Now.AddSeconds(1440), TimeSpan.Zero);
            }
            return result;
        }

        public override List<Gender> GetGenders()
        {
            Debug.Print("CachedLookupRepository:GetGenders");
            var cacheKey = "Gender";
            var result = HttpRuntime.Cache[cacheKey] as List<Gender>;
            if (result != null) return result;
            lock (CacheLockObject)
            {
                result = HttpRuntime.Cache[cacheKey] as List<Gender>;
                if (result != null) return result;
                result = base.GetGenders();
                HttpRuntime.Cache.Insert(cacheKey, result, null,
                    DateTime.Now.AddSeconds(1440), TimeSpan.Zero);
            }
            return result;
        }

        public override List<Race> GetRaces()
        {
            Debug.Print("CachedLookupRepository:GetRaces");
            var cacheKey = "Race";
            var result = HttpRuntime.Cache[cacheKey] as List<Race>;
            if (result != null) return result;
            lock (CacheLockObject)
            {
                result = HttpRuntime.Cache[cacheKey] as List<Race>;
                if (result != null) return result;
                result = base.GetRaces();
                HttpRuntime.Cache.Insert(cacheKey, result, null,
                    DateTime.Now.AddSeconds(1440), TimeSpan.Zero);
            }
            return result;
        }

        public override List<LicenseType> GetLicenseTypes()
        {
            Debug.Print("CachedLookupRepository:GetLicenseTypes");
            var cacheKey = "LicenseType";
            var result = HttpRuntime.Cache[cacheKey] as List<LicenseType>;
            if (result != null) return result;
            lock (CacheLockObject)
            {
                result = HttpRuntime.Cache[cacheKey] as List<LicenseType>;
                if (result != null) return result;
                result = base.GetLicenseTypes();
                HttpRuntime.Cache.Insert(cacheKey, result, null,
                    DateTime.Now.AddSeconds(1440), TimeSpan.Zero);
            }
            return result;
        }

        public override List<EyeColor> GetEyeColors()
        {
            Debug.Print("CachedLookupRepository:GetEyeColors");
            var cacheKey = "EyeColor";
            var result = HttpRuntime.Cache[cacheKey] as List<EyeColor>;
            if (result != null) return result;
            lock (CacheLockObject)
            {
                result = HttpRuntime.Cache[cacheKey] as List<EyeColor>;
                if (result != null) return result;
                result = base.GetEyeColors();
                HttpRuntime.Cache.Insert(cacheKey, result, null,
                    DateTime.Now.AddSeconds(1440), TimeSpan.Zero);
            }
            return result;
        }

        public override List<HairColor> GetHairColors()
        {
            Debug.Print("CachedLookupRepository:GetHairColors");
            var cacheKey = "HairColor";
            var result = HttpRuntime.Cache[cacheKey] as List<HairColor>;
            if (result != null) return result;
            lock (CacheLockObject)
            {
                result = HttpRuntime.Cache[cacheKey] as List<HairColor>;
                if (result != null) return result;
                result = base.GetHairColors();
                HttpRuntime.Cache.Insert(cacheKey, result, null,
                    DateTime.Now.AddSeconds(1440), TimeSpan.Zero);
            }
            return result;
        }

        public override List<HairPattern> GetHairPatterns()
        {
            Debug.Print("CachedLookupRepository:GetHairPatterns");
            var cacheKey = "HairPattern";
            var result = HttpRuntime.Cache[cacheKey] as List<HairPattern>;
            if (result != null) return result;
            lock (CacheLockObject)
            {
                result = HttpRuntime.Cache[cacheKey] as List<HairPattern>;
                if (result != null) return result;
                result = base.GetHairPatterns();
                HttpRuntime.Cache.Insert(cacheKey, result, null,
                    DateTime.Now.AddSeconds(1440), TimeSpan.Zero);
            }
            return result;
        }

        public override List<MaritialStatus> GetMaritialStatuses()
        {
            Debug.Print("CachedLookupRepository:GetMaritialStatuses");
            var cacheKey = "MaritialStatus";
            var result = HttpRuntime.Cache[cacheKey] as List<MaritialStatus>;
            if (result != null) return result;
            lock (CacheLockObject)
            {
                result = HttpRuntime.Cache[cacheKey] as List<MaritialStatus>;
                if (result != null) return result;
                result = base.GetMaritialStatuses();
                HttpRuntime.Cache.Insert(cacheKey, result, null,
                    DateTime.Now.AddSeconds(1440), TimeSpan.Zero);
            }
            return result;
        }

        public override List<AddressType> GetAddressTypes()
        {
            Debug.Print("CachedLookupRepository:GetAddressTypes");
            var cacheKey = "AddressType";
            var result = HttpRuntime.Cache[cacheKey] as List<AddressType>;
            if (result != null) return result;
            lock (CacheLockObject)
            {
                result = HttpRuntime.Cache[cacheKey] as List<AddressType>;
                if (result != null) return result;
                result = base.GetAddressTypes();
                HttpRuntime.Cache.Insert(cacheKey, result, null,
                    DateTime.Now.AddSeconds(1440), TimeSpan.Zero);
            }
            return result;
        }

        public override List<ConfidentialityType> GetConfidentialityTypes(User user)
        {
            Debug.Print("CachedLookupRepository:GetConfidentialityTypes");
            var cacheKey = "ConfidentialityType";
            var result = HttpRuntime.Cache[cacheKey] as List<ConfidentialityType>;
            if (result != null) return result;
            lock (CacheLockObject)
            {
                result = HttpRuntime.Cache[cacheKey] as List<ConfidentialityType>;
                if (result != null) return result;
                result = base.GetConfidentialityTypes(user);
                HttpRuntime.Cache.Insert(cacheKey, result, null,
                    DateTime.Now.AddSeconds(1440), TimeSpan.Zero);
            }
            return result;
        }

        public override List<ConfidentialityType> GetConfidentialityTypes()
        {
            Debug.Print("CachedLookupRepository:GetConfidentialityTypes");
            var cacheKey = "ConfidentialityType";
            var result = HttpRuntime.Cache[cacheKey] as List<ConfidentialityType>;
            if (result != null) return result;
            lock (CacheLockObject)
            {
                result = HttpRuntime.Cache[cacheKey] as List<ConfidentialityType>;
                if (result != null) return result;
                result = base.GetConfidentialityTypes();
                HttpRuntime.Cache.Insert(cacheKey, result, null,
                    DateTime.Now.AddSeconds(1440), TimeSpan.Zero);
            }
            return result;
        }

        public override List<OrganizationType> GetOrganizationTypes()
        {
            Debug.Print("CachedLookupRepository:GetOrganizationTypes");
            var cacheKey = "OrganizationType";
            var result = HttpRuntime.Cache[cacheKey] as List<OrganizationType>;
            if (result != null) return result;
            lock (CacheLockObject)
            {
                result = HttpRuntime.Cache[cacheKey] as List<OrganizationType>;
                if (result != null) return result;
                result = base.GetOrganizationTypes();
                HttpRuntime.Cache.Insert(cacheKey, result, null,
                    DateTime.Now.AddSeconds(1440), TimeSpan.Zero);
            }
            return result;
        }

        public override List<WebIncidentType> GetWebIncidentTypes()
        {
            Debug.Print("CachedLookupRepository:GetWebIncidentTypes");
            var cacheKey = "WebIncidentType";
            var result = HttpRuntime.Cache[cacheKey] as List<WebIncidentType>;
            if (result != null) return result;
            lock (CacheLockObject)
            {
                result = HttpRuntime.Cache[cacheKey] as List<WebIncidentType>;
                if (result != null) return result;
                result = base.GetWebIncidentTypes();
                HttpRuntime.Cache.Insert(cacheKey, result, null,
                    DateTime.Now.AddSeconds(1440), TimeSpan.Zero);
            }
            return result;
        }

        public override List<EventDocumentationType> GetEventDocumentationTypes()
        {
            Debug.Print("CachedLookupRepository:GetEventDocumentationTypes");
            var cacheKey = "EventDocumentationType";
            var result = HttpRuntime.Cache[cacheKey] as List<EventDocumentationType>;
            if (result != null) return result;
            lock (CacheLockObject)
            {
                result = HttpRuntime.Cache[cacheKey] as List<EventDocumentationType>;
                if (result != null) return result;
                result = base.GetEventDocumentationTypes();
                HttpRuntime.Cache.Insert(cacheKey, result, null,
                    DateTime.Now.AddSeconds(1440), TimeSpan.Zero);
            }
            return result;
        }

        public override List<VehicleMake> GetVehicleMakes()
        {
            Debug.Print("CachedLookupRepository:GetVehicleMakes");
            var cacheKey = "VehicleMake";
            var result = HttpRuntime.Cache[cacheKey] as List<VehicleMake>;
            if (result != null) return result;
            lock (CacheLockObject)
            {
                result = HttpRuntime.Cache[cacheKey] as List<VehicleMake>;
                if (result != null) return result;
                result = base.GetVehicleMakes();
                HttpRuntime.Cache.Insert(cacheKey, result, null,
                    DateTime.Now.AddSeconds(1440), TimeSpan.Zero);
            }
            return result;
        }

        public override List<VehicleModel> GetVehicleModels()
        {
            Debug.Print("CachedLookupRepository:GetVehicleModels");
            var cacheKey = "VehicleModel";
            var result = HttpRuntime.Cache[cacheKey] as List<VehicleModel>;
            if (result != null) return result;
            lock (CacheLockObject)
            {
                result = HttpRuntime.Cache[cacheKey] as List<VehicleModel>;
                if (result != null) return result;
                result = base.GetVehicleModels();
                HttpRuntime.Cache.Insert(cacheKey, result, null,
                    DateTime.Now.AddSeconds(1440), TimeSpan.Zero);
            }
            return result;
        }

        public override List<VehicleType> GetVehicleTypes()
        {
            Debug.Print("CachedLookupRepository:GetVehicleTypes");
            var cacheKey = "VehicleType";
            var result = HttpRuntime.Cache[cacheKey] as List<VehicleType>;
            if (result != null) return result;
            lock (CacheLockObject)
            {
                result = HttpRuntime.Cache[cacheKey] as List<VehicleType>;
                if (result != null) return result;
                result = base.GetVehicleTypes();
                HttpRuntime.Cache.Insert(cacheKey, result, null,
                    DateTime.Now.AddSeconds(1440), TimeSpan.Zero);
            }
            return result;
        }

        public override List<VehicleColor> GetVehicleColors()
        {
            Debug.Print("CachedLookupRepository:GetVehicleColors");
            var cacheKey = "VehicleColor";
            var result = HttpRuntime.Cache[cacheKey] as List<VehicleColor>;
            if (result != null) return result;
            lock (CacheLockObject)
            {
                result = HttpRuntime.Cache[cacheKey] as List<VehicleColor>;
                if (result != null) return result;
                result = base.GetVehicleColors();
                HttpRuntime.Cache.Insert(cacheKey, result, null,
                    DateTime.Now.AddSeconds(1440), TimeSpan.Zero);
            }
            return result;
        }

        public override List<NewsSourceType> GetNewSourceType()
        {
            Debug.Print("CachedLookupRepository:GetNewSourceType");
            var cacheKey = "NewsSourceType";
            var result = HttpRuntime.Cache[cacheKey] as List<NewsSourceType>;
            if (result != null) return result;
            lock (CacheLockObject)
            {
                result = HttpRuntime.Cache[cacheKey] as List<NewsSourceType>;
                if (result != null) return result;
                result = base.GetNewSourceType();
                HttpRuntime.Cache.Insert(cacheKey, result, null,
                    DateTime.Now.AddSeconds(1440), TimeSpan.Zero);
            }
            return result;
        }

        public override List<MediaType> GetMediaTypes()
        {
            Debug.Print("CachedLookupRepository:GetMediaTypes");
            var cacheKey = "MediaType";
            var result = HttpRuntime.Cache[cacheKey] as List<MediaType>;
            if (result != null) return result;
            lock (CacheLockObject)
            {
                result = HttpRuntime.Cache[cacheKey] as List<MediaType>;
                if (result != null) return result;
                result = base.GetMediaTypes();
                HttpRuntime.Cache.Insert(cacheKey, result, null,
                    DateTime.Now.AddSeconds(1440), TimeSpan.Zero);
            }
            return result;
        }

        public override List<ImageType> GetImageTypes()
        {
            Debug.Print("CachedLookupRepository:GetImageTypes");
            var cacheKey = "ImageType";
            var result = HttpRuntime.Cache[cacheKey] as List<ImageType>;
            if (result != null) return result;
            lock (CacheLockObject)
            {
                result = HttpRuntime.Cache[cacheKey] as List<ImageType>;
                if (result != null) return result;
                result = base.GetImageTypes();
                HttpRuntime.Cache.Insert(cacheKey, result, null,
                    DateTime.Now.AddSeconds(1440), TimeSpan.Zero);
            }
            return result;
        }

        public override List<MimeType> GetMimeTypes()
        {
            Debug.Print("CachedLookupRepository:GetMimeTypes");
            var cacheKey = "MimeType";
            var result = HttpRuntime.Cache[cacheKey] as List<MimeType>;
            if (result != null) return result;
            lock (CacheLockObject)
            {
                result = HttpRuntime.Cache[cacheKey] as List<MimeType>;
                if (result != null) return result;
                result = base.GetMimeTypes();
                HttpRuntime.Cache.Insert(cacheKey, result, null,
                    DateTime.Now.AddSeconds(1440), TimeSpan.Zero);
            }
            return result;
        }

        public override List<RenewalPermmisionType> RenewalPermissionTypes()
        {
            Debug.Print("CachedLookupRepository:RenewalPermissionTypes");
            var cacheKey = "RenewalPermmisionType";
            var result = HttpRuntime.Cache[cacheKey] as List<RenewalPermmisionType>;
            if (result != null) return result;
            lock (CacheLockObject)
            {
                result = HttpRuntime.Cache[cacheKey] as List<RenewalPermmisionType>;
                if (result != null) return result;
                result = base.RenewalPermissionTypes();
                HttpRuntime.Cache.Insert(cacheKey, result, null,
                    DateTime.Now.AddSeconds(1440), TimeSpan.Zero);
            }
            return result;
        }

        public override List<NewsSource> GetNewsSources()
        {
            Debug.Print("CachedLookupRepository:GetNewsSources");
            var cacheKey = "NewsSource";
            var result = HttpRuntime.Cache[cacheKey] as List<NewsSource>;
            if (result != null) return result;
            lock (CacheLockObject)
            {
                result = HttpRuntime.Cache[cacheKey] as List<NewsSource>;
                if (result != null) return result;
                result = base.GetNewsSources();
                HttpRuntime.Cache.Insert(cacheKey, result, null,
                    DateTime.Now.AddSeconds(1440), TimeSpan.Zero);
            }
            return result;
        }

        public override List<CorrespondenceType> GetCorrespondenceTypes()
        {
            Debug.Print("CachedLookupRepository:GetCorrespondenceTypes");
            var cacheKey = "CorrespondenceType";
            var result = HttpRuntime.Cache[cacheKey] as List<CorrespondenceType>;
            if (result != null) return result;
            lock (CacheLockObject)
            {
                result = HttpRuntime.Cache[cacheKey] as List<CorrespondenceType>;
                if (result != null) return result;
                result = base.GetCorrespondenceTypes();
                HttpRuntime.Cache.Insert(cacheKey, result, null,
                    DateTime.Now.AddSeconds(1440), TimeSpan.Zero);
            }
            return result;
        }

        public override List<LibraryCategoryType> GetLibraryCategoryTypes()
        {
            Debug.Print("CachedLookupRepository:GetLibraryCategoryTypes");
            var cacheKey = "LibraryCategoryType";
            var result = HttpRuntime.Cache[cacheKey] as List<LibraryCategoryType>;
            if (result != null) return result;
            lock (CacheLockObject)
            {
                result = HttpRuntime.Cache[cacheKey] as List<LibraryCategoryType>;
                if (result != null) return result;
                result = base.GetLibraryCategoryTypes();
                HttpRuntime.Cache.Insert(cacheKey, result, null,
                    DateTime.Now.AddSeconds(1440), TimeSpan.Zero);
            }
            return result;
        }

        public override List<PublishedType> GetPublishedTypes()
        {
            Debug.Print("CachedLookupRepository:GetPublishedTypes");
            var cacheKey = "TagType";
            var result = HttpRuntime.Cache[cacheKey] as List<PublishedType>;
            if (result != null) return result;
            lock (CacheLockObject)
            {
                result = HttpRuntime.Cache[cacheKey] as List<PublishedType>;
                if (result != null) return result;
                result = base.GetPublishedTypes();
                HttpRuntime.Cache.Insert(cacheKey, result, null,
                    DateTime.Now.AddSeconds(1440), TimeSpan.Zero);
            }
            return result;
        }

        public override List<TagType> GetTagTypes()
        {
            Debug.Print("CachedLookupRepository:GetTagTypes");
            var cacheKey = "TagType";
            var result = HttpRuntime.Cache[cacheKey] as List<TagType>;
            if (result != null) return result;
            lock (CacheLockObject)
            {
                result = HttpRuntime.Cache[cacheKey] as List<TagType>;
                if (result != null) return result;
                result = base.GetTagTypes();
                HttpRuntime.Cache.Insert(cacheKey, result, null,
                    DateTime.Now.AddSeconds(1440), TimeSpan.Zero);
            }
            return result;
        }

        public override List<UserType> GetUserTypes()
        {
            Debug.Print("CachedLookupRepository:GetUserTypes");
            var cacheKey = "UserType";
            var result = HttpRuntime.Cache[cacheKey] as List<UserType>;
            if (result != null) return result;
            lock (CacheLockObject)
            {
                result = HttpRuntime.Cache[cacheKey] as List<UserType>;
                if (result != null) return result;
                result = base.GetUserTypes();
                HttpRuntime.Cache.Insert(cacheKey, result, null,
                    DateTime.Now.AddSeconds(1440), TimeSpan.Zero);
            }
            return result;
        }

        public override List<WebsiteGroupType> GetWebsiteGroupTypes()
        {
            Debug.Print("CachedLookupRepository:GetWebsiteGroupTypes");
            var cacheKey = "WebsiteGroupType";
            var result = HttpRuntime.Cache[cacheKey] as List<WebsiteGroupType>;
            if (result != null) return result;
            lock (CacheLockObject)
            {
                result = HttpRuntime.Cache[cacheKey] as List<WebsiteGroupType>;
                if (result != null) return result;
                result = base.GetWebsiteGroupTypes();
                HttpRuntime.Cache.Insert(cacheKey, result, null,
                    DateTime.Now.AddSeconds(1440), TimeSpan.Zero);
            }
            return result;
        }

        public override List<Religion> GetReligions()
        {
            Debug.Print("CachedLookupRepository:GetReligions");
            var cacheKey = "Religion";
            var result = HttpRuntime.Cache[cacheKey] as List<Religion>;
            if (result != null) return result;
            lock (CacheLockObject)
            {
                result = HttpRuntime.Cache[cacheKey] as List<Religion>;
                if (result != null) return result;
                result = base.GetReligions();
                HttpRuntime.Cache.Insert(cacheKey, result, null,
                    DateTime.Now.AddSeconds(1440), TimeSpan.Zero);
            }
            return result;
        }

        public override List<ContactTopic> GetContactTopics()
        {
            Debug.Print("CachedLookupRepository:GetContactTopics");
            var cacheKey = "ContactTopic";
            var result = HttpRuntime.Cache[cacheKey] as List<ContactTopic>;
            if (result != null) return result;
            lock (CacheLockObject)
            {
                result = HttpRuntime.Cache[cacheKey] as List<ContactTopic>;
                if (result != null) return result;
                result = base.GetContactTopics();
                HttpRuntime.Cache.Insert(cacheKey, result, null,
                    DateTime.Now.AddSeconds(1440), TimeSpan.Zero);
            }
            return result;
        }

        public override List<ContactType> GetContactTypes()
        {
            Debug.Print("CachedLookupRepository:GetContactTypes");
            var cacheKey = "ContactType";
            var result = HttpRuntime.Cache[cacheKey] as List<ContactType>;
            if (result != null) return result;
            lock (CacheLockObject)
            {
                result = HttpRuntime.Cache[cacheKey] as List<ContactType>;
                if (result != null) return result;
                result = base.GetContactTypes();
                HttpRuntime.Cache.Insert(cacheKey, result, null,
                    DateTime.Now.AddSeconds(1440), TimeSpan.Zero);
            }
            return result;
        }

    }

    public interface ILookupRepository
    {
        List<EventType> GetEventTypes();
        List<AudioVideoType> GetAudioVideoTypes();
        List<Prefix> GetPrefixes();
        List<Suffix> GetSuffixes();
        List<Gender> GetGenders();
        List<Race> GetRaces();
        List<LicenseType> GetLicenseTypes();
        List<EyeColor> GetEyeColors();
        List<HairColor> GetHairColors();
        List<HairPattern> GetHairPatterns();
        List<State> GetStates();
        List<MaritialStatus> GetMaritialStatuses();
        List<AddressType> GetAddressTypes();
        List<ApprovalStatus> GetApprovalStatuses();
        List<ActiveStatus> GetActiveStatuses();
        List<MovementClass> GetMovementClasses();
        List<ConfidentialityType> GetConfidentialityTypes(User user);
        List<ConfidentialityType> GetConfidentialityTypes();
        List<RemovalStatus> GetRemovalStatus();
        List<PrimaryStatus> GetPrimaryStatuses();
        List<RelationshipType> GetRelationshipTypes();
        List<OrganizationType> GetOrganizationTypes();
        List<ChapterType> GetChapterTypes();
        List<ContactInfoType> GetContactInfoTypes();
        List<WebIncidentType> GetWebIncidentTypes();
        List<EventDocumentationType> GetEventDocumentationTypes();
        List<VehicleMake> GetVehicleMakes();
        List<VehicleModel> GetVehicleModels();
        List<VehicleType> GetVehicleTypes();
        List<VehicleColor> GetVehicleColors();
        List<NewsSourceType> GetNewSourceType();
        List<MediaType> GetMediaTypes();
        List<ImageType> GetImageTypes();
        List<MimeType> GetMimeTypes();
        List<RenewalPermmisionType> RenewalPermissionTypes();
        List<NewsSource> GetNewsSources();
        List<ContactTopic> GetContactTopics();
        List<ContactType> GetContactTypes();
        List<CorrespondenceType> GetCorrespondenceTypes();
        List<LibraryCategoryType> GetLibraryCategoryTypes();
        List<PublishedType> GetPublishedTypes();
        List<TagType> GetTagTypes();
        List<UserType> GetUserTypes();
        List<WebsiteGroupType> GetWebsiteGroupTypes();
        List<Religion> GetReligions();

    }
}
