using MyEveryNote.BusinessLayer.Abstract;
using MyEveryNote.BusinessLayer.Results;
using MyEveryNote.Common.Helpers;
using MyEveryNote.DataAccessLayer.EntityFramework;
using MyEveryNote.Entity;
using MyEveryNote.Entity.Messages;
using MyEveryNote.Entity.ValueObjects;
using System;


namespace MyEveryNote.BusinessLayer
{
    public class EveryNoteUserManager : ManagerBase<EveryNoteUser>
    {
        public BusinessLayerResult<EveryNoteUser> RegisterUser(RegisterViewModel data)
        {
            EveryNoteUser user = Find(x => x.Username == data.Username || x.Email == data.Email);
            BusinessLayerResult<EveryNoteUser> layerResult = new BusinessLayerResult<EveryNoteUser>();

            if (user != null)
            {
                if (user.Username == data.Username)
                {
                    layerResult.AddErrorMessage(ErrorMessages.UserNameAlreadyExists, "Username Kullanımda.");
                }

                if (user.Email == data.Email)
                {
                    layerResult.AddErrorMessage(ErrorMessages.EmailAlreadyExists, "Email Kullanımda.");
                }
            }
            else
            {
            

                int dbResult = base.Insert(new EveryNoteUser()
                {
                    Username = data.Username,
                    Email = data.Email,
                    ProfileImageFileName = "profile_foto.png",
                    Password = data.Password,
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = false,
                    IsAdmin = false,
                });

                if(dbResult > 0)
                {
                    layerResult.Result = Find(x => x.Email == data.Email && x.Username == data.Username);
             
                    string siteUri = ConfigHelper.Get<string>("SiteRootUri");
                    string activateUri = $"{siteUri}Home/UserActivate/{layerResult.Result.ActivateGuid}";
                    string body = $"Merhaba {layerResult.Result.Username}; <br> Hesabınızı aktifleştirmek için <a href='{activateUri}' target='_blank'> tıklayınız </a>  ";
                    MailHelper.SendMail(body, layerResult.Result.Email, "MyEveryNote Hesap Aktifleştirme");

                    layerResult.Result = Find(x => x.Email == data.Email && x.Username == data.Username);
                }
            }
            return layerResult;
        }

        public BusinessLayerResult<EveryNoteUser> GetUserById(int id)
        {
            BusinessLayerResult<EveryNoteUser> res = new BusinessLayerResult<EveryNoteUser>();
            res.Result = Find(x => x.Id == id);

            if(res.Result == null)
            {
                res.AddErrorMessage(ErrorMessages.UserNotFound, "Kullanıcı Bulunamadı.");
            }

            return res;
           
        }

        public BusinessLayerResult<EveryNoteUser> Login(LoginViewModel data)
        {
            BusinessLayerResult<EveryNoteUser> res = new BusinessLayerResult<EveryNoteUser>();
            res.Result = Find(x => x.Username == data.Username && x.Password == data.Password);

            if (res.Result != null)
            {
                if (!res.Result.IsActive)
                {
                    res.AddErrorMessage(ErrorMessages.UserIsNotActive, "Kullanıcı Aktifleştirilmedi.");
                }
            }
            else
            {
                res.AddErrorMessage(ErrorMessages.UserNameOrPassWrong, "Kullanıcı Adı veya şifre yanlış.");
            }

            return res;

        }

        public BusinessLayerResult<EveryNoteUser> UpdateProfile(EveryNoteUser data)
        {
            EveryNoteUser db_user = Find(x => x.Username == data.Username || x.Email == data.Email);
            BusinessLayerResult<EveryNoteUser> res = new BusinessLayerResult<EveryNoteUser>();

            if (db_user != null && db_user.Id != data.Id)
            {
                if (db_user.Username == data.Username)
                {
                    res.AddErrorMessage(ErrorMessages.EmailAlreadyExists, "Kullanıcı Adı Kayıtlı");
                }
                if (db_user.Email == data.Email)
                {
                    res.AddErrorMessage(ErrorMessages.EmailAlreadyExists, "Eposta Kayıtlı");
                }

                return res;
            }

            res.Result = Find(x => x.Id == data.Id);
            res.Result.Email = data.Email;
            res.Result.Name = data.Name;
            res.Result.Surname = data.Surname;
            res.Result.Password = data.Password;
            res.Result.Username = data.Username;

            if (string.IsNullOrEmpty(data.ProfileImageFileName) == false)
            {
                res.Result.ProfileImageFileName = data.ProfileImageFileName;
            }

            if (base.Update(res.Result) == 0)
            {
                res.AddErrorMessage(ErrorMessages.ProfileCouldNotUpdate, "Profil Güncellenemedi.");
            }
            return res;
        }

