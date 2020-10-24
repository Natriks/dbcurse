using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using DBCurse.DataAdapters;
using DBCurse.ProductiveModel;

namespace DBCurse.Models
{
    static class DataModel
    {
        static public MySqlConnection DBConnection;
        static public ObservableCollection<Client> Clients { get; set; }
        static private ClientsDataAdapter clientDataAdapter;
        static public ObservableCollection<Worker> Workers { get; set; }
        static private WorkersDataAdapter workerDataAdapter;
        static public ObservableCollection<Department> Departments { get; set; }
        static private DepartmentsDataAdapter departmentDataAdapter;
        static public ObservableCollection<ProjectType> ProjectTypes { get; set; }
        static private ProjectTypesDataAdapter projectTypeDataAdapter;
        static public ObservableCollection<Position> Positions { get; set; }
        static private PositionsDataAdapter positionDataAdapter;
        static public ObservableCollection<Investment> Investments { get; set; }
        static private InvestmentsDataAdapter investmentDataAdapter;
        static public ObservableCollection<Investor> Investors { get; set; }
        static private InvestorsDataAdapter investorDataAdapter;
        static public ObservableCollection<Agreement> Agreements { get; set; }
        static private AgreementsDataAdapter agreementDataAdapter;
        static public ObservableCollection<Operation> Operations { get; set; }
        static private OperationsDataAdapter operationDataAdapter;
        static public ObservableCollection<Stage> Stages { get; set; }
        static private StagesDataAdapter stageDataAdapter;
        static public ObservableCollection<Project> Projects { get; set; }
        static private ProjectsDataAdapter projectDataAdapter;
        static public ObservableCollection<Productive> Productives { get; set; }
        static private ProductivesDataAdapter productiveDataAdapter;
        static public ObservableCollection<ProductAttribute> ProductAttributes { get; set; }
        static private ProductAttributesDataAdapter productAttributeDataAdapter;
        static public ObservableCollection<ProductAttributeValue> ProductAttributeValues { get; set; }
        static private ProductAttrValuesDataAdapter productAttributeValueDataAdapter;
        static public ObservableCollection<ProjectParticipation> ProjectParticipations { get; set; }
        static private ProjectParticipationDataAdapter projectParticipationDataAdapter;
        static public ObservableCollection<WorkersInDepartment> WorkersInDepartments { get; set; }
        static private WorkersInDepartmentsDataAdapter workersInDepartmentsDataAdapter;
        static public ObservableCollection<ProjectStage> ProjectStagesCollection { get; set; }
        static private ProjectStagesDataAdapter projectStagesDataAdapter;
        static public  ObservableCollection<Worker> ReadOnlyWorkers { get { return Workers; } private set { } }
       static public ObservableCollection<string> PositionsCollection { get; set; }
        static DataModel()
        {
            //DBConnection = new MySqlConnection("server=82.179.88.34; port=3306; Uid=IAS15.VasiljhevKA; Pwd=K?s3jF2}J_; database=IAS15_VasiljhevKA; charset=utf8");
            DBConnection = new MySqlConnection("server=82.179.88.34; port=3306; Uid=IAS15.VasiljhevKA; Pwd=K?s3jF2}J_; database=IAS15_VasiljhevKA; charset=utf8");
            DBConnection.Open();

            Clients = new ObservableCollection<Client>();
            clientDataAdapter = new ClientsDataAdapter(DBConnection, Clients);
            clientDataAdapter.Fill();

            Workers = new ObservableCollection<Worker>();
            workerDataAdapter = new WorkersDataAdapter(DBConnection, Workers);
            workerDataAdapter.Fill();

            Departments = new ObservableCollection<Department>();
            departmentDataAdapter = new DepartmentsDataAdapter(DBConnection, Departments);
            departmentDataAdapter.Fill();

            ProjectTypes = new ObservableCollection<ProjectType>();
            projectTypeDataAdapter = new ProjectTypesDataAdapter(DBConnection, ProjectTypes);
            projectTypeDataAdapter.Fill();

            Positions = new ObservableCollection<Position>();
            positionDataAdapter = new PositionsDataAdapter(DBConnection, Positions);
            positionDataAdapter.Fill();

            Investments = new ObservableCollection<Investment>();
            investmentDataAdapter = new InvestmentsDataAdapter(DBConnection, Investments);
            investmentDataAdapter.Fill();

            Investors = new ObservableCollection<Investor>();
            investorDataAdapter = new InvestorsDataAdapter(DBConnection, Investors);
            investorDataAdapter.Fill();

            Operations = new ObservableCollection<Operation>();
            operationDataAdapter = new OperationsDataAdapter(DBConnection, Operations);
            operationDataAdapter.Fill();

            Agreements = new ObservableCollection<Agreement>();
            agreementDataAdapter = new AgreementsDataAdapter(DBConnection, Agreements);
            agreementDataAdapter.Fill();

            Stages = new ObservableCollection<Stage>();
            stageDataAdapter = new StagesDataAdapter(DBConnection, Stages);
            stageDataAdapter.Fill();

            Projects = new ObservableCollection<Project>();
            projectDataAdapter = new ProjectsDataAdapter(DBConnection, Projects);
            projectDataAdapter.Fill();

            Productives = new ObservableCollection<Productive>();
            productiveDataAdapter = new ProductivesDataAdapter(DBConnection, Productives);
            productiveDataAdapter.Fill();

            ProductAttributeValues = new ObservableCollection<ProductAttributeValue>();
            productAttributeValueDataAdapter = new ProductAttrValuesDataAdapter(DBConnection, ProductAttributeValues);
            productAttributeValueDataAdapter.Fill();

            ProductAttributes = new ObservableCollection<ProductAttribute>();
            productAttributeDataAdapter = new ProductAttributesDataAdapter(DBConnection, ProductAttributes);
            productAttributeDataAdapter.Fill();

            ProjectParticipations = new ObservableCollection<ProjectParticipation>();
            projectParticipationDataAdapter = new ProjectParticipationDataAdapter(DBConnection, ProjectParticipations);
            projectParticipationDataAdapter.Fill();

            WorkersInDepartments = new ObservableCollection<WorkersInDepartment>();
            workersInDepartmentsDataAdapter = new WorkersInDepartmentsDataAdapter(DBConnection, WorkersInDepartments);
            workersInDepartmentsDataAdapter.Fill();

            ProjectStagesCollection = new ObservableCollection<ProjectStage>();
            projectStagesDataAdapter = new ProjectStagesDataAdapter(DBConnection, ProjectStagesCollection);
            projectStagesDataAdapter.Fill();
        }
    }
}
