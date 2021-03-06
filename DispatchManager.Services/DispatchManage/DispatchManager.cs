﻿using System;
using DispatchManager.Data.Models;
using DispatchManager.Infrastructure.Repository;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Linq;
using DispatchManager.Services.Common;
using System.Collections.Generic;
using AutoMapper;

namespace DispatchManager.Services.DispatchManage
{
    public class DispatchManager : ManagerBase, IDispatchManager
    {
        public OpResult SaveTruck(Truck truck)
        {
            var uow = Uow.Reposiotry<Truck>();
            OpResult op = new OpResult() { };
            try
            {
                if (truck == null)
                {
                    op.Ok = false;
                    op.Errors.Add("truck can not be null");
                    return op;
                }
                if (string.IsNullOrEmpty(truck.TruckName))
                {
                    op.Ok = false;
                    op.Errors.Add("Truck name is required!");
                    return op;
                } 
                if (truck.Id > 0)
                {
                    var existedTruck = uow.Get(truck.Id); 
                    if (existedTruck != null) {
                        uow.Detach(existedTruck);
                        existedTruck = Mapper.Map<Truck>(truck);
                        uow.Update(existedTruck);
                    } 
                }
                else
                {
                    uow.Add(truck); 
                }
                Uow.Commit();
            }
            catch (SqlException se)
            {
                op.Ok = false;
                op.Errors.Add(se.Message);
            }

            return op;

        }

        public Truck SearchTruck(ITruckAPI api, string vinNbr)
        {
            if (string.IsNullOrEmpty(vinNbr))
            {
                return null;
            }
            var strResult = api.RetrieveTruck(vinNbr);
            return SaveJsonToTruck(strResult);
        }

        public List<Truck> SearchMultiTruck(ITruckAPI truckAPI, string vinNbrs)
        {
            if (string.IsNullOrEmpty(vinNbrs))
            {
                return null;
            }
            List<Truck> trucks = new List<Truck>();
            var numbers = vinNbrs.Split('|');

            if (numbers.Length > 5)
            {
                return null;
            }

            foreach (var number in numbers)
            {
                var strTruck = truckAPI.RetrieveTruck(number);
                trucks.Add(SaveJsonToTruck(strTruck));
            }
            return trucks;
        }

        private Truck SaveJsonToTruck(string strJson)
        {
            var truck = new Truck();
            if (string.IsNullOrEmpty(strJson))
            {
                return truck;
            }
            try
            {
                var result = JsonConvert.DeserializeObject<Response>(strJson);
                var variables = result.Results;
                truck.VinNumber = result.SearchCriteria.Remove(0, 4);
                if (variables != null)
                {
                    foreach (var varible in variables)
                    {
                        switch (varible.Variable)
                        {
                            case "Manufacturer Name":
                                truck.ManufacturerName = varible.Value;
                                break;
                            case "Model":
                                truck.Model = varible.Value;
                                break;
                            case "Model Year":
                                truck.ModelYear = varible.Value;
                                break;
                            case "Engine Model":
                                truck.EngineModel = varible.Value;
                                break;
                            case "Vehicle Type":
                                truck.VehicleType = varible.Value;
                                break;
                            case "Transmission Style":
                                truck.TransmissionStyle = varible.Value;
                                break;
                            case "Transmission Speeds":
                                truck.TransmissionSpeeds = varible.Value;
                                break;

                            case "Error Code":
                                truck.ErrorCode = varible.Value;
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return truck;
        }

        public OpResult SaveTruckViewModel(int truckId)
        {
            throw new NotImplementedException();
        }
    }
}
