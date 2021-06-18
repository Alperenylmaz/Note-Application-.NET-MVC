using MyEveryNote.BusinessLayer;
using MyEveryNote.BusinessLayer.Results;
using MyEveryNote.Entity;
using MyEveryNote.Entity.Messages;
using MyEveryNote.Entity.ValueObjects;
using MyEveryNote.Web.Models;
using MyEveryNote.Web.ViewModels;
using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyEveryNote.Web.Controllers
{
    public class HomeController : Controller
    {
        private NoteManager noteManager = new NoteManager();
        private CategoryManager categoryManager = new CategoryManager();
        private EveryNoteUserManager everyNoteUserManager = new EveryNoteUserManager();

        // GET: Home
        public ActionResult Index()
        {
            return View(noteManager.ListQueryable().OrderByDescending(x => x.ModifiedOn).ToList());
        }

        public ActionResult ByCategoryId(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category category = categoryManager.Find(x=>x.Id == id.Value);

            if (category == null)
            {
                return HttpNotFound();
            }

            return View("Index", category.Notes.OrderByDescending(x => x.ModifiedOn).ToList());
        }

        public ActionResult MostLiked()
        {
            NoteManager nm = new NoteManager();
            return View("Index", nm.ListQueryable().OrderByDescending(x => x.LikeCount).ToList());
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult ShownProfile()
        {
            BusinessLayerResult<EveryNoteUser> res = everyNoteUserManager.GetUserById(CurrentSession.User.Id);

            if (res.Errors.Count > 0 || res == null)
            {
                ErrorViewModel modelErr = new ErrorViewModel()
                {
                    Title = "Profile Getirilirken Bir Sorun Oluştu",
                    RedirectingURL = "/Home/Index",
                    RedirectingTimeout = 3000,
                };
            }
            return View(res.Result);
        }

        public ActionResult EditProfile()
        {
            EveryNoteUserManager eum = new EveryNoteUserManager();
            BusinessLayerResult<EveryNoteUser> res = eum.GetUserById(CurrentSession.User.Id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel mdlError = new ErrorViewModel()
                {
                    Title = "Hata",
                    Items = res.Errors
                };
                return View("Error", mdlError);
            }
            return View(res.Result);
        }

        [HttpPost]
        public ActionResult EditProfile(EveryNoteUser user , HttpPostedFileBase ProfileImage)
        {
            ModelState.Remove("ModifiedUsername");
            if(ModelState.IsValid)
            {
                if (ProfileImage != null &&
                (ProfileImage.ContentType == "image/jpeg" ||
                ProfileImage.ContentType == "image/jpg" ||
                ProfileImage.ContentType == "image/png"))
                {
                    string filename = $"user_{user.Id}.{ProfileImage.ContentType.Split('/')[1]}";

                    ProfileImage.SaveAs(Server.MapPath($"~/images/{filename}"));
                    user.ProfileImageFileName = filename;
                }

                EveryNoteUserManager eum = new EveryNoteUserManager();
                BusinessLayerResult<EveryNoteUser> res = eum.UpdateProfile(user);

                if (res.Errors.Count > 0)
                {
                    ErrorViewModel mdlError = new ErrorViewModel()
                    {
                        Title = "Profil Güncellenemedi.",
                        Items = res.Errors,
                        RedirectingURL = "/Home/EditProfile"
                    };

                    return View("Error", mdlError);
                }

                CurrentSession.Set<EveryNoteUser>("login", res.Result);

                return RedirectToAction("ShownProfile");
            }
            return View(user);
            
        }

        public ActionResult RemoveProfile()
        {
        
            EveryNoteUserManager eum = new EveryNoteUserManager();
            BusinessLayerResult<EveryNoteUser> res = eum.RemoveByUserId(CurrentSession.User.Id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel mdlError = new ErrorViewModel()
                {
                    Title = "Profil Silinemedi.",
                    Items = res.Errors,
                    RedirectingURL = "/Home/ShownProfile"
                };
                return View("Error", mdlError);
            }

            Session.Clear();

            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                EveryNoteUserManager um = new EveryNoteUserManager();
                BusinessLayerResult<EveryNoteUser> res = um.Login(model);

                if (res.Errors.Count > 0)
                {
                    if (res.Errors.Find(x => x.Code == ErrorMessages.UserIsNotActive) != null)
                    {
                        ViewBag.SetLink = "http://Home/Activate/1234-468-547120";
                    }

                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }
                CurrentSession.Set<EveryNoteUser>("login",res.Result);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                EveryNoteUserManager um = new EveryNoteUserManager();
                BusinessLayerResult<EveryNoteUser> res = um.RegisterUser(model);

                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }

                OkeyViewModel okeyModel = new OkeyViewModel()
                {
                    Title = "Kayıt Olma Başarılı",
                    RedirectingURL = "/Home/Login",
                    RedirectingTimeout = 5000,
                };

                okeyModel.Items.Add("Başarılı Bir Şekilde Kayıt Oldunuz 5sn Sonra Giriş Sayfasına Yönlendirileceksiniz. Aktivasyon Linkiniz Mail Adresinize Gönderilmiştir. Giriş Yapmadan Önce Lütfen E-Postanızı Kontrol Ediniz.");

                return View("Okey", okeyModel);
            }
            return View(model);
        }

        public ActionResult UserActivate(Guid id)
        {
            EveryNoteUserManager eum = new EveryNoteUserManager();
            BusinessLayerResult<EveryNoteUser> res = eum.ActiveUser(id);

            if (res.Errors.Count > 0)
            {
                TempData["Errors"] = res.Errors;

                ErrorViewModel modelError = new ErrorViewModel()
                {
                    Title = "Bir Hata Oluştu",
                    Header = "Activasyon Sırasında Bir Hata Oluştu Lütfen Tekrar Deneyiniz.",
                    RedirectingURL = "/Home/Index"
                };

                modelError.Items.AddRange(res.Errors);
                return View("Error", modelError);
            }

            OkeyViewModel okeyModel = new OkeyViewModel()
            {
                Title = "İşlem Başarılı",
                Header = "Hesabınız Aktif",
                RedirectingURL = "/Home/Index",
                RedirectingTimeout = 10000
            };

            okeyModel.Items.Add("Hesabınız Başarılı Bir Şekilde Aktive Edildi. 10sn Sonra Giriş Sayfasına Yönlendirileceksiniz Username ve Parola ile Sisteme Giriş Yapabilirsiniz");

            return View("Okey",okeyModel);
        }

        public ActionResult Logout()
        {
            Session.Clear();

            return RedirectToAction("Index");
        }
    }
}