using Sample.Entities;
using Sample.Models.Auth;
using Sample.Request;
using Sample.Models.Catalogue;
using Sample.Utilities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Sample.Request.Auth;
using Sample.Entities.Catalogue;
using Sample.Request.Catalogue;
using Sample.Entities.Auth;

namespace Sample.Models.AutoMapper
{
    public class AppAutoMapper : Profile
    {
        public AppAutoMapper()
        {
            CreateMap<UserModel, Users>().ReverseMap();
            CreateMap<UserRequest,Users>().ReverseMap();
            CreateMap<PagedList<UserModel>, PagedList<Users>>().ReverseMap();

            CreateMap<UserGroupModel, UserGroups>().ReverseMap();
            CreateMap<UserGroups, UserGroupRequest>().ReverseMap();
            CreateMap<PagedList<UserGroupModel>, PagedList<UserGroups>>().ReverseMap();

            CreateMap<UserInGroupModel, UserInGroups>().ReverseMap();
            CreateMap<PagedList<UserInGroupModel>, PagedList<UserInGroups>>().ReverseMap();

            CreateMap<PermissionModel, Permissions>().ReverseMap();
            CreateMap<Permissions, PermissionRequest>().ReverseMap();
            CreateMap<PagedList<PermissionModel>, PagedList<Permissions>>().ReverseMap();

            CreateMap<PermitObjectModel, PermitObjects>().ReverseMap();
            CreateMap<PermitObjects, PermitObjectRequest>().ReverseMap();
            CreateMap<PagedList<PermitObjectModel>, PagedList<PermitObjects>>().ReverseMap();

            CreateMap<PermitObjectPermissionModel, PermitObjectPermissions>().ReverseMap();
            CreateMap<PermitObjectPermissions, PermitObjectPermissionRequest>().ReverseMap();
            CreateMap<PagedList<PermitObjectPermissionModel>, PagedList<PermitObjectPermissions>>().ReverseMap();

            CreateMap<UserFileModel, UserFiles>().ReverseMap();
            CreateMap<PagedList<UserFileModel>, PagedList<UserFiles>>().ReverseMap();

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

            CreateMap<BranchImageModel, BranchImage>().ReverseMap();
            CreateMap<BranchImageRequest, BranchImage>().ReverseMap();
            CreateMap<PagedList<BranchImageModel>, PagedList<BranchImage>>().ReverseMap();

            CreateMap<FloorModel, Floor>().ReverseMap();
            CreateMap<FloorRequest, Floor>().ReverseMap();
            CreateMap<PagedList<FloorModel>, PagedList<Floor>>().ReverseMap();

            CreateMap<PackageModel, Package>().ReverseMap();
            CreateMap<PackageRequest, Package>().ReverseMap();
            CreateMap<PagedList<PackageModel>, PagedList<Package>>().ReverseMap();

            CreateMap<VehicleFeeConfigModel, VehicleFeeConfig>().ReverseMap();
            CreateMap<VehicleFeeConfigRequest, VehicleFeeConfig>().ReverseMap();
            CreateMap<PagedList<VehicleFeeConfigModel>, PagedList<VehicleFeeConfig>>().ReverseMap();

            CreateMap<UtilitiesConfigModel, UtilitiesConfig>().ReverseMap();
            CreateMap<UtilitiesConfigRequest, UtilitiesConfig>().ReverseMap();
            CreateMap<PagedList<UtilitiesConfigModel>, PagedList<UtilitiesConfig>>().ReverseMap();

            CreateMap<NotificationUserModel, NotificationUser>().ReverseMap();
            CreateMap<NotificationUserRequest, NotificationUser>().ReverseMap();
            CreateMap<PagedList<NotificationUserModel>, PagedList<NotificationUser>>().ReverseMap();

            CreateMap<CustomerResourceModel, CustomerResources>().ReverseMap();
            CreateMap<FeedBackTypeRequest, CustomerResources>().ReverseMap();
            CreateMap<PagedList<CustomerResourceModel>, PagedList<CustomerResources>>().ReverseMap();

            CreateMap<FeedBackTypeModel, FeedBackType>().ReverseMap();
            CreateMap<FeedBackTypeRequest, FeedBackType>().ReverseMap();
            CreateMap<PagedList<FeedBackTypeModel>, PagedList<FeedBackType>>().ReverseMap();

            CreateMap<NotificationModel, Notification>().ReverseMap();
            CreateMap<NotificationRequest, Notification>().ReverseMap();
            CreateMap<PagedList<NotificationModel>, PagedList<Notification>>().ReverseMap();

            CreateMap<LicenseModel, License>().ReverseMap();
            CreateMap<LicenseRequest, License>().ReverseMap();
            CreateMap<PagedList<LicenseModel>, PagedList<License>>().ReverseMap();

            CreateMap<LicenseSampleModel, LicenseSample>().ReverseMap();
            CreateMap<LicenseSampleRequest, LicenseSample>().ReverseMap();
            CreateMap<PagedList<LicenseSampleModel>, PagedList<LicenseSample>>().ReverseMap();

            CreateMap<FeedbackModel, Feedback>().ReverseMap();
            CreateMap<FeedbackRequest, Feedback>().ReverseMap();
            CreateMap<PagedList<FeedbackModel>, PagedList<Feedback>>().ReverseMap();

            CreateMap<FeedbackCommentModel, FeedbackComment>().ReverseMap();
            CreateMap<FeedbackCommentRequest, FeedbackComment>().ReverseMap();
            CreateMap<PagedList<FeedbackCommentModel>, PagedList<FeedbackComment>>().ReverseMap();

            CreateMap<PaymentMethodModel, PaymentMethod>().ReverseMap();
            CreateMap<PaymentMethodRequest, PaymentMethod>().ReverseMap();
            CreateMap<PagedList<PaymentMethodModel>, PagedList<PaymentMethod>>().ReverseMap();

            CreateMap<RoomModel, Room>().ReverseMap();
            CreateMap<RoomRequest, Room>().ReverseMap();
            CreateMap<PagedList<RoomModel>, PagedList<Room>>().ReverseMap();

            CreateMap<RoomImageModel, RoomImage>().ReverseMap();
            CreateMap<RoomImageRequest, RoomImage>().ReverseMap();
            CreateMap<PagedList<RoomImageModel>, PagedList<RoomImage>>().ReverseMap();

            CreateMap<RoomTypeModel, RoomType>().ReverseMap();
            CreateMap<RoomTypeRequest, RoomType>().ReverseMap();
            CreateMap<PagedList<RoomTypeModel>, PagedList<RoomType>>().ReverseMap();
        }
    

    }
}
