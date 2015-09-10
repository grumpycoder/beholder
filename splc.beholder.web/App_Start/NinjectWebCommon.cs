using splc.data;
using splc.data.repository;

[assembly: WebActivator.PreApplicationStartMethod(typeof(splc.beholder.web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(splc.beholder.web.App_Start.NinjectWebCommon), "Stop")]

namespace splc.beholder.web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<ACDBContext>().To<ACDBContext>().InRequestScope();
            kernel.Bind<IUserRepository>().To<UserRepository>().InRequestScope();
            kernel.Bind<ILookupRepository>().To<LookupRepository>().InRequestScope();
            kernel.Bind<IPersonRepository>().To<PersonRepository>().InRequestScope();
            kernel.Bind<IBeholderPersonRepository>().To<BeholderPersonRepository>().InRequestScope();

            kernel.Bind<ICommonPersonRepository>().To<CommonPersonRepository>().InRequestScope();
            kernel.Bind<IOrganizationRepository>().To<OrganizationRepository>().InRequestScope();
            kernel.Bind<IChapterRepository>().To<ChapterRepository>().InRequestScope();
            kernel.Bind<IEventRepository>().To<EventRepository>().InRequestScope();
            kernel.Bind<IVehicleRepository>().To<VehicleRepository>().InRequestScope();
            kernel.Bind<IMediaImageRepository>().To<MediaImageRepository>().InRequestScope();
            kernel.Bind<IMediaAudioVideoRepository>().To<MediaAudioVideoRepository>().InRequestScope();
            kernel.Bind<IContactInfoPersonRelRepository>().To<ContactInfoPersonRelRepository>().InRequestScope();
            kernel.Bind<IContactInfoRepository>().To<ContactInfoRepository>().InRequestScope();
            kernel.Bind<IContactRepository>().To<ContactRepository>().InRequestScope();
            kernel.Bind<ICommentRepository>().To<CommentRepository>().InRequestScope();
            kernel.Bind<IMediaCorrespondenceRepository>().To<MediaCorrespondenceRepository>().InRequestScope();
            kernel.Bind<IMediaPublishedRepository>().To<MediaPublishedRepository>().InRequestScope();
            kernel.Bind<INewsSourceRepository>().To<NewsSourceRepository>().InRequestScope();
            kernel.Bind<ISubscriptionRepository>().To<SubscriptionRepository>().InRequestScope();
            kernel.Bind<ISearchRepository>().To<SearchRepository>().InRequestScope();
            kernel.Bind<IApprovalRepository>().To<ApprovalRepository>().InRequestScope();
            kernel.Bind<IMediaWebsiteEGroupRepository>().To<MediaWebsiteEGroupRepository>().InRequestScope();

            kernel.Bind<IAliasRepository>().To<AliasRepository>().InRequestScope();
            kernel.Bind<IAliasPersonRelRepository>().To<AliasPersonRelRepository>().InRequestScope();
            kernel.Bind<IAliasOrganizationRelRepository>().To<AliasOrganizationRelRepository>().InRequestScope();
            kernel.Bind<IAliasChapterRelRepository>().To<AliasChapterRelRepository>().InRequestScope();

            kernel.Bind<IAddressRepository>().To<AddressRepository>().InRequestScope();
            kernel.Bind<IAddressChapterRelRepository>().To<AddressChapterRelRepository>().InRequestScope();
            kernel.Bind<IAddressContactRelRepository>().To<AddressContactRelRepository>().InRequestScope();
            kernel.Bind<IAddressPersonRelRepository>().To<AddressPersonRelRepository>().InRequestScope();
            kernel.Bind<IAddressEventRelRepository>().To<AddressEventRelRepository>().InRequestScope();
            kernel.Bind<IAddressSubscriptionsRelRepository>().To<AddressSubscriptionsRelRepository>().InRequestScope();

            //kernel.Bind<ICommonPersonRepository>().To<CommonPersonRepository>().InRequestScope();
            //kernel.Bind<IBeholderPersonRepository>().To<BeholderPersonRepository>().InRequestScope();
            //kernel.Bind<IAddressRepository>().To<AddressRepository>().InRequestScope();
            //kernel.Bind<IAddressPersonRelRepository>().To<AddressPersonRelRepository>().InRequestScope();
            //kernel.Bind<IPersonMediaImageRelRepository>().To<PersonMediaImageRelRepository>().InRequestScope();
            //kernel.Bind<IPersonEventRelRepository>().To<PersonEventRelRepository>().InRequestScope();

            //kernel.Bind<IVehicleRepository>().To<VehicleRepository>().InRequestScope();
            //kernel.Bind<IPersonVehicleRelRepository>().To<PersonVehicleRelRepository>().InRequestScope();

        }
    }
}
