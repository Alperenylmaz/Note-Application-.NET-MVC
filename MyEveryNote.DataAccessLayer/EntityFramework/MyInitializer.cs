using MyEveryNote.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MyEveryNote.DataAccessLayer
{
    public class MyInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            EveryNoteUser admin = new EveryNoteUser()
            {
                Name = "Alperen",
                Surname = "Yılmaz",
                Email = "alperenylmaz61@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                ProfileImageFileName = "profile_foto.png",
                IsAdmin = true,
                Username = "alperenylmaz",
                Password = "123",
                CreationOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUsername = "alperenylmaz"
            };

            EveryNoteUser standart = new EveryNoteUser()
            {
                Name = "Ali",
                Surname = "Veli",
                Email = "aliveli@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = false,
                Username = "aliveli",
                ProfileImageFileName = "profile_foto.png",
                Password = "1234",
                CreationOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUsername = "alperenylmaz"
            };

            for (int i = 0; i < 10; i++)
            {
                EveryNoteUser user = new EveryNoteUser()
                {
                    Name = FakeData.NameData.GetFirstName(),
                    Surname = FakeData.NameData.GetSurname(),
                    Email = FakeData.NetworkData.GetEmail(),
                    ActivateGuid = Guid.NewGuid(),
                    ProfileImageFileName = "profile_foto.png",
                    IsActive = true,
                    IsAdmin = false,
                    Username = $"user {i}",
                    Password = "1234",
                    CreationOn = DateTime.Now,
                    ModifiedOn = DateTime.Now.AddMinutes(5),
                    ModifiedUsername = $"user {i}"
                };
                context.EveryNoteUser.Add(user);
            }

            //context.SaveChanges();

            context.EveryNoteUser.Add(admin);
            context.EveryNoteUser.Add(standart);

            context.SaveChanges();
            //Adding fake category..

            List<EveryNoteUser> userlist = context.EveryNoteUser.ToList();

            for (int i = 0; i < 10; i++)
            {
                Category category = new Category()
                {
                    Title = FakeData.PlaceData.GetCity(),
                    Description = FakeData.TextData.GetSentence(),
                    CreationOn = DateTime.Now,
                    ModifiedOn = DateTime.Now.AddMinutes(5),
                    ModifiedUsername = "alperenylmaz"
                };

                context.Categories.Add(category);

                //Veritabanından çek.

                //Adding fake note..
                for (int k = 0; k < FakeData.NumberData.GetNumber(5, 9); k++)
                {
                    EveryNoteUser owner = userlist[FakeData.NumberData.GetNumber(0, userlist.Count - 1)];
                    Note note = new Note()
                    {
                        Title = FakeData.NameData.GetCompanyName(),
                        Text = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(1, 3)),
                        Category = category,
                        IsDraft = false,
                        LikeCount = FakeData.NumberData.GetNumber(1, 9),
                        Owner = owner,
                        CreationOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedUsername = owner.Username
                    };

                    category.Notes.Add(note);

                    //Adding fake comments
                    for (int j = 0; j < FakeData.NumberData.GetNumber(3, 5); j++)
                    {
                        EveryNoteUser comment_owner = userlist[FakeData.NumberData.GetNumber(0, userlist.Count - 1)];

                        Comment com = new Comment()
                        {
                            Text = FakeData.TextData.GetSentence(),
                            CreationOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedUsername = comment_owner.Username,
                            Note = note,
                            Owner = comment_owner
                        };

                        context.Comments.Add(com);

                       
                    }
                    //Adding fake Liked
                    for (int l = 0; l < note.LikeCount; l++)
                    {
                        Liked liked = new Liked()
                        {
                            LikedUser = userlist[l],
                        };
                        note.Likes.Add(liked);
                    }
                }
            }
            context.SaveChanges();
        }
    }
}
