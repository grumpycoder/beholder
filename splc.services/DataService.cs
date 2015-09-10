using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using splc.infrastructure.Repository;
using splc.domain.Models;

namespace splc.services
{
    public class DataService
    {
        private readonly IKeyedRepository<int, ActiveStatus> _activeStatusRepo;
        private readonly IKeyedRepository<int, Address> _addressRepo;
        private readonly IKeyedRepository<int, AddressChapterRel> _addressChapterRelRepo;
        private readonly IKeyedRepository<int, AddressContactRel> _addressContactRelRepo;
        private readonly IKeyedRepository<int, AddressEventRel> _addressEventRelRepo;
        private readonly IKeyedRepository<int, AddressPersonRel> _addressPersonRelRepo;
        private readonly IKeyedRepository<int, AddressSubscriptionsRel> _addressSubscriptionsRelRepo;
        private readonly IKeyedRepository<int, AddressType> _addressTypeRepo;
        private readonly IKeyedRepository<int, AddressVehicleTagRel> _addressVehicleTagRelRepo;
        private readonly IKeyedRepository<int, Alias> _aliasRepo;
        private readonly IKeyedRepository<int, AliasChapterRel> _aliasChapterRelRepo;
        private readonly IKeyedRepository<int, AliasCompanyRel> _aliasCompanyRelRepo;
        private readonly IKeyedRepository<int, AliasOrganizationRel> _aliasOrganizationRelRepo;
        private readonly IKeyedRepository<int, AliasPersonRel> _aliasPersonRelRepo;
        private readonly IKeyedRepository<int, ApprovalStatus> _approvalStatusRepo;
        private readonly IKeyedRepository<int, AudioVideoType> _audioVideoTypeRepo;
        private readonly IKeyedRepository<int, BeholderCompanyType> _beholderCompanyTypeRepo;
        private readonly IKeyedRepository<int, BeholderContact> _beholderContactRepo;
        private readonly IKeyedRepository<int, BeholderContactType> _beholderContactTypeRepo;
        private readonly IKeyedRepository<int, BeholderPerson> _beholderPersonRepo;
        private readonly IKeyedRepository<int, BusinessClass> _businessClassRepo;
        private readonly IKeyedRepository<int, Chapter> _chapterRepo;
        private readonly IKeyedRepository<int, ChapterStatusHistory> _chapterStatusHistoryRepo;
        private readonly IKeyedRepository<int, ChapterType> _chapterTypeRepo;
        private readonly IKeyedRepository<int, CommonCompanyType> _commonCompanyTypeRepo;
        private readonly IKeyedRepository<int, CommonContact> _commonContactRepo;
        private readonly IKeyedRepository<int, CommonContactType> _commonContactTypeRepo;
        private readonly IKeyedRepository<int, CommonPerson> _commonPersonRepo;
        private readonly IKeyedRepository<int, Company> _companyRepo;
        private readonly IKeyedRepository<int, ConfidentialityType> _confidentialityTypeRepo;
        private readonly IKeyedRepository<int, ContactChapterRel> _contactChapterRelRepo;
        private readonly IKeyedRepository<int, ContactCompanyRel> _contactCompanyRelRepo;
        private readonly IKeyedRepository<int, ContactPersonRel> _contactPersonRelRepo;
        private readonly IKeyedRepository<int, ContactTopic> _contactTopicRepo;
        private readonly IKeyedRepository<int, CorrespondenceType> _correspondenceTypeRepo;
        private readonly IKeyedRepository<int, Event> _eventRepo;
        private readonly IKeyedRepository<int, EventDocumentationType> _eventDocumentationTypeRepo;
        private readonly IKeyedRepository<int, EventType> _eventTypeRepo;
        private readonly IKeyedRepository<int, EyeColor> _eyeColorRepo;
        private readonly IKeyedRepository<int, Gender> _genderRepo;
        private readonly IKeyedRepository<int, HairColor> _hairColorRepo;
        private readonly IKeyedRepository<int, HairPattern> _hairPatternRepo;
        private readonly IKeyedRepository<int, ImageType> _imageTypeRepo;
        private readonly IKeyedRepository<int, LicenseType> _licenseTypeRepo;
        private readonly IKeyedRepository<int, MaritialStatus> _maritialStatusRepo;
        private readonly IKeyedRepository<int, MediaType> _mediaTypeRepo;
        private readonly IKeyedRepository<int, MotorcycleMake> _motorcycleMakeRepo;
        private readonly IKeyedRepository<int, MotorcycleModel> _motorcycleModelRepo;
        private readonly IKeyedRepository<int, MovementClass> _movementClassRepo;
        private readonly IKeyedRepository<int, NewsSourceType> _newsSourceTypeRepo;
        private readonly IKeyedRepository<int, ObjectType> _objectTypeRepo;
        private readonly IKeyedRepository<int, Organization> _organizationRepo;
        private readonly IKeyedRepository<int, OrganizationPersonRel> _organizationPersonRelRepo;
        private readonly IKeyedRepository<int, OrganizationStatusHistory> _organizationStatusHistoryRepo;
        private readonly IKeyedRepository<int, OrganizationType> _organizationTypeRepo;
        private readonly IKeyedRepository<int, PersonScreenName> _personScreenNameRepo;
        private readonly IKeyedRepository<int, Prefix> _prefixRepo;
        private readonly IKeyedRepository<int, PrimaryStatus> _primaryStatusRepo;
        private readonly IKeyedRepository<int, PublishedType> _publishedTypeRepo;
        private readonly IKeyedRepository<int, Race> _raceRepo;
        private readonly IKeyedRepository<int, Religion> _religionRepo;
        private readonly IKeyedRepository<int, RenewalPermmisionType> _renewalPermmisionTypeRepo;
        private readonly IKeyedRepository<int, State> _stateRepo;
        private readonly IKeyedRepository<int, Subscription> _subscriptionRepo;
        private readonly IKeyedRepository<int, Suffix> _suffixRepo;
        private readonly IKeyedRepository<int, TagType> _tagTypeRepo;
        private readonly IKeyedRepository<int, User> _userRepo;
        private readonly IKeyedRepository<int, UserType> _userTypeRepo;
        private readonly IKeyedRepository<int, Vehicle> _vehicleRepo;
        private readonly IKeyedRepository<int, VehicleMake> _vehicleMakeRepo;
        private readonly IKeyedRepository<int, VehicleModel> _vehicleModelRepo;
        private readonly IKeyedRepository<int, VehicleColor> _vehicleColorRepo;
        private readonly IKeyedRepository<int, VehicleTag> _vehicleTagRepo;
        private readonly IKeyedRepository<int, VehicleType> _vehicleTypeRepo;
        private readonly IKeyedRepository<int, WebIncidentType> _webIncidentTypeRepo;
        private readonly IKeyedRepository<int, WebsiteGroupType> _websiteGroupTypeRepo;

