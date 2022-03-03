using Sample.Entities;
//using Sample.Models.Auth;
using Sample.Request;
//using Sample.Models.Catalogue;
using Sample.Utilities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Sample.Request.Auth;
using Sample.Models.Catalogue;
using Sample.Entities.Catalogue;
using Sample.Request.Catalogue;
//using Sample.Entities.Catalogue;
//using Sample.Entities.Auth;

namespace Sample.Models.AutoMapper
{
    public class AppAutoMapper : Profile
    {
        public AppAutoMapper()
        {
            CreateMap<UserModel, Users>().ReverseMap();
            CreateMap<UserRequest,Users>().ReverseMap();
            CreateMap<PagedList<UserModel>, PagedList<Users>>().ReverseMap();

            //CreateMap<UserGroupModel, UserGroups>().ReverseMap();
            //CreateMap<UserGroups, UserGroupRequest>().ReverseMap();
            //CreateMap<PagedList<UserGroupModel>, PagedList<UserGroups>>().ReverseMap();

            //CreateMap<UserInGroupModel, UserInGroups>().ReverseMap();
            //CreateMap<PagedList<UserInGroupModel>, PagedList<UserInGroups>>().ReverseMap();

            //CreateMap<PermissionModel, Permissions>().ReverseMap();
            //CreateMap<Permissions, PermissionsRequest>().ReverseMap();
            //CreateMap<PagedList<PermissionModel>, PagedList<Permissions>>().ReverseMap();

            //CreateMap<PermitObjectModel, PermitObjects>().ReverseMap();
            //CreateMap<PermitObjects, PermitObjectRequest>().ReverseMap();
            //CreateMap<PagedList<PermitObjectModel>, PagedList<PermitObjects>>().ReverseMap();

            //CreateMap<PermitObjectPermissionModel, PermitObjectPermissions>().ReverseMap();
            //CreateMap<PermitObjectPermissions, PermitObjectPermissionRequest>().ReverseMap();
            //CreateMap<PagedList<PermitObjectPermissionModel>, PagedList<PermitObjectPermissions>>().ReverseMap();

            //CreateMap<UserFileModel, UserFiles>().ReverseMap();
            //CreateMap<PagedList<UserFileModel>, PagedList<UserFiles>>().ReverseMap();

            #region Catalogue

            //CreateMap<CountryModel, Countries>().ReverseMap();
            //CreateMap<PagedList<CountryModel>, PagedList<Countries>>().ReverseMap();

            //CreateMap<NationModel, Nations>().ReverseMap();
            //CreateMap<PagedList<NationModel>, PagedList<Nations>>().ReverseMap();

            CreateMap<CityModel, Cities>().ReverseMap();
            CreateMap<CityRequest, Cities>().ReverseMap();
            CreateMap<PagedList<CityModel>, PagedList<Cities>>().ReverseMap();

            CreateMap<DistrictModel, Districts>().ReverseMap();
            CreateMap<DistrictRequest, Districts>().ReverseMap();
            CreateMap<PagedList<DistrictModel>, PagedList<Districts>>().ReverseMap();

            CreateMap<WardModel, Wards>().ReverseMap();
            CreateMap<WardRequest, Wards>().ReverseMap();
            CreateMap<PagedList<WardModel>, PagedList<Wards>>().ReverseMap();

            #endregion

            CreateMap<BranchModel, Branch>().ReverseMap();
            CreateMap<BranchRequest, Branch>().ReverseMap();
            CreateMap<PagedList<BranchModel>, PagedList<Branch>>().ReverseMap();


            CreateMap<PackageModel, Package>().ReverseMap();
            CreateMap<PackageRequest, Package>().ReverseMap();
            CreateMap<PagedList<PackageModel>, PagedList<Package>>().ReverseMap();

        }

    }
}
