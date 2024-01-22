using EntityTask.DAL;
using EntityTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityTask.Services
{
    internal class GroupService
    {
        AppDbContext context = new AppDbContext();

        public void AddGroup(Group group)
        {
            context.Groups.Add(group);
            Console.WriteLine("Group Added");
            context.SaveChanges();
        }
        public void RemoveGroup(int id)
        {
            var removedGroup = context.Groups.Find(id);
            if (removedGroup != null)
            {
                context.Groups.Remove(removedGroup);
                Console.WriteLine("Group deleted");
            }
            else
            {
                Console.WriteLine("There is no such group");
            }
            context.SaveChanges();
        }
        public void GetAllGroups()
        {
            List<Group> groups = context.Groups.ToList();
            if (groups.Count > 0)
            {
                foreach (Group group in groups)
                {
                    Console.WriteLine($"Group ID: {group.Id} Group name: {group.Name}");
                }
            }
            else { Console.WriteLine("There is no such group"); }
        }
        public void UpdateGroup(int id)
        {
            var updatedGroup = context.Groups.Find(id);
            if (updatedGroup != null)
            {
                Console.Write("Enter new name of group: ");
                string newName = Console.ReadLine();
                updatedGroup.Name = newName;
                Console.WriteLine($"Group name updated successfully:{newName}");
            }
            else
            {
                Console.WriteLine("There is no such group");
            }

        }
    }
}



