using MyEveryNote.Entity;
using MyEveryNote.DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyEveryNote.BusinessLayer
{
    public class Test
    {
        private Repository<Category> repo_category = new Repository<Category>();
        private Repository<EveryNoteUser> repo_user = new Repository<EveryNoteUser>();
        private Repository<Note> repo_note = new Repository<Note>();
        private Repository<Comment> repo_comment = new Repository<Comment>();
        // her nesne oluştuğuda database context nesnesi oluşuyor.
        public Test()
        {
            DataAccessLayer.DatabaseContext db = new DataAccessLayer.DatabaseContext();
            db.EveryNoteUser.ToList();

            //List<Category> list = repo_category.List();
            //List<Category> list2 = repo_category.List(x => x.Id > 5);
        }

        public void InsertTest()
        {
            int result = repo_user.Insert(new EveryNoteUser()
            {
                Name = "Deneme",
                Surname = "Deneme",
                Email = "deneme@deneme.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = true,
                Username = "deneme1",
                Password = "123",
                CreationOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUsername = "deneme"
            });
        }

        public void UpdateTest()
        {
            EveryNoteUser user = repo_user.Find(x => x.Username == "deneme1");

            if (user != null)
            {
                user.Username = "deneme2";
                int result = repo_user.Update(user);
            }
        }

        public void DeleteTest()
        {
            EveryNoteUser user = repo_user.Find(x => x.Username == "deneme2");

            if (user != null)
            {
                int result = repo_user.Delete(user);
            }
        }

        public void CommentTest()
        {
            EveryNoteUser user = repo_user.Find(x => x.Id == 4);
            Note note = repo_note.Find(x => x.Id == 5);

            Comment comment = new Comment()
            {
                Text = "Bu bir testtir3...",
                CreationOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                ModifiedUsername = "alp",
                Note = note,
                Owner = user
            };

            repo_comment.Insert(comment);
        }
    }
}
