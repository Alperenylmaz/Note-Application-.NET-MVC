using MyEveryNote.BusinessLayer.Abstract;
using MyEveryNote.Entity;
using System.Linq;

namespace MyEveryNote.BusinessLayer
{
    public class CategoryManager : ManagerBase<Category>
    {
        public override int Delete(Category category)
        {
            NoteManager noteManager = new NoteManager();
            LikedManager likedManager = new LikedManager();
            CommentManager commentManager = new CommentManager();

            //Kategori ile ilişkili notların silinmesi gerekiyor.
            foreach (Note note in category.Notes.ToList())
            {
                //Note ile ilişkili likelerin silinmesi.
                foreach (Liked like in note.Likes.ToList())
                {
                    likedManager.Delete(like);
                }

                foreach (Comment comment in note.Comments.ToList())
                {
                    commentManager.Delete(comment);
                }

                noteManager.Delete(note);
            }
            return base.Delete(category);
        }
    }
}
