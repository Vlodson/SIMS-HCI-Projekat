using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model;
using Repository;
using HospitalMain.Enums;

namespace Service
{
    public class EquipmentService
    {

        private readonly EquipmentRepo _equipmentRepo;
        private readonly RoomRepo _roomRepo;

        public EquipmentService(EquipmentRepo equipmentRepo, RoomRepo roomRepo)
        {
            _equipmentRepo = equipmentRepo;
            _roomRepo = roomRepo;
        }

        public bool CreateEquipment(String equipmentId, String roomId, EquipmentTypeEnum type)
        {
            Equipment equipment = new Equipment(equipmentId, roomId, type);
            return _equipmentRepo.NewEquipment(equipment);
        }

        public bool RemoveEquipment(String equipmentId, String roomId)
        {
            _roomRepo.RemoveEquipment(roomId, equipmentId);
            return _equipmentRepo.DeleteEquipment(equipmentId);
        }

        public void EditEquipment(String equipmentId, String newRoomId, EquipmentTypeEnum newType)
        {
            _equipmentRepo.SetEquipment(equipmentId, newRoomId, newType);
        }

        public Equipment ReadEquipment(String equipmentId)
        {
            return _equipmentRepo.GetEquipment(equipmentId);
        }

        public ObservableCollection<Equipment> ReadAll()
        {
            return _equipmentRepo.Equipment;
        }

        public ObservableCollection<Equipment> QueryEquipment(String query)
        {
            List<Equipment> equipmentList = new List<Equipment>(_equipmentRepo.Equipment);

            // if empty string then send reset
            if(String.IsNullOrEmpty(query))
                return _equipmentRepo.Equipment;

            // if not empty string then parse
            String[] kw_split = { "<to>", "<and>", "<or>", "<not>"};

            // remove keywords from query
            List<String> vars = new List<String>(query.Split(kw_split, StringSplitOptions.TrimEntries));

            // extract keywords from query
            List<String> kws = new List<String>();
            Regex re = new Regex(@"<.*>");
            MatchCollection regex_mathes = re.Matches(query);

            foreach(Match match in regex_mathes)
                kws.Add(match.Value);

            // do what you need to with vars and kws
            if(vars.Count == 1)
            {

            }

            return null;
        }

        public bool LoadEquipment()
        {
            return _equipmentRepo.LoadEquipment();
        }

        public bool SaveEquipment()
        {
            return _equipmentRepo.SaveEquipment();
        }
    }
}
