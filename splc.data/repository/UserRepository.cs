using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using splc.domain.Models;

namespace splc.data.repository
{
    public class UserRepository : IUserRepository
    {
        readonly ACDBContext context;

        public UserRepository(ACDBContext Context)
        {
            context = Context;
        }

        public User Find(int id)
        {
            var user = context.Users.Find(id);
            //TODO: Need to use an ordinal field value instead of sort order
            user.MaxConfidentialityLevel = user.GroupUsers.Where(x => x.DateDeleted == null).Max(g => g.Group.ConfidentialityType.SortOrder);
            return context.Users.Find(id);
        }

        public User Find(string username)
        {
            var user = context.Users.Where(x => x.UserName == username && x.DateDeleted == null).FirstOrDefault();
            //TODO: Need to use an ordinal field value instead of sort order
            if (user != null)
            {
                user.MaxConfidentialityLevel = user.GroupUsers.Where(x => x.DateDeleted == null).Max(g => g.Group.ConfidentialityType.SortOrder);
            }
            return user;
        }

        public IQueryable<User> GetUsers()
        {
            return context.Users.Where(x => x.DateDeleted == null).OrderBy(u => u.UserName);
        }


        public IQueryable<User> GetUsers(Expression<Func<User, bool>> filter)
        {
            return context.Users.Where(filter).Where(x => x.DateDeleted == null).OrderBy(u => u.UserName);
        }

        public IQueryable<GroupUser> GetUserGroups(int id)
        {
            return context.GroupUsers.Where(x => x.UserId == id && x.DateDeleted == null);
        }

        public void DeleteGroupUser(int id)
        {
            var groupUser = context.GroupUsers.Find(id);
            context.GroupUsers.Remove(groupUser);
        }

        public void DeleteGroup(int id)
        {
            var group = context.Groups.Find(id);
            //Delete users in group relationship 
            context.GroupUsers.RemoveRange(group.GroupUsers);
            context.Groups.Remove(group);
        }

        public Group GetGroup(int id)
        {
            var group = context.Groups.Find(id);
            return group;
        }

        public IQueryable<Group> GetGroups()
        {
            return context.Groups.Where(x => x.DateDeleted == null).OrderBy(g => g.Name);
        }

        public IQueryable<Group> GetGroups(Expression<Func<Group, bool>> filter)
        {
            return context.Groups.Where(filter).Where(x => x.DateDeleted == null).OrderBy(g => g.Name);
        }

        public IQueryable<User> All
        {
            get
            {
                return context.Users;
            }
        }

        public IQueryable<User> AllIncluding(params Expression<Func<User, object>>[] includeProperties)
        {
            IQueryable<User> query = context.Users;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public void InsertOrUpdate(User user)
        {
            if (user.Id == default(int))
            {
                // New entity
                //context.CommonPersons.Add(user.CommonPerson);
                context.Users.Add(user);
            }
            else
            {
                // Existing entity
                context.Entry(user).State = EntityState.Modified;
                //context.Entry(user.CommonPerson).State = EntityState.Modified;
            }
        }

        public void InsertOrUpdate(GroupUser groupUser)
        {
            if (groupUser.Id == default(int))
            {
                // New entity
                context.GroupUsers.Add(groupUser);
            }
            else
            {
                // Existing entity
                context.Entry(groupUser).State = EntityState.Modified;
            }
        }

        public void InsertOrUpdate(Group group)
        {
            if (group.Id == default(int))
            {
                // New entity
                context.Groups.Add(group);
            }
            else
            {
                // Existing entity
                if (context.Entry(group).State == EntityState.Detached)
                {
                    var entityKey = context.Groups.Create().GetType().GetProperty("Id").GetValue(group);

                    context.Entry(context.Set<Group>().Find(entityKey)).CurrentValues.SetValues(group);

                }
                //context.Entry(group).State = EntityState.Modified;
            }
        }

        public void Disable(int id, User user)
        {
            var user1 = context.Users.Find(id);
            user1.Disabled = true;
            user1.ModifiedUserId = user.Id;
        }

        public void Enable(int id, User user)
        {
            var user1 = context.Users.Find(id);
            user1.Disabled = false;
            user1.ModifiedUserId = user.Id;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }

    public interface IUserRepository : IDisposable
    {
        User Find(int id);

        User Find(string id);

        IQueryable<User> GetUsers();

        IQueryable<User> GetUsers(Expression<Func<User, bool>> filter);

        IQueryable<GroupUser> GetUserGroups(int id);

        void DeleteGroupUser(int id);

        void DeleteGroup(int id);

        Group GetGroup(int id);

        IQueryable<Group> GetGroups();

        IQueryable<Group> GetGroups(Expression<Func<Group, bool>> filter);

        IQueryable<User> All { get; }

        IQueryable<User> AllIncluding(params Expression<Func<User, object>>[] includeProperties);

        void InsertOrUpdate(User user);

        void InsertOrUpdate(GroupUser groupUser);

        void InsertOrUpdate(Group group);

        void Disable(int id, User user);

        void Enable(int id, User user);

        void Save();

    }
}