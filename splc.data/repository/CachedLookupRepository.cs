using splc.domain.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;

namespace splc.data.repository
{
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


        public override int SaveEventType(EventType model)
        {
            HttpRuntime.Cache.Remove(model.GetType().Name);
            return base.SaveEventType(model);
        }

        public override int SavePrefixes(Prefix model)
        {
            HttpRuntime.Cache.Remove(model.GetType().Name);
            return base.SavePrefixes(model);
        }

        public override int SaveSuffixes(Suffix model)
        {
            return base.SaveSuffixes(model);
        }

        public override int SaveGenders(Gender model)
        {
            return base.SaveGenders(model);
        }

        public override int SaveRaces(Race model)
        {
            return base.SaveRaces(model);
        }

        public override int SaveLicenseTypes(LicenseType model)
        {
            return base.SaveLicenseTypes(model);
        }

        public override int SaveEyeColors(EyeColor model)
        {
            return base.SaveEyeColors(model);
        }

        public override int SaveHairColors(HairColor model)
        {
            return base.SaveHairColors(model);
        }

        public override int SaveHairPatterns(HairPattern model)
        {
            return base.SaveHairPatterns(model);
        }

        public override int SaveStates(State model)
        {
            return base.SaveStates(model);
        }

        public override int SaveMaritialStatuses(MaritialStatus model)
        {
            return base.SaveMaritialStatuses(model);
        }

        public override int SaveAddressTypes(AddressType model)
        {
            return base.SaveAddressTypes(model);
        }

        public override int SaveApprovalStatuses(ApprovalStatus model)
        {
            return base.SaveApprovalStatuses(model);
        }

        public override int SaveActiveStatuses(ActiveStatus model)
        {
            return base.SaveActiveStatuses(model);
        }

        public override int SaveMovementClasses(MovementClass model)
        {
            return base.SaveMovementClasses(model);
        }

        public override int SaveConfidentialityTypes(ConfidentialityType model)
        {
            return base.SaveConfidentialityTypes(model);
        }

        public override int SaveRemovalStatus(RemovalStatus model)
        {
            return base.SaveRemovalStatus(model);
        }

        public override int SavePrimaryStatuses(PrimaryStatus model)
        {
            return base.SavePrimaryStatuses(model);
        }

        public override int SaveRelationshipTypes(RelationshipType model)
        {
            return base.SaveRelationshipTypes(model);
        }

        public override int SaveOrganizationTypes(OrganizationType model)
        {
            return base.SaveOrganizationTypes(model);
        }

        public override int SaveChapterTypes(ChapterType model)
        {
            return base.SaveChapterTypes(model);
        }

        public override int SaveContactInfoTypes(ContactInfoType model)
        {
            return base.SaveContactInfoTypes(model);
        }

        public override int SaveWebIncidentTypes(WebIncidentType model)
        {
            return base.SaveWebIncidentTypes(model);
        }

        public override int SaveEventDocumentationTypes(EventDocumentationType model)
        {
            return base.SaveEventDocumentationTypes(model);
        }

        public override int SaveVehicleMakes(VehicleMake model)
        {
            return base.SaveVehicleMakes(model);
        }

        public override int SaveVehicleModels(VehicleModel model)
        {
            return base.SaveVehicleModels(model);
        }

        public override int SaveVehicleTypes(VehicleType model)
        {
            return base.SaveVehicleTypes(model);
        }

        public override int SaveVehicleColors(VehicleColor model)
        {
            return base.SaveVehicleColors(model);
        }

        public override int SaveNewSourceType(NewsSourceType model)
        {
            return base.SaveNewSourceType(model);
        }

        public override int SaveMediaTypes(MediaType model)
        {
            return base.SaveMediaTypes(model);
        }

        public override int SaveImageTypes(ImageType model)
        {
            return base.SaveImageTypes(model);
        }

        public override int SaveMimeTypes(MimeType model)
        {
            return base.SaveMimeTypes(model);
        }

        public override int SaveRenewalPermissionTypes(RenewalPermmisionType model)
        {
            return base.SaveRenewalPermissionTypes(model);
        }

        public override int SaveNewsSources(NewsSource model)
        {
            return base.SaveNewsSources(model);
        }

        public override int SaveContactTopics(ContactTopic model)
        {
            return base.SaveContactTopics(model);
        }

        public override int SaveContactTypes(ContactType model)
        {
            return base.SaveContactTypes(model);
        }

        public override int SaveCorrespondenceTypes(CorrespondenceType model)
        {
            return base.SaveCorrespondenceTypes(model);
        }

        public override int SaveLibraryCategoryTypes(LibraryCategoryType model)
        {
            return base.SaveLibraryCategoryTypes(model);
        }

        public override int SavePublishedTypes(PublishedType model)
        {
            return base.SavePublishedTypes(model);
        }

        public override int SaveTagTypes(TagType model)
        {
            return base.SaveTagTypes(model);
        }

        public override int SaveUserTypes(UserType model)
        {
            return base.SaveUserTypes(model);
        }

        public override int SaveWebsiteGroupTypes(WebsiteGroupType model)
        {
            return base.SaveWebsiteGroupTypes(model);
        }

        public override int SaveReligions(Religion model)
        {
            return base.SaveReligions(model);
        }

    }
}