        private readonly IUnitOfWork _unitOfWork;

        public ActiveStatusService ActiveStatusSvc { get; private set; }
        public AddressService AddressServiceSvc { get; private set; }
        public AddressChapterRelService AddressChapterRelSvc { get; private set; }
        public AddressContactRelService AddressContactRelSvc { get; private set; }
        public AddressEventRelService AddressEventRelSvc { get; private set; }
        public AddressPersonRelService AddressPersonRelSvc { get; private set; }
        public AddressSubscriptionRelService AddressSubscriptionRelSvc { get; private set; }
        public AddressTypeService AddressTypeSvc { get; private set; }
        public AddressVehicleTagRelService AddressVehicleTagRelSvc { get; private set; }
        public AliasService AliasSvc { get; private set; }
        public AliasChapterRelService AliasChapterRelSvc { get; private set; }
        public AliasCompanyRelService AliasCompanyRelSvc { get; private set; }
        public AliasOrganizationRelService AliasOrganizationRelSvc { get; private set; }
        public AliasPersonRelService AliasPersonRelSvc { get; private set; }
        public ApprovalStatusService ApprovalStatusSvc { get; private set; }
        public AudioVideoTypeService AudioVideoTypeSvc { get; private set; }
        public BeholderCompanyTypeService BeholderCompanyTypeSvc { get; private set; }
        public BeholderContactService BeholderContactSvc { get; private set; }
        public BeholderContactTypeService BeholderContactTypeSvc { get; private set; }
        public BusinessClassService BusinessClassSvc { get; private set; }
        public BeholderPersonService BeholderPersonSvc { get; private set; }
        public ChapterService ChapterSvc { get; private set; }
        public ChapterStatusHistoryService ChapterStatusHistorySvc { get; private set; }
        public ChapterTypeService ChapterTypeSvc { get; private set; }
        public CommonCompanyTypeService CommonCompanyTypeSvc { get; private set; }
        public CommonContactService CommonContactSvc { get; private set; }
        public CommonPersonService CommonPersonSvc { get; private set; }
        public CompanyService CompanySvc { get; private set; }
        public ConfidentialityTypeService ConfidentialityTypeSvc { get; private set; }
        public ContactChapterRelService ContactChapterRelSvc { get; private set; }
        public ContactCompanyRelService ContactCompanyRelSvc { get; private set; }
        public ContactPersonRelService ContactPersonRelSvc { get; private set; }
        public ContactTopicService ContactTopicSvc { get; private set; }
        public CorrespondenceTypeService CorrespondenceTypeSvc { get; private set; }
        public EventService EventSvc { get; private set; }
        public EventDocumentationTypeService EventDocumentationTypeSvc { get; private set; }
        public EventTypeService EventTypeSvc { get; private set; }
        public EyeColorService EyeColorSvc { get; private set; }
        public GenderService GenderSvc { get; private set; }
        public HairColorService HairColorSvc { get; private set; }
        public HairPatternService HairPatternSvc { get; private set; }
        public ImageTypeService ImageTypeSvc { get; private set; }
        public LicenseTypeService LicenseTypeSvc { get; private set; }
        public MaritialStatusService MaritialStatusSvc { get; private set; }
        public MediaTypeService MediaTypeSvc { get; private set; }
        public MotorcycleMakeService MotorcycleMakeSvc { get; private set; }
        public MotorcycleModelService MotorcycleModelSvc { get; private set; }
        public MovementClassService MovementClassSvc { get; private set; }
        public NewsSourceTypeService NewsSourceTypeSvc { get; private set; }
        public ObjectTypeService ObjectTypeSvc { get; private set; }
        public OrganizationService OrganizationSvc { get; private set; }
        public OrganizationPersonRelService OrganizationPersonRelSvc { get; private set; }
        public OrganizationStatusHistoryService OrganizationStatusHistorySvc { get; private set; }
        public OrganizationTypeService OrganizationTypeSvc { get; private set; }
        public PersonScreenNameService PersonScreenNameSvc { get; private set; }
        public PrefixService PrefixSvc { get; private set; }
        public PrimaryStatusService PrimaryStatusSvc { get; private set; }
        public PublishedTypeService PublishedTypeSvc { get; private set; }
        public RaceService RaceSvc { get; private set; }
        public ReligionService ReligionSvc { get; private set; }
        public RenewalPermissionTypeService RenewalPermissionTypeSvc { get; private set; }
        public StateService StateSvc { get; private set; }
        public SubscriptionService SubscriptionSvc { get; private set; }
        public SuffixService SuffixSvc { get; private set; }
        public TagTypeService TagTypeSvc { get; private set; }
        public UserService UserSvc { get; private set; }
        public UserTypeService UserTypeSvc { get; private set; }
        public VehicleService VehicleSvc { get; private set; }
        public VehicleColorService VehicleColorSvc { get; private set; }
        public VehicleMakeService VehicleMakeSvc { get; private set; }
        public VehicleModelService VehicleModelSvc { get; private set; }
        public VehicleTagService VehicleTagSvc { get; private set; }
        public VehicleTypeService VehicleTypeSvc { get; private set; }
        public WebIncidentTypeService WebIncidentTypeSvc { get; private set; }
        public WebsiteGroupTypeService WebsiteGroupTypeSvc { get; private set; }

