﻿using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Model
{
    public class Medicine: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(String name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private String id;
        private String name;
        private MedicineTypeEnum type;
        private ObservableCollection<IngredientEnum> ingredients;
        private MedicineStatusEnum status;
        private String comment;

        public String Id
        {
            get { return id; }
            set
            {
                if(id != value)
                {
                    id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        public String Name
        {
            get { return name; }
            set
            {
                if(name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public MedicineTypeEnum Type
        {
            get { return type; }
            set
            {
                if(type != value)
                {
                    type = value;
                    OnPropertyChanged("Type");
                }
            }
        }

        public ObservableCollection<IngredientEnum> Ingredients { get; set; }

        public MedicineStatusEnum Status
        {
            get { return status; }
            set
            {
                if(status != value)
                {
                    status = value;
                    OnPropertyChanged("Status");
                }
            }
        }

        public String Comment
        {
            get { return comment; }
            set
            {
                if(comment != value)
                {
                    comment = value;
                    OnPropertyChanged("Comment");
                }
            }
        }

        public Medicine() { }

        public Medicine(String id, String name, MedicineTypeEnum type, ObservableCollection<IngredientEnum> ingredients, MedicineStatusEnum status, String comment)
        {
            Id = id;
            Name = name;
            Type = type;
            Ingredients = ingredients;
            Status = status;
            Comment = comment;
        }

        public Medicine(Medicine medicine)
        {
            this.Id = medicine.Id;
            this.Name = medicine.Name;
            this.Type = medicine.Type;
            this.Ingredients = medicine.Ingredients;
            this.Status = medicine.Status;
            this.Comment = medicine.Comment;
        }
 
    }
}
