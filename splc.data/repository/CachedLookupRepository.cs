using splc.domain.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Web;
using splc.data.Utility;

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
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceNameFromList();
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
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceNameFromList();
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
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceNameFromList();
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
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceNameFromList();
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
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceNameFromList();
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
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceNameFromList();
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
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceNameFromList();
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
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceNameFromList();
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
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceNameFromList();
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
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceNameFromList();
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
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceNameFromList();
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
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceNameFromList();
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
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceNameFromList();
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
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceNameFromList();
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
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceNameFromList();
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
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceNameFromList();
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
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceNameFromList();
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
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceNameFromList();
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
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceNameFromList();
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
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceNameFromList();
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
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceNameFromList();
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
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceNameFromList();
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
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceNameFromList();
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
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceNameFromList();
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
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceNameFromList();
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
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceNameFromList();
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
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceNameFromList();
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
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceNameFromList();
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
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceNameFromList();
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
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceNameFromList();
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
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceNameFromList();
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
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceNameFromList();
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
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceNameFromList();
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
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceNameFromList();
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
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceNameFromList();
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
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceNameFromList();
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
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceNameFromList();
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
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceNameFromList();
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
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceNameFromList();
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
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceNameFromList();
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
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceNameFromList();
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
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceNameFromList();
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
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceNameFromList();
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
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceNameFromList();
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
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceNameFromList();
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

        ///// SAVE METHODS /////
        public override int SaveEventType(EventType model)
        {
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceName();
            HttpRuntime.Cache.Remove(cacheKey);
            return base.SaveEventType(model);
        }

        public override int SavePrefixes(Prefix model)
        {
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceName();
            HttpRuntime.Cache.Remove(cacheKey);
            return base.SavePrefixes(model);
        }

        public override int SaveSuffixes(Suffix model)
        {
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceName();
            HttpRuntime.Cache.Remove(cacheKey);
            return base.SaveSuffixes(model);
        }

        public override int SaveGenders(Gender model)
        {
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceName();
            HttpRuntime.Cache.Remove(cacheKey);
            return base.SaveGenders(model);
        }

        public override int SaveRaces(Race model)
        {
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceName();
            HttpRuntime.Cache.Remove(cacheKey);
            return base.SaveRaces(model);
        }

        public override int SaveLicenseTypes(LicenseType model)
        {
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceName();
            HttpRuntime.Cache.Remove(cacheKey);
            return base.SaveLicenseTypes(model);
        }

        public override int SaveEyeColors(EyeColor model)
        {
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceName();
            HttpRuntime.Cache.Remove(cacheKey);
            return base.SaveEyeColors(model);
        }

        public override int SaveHairColors(HairColor model)
        {
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceName();
            HttpRuntime.Cache.Remove(cacheKey);
            return base.SaveHairColors(model);
        }

        public override int SaveHairPatterns(HairPattern model)
        {
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceName();
            HttpRuntime.Cache.Remove(cacheKey);
            return base.SaveHairPatterns(model);
        }

        public override int SaveStates(State model)
        {
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceName();
            HttpRuntime.Cache.Remove(cacheKey);
            return base.SaveStates(model);
        }

        public override int SaveMaritialStatuses(MaritialStatus model)
        {
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceName();
            HttpRuntime.Cache.Remove(cacheKey);
            return base.SaveMaritialStatuses(model);
        }

        public override int SaveAddressTypes(AddressType model)
        {
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceName();
            HttpRuntime.Cache.Remove(cacheKey);
            return base.SaveAddressTypes(model);
        }

        public override int SaveApprovalStatuses(ApprovalStatus model)
        {
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceName();
            HttpRuntime.Cache.Remove(cacheKey);
            return base.SaveApprovalStatuses(model);
        }

        public override int SaveActiveStatuses(ActiveStatus model)
        {
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceName();
            HttpRuntime.Cache.Remove(cacheKey);
            return base.SaveActiveStatuses(model);
        }

        public override int SaveMovementClasses(MovementClass model)
        {
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceName();
            HttpRuntime.Cache.Remove(cacheKey);
            return base.SaveMovementClasses(model);
        }

        public override int SaveConfidentialityTypes(ConfidentialityType model)
        {
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceName();
            HttpRuntime.Cache.Remove(cacheKey);
            return base.SaveConfidentialityTypes(model);
        }

        public override int SaveRemovalStatus(RemovalStatus model)
        {
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceName();
            HttpRuntime.Cache.Remove(cacheKey);
            return base.SaveRemovalStatus(model);
        }

        public override int SavePrimaryStatuses(PrimaryStatus model)
        {
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceName();
            HttpRuntime.Cache.Remove(cacheKey);
            return base.SavePrimaryStatuses(model);
        }

        public override int SaveRelationshipTypes(RelationshipType model)
        {
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceName();
            HttpRuntime.Cache.Remove(cacheKey);
            return base.SaveRelationshipTypes(model);
        }

        public override int SaveOrganizationTypes(OrganizationType model)
        {
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceName();
            HttpRuntime.Cache.Remove(cacheKey);
            return base.SaveOrganizationTypes(model);
        }

        public override int SaveChapterTypes(ChapterType model)
        {
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceName();
            HttpRuntime.Cache.Remove(cacheKey);
            return base.SaveChapterTypes(model);
        }

        public override int SaveContactInfoTypes(ContactInfoType model)
        {
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceName();
            HttpRuntime.Cache.Remove(cacheKey);
            return base.SaveContactInfoTypes(model);
        }

        public override int SaveWebIncidentTypes(WebIncidentType model)
        {
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceName();
            HttpRuntime.Cache.Remove(cacheKey);
            return base.SaveWebIncidentTypes(model);
        }

        public override int SaveEventDocumentationTypes(EventDocumentationType model)
        {
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceName();
            HttpRuntime.Cache.Remove(cacheKey);
            return base.SaveEventDocumentationTypes(model);
        }

        public override int SaveVehicleMakes(VehicleMake model)
        {
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceName();
            HttpRuntime.Cache.Remove(cacheKey);
            return base.SaveVehicleMakes(model);
        }

        public override int SaveVehicleModels(VehicleModel model)
        {
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceName();
            HttpRuntime.Cache.Remove(cacheKey);
            return base.SaveVehicleModels(model);
        }

        public override int SaveVehicleTypes(VehicleType model)
        {
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceName();
            HttpRuntime.Cache.Remove(cacheKey);
            return base.SaveVehicleTypes(model);
        }

        public override int SaveVehicleColors(VehicleColor model)
        {
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceName();
            HttpRuntime.Cache.Remove(cacheKey);
            return base.SaveVehicleColors(model);
        }

        public override int SaveNewSourceType(NewsSourceType model)
        {
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceName();
            HttpRuntime.Cache.Remove(cacheKey);
            return base.SaveNewSourceType(model);
        }

        public override int SaveMediaTypes(MediaType model)
        {
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceName();
            HttpRuntime.Cache.Remove(cacheKey);
            return base.SaveMediaTypes(model);
        }

        public override int SaveImageTypes(ImageType model)
        {
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceName();
            HttpRuntime.Cache.Remove(cacheKey);
            return base.SaveImageTypes(model);
        }

        public override int SaveMimeTypes(MimeType model)
        {
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceName();
            HttpRuntime.Cache.Remove(cacheKey);
            return base.SaveMimeTypes(model);
        }

        public override int SaveRenewalPermissionTypes(RenewalPermmisionType model)
        {
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceName();
            HttpRuntime.Cache.Remove(cacheKey);
            return base.SaveRenewalPermissionTypes(model);
        }

        public override int SaveNewsSources(NewsSource model)
        {
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceName();
            HttpRuntime.Cache.Remove(cacheKey);
            return base.SaveNewsSources(model);
        }

        public override int SaveContactTopics(ContactTopic model)
        {
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceName();
            HttpRuntime.Cache.Remove(cacheKey);
            return base.SaveContactTopics(model);
        }

        public override int SaveContactTypes(ContactType model)
        {
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceName();
            HttpRuntime.Cache.Remove(cacheKey);
            return base.SaveContactTypes(model);
        }

        public override int SaveCorrespondenceTypes(CorrespondenceType model)
        {
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceName();

            HttpRuntime.Cache.Remove(cacheKey);
            return base.SaveCorrespondenceTypes(model);
        }

        public override int SaveLibraryCategoryTypes(LibraryCategoryType model)
        {
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceName();
            HttpRuntime.Cache.Remove(cacheKey);
            return base.SaveLibraryCategoryTypes(model);
        }

        public override int SavePublishedTypes(PublishedType model)
        {
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceName();
            HttpRuntime.Cache.Remove(cacheKey);
            return base.SavePublishedTypes(model);
        }

        public override int SaveTagTypes(TagType model)
        {
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceName();
            HttpRuntime.Cache.Remove(cacheKey);
            return base.SaveTagTypes(model);
        }

        public override int SaveUserTypes(UserType model)
        {
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceName();
            HttpRuntime.Cache.Remove(cacheKey);
            return base.SaveUserTypes(model);
        }

        public override int SaveWebsiteGroupTypes(WebsiteGroupType model)
        {
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceName();
            HttpRuntime.Cache.Remove(cacheKey);
            return base.SaveWebsiteGroupTypes(model);
        }

        public override int SaveReligions(Religion model)
        {
            var cacheKey = ((MethodInfo)MethodBase.GetCurrentMethod()).ReturnType.GetNiceName();
            HttpRuntime.Cache.Remove(cacheKey);
            return base.SaveReligions(model);
        }

    }
}