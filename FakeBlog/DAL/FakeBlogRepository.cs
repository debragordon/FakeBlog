using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FakeBlog.Models;
using System.Data.Common;

namespace FakeBlog.DAL
{
    public class FakeBlogRepository : IRepository
    {
        // public FakeBlogContext Context { get; set; }

        public FakeBlogRepository()
        {
            //Context = new FakeBlogContext();
        }

        public FakeBlogRepository(FakeBlogContext context)
        {
           // Context = context;
        }

        public void AddPost(Post newPost, ApplicationUser userHere) 
        {
           // Context.Posts.Add(newPost);
            // Context.SaveChanges();
        }

        public Post GetPost(int postId)
        {
            //Post postIWant = Context.Posts.FirstOrDefault(p => p.PostId == postId);
            //return postIWant;
        }

        public void EditPostBody(int postId, string newContents)
        {
            //Post postToEdit = GetPost(postId);
            //if (postToEdit.Contents != null)
            //{
                //postToEdit.Contents = newContents;
                //Context.SaveChanges();
            }
        }

        public void EditPostTitle(int postId, string newTitle)
        {
            //Post postToEdit = GetPost(postId);
            //if (postToEdit.Title != null)
            {
               // postToEdit.Title = newTitle;
                //Context.SaveChanges();
            }
        }

        public void PublishPost(int postId)
        {
            //Post postToEdit = GetPost(postId);
            //if (postToEdit.IsDraft.Equals(true))
            {
                //postToEdit.IsDraft = false;
                //Context.SaveChanges();
            }
        }

        public void RemovePost(int postId)
        {
            //Post postToRemove = GetPost(postId);
            //if (postToRemove != null)
            {
                //Context.Posts.Remove(postToRemove);
                //Context.SaveChanges();
            }
        }
    }
}