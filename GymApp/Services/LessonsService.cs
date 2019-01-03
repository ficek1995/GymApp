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

    }
}
