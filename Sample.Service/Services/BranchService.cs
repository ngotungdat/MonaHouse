using AutoMapper;
using Sample.Entities;
using Sample.Entities.Search;
using Sample.Interface.DbContext;
using Sample.Interface.Services;
using Sample.Interface.Services.DomainServices;
using Sample.Interface.UnitOfWork;
using Sample.Service.Services.DomainServices;
using Sample.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Service.Services
{
    public class BranchService : DomainService<Branch, BranchSearch>, IBranchService
    {
        protected IAppDbContext coreDbContext;
        protected IBranchImageService BranchImageService;
        public BranchService(IAppUnitOfWork unitOfWork
            , IMapper mapper
            , IAppDbContext coreDbContext
            , IBranchImageService branchImageService
            ) : base(unitOfWork, mapper)
        {
            this.coreDbContext = coreDbContext;
            this.BranchImageService = branchImageService;
        }
        protected override string GetStoreProcName()
        {
            return "Branch_GetPagingData";
        }
        public override async Task<bool> CreateAsync(Branch item)
        {
            using (var dbContextTransaction = coreDbContext.Database.BeginTransaction())
            {
                try
                {
                    if (item != null)
                    {
                        await this.unitOfWork.Repository<Branch>().CreateAsync(item);
                        await this.unitOfWork.SaveAsync();
                        var addBranchImage = item.AddBranchImages.Split(";");
                        if (addBranchImage.Length>0 && addBranchImage[0] == "") { return true; } 
                        List<BranchImage> addBranchImages = new List<BranchImage>();
                        for (int i = 0; i < addBranchImage.Count(); i++) {
                            BranchImage branchImage = new BranchImage();
                            branchImage.Id = 0;
                            branchImage.BranchId = item.Id;
                            branchImage.Link = addBranchImage[i];
                            branchImage.CreatedBy = item.CreatedBy;
                            //
                            addBranchImages.Add(branchImage);
                        }
                        await this.unitOfWork.Repository<BranchImage>().CreateAsync(addBranchImages);
                        /*foreach(BranchImage jtem in item.BranchImages)
                        {
                            jtem.BranchId = item.Id;
                            jtem.CreatedBy = item.CreatedBy;
                            await this.unitOfWork.Repository<BranchImage>().CreateAsync(jtem);
                        }*/
                        await unitOfWork.SaveAsync();
                        await dbContextTransaction.CommitAsync();
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    await dbContextTransaction.RollbackAsync();
                    throw new Exception(ex.Message);
                }
            }
        }

        public override async Task<bool> UpdateAsync(Branch item)
        {

            try
            {
      
                var existItem = await this.GetByIdAsync(item.Id);
                
                    
                        //existItem =  this.GetById(item.Id);
                        if (existItem != null)
                        {
                            this.unitOfWork.Repository<Branch>().Update(item);
                            await this.unitOfWork.SaveAsync();
                            //foreach (BranchImage jtem in item.BranchImages)
                            //{
                            //    jtem.BranchId = item.Id;
                            //    jtem.CreatedBy = item.CreatedBy;
                            //    await this.unitOfWork.Repository<BranchImage>().CreateAsync(jtem);
                            //}
                            //await unitOfWork.SaveAsync();
                            
                            // thêm ảnh
                            var addBranchImage = item.AddBranchImages.Split(";");
                            if (addBranchImage.Length > 0 && addBranchImage[0] == "") { }
                            else {
                                List<BranchImage> addBranchImages = new List<BranchImage>();
                                for (int i = 0; i < addBranchImage.Count(); i++)
                                {
                                    BranchImage branchImage = new BranchImage();
                                    branchImage.Id = 0;
                                    branchImage.BranchId = item.Id;
                                    branchImage.Link = addBranchImage[i];
                                    branchImage.CreatedBy = item.CreatedBy;
                                    branchImage.Active = true;
                                    branchImage.Deleted = false;
                                    //
                                    addBranchImages.Add(branchImage);
                                }
                                var rs= await BranchImageService.CreateAsync(addBranchImages);
                                if (rs == false) return false;
                            }

                            // xóa ảnh
                            var deleteBranchImage = item.DeleteBranchImages.Split(";");
                            if (deleteBranchImage.Length > 0 && deleteBranchImage[0] == "") { return true; }
                            List<BranchImage> deleteBranchImages = new List<BranchImage>();
                            for (int i = 0; i < deleteBranchImage.Count(); i++)
                            {
                                //
                                var res = await BranchImageService.DeleteAsync(Int32.Parse(deleteBranchImage[i]));
                                if (res == false) return false;
                            }
                            return true;
                        }
                        return false;
                    
                    
                
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
    }
}
