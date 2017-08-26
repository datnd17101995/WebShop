using Data.Infrastructure;
using Data.Repositories;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IPostService
    {
        void Add(Post post);
        void Update();
        void Delete(int id);
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetAllPagging(int page, int pageSize, out int totalRow);
        Post GetById(int id);
        IEnumerable<Post> GetAllByTagPagging(int page, int pageSize, out int totalRow);
    }
    public class PostService : IPostService
    {
        IPostRepository _postReporsitory;
        IUnitOfWord _unitOfWord;

        public PostService(IPostRepository postRepository, IUnitOfWord unitOfWord)
        {
            this._postReporsitory = postRepository;
            this._unitOfWord = unitOfWord;
        }
        public void Add(Post post)
        {
            _postReporsitory.Add(post);
        }

        public void Delete(int id)
        {
            _postReporsitory.Delete(id);
        }

        public IEnumerable<Post> GetAll()
        {
            return _postReporsitory.GetAll(new string[] { "PostCategory" });
        }

        public IEnumerable<Post> GetAllByTagPagging(int page, int pageSize, out int totalRow)
        {
            return _postReporsitory.GetMultiPagging(x => x.Status.Value, out totalRow, page, pageSize);
        }

        public IEnumerable<Post> GetAllPagging(int page, int pageSize, out int totalRow)
        {
            throw new NotImplementedException();
        }

        public Post GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
