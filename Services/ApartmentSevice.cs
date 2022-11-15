﻿using Microsoft.EntityFrameworkCore;
using MountHebronAppApi.Context;
using MountHebronAppApi.Contracts;
using MountHebronAppApi.Mapper;
using MountHebronAppApi.Models;

namespace MountHebronAppApi.Services
{
    public class ApartmentSevice : IApartmentService
    {
        private readonly ApartmentContext _context;
        private readonly IApartmentMapper _map;

        public ApartmentSevice(ApartmentContext context, IApartmentMapper map)
        {
            _context = context;
            _map = map;
        }

        //Apartment
        public async Task<Apartment> AddNewApartment(ApartmentRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ApartmentResponse>> GetApartments()
        {
            var apartment = await _context.Apartments.AsNoTracking().ToListAsync();
            var category = await _context.Categories.AsNoTracking().ToListAsync();

            return _map.GetApartments(apartment, category);
        }

        public async Task<ApartmentResponse> GetApartment(Guid uid)
        {
            throw new NotImplementedException();
        }

        public Task DeleteApartment(Guid uid)
        {
            throw new NotImplementedException();
        }

        //Blogs
        public Task<BlogRequest> AddNewBlog(BlogRequest model)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BlogResponse>> GetBlogs()
        {
            throw new NotImplementedException();
        }

        public Task<BlogResponse> GetBlog()
        {
            throw new NotImplementedException();
        }

        public Task DeleteBlog(Guid uid)
        {
            throw new NotImplementedException();
        }

        //Category
        public Task<CategoryRequest> AddNewCategory(CategoryRequest request)
        {
            throw new NotImplementedException();
        }



        public Task<IEnumerable<CategoryResponse>> GetCategories()
        {
            throw new NotImplementedException();
        }

        public Task<CategoryResponse> GetCategory()
        {
            throw new NotImplementedException();
        }

        //JoinMembers
        //Add Requested member
        public Task<MemberRequest> NewMembers(MemberRequest model)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MemberResponse>> GetMembers()
        {
            throw new NotImplementedException();
        }

        public Task<MemberResponse> GetMember(Guid uid)
        {
            throw new NotImplementedException();
        }

        //
        //Join TEam
        //Create Request to Join membership
        public async Task<JoinRequest> AddNewJoin(JoinRequest model)
        {
            var response = _map.AddNewMemberRequest(model);

            await _context.AddAsync(response);
            await _context.SaveChangesAsync();

            return model;
        }
    }
}
