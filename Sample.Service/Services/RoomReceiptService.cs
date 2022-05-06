using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sample.Entities;
using Sample.Entities.DomainEntities;
using Sample.Entities.Search;
using Sample.Interface.Services;
using Sample.Interface.UnitOfWork;
using Sample.Request;
using Sample.Service.Services.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Service.Services
{
    public class RoomReceiptService : DomainService<RoomReceipt, RoomReceiptSearch>, IRoomReceiptService
    {
        protected IElectricWaterBillService electricWaterBillService;
        protected IRoomUtilitiService roomUtilitiService;
        protected IRoomService roomService;

        public RoomReceiptService(IAppUnitOfWork unitOfWork, IMapper mapper
            , IElectricWaterBillService ElectricWaterBillService
            , IRoomUtilitiService RoomUtilitiService
            , IRoomService RoomService
            ) : base(unitOfWork, mapper)
        {
            this.electricWaterBillService = ElectricWaterBillService;
            this.roomUtilitiService = RoomUtilitiService;
            this.roomService = RoomService;
        }
        protected override string GetStoreProcName()
        {
            return "RoomReceipt_GetPagingData";
        }
        public override async Task<bool> CreateAsync(RoomReceipt item)
        {
            // lấy ra danh sách ghi điện theo tháng và năm
            IList<ElectricWaterBill> ElectricWaterBills = await electricWaterBillService.GetAsync(p => p.RoomId == item.RoomId && p.WriteDate.Value.Month == item.Date.Value.Month && p.WriteDate.Value.Year == item.Date.Value.Year);
            // kiểm tra ngày dọn vào của người đại diện trong phòng để tính toán điện nước
            // chưa có chức năng dọn vào !!!!!!!!!!!!!!!

            // tiền điện nước tháng đó
            double? electric_bill = 0;
            double? water_bill = 0;
            foreach (ElectricWaterBill electricWaterBill in ElectricWaterBills)
            {
                if (electricWaterBill.ElectricBill != null)
                {
                    electric_bill += electricWaterBill.ElectricBill;
                }
                if (electricWaterBill.WaterBill != null)
                {
                    water_bill += electricWaterBill.WaterBill;
                }
            }
            // lấy danh sách dịch vụ của phòng
            IList<RoomUtilities> roomUtilities = await roomUtilitiService.GetAsync(p => p.RoomId == item.RoomId);
            // tiền dich vụ của phòng
            double? room_utilitie_bill = 0;
            foreach (RoomUtilities roomUtilitie in roomUtilities)
            {
                if (roomUtilitie.Price != null)
                {
                    room_utilitie_bill += roomUtilitie.Price;
                }

            }
            // lấy giá phòng
            double? room_price_bill = 0;
            if (room_price_bill != null)
            {
                room_price_bill = roomService.GetById(item.RoomId).Price;
            }
            // tổng giá tiền phải chi chả theo tháng 
            double total_money_bill = 0;
            total_money_bill = (double)(room_price_bill + electric_bill + water_bill + room_utilitie_bill);

            //
            item.ElectricBill = electric_bill;
            item.WaterBill = water_bill;
            item.RoomUtilitieBill = room_utilitie_bill;
            item.RoomBill = room_price_bill;
            item.TotalBill = total_money_bill;
            item.FinalBill = total_money_bill + item.PlusBill - item.SubBill;
            
            return await base.CreateAsync(item);
        }
    }
}