        public DataService(IKeyedRepository<int, BeholderPerson> perRepo,
            IKeyedRepository<int, ActiveStatus> activeStatusRepo,
            IKeyedRepository<int, Address> addressRepo,
            IKeyedRepository<int, AddressChapterRel> addressChapterRelRepo,
            IKeyedRepository<int, AddressContactRel> addressContactRelRepo,
            IKeyedRepository<int, AddressEventRel> addressEventRelRepo,
            IKeyedRepository<int, AddressPersonRel> addressPersonRelRepo,
            IKeyedRepository<int, AddressSubscriptionsRel> addressSubscriptionsRelRepo,
            IKeyedRepository<int, AddressType> addressTypeRepo,
            IKeyedRepository<int, AddressVehicleTagRel> addressVehicleTagRelRepo,
            IKeyedRepository<int, Alias> aliasRepo,
            IKeyedRepository<int, AliasChapterRel> aliasChapterRelRepo,
            IKeyedRepository<int, AliasCompanyRel> aliasCompanyRelRepo,
            IKeyedRepository<int, AliasOrganizationRel> aliasOrganizationRelRepo,
            IKeyedRepository<int, AliasPersonRel> aliasPersonRelRepo,
            IKeyedRepository<int, ApprovalStatus> approvalStatusRepo,
            IKeyedRepository<int, AudioVideoType> audioVideoTypeRepo,
            IKeyedRepository<int, BeholderCompanyType> beholderCompanyTypeRepo,
            IKeyedRepository<int, BeholderContact> beholderContactRepo,
            IKeyedRepository<int, BeholderContactType> beholderContactTypeRepo,
            IKeyedRepository<int, BeholderPerson> beholderPersonRepo,
            IKeyedRepository<int, BusinessClass> businessClassRepo,
            IKeyedRepository<int, Chapter> chapterRepo,
            IKeyedRepository<int, ChapterStatusHistory> chapterStatusHistoryRepo,
            IKeyedRepository<int, ChapterType> chapterTypeRepo,
            IKeyedRepository<int, CommonCompanyType> commonCompanyTypeRepo,
            IKeyedRepository<int, CommonContact> commonContactRepo,
            IKeyedRepository<int, CommonContactType> commonContactTypeRepo,
            IKeyedRepository<int, CommonPerson> commonPersonRepo,
            IKeyedRepository<int, Company> companyRepo,
            IKeyedRepository<int, ConfidentialityType> confidentialityTypeRepo,
            IKeyedRepository<int, ContactChapterRel> contactChapterRelRepo,
            IKeyedRepository<int, ContactCompanyRel> contactCompanyRelRepo,
            IKeyedRepository<int, ContactPersonRel> contactPersonRelRepo,
            IKeyedRepository<int, ContactTopic> contactTopicRepo,
            IKeyedRepository<int, CorrespondenceType> correspondenceTypeRepo,
            IKeyedRepository<int, Event> eventRepo,
            IKeyedRepository<int, EventDocumentationType> eventDocumentationTypeRepo,
            IKeyedRepository<int, EventType> eventTypeRepo,
            IKeyedRepository<int, EyeColor> eyeColorRepo,
            IKeyedRepository<int, Gender> genderRepo,
            IKeyedRepository<int, HairColor> hairColorRepo,
            IKeyedRepository<int, HairPattern> hairPatternRepo,
            IKeyedRepository<int, ImageType> imageTypeRepo,
            IKeyedRepository<int, LicenseType> licenseTypeRepo,
            IKeyedRepository<int, MaritialStatus> maritialStatusRepo,
            IKeyedRepository<int, MediaType> mediaTypeRepo,
            IKeyedRepository<int, MotorcycleMake> motorcycleMakeRepo,
            IKeyedRepository<int, MotorcycleModel> motorcycleModelRepo,
            IKeyedRepository<int, MovementClass> movementClassRepo,
            IKeyedRepository<int, NewsSourceType> newsSourceTypeRepo,
            IKeyedRepository<int, ObjectType> objectTypeRepo,
            IKeyedRepository<int, Organization> organizationRepo,
            IKeyedRepository<int, OrganizationPersonRel> organizationPersonRelRepo,
            IKeyedRepository<int, OrganizationStatusHistory> organizationStatusHistoryRepo,
            IKeyedRepository<int, OrganizationType> organizationTypeRepo,
            IKeyedRepository<int, PersonScreenName> personScreenNameRepo,
            IKeyedRepository<int, Prefix> prefixRepo,
            IKeyedRepository<int, PrimaryStatus> primaryStatusRepo,
            IKeyedRepository<int, PublishedType> publishedTypeRepo,
            IKeyedRepository<int, Race> raceRepo,
            IKeyedRepository<int, Religion> religionRepo,
            IKeyedRepository<int, RenewalPermmisionType> renewalPermmisionTypeRepo,
            IKeyedRepository<int, State> stateRepo,
            IKeyedRepository<int, Subscription> subscriptionRepo,
            IKeyedRepository<int, Suffix> suffixRepo,
            IKeyedRepository<int, TagType> tagTypeRepo,
            IKeyedRepository<int, User> userRepo,
            IKeyedRepository<int, UserType> userTypeRepo,
            IKeyedRepository<int, Vehicle> vehicleRepo,
            IKeyedRepository<int, VehicleColor> vehicleColorRepo,
            IKeyedRepository<int, VehicleMake> vehicleMakeRepo,
            IKeyedRepository<int, VehicleModel> vehicleModelRepo,
            IKeyedRepository<int, VehicleTag> vehicleTagRepo,
            IKeyedRepository<int, VehicleType> vehicleTypeRepo,
            IKeyedRepository<int, WebIncidentType> webIncidentTypeRepo,
            IKeyedRepository<int, WebsiteGroupType> websiteGroupTypeRepo,
            IUnitOfWork unitOfWork)
        {
            _activeStatusRepo = activeStatusRepo;
            _addressRepo = addressRepo;
            _addressChapterRelRepo = addressChapterRelRepo;
            _addressContactRelRepo = addressContactRelRepo;
            _addressEventRelRepo = addressEventRelRepo;
            _addressPersonRelRepo = addressPersonRelRepo;
            _addressSubscriptionsRelRepo = addressSubscriptionsRelRepo;
            _addressTypeRepo = addressTypeRepo;
            _addressVehicleTagRelRepo = addressVehicleTagRelRepo;
            _aliasRepo = aliasRepo;
            _aliasChapterRelRepo = aliasChapterRelRepo;
            _aliasCompanyRelRepo = aliasCompanyRelRepo;
            _aliasOrganizationRelRepo = aliasOrganizationRelRepo;
            _aliasPersonRelRepo = aliasPersonRelRepo;
            _approvalStatusRepo = approvalStatusRepo;
            _audioVideoTypeRepo = audioVideoTypeRepo;
            _beholderCompanyTypeRepo = beholderCompanyTypeRepo;
            _beholderContactRepo = beholderContactRepo;
            _beholderContactTypeRepo = beholderContactTypeRepo;
            _beholderPersonRepo = beholderPersonRepo;
            _businessClassRepo = businessClassRepo;
            _chapterRepo = chapterRepo;
            _chapterStatusHistoryRepo = chapterStatusHistoryRepo;
            _chapterTypeRepo = chapterTypeRepo;
            _commonCompanyTypeRepo = commonCompanyTypeRepo;
            _commonContactRepo = commonContactRepo;
            _commonContactTypeRepo = commonContactTypeRepo;
            _commonPersonRepo = commonPersonRepo;
            _companyRepo = companyRepo;
            _confidentialityTypeRepo = confidentialityTypeRepo;
            _contactChapterRelRepo = contactChapterRelRepo;
            _contactCompanyRelRepo = contactCompanyRelRepo;
            _contactPersonRelRepo = contactPersonRelRepo;
            _contactTopicRepo = contactTopicRepo;
            _correspondenceTypeRepo = correspondenceTypeRepo;
            _eventRepo = eventRepo;
            _eventDocumentationTypeRepo = eventDocumentationTypeRepo;
            _eventTypeRepo = eventTypeRepo;
            _eyeColorRepo = eyeColorRepo;
            _genderRepo = genderRepo;
            _hairColorRepo = hairColorRepo;
            _hairPatternRepo = hairPatternRepo;
            _imageTypeRepo =imageTypeRepo;
            _licenseTypeRepo = licenseTypeRepo;
            _maritialStatusRepo = maritialStatusRepo;
            _mediaTypeRepo = mediaTypeRepo;
            _motorcycleMakeRepo = motorcycleMakeRepo;
            _motorcycleModelRepo = motorcycleModelRepo;
            _movementClassRepo = movementClassRepo;
            _newsSourceTypeRepo = newsSourceTypeRepo;
            _objectTypeRepo = objectTypeRepo;
            _organizationPersonRelRepo = organizationPersonRelRepo;
            _organizationRepo = organizationRepo;
            _organizationStatusHistoryRepo = organizationStatusHistoryRepo;
            _organizationTypeRepo = organizationTypeRepo;
            _personScreenNameRepo = personScreenNameRepo;
            _prefixRepo = prefixRepo;
            _primaryStatusRepo = primaryStatusRepo;
            _publishedTypeRepo =publishedTypeRepo;
            _raceRepo = raceRepo;
            _religionRepo = religionRepo;
            _renewalPermmisionTypeRepo = renewalPermmisionTypeRepo;
            _stateRepo = stateRepo;
            _subscriptionRepo = subscriptionRepo;
            _suffixRepo = suffixRepo;
            _tagTypeRepo = tagTypeRepo;
            _userRepo = userRepo;
            _userTypeRepo = userTypeRepo;
            _vehicleRepo = vehicleRepo;
            _vehicleColorRepo = vehicleColorRepo;
            _vehicleMakeRepo = vehicleMakeRepo;
            _vehicleModelRepo = vehicleModelRepo;
            _vehicleTagRepo = vehicleTagRepo;
            _vehicleTypeRepo = vehicleTypeRepo;
            _webIncidentTypeRepo = webIncidentTypeRepo;
            _websiteGroupTypeRepo = websiteGroupTypeRepo;
            _unitOfWork = unitOfWork;

            ActiveStatusSvc = new ActiveStatusService(activeStatusRepo);
            AddressServiceSvc = new AddressService(addressRepo);
            AddressChapterRelSvc = new  AddressChapterRelService(addressChapterRelRepo);
            AddressContactRelSvc = new  AddressContactRelService(addressContactRelRepo);
            AddressEventRelSvc = new AddressEventRelService(addressEventRelRepo);
            AddressPersonRelSvc = new AddressPersonRelService(addressPersonRelRepo);
            AddressSubscriptionRelSvc  = new AddressSubscriptionRelService(addressSubscriptionsRelRepo);
            AddressTypeSvc = new AddressTypeService(addressTypeRepo);
            AddressVehicleTagRelSvc = new AddressVehicleTagRelService(addressVehicleTagRelRepo);
            AliasSvc  = new AliasService(aliasRepo);
            AliasChapterRelSvc = new AliasChapterRelService(aliasChapterRelRepo);
            AliasCompanyRelSvc = new AliasCompanyRelService(aliasCompanyRelRepo);
            AliasOrganizationRelSvc = new AliasOrganizationRelService(aliasOrganizationRelRepo);
            AliasPersonRelSvc = new AliasPersonRelService(aliasPersonRelRepo);
            ApprovalStatusSvc  = new ApprovalStatusService(approvalStatusRepo);
            AudioVideoTypeSvc  = new AudioVideoTypeService(audioVideoTypeRepo);
            BeholderCompanyTypeSvc  = new BeholderCompanyTypeService(beholderCompanyTypeRepo);
            BeholderContactSvc = new BeholderContactService(beholderContactRepo);
            BeholderContactTypeSvc  = new BeholderContactTypeService(beholderContactTypeRepo);
            BusinessClassSvc  = new BusinessClassService(businessClassRepo);
            BeholderPersonSvc  = new BeholderPersonService(beholderPersonRepo);
            ChapterSvc  = new ChapterService(chapterRepo);
            ChapterStatusHistorySvc  = new ChapterStatusHistoryService(chapterStatusHistoryRepo);
            ChapterTypeSvc  = new ChapterTypeService(chapterTypeRepo);
            CommonCompanyTypeSvc = new CommonCompanyTypeService(commonCompanyTypeRepo);
            CommonContactSvc = new CommonContactService(commonContactRepo);
            CommonPersonSvc = new CommonPersonService(commonPersonRepo);
            CompanySvc  = new CompanyService(companyRepo);
            ConfidentialityTypeSvc  = new ConfidentialityTypeService(confidentialityTypeRepo);
            ContactChapterRelSvc = new ContactChapterRelService(contactChapterRelRepo);
            ContactCompanyRelSvc = new ContactCompanyRelService(contactCompanyRelRepo);
            ContactPersonRelSvc = new ContactPersonRelService(contactPersonRelRepo);
            ContactTopicSvc  = new ContactTopicService(contactTopicRepo);
            CorrespondenceTypeSvc  = new CorrespondenceTypeService(correspondenceTypeRepo);
            EventSvc  = new EventService(eventRepo);
            EventDocumentationTypeSvc  = new EventDocumentationTypeService(eventDocumentationTypeRepo);
            EventTypeSvc = new EventTypeService(eventTypeRepo);
            EyeColorSvc = new EyeColorService(eyeColorRepo);
            GenderSvc = new GenderService(genderRepo);
            HairColorSvc= new HairColorService(hairColorRepo);
            HairPatternSvc  = new HairPatternService(hairPatternRepo);
            ImageTypeSvc = new ImageTypeService(imageTypeRepo);
            LicenseTypeSvc = new LicenseTypeService(licenseTypeRepo);
            MaritialStatusSvc  = new MaritialStatusService(maritialStatusRepo);
            MediaTypeSvc = new MediaTypeService(mediaTypeRepo);
            MotorcycleMakeSvc = new MotorcycleMakeService(motorcycleMakeRepo);
            MotorcycleModelSvc = new MotorcycleModelService(motorcycleModelRepo);
            MovementClassSvc  = new MovementClassService(movementClassRepo);
            NewsSourceTypeSvc = new NewsSourceTypeService(newsSourceTypeRepo);
            ObjectTypeSvc = new ObjectTypeService(objectTypeRepo);
            OrganizationSvc = new OrganizationService(organizationRepo);
            OrganizationPersonRelSvc = new OrganizationPersonRelService(organizationPersonRelRepo);
            OrganizationStatusHistorySvc = new OrganizationStatusHistoryService(organizationStatusHistoryRepo);
            OrganizationTypeSvc = new OrganizationTypeService(organizationTypeRepo);
            PersonScreenNameSvc  = new PersonScreenNameService(personScreenNameRepo);
            PrefixSvc  = new PrefixService(prefixRepo);
            PrimaryStatusSvc  = new PrimaryStatusService(primaryStatusRepo);
            PublishedTypeSvc  = new PublishedTypeService(publishedTypeRepo);
            RaceSvc = new RaceService(raceRepo);
            ReligionSvc  = new ReligionService(religionRepo);
            RenewalPermissionTypeSvc = new RenewalPermissionTypeService(renewalPermmisionTypeRepo);
            StateSvc = new StateService(stateRepo);
            SubscriptionSvc = new SubscriptionService(subscriptionRepo);
            SuffixSvc = new SuffixService(suffixRepo);
            TagTypeSvc = new TagTypeService(tagTypeRepo);
            UserSvc = new UserService(userRepo);
            UserTypeSvc = new UserTypeService(userTypeRepo);
            VehicleSvc = new VehicleService(vehicleRepo);
            VehicleColorSvc = new VehicleColorService(vehicleColorRepo);
            VehicleMakeSvc = new VehicleMakeService(vehicleMakeRepo);
            VehicleModelSvc = new VehicleModelService(vehicleModelRepo);
            VehicleTagSvc = new VehicleTagService(vehicleTagRepo);
            VehicleTypeSvc = new VehicleTypeService(vehicleTypeRepo);
            WebIncidentTypeSvc  = new WebIncidentTypeService(webIncidentTypeRepo);
            WebsiteGroupTypeSvc = new WebsiteGroupTypeService(websiteGroupTypeRepo);
        }


        //public Location GetCurrentLocation(int truckId)
        //{
        //    return _locationRepo
        //        .FilterBy(x => x.Truck.Id == truckId)
        //        .OrderByDescending(c => c.Timestamp)
        //        .FirstOrDefault();
        //}

        public void Commit()
        {
            _unitOfWork.Save();
        }
    }
}