        public BusinessLayerResult<EveryNoteUser> RemoveByUserId(int id)
        {
            BusinessLayerResult<EveryNoteUser> res = new BusinessLayerResult<EveryNoteUser>();
            EveryNoteUser db_user = Find(x => x.Id == id);

            if (db_user != null)
            {
                if (Delete(db_user) == 0)
                {
                    res.AddErrorMessage(ErrorMessages.UserCouldNotDelete, "Kullanıcı Silinemedi");
                }
            }
            else
            {
                res.AddErrorMessage(ErrorMessages.UserNotFound, "Kullanıcı Bulunamadı");
            }
            return res;
        }

        public BusinessLayerResult<EveryNoteUser> ActiveUser(Guid activeId)
        {
            BusinessLayerResult<EveryNoteUser> res = new BusinessLayerResult<EveryNoteUser>();

            res.Result = Find(x => x.ActivateGuid == activeId);

            if (res.Result != null)
            {
                if (res.Result.IsActive)
                {
                    res.AddErrorMessage(ErrorMessages.UserAlreadyActive, "Kullanıcı Zaten Aktif.");
                    return res;
                }
                res.Result.IsActive = true;
                Update(res.Result);
            }
            else
            {
                res.AddErrorMessage(ErrorMessages.ActiveIdDoesNotExists, "Aktifleştirilecek Kullanıcı Bulunamadı.");
            }

            return res;
        }

        //Method Hiding
        public new BusinessLayerResult<EveryNoteUser> Insert(EveryNoteUser data)
        {
            EveryNoteUser user = Find(x => x.Username == data.Username || x.Email == data.Email);
            BusinessLayerResult<EveryNoteUser> layerResult = new BusinessLayerResult<EveryNoteUser>();

            layerResult.Result = data;

            if (user != null)
            {
                if (user.Username == data.Username)
                {
                    layerResult.AddErrorMessage(ErrorMessages.UserNameAlreadyExists, "Username Kullanımda.");
                }

                if (user.Email == data.Email)
                {
                    layerResult.AddErrorMessage(ErrorMessages.EmailAlreadyExists, "Email Kullanımda.");
                }
            }
            else
            {
                layerResult.Result.ProfileImageFileName = "profile_foto.png";
                layerResult.Result.ActivateGuid = Guid.NewGuid();


                if (base.Insert(layerResult.Result) == 0)
                {
                    layerResult.AddErrorMessage(ErrorMessages.EmailAlreadyExists, "Kullanıcı zaten var");
                } 

                
            }
            return layerResult;
        }

        public new BusinessLayerResult<EveryNoteUser> Update(EveryNoteUser data)
        {
            EveryNoteUser db_user = Find(x => x.Username == data.Username || x.Email == data.Email);
            BusinessLayerResult<EveryNoteUser> res = new BusinessLayerResult<EveryNoteUser>();
            res.Result = data;

            if (db_user != null && db_user.Id != data.Id)
            {
                if (db_user.Username == data.Username)
                {
                    res.AddErrorMessage(ErrorMessages.EmailAlreadyExists, "Kullanıcı Adı Kayıtlı");
                }
                if (db_user.Email == data.Email)
                {
                    res.AddErrorMessage(ErrorMessages.EmailAlreadyExists, "Eposta Kayıtlı");
                }

                return res;
            }

            res.Result = Find(x => x.Id == data.Id);
            res.Result.Email = data.Email;
            res.Result.Name = data.Name;
            res.Result.Surname = data.Surname;
            res.Result.Password = data.Password;
            res.Result.Username = data.Username;
            res.Result.IsActive = data.IsActive;
            res.Result.IsAdmin = data.IsAdmin;

            

            if (base.Update(res.Result) == 0)
            {
                res.AddErrorMessage(ErrorMessages.ProfileCouldNotUpdate, "Profil Güncellenemedi.");
            }
            return res;
        }
    }
}
