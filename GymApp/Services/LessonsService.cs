using GymApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GymApp.Services
{
    public static class LessonsService
    {
        public static Lesson Add(Lesson lesson, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                lesson.UserId = userId;
                context.Lessons.Add(lesson);
                context.SaveChanges();

                return lesson;
            }
        }

        public static List<Lesson> GetList()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Lessons.ToList();
            }
        }

        public static Lesson GetById(long id)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Lessons.SingleOrDefault(x => x.Id == id);
            }
        }

        public static void Delete(long id)
        {
            using (var context = new ApplicationDbContext())
            {
                var eventToRemove = context.Lessons.SingleOrDefault(x => x.Id == id);
                context.Lessons.Remove(eventToRemove);
                context.SaveChanges();
            }
        }

        public static void JoinToLesson(long lessonId, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                ApplicationUser user = new ApplicationUser { Id = userId };
                context.Users.Add(user);
                context.Users.Attach(user);

                Lesson lesson = new Lesson { Id = lessonId };
                context.Lessons.Add(lesson);
                context.Lessons.Attach(lesson);

                user.Lessons.Add(lesson);

                context.SaveChanges();
            }
        }

        public static int GetLessonUsersCount(long lessonId)
        {
            using (var context = new ApplicationDbContext())
            {
                var result = (
                    from a in context.Users
            from b in a.Lessons
            join c in context.Users on b.UserId equals c.Id
                    where b.Id == lessonId
                    select new 
                    {
                        ID = c.Id,
                    }).ToList().Count();

                return result;
            }
        }
    }
}
