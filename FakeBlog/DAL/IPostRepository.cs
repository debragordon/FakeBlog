using FakeBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBlog.DAL
{
    public interface IPostRepository
    {
        // List of methods to help deliver features
        // Create
        void AddPost(Post newPost, ApplicationUser userHere);

        // Read
        Post GetPost(int postId);
        //List<Post> GetPostsFromAuthor(string authorId);

        // Update
        void PublishPost(int postId);
        void EditPostTitle(int postId, string newTitle);
        void EditPostBody(int postId, string newContents);

        // Delete
        void RemovePost(int postId);
    }
}
