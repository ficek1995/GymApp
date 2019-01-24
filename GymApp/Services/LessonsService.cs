using GymApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace GymApp.Services
{
    public static class LessonsService
    {

        public static List<string> GetLessonUsers(long lessonId)
        {
            using (var context = new ApplicationDbContext())
            {
                List<UserLesson> result = context.Database.SqlQuery<UserLesson>(
                       "SELECT LessonRefId, UserRefId FROM dbo.LessonsUsers")
                       .Where(x => x.LessonRefId == lessonId)
                       .ToList();

                return result.Select(x => x.UserRefId).ToList();
            }
        }

        public static bool  JoinedToLesson(long lessonId, string userId)
        {
            var users = GetLessonUsers(lessonId);
            var userInLesson = users.FirstOrDefault(x => x == userId);
            
            return userInLesson == null;
        }

        public static void SendCancelEmail(long lessonId) 
        {
            using (var context = new ApplicationDbContext())
            {
                var users = GetLessonUsers(lessonId);

                foreach(var item in users)
                {
                    var  user = context.Users.SingleOrDefault(x => x.Id == item);
                    var lesson = context.Lessons.SingleOrDefault(x => x.Id == lessonId);
                    MailService.SendMail("GymApp Powiadomienie", $"Witaj {user.FirstName} {user.LastName} " +
                        $"Zajęcia : {lesson.Title} zostały odwołane lub przesunięte. P" +
                        $"rosimy o sprawdzenie Grafiku", user.Email);

                }
            }
        }

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
            SendCancelEmail(id);
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
