using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBCurse.Models;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace DBCurse.ProductiveModel
{
    class ProductiveSolver
    {
        public Worker BestWorker { get; private set; }
        public Project Project { get; set; }
        private string Position { get; set; }
        /// <summary>
        /// Список работников для рассмотрения.
        /// </summary>
        private ObservableCollection<Worker> Workers;
        private ObservableCollection<Worker> WorkersToDelete;
        private ObservableCollection<Productive> Rules;
        private ObservableCollection<ProductAttribute> ProductAttributes;
        private Dictionary<Worker, string> WorkersBusy;
        private Dictionary<Worker, string> WorkersPerformance;
        private Dictionary<Worker, string> WorkersPriority;
        /// <summary>
        /// Срочность проекта. Представлено в виде количества дней.
        /// </summary>
        public int ProjectUrgency { get; set; }
        /// <summary>
        /// Значимость проекта. Представлено в виде суммы заключенного договора.
        /// </summary>
        public int ProjectValue { get; set; }
        /// <summary>
        /// Срочность проекта в формате продукционной модели.
        /// </summary>
        public string ProjectUrgencyAttr { get; set; }
        /// <summary>
        /// Значимость проекта в формате продукционной модели.
        /// </summary>
        public string ProjectValueAttr { get; set; }
        public ProductiveSolver(Project p, string position)
        {
            this.Project = p;
            this.Workers = new ObservableCollection<Worker>();
            this.WorkersToDelete = new ObservableCollection<Worker>();
            //this.Workers = DataModel.ReadOnlyWorkers;
            AddWorkers();
            RemoveExistingWorkers();
            this.Rules = new ObservableCollection<Productive>();
            this.Rules = DataModel.Productives;
            this.ProductAttributes = new ObservableCollection<ProductAttribute>();
            this.ProductAttributes = DataModel.ProductAttributes;
            this.Position = position;
            RemoveWrongPositionWorkers();

            ProjectUrgency = CalculateProjectUrgency();
            ProjectValue = CalculateProjectValue();
            ProjectUrgencyAttr = CalculateProjectUrgencyAttr();
            ProjectValueAttr = CalculateProjectValueAttr();
            WorkersBusy = new Dictionary<Worker, string>();
            WorkersPerformance = new Dictionary<Worker, string>();
            WorkersPriority = new Dictionary<Worker, string>();
            CalculateWorkerBusyAttr();
            CalculateWorkerPerformanceAttr();
            SetWorkersPriority();
        }
        private void AddWorkers()
        {
            foreach (Worker w in DataModel.Workers)
            {
                this.Workers.Add(w.Clone() as Worker);
            }
        }
        private void RemoveExistingWorkers()
        {
            WorkersToDelete.Clear();
            foreach (Worker pw in this.Project.WorkersCollection)
            {
                foreach (Worker w in this.Workers)
                {
                    if (w.Name == pw.Name && w.Surname == pw.Surname && w.Patronomic == pw.Patronomic && w.PassportNumber == pw.PassportNumber)
                        this.WorkersToDelete.Add(w);
                }
            }
            foreach (Worker w in this.WorkersToDelete)
            {
                this.Workers.Remove(w);
            }
        }
        private void RemoveWrongPositionWorkers()
        {
            WorkersToDelete.Clear();
            foreach (Worker w in this.Workers)
            {
                if (!IsPositionExists(w))
                    this.WorkersToDelete.Add(w);
            }
            foreach (Worker w in this.WorkersToDelete)
            {
                this.Workers.Remove(w);
            }
        }
        private bool IsPositionExists(Worker w)
        {
            if (w.PositionName == this.Position)
                return true;
            else
                return false;
        }
        private int CalculateProjectUrgency()
        {
            if (!Project.EndDateIsPast())
                return (int)ConvertFromDateToDouble(Project.EndDate) - (int)ConvertFromDateToDouble(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            else System.Windows.Forms.MessageBox.Show("Вы ввели неактуальный проект, работники назначаться не будут.");
            return -1;
        }
        private int CalculateProjectValue()
        {
            foreach (Agreement a in DataModel.Agreements)
            {
                if (a.Id == Project.AgreementID)
                    return a.Amount;
            }
            System.Windows.Forms.MessageBox.Show("Не найден договор, работники назначаться не будут.");
            return -1;
        }
        private int CalculateWorkerBusy(Worker w)
        {
            return w.GetProjectsPartisipationCount();
        }
        private int CalculateWorkerPerformance(Worker w)
        {
            return w.Performance;
        }
        private string CalculateProjectUrgencyAttr()
        {
            foreach (ProductAttribute pa in DataModel.ProductAttributes)
            {
                foreach (ProductAttributeValue pav in pa.AttributeValuesCollection)
                {
                    if (CalculateProjectUrgency() >= pav.Min && CalculateProjectUrgency() <= pav.Max && pa.Name == "Срочность проекта")
                    {
                        return pav.NameValue;
                    }
                }
            }
            return null;
        }
        private string CalculateProjectValueAttr()
        {
            foreach (ProductAttribute pa in DataModel.ProductAttributes)
            {
                foreach (ProductAttributeValue pav in pa.AttributeValuesCollection)
                {
                    if (CalculateProjectValue() >= pav.Min && CalculateProjectValue() <= pav.Max && pa.Name == "Значимость проекта")
                    {
                        return pav.NameValue;
                    }
                }
            }
            return null;
        }
        private void CalculateWorkerBusyAttr()
        {
            foreach (ProductAttribute pa in DataModel.ProductAttributes)
            {
                foreach (ProductAttributeValue pav in pa.AttributeValuesCollection)
                {
                    foreach (Worker w in Workers)
                    {
                        if (w.GetProjectsPartisipationCount() >= pav.Min && w.GetProjectsPartisipationCount() <= pav.Max && pa.Name == "Занятость сотрудника")
                        {
                            WorkersBusy.Add(w, pav.NameValue);
                        }
                    }
                }
            }
        }
        private void CalculateWorkerPerformanceAttr()
        {
            foreach (ProductAttribute pa in DataModel.ProductAttributes)
            {
                foreach (ProductAttributeValue pav in pa.AttributeValuesCollection)
                {
                    foreach (Worker w in Workers)
                    {
                        if (CalculateWorkerPerformance(w) >= pav.Min && CalculateWorkerPerformance(w) <= pav.Max && pa.Name == "Производительность сотрудника")
                        {
                            WorkersPerformance.Add(w, pav.NameValue);
                        }
                    }
                }
            }
            if (WorkersPerformance.Count == 0) System.Windows.Forms.MessageBox.Show("Производительность сотрудников не совпадает со значениями в продукционной модели.");
        }
        private Worker SetWorkersPriority()
        {
            foreach (Productive rule in Rules)
            {
                foreach (KeyValuePair<Worker, string> busy in WorkersBusy)
                {
                    foreach (KeyValuePair<Worker, string> performance in WorkersPerformance)
                    {
                        if (busy.Key == performance.Key)
                            if (ProjectValueAttr == rule.ProjectValue && ProjectUrgencyAttr == rule.ProjectUrgency && busy.Value == rule.WorkerBusy && performance.Value == rule.WorkerPerformance)
                            {
                                WorkersPriority.Add(busy.Key, rule.Priority);
                            }
                    }
                }
            }
            return new Worker();
        }
        public Worker ChooseWorker()
        {
            int max = int.MinValue;
            foreach (KeyValuePair<Worker, string> p in WorkersPriority)
            {
                if (GetPriorityValue(p.Value) > max) max = GetPriorityValue(p.Value);
            }
            foreach (KeyValuePair<Worker, string> p in WorkersPriority)
            {
                if (GetPriorityValue(p.Value) == max)
                {
                    BestWorker = p.Key;
                    return p.Key;
                }
            }
            return null;
        }
        private int GetPriorityValue(string priority)
        {
            switch (priority)
            {
                case "Очень высокий":
                    return 5;
                case "Высокий":
                    return 4;
                case "Средний":
                    return 3;
                case "Низкий":
                    return 2;
                case "Очень Низкий":
                    return 1;
            }
            return -1;
        }
        private double ConvertFromDateToDouble(string s)
        {
            double d = 0;
            int years = int.Parse(s[0].ToString() + s[1].ToString() + s[2].ToString() + s[3].ToString());
            int month = int.Parse(s[5].ToString() + s[6].ToString());
            int days = int.Parse(s[8].ToString() + s[9].ToString());
            int str;
            d = years - DateTime.Now.Year;
            if (d <= 0)
                str = 0;
            else
                str = (int)d * 365;
            str += month * 30;
            str += days;
            d = str;
            return d;
        }
    }
}
