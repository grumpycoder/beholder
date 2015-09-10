using System.Linq;
using System.Web.Mvc;
using splc.data;

namespace splc.beholder.web.Controllers
{
    public class ValidationController : Controller
    {

        public readonly ACDBContext context; 

        public ValidationController(ACDBContext context)
        {
            this.context = context;
        }

        public JsonResult UserNameAlreadyExists(int? id, string username)
        {
            var model = context.Users.Where(x => x.Id != id && x.UserName == username);
            return Json(!model.Any());
        }

        public JsonResult PersonHasPrimaryAlias(int? id, int? primaryStatusId, int? personId)
        {
            var model = context.AliasPersonRels.Where(x => x.Id != id && x.PersonId == personId && x.PrimaryStatusId == primaryStatusId && x.PrimaryStatusId == 1 && x.DateDeleted == null);
            return Json(!model.Any());
        }

        public JsonResult PersonHasSameAlias(int? id, string aliasname, int? personId)
        {
            var model = context.AliasPersonRels.Where(x => x.Id != id && x.Alias.AliasName == aliasname && x.DateDeleted == null);
            return Json(!model.Any());
        }

        public JsonResult ChapterPersonAlreadyExists(int? id, int? chapterid, int? personid)
        {
            var model = context.ChapterPersonRels.Where(x => x.Id != id && x.ChapterId == chapterid && x.BeholderPersonId == personid && x.Chapter.DateDeleted == null && x.BeholderPerson.DateDeleted == null);
            return Json(!model.Any());
        }

        public JsonResult PersonHasPrimaryScreenName(int? id, int? primaryStatusId, int? beholderpersonId)
        {
            var model = context.PersonScreenNames.Where(x => x.Id != id && x.BeholderPersonId == beholderpersonId && x.PrimaryStatusId == primaryStatusId && x.PrimaryStatusId == 1 && x.DateDeleted == null);
            return Json(!model.Any());
        }

        public JsonResult PersonHasPrimaryAddress(int? id, int? primaryStatusId, int? personId)
        {
            var model = context.AddressPersonRels.Where(x => x.Id != id && x.PersonId == personId && x.PrimaryStatusId == primaryStatusId && x.PrimaryStatusId == 1 && x.DateDeleted == null);
            return Json(!model.Any());    
        }

        public JsonResult PersonHasPrimaryContactInfo(int? id, int? primaryStatusId, int? personId)
        {
            var model = context.ContactInfoPersonRels.Where(x => x.Id != id && x.PersonId == personId && x.PrimaryStatusId == primaryStatusId && x.PrimaryStatusId == 1 && x.DateDeleted == null);
            return Json(!model.Any());
        }

        public JsonResult PersonHasPrimaryContact(int? id, int? primaryStatusId, int? personId)
        {
            var model = context.PersonContactRels.Where(x => x.Id != id && x.PersonId == personId && x.PrimaryStatusId == primaryStatusId && x.PrimaryStatusId == 1);
            return Json(!model.Any());
        }


        public JsonResult OrganizationHasPrimaryAlias(int? id, int? primaryStatusId, int? organizationId)
        {
            var model = context.AliasOrganizationRels.Where(x => x.Id != id && x.OrganizationId == organizationId && x.PrimaryStatusId == primaryStatusId && x.PrimaryStatusId == 1 && x.DateDeleted == null);
            return Json(!model.Any());
        }

        public JsonResult ChapterHasPrimaryAlias(int? id, int? primaryStatusId, int? chapterId)
        {
            var model = context.AliasChapterRels.Where(x => x.Id != id && x.ChapterId == chapterId && x.PrimaryStatusId == primaryStatusId && x.PrimaryStatusId == 1 && x.DateDeleted == null);
            return Json(!model.Any());
        }

        public JsonResult ChapterHasPrimaryAddress(int? id, int? primaryStatusId, int? chapterId)
        {
            var model = context.AddressChapterRels.Where(x => x.Id != id && x.ChapterId == chapterId && x.PrimaryStatusId == primaryStatusId && x.PrimaryStatusId == 1 && x.DateDeleted == null);
            return Json(!model.Any());
        }

        public JsonResult ChapterHasPrimaryContactInfo(int? id, int? primaryStatusId, int? chapterId)
        {
            var model = context.ChapterContactInfoRels.Where(x => x.Id != id && x.ChapterId == chapterId && x.PrimaryStatusId == primaryStatusId && x.PrimaryStatusId == 1 && x.DateDeleted == null);
            return Json(!model.Any());
        }

        public JsonResult ChapterHasPrimaryContact(int? id, int? primaryStatusId, int? chapterId)
        {
            var model = context.ChapterContactRels.Where(x => x.Id != id && x.ChapterId == chapterId && x.PrimaryStatusId == primaryStatusId && x.PrimaryStatusId == 1);
            return Json(!model.Any());
        }

        public JsonResult ContactHasPrimaryAddress(int? id, int? primaryStatusId, int? contactId)
        {
            var model = context.AddressContactRels.Where(x => x.Id != id && x.ContactId == contactId && x.PrimaryStatusId == primaryStatusId && x.PrimaryStatusId == 1 && x.DateDeleted == null);
            return Json(!model.Any());
        }

    }
}