using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;

using Admin.Views;
using Admin.ViewModel;

namespace Admin
{
    public static class Demo
    {
        public static async void DoDemo()
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            var app = Application.Current as App;
            MessageBox.Show("Starting demo");

            // select room from main menu
            mainWindow.CurrentView = new MainMenuView();
            app.roomController.SetClipboardRoom(app.roomController.ReadRoom(app.roomController.ReadAll().First().Id));
            await Task.Delay(1000);

            // choose form
            mainWindow.Width = 430;
            mainWindow.Height = 750;
            mainWindow.CurrentView = new ChooseFormView();
            await Task.Delay(2000);

            // equipment transfer
            var win = new ScheduleEquipmentTransferViewModel();
            mainWindow.CurrentView = new ScheduleEquipmentTransferView(win);
            win.SelectedEquipment = win.AvailableEquipment.First();
            await Task.Delay(2000);
            // submenu
            mainWindow.Width = 750;
            mainWindow.Height = 430;
            mainWindow.CurrentView = new HospitalLayoutSubmenuView(new ScheduleEquipmentTransferView(), win, "transfer");
            app.roomController.SetSelectedRoom(app.roomController.ReadRoom(app.roomController.ReadAll().Last().Id));
            await Task.Delay(1000);
            // return from submenu
            mainWindow.Width = 430;
            mainWindow.Height = 750;
            mainWindow.CurrentView = new ScheduleEquipmentTransferView(win);
            await Task.Delay(2000);
            win.DestinationRoom = app.roomController.GetSelectedRoom();
            win.SelectedRoomNb = win.DestinationRoom.RoomNb.ToString();
            await Task.Delay(2000);
            win.StartDate = DateTime.Now;
            win.StartTime = "12:00";
            await Task.Delay(2000);
            win.EndDate = DateTime.Now;
            win.EndTime = "13:00";
            await Task.Delay(1000);
            win.OnRecord();

            // record equipment transfer
            mainWindow.CurrentView = new RecordEquipmentTransferView();
            await Task.Delay(3000);

            // return to main menu
            mainWindow.Height = 430;
            mainWindow.Width = 750;
            mainWindow.CurrentView = new MainMenuView();
            app.roomController.SetClipboardRoom(app.roomController.ReadRoom(app.roomController.ReadAll().First().Id));
            await Task.Delay(1000);

            // choose form
            mainWindow.Width = 430;
            mainWindow.Height = 750;
            mainWindow.CurrentView = new ChooseFormView();
            await Task.Delay(2000);

            // renovations
            var reno = new ScheduleRenovationViewModel();
            mainWindow.CurrentView = new ScheduleRenovationView(reno);
            reno.SelectedRenovationType = reno.RenovationTypes.First();
            await Task.Delay(2000);
            reno.StartDate = DateTime.Now;
            await Task.Delay(2000);
            reno.EndDate = DateTime.Now.AddDays(1);
            await Task.Delay(2000);
            reno.SplitMerge = "ordinary";
            reno.OnRecord();

            // record reno
            mainWindow.CurrentView = new RecordRenovationView();
            await Task.Delay(3000);

            // back to main menu
            mainWindow.Height = 430;
            mainWindow.Width = 750;
            mainWindow.CurrentView = new MainMenuView();
            await Task.Delay(2000);

            // equipment table
            mainWindow.Height = 750;
            mainWindow.Width = 430;
            var etb = new EquipmentTableViewModel();
            mainWindow.CurrentView = new EquipmentTableView(etb);
            await Task.Delay(2000);
            etb.SelectedEquipment = etb.Equipment.First();
            await Task.Delay(2000);
            etb.OnRemove();
            await Task.Delay(2000);

            // rooms and query
            var rtb = new RoomTableViewModel();
            mainWindow.CurrentView = new RoomTableView(rtb);
            await Task.Delay(2000);
            rtb.Search = "Floor: <le> 2";
            await Task.Delay(2000);
            rtb.OnQuery();
            await Task.Delay(2000);

            // main menu
            mainWindow.Height = 430;
            mainWindow.Width = 750;
            mainWindow.CurrentView = new MainMenuView();
            await Task.Delay(2000);

            // order stuff
            mainWindow.Height = 750;
            mainWindow.Width = 430;
            var op = new OrderProductsViewModel();
            mainWindow.CurrentView = new OrderProductsView(op);
            await Task.Delay(2000);
            op.SelectedOrderType = op.OrderType.First();
            await Task.Delay(2000);
            op.SelectedProductType = op.ProductType.First();
            await Task.Delay(2000);
            op.Amount = "3";
            await Task.Delay(2000);
            op.ArrivalDate = DateTime.Now;
            await Task.Delay(2000);

            // main menu
            mainWindow.Height = 430;
            mainWindow.Width = 750;
            mainWindow.CurrentView = new MainMenuView();
            await Task.Delay(2000);

            // medicine check
            mainWindow.Height = 750;
            mainWindow.Width = 430;
            var mc = new RequestMedicineCheckViewModel();
            mainWindow.CurrentView = new RequestMedicineCheckView(mc);
            await Task.Delay(2000);
            mc.SelectedMedicine = mc.MedicineList.First();
            await Task.Delay(4000);
            mc.SelectedDoctor = mc.Doctors.First();
            await Task.Delay(2000);
            mc.ArrivalDate = DateTime.Now;
            await Task.Delay(2000);
            mc.Comment = "Lorem ipsum dolorit est";
            await Task.Delay(2000);

            // end
            mainWindow.Width = 750;
            mainWindow.Height = 430;
            mainWindow.CurrentView = new MainMenuView();
        }
    }
}